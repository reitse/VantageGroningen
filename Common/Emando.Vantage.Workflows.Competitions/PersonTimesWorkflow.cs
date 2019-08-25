using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Competitions;
using Emando.Vantage.Components;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class PersonTimesWorkflow : IDisposable
    {
        private readonly ICompetitionContext context;
        private readonly IDistanceDisciplineExpertManager distanceExpertManager;
        private readonly IDisciplineCalculatorManager calculatorManager;
        private bool isDisposed;

        public PersonTimesWorkflow(ICompetitionContext context, IDisciplineCalculatorManager calculatorManager, IDistanceDisciplineExpertManager distanceExpertManager)
        {
            this.context = context;
            this.calculatorManager = calculatorManager;
            this.distanceExpertManager = distanceExpertManager;
        }

        public bool AutoDetectChangesEnabled
        {
            get { return context.AutoDetectChangesEnabled; }
            set { context.AutoDetectChangesEnabled = value; }
        }

        public IQueryable<PersonTime> PersonTimes => context.PersonTimes;

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        private IQueryable<PersonTime> Query(string licenseIssuerId, string licenseDiscipline, string distanceDiscipline, int distance)
        {
            return from pt in context.PersonTimes
                   where pt.LicenseIssuerId == licenseIssuerId
                       && licenseDiscipline.StartsWith(pt.LicenseDiscipline)
                       && pt.DistanceDiscipline == distanceDiscipline
                       && pt.Distance == distance
                   orderby pt.Time
                   select pt;
        }

        private IQueryable<PersonTime> Query(string licenseIssuerId, string licenseDiscipline, string distanceDiscipline, int distance, string licenseKey)
        {
            return from pt in context.PersonTimes
                   where pt.LicenseIssuerId == licenseIssuerId
                       && licenseDiscipline.StartsWith(pt.LicenseDiscipline)
                       && pt.LicenseKey == licenseKey
                       && pt.DistanceDiscipline == distanceDiscipline
                       && pt.Distance == distance
                   orderby pt.Time
                   select pt;
        }

        public async Task<IPersonLicenseTime> FindHistoricalTimeAsync(string licenseIssuerId, string discipline, string distanceDiscipline, int distanceValue,
            string licenseKey,
            params IHistoricalTimeSelector[] selectors)
        {
            var expert = calculatorManager.Find(discipline);
            if (expert == null)
                throw new InvalidDisciplineException();

            var query = Query(licenseIssuerId, discipline, distanceDiscipline, distanceValue, licenseKey);
            foreach (var selector in selectors)
            {
                PersonTime time = null;

                var personTimeSelector = selector as IPersonTimeSelector;
                if (personTimeSelector != null)
                    time = await personTimeSelector.Query(expert, query).OrderBy(pt => pt.Time).FirstOrDefaultAsync();

                if (time != null)
                    return time;
            }

            return null;
        }

        public async Task<IList<RankedPersonTime>> GetRankingAsync(string licenseIssuerId, string discipline, string distanceDiscipline, int distance,
            TimeSpan? toTime, int? count, params IHistoricalTimeSelector[] selectors)
        {
            var expert = calculatorManager.Find(discipline);
            if (expert == null)
                throw new InvalidDisciplineException();

            var query = Query(licenseIssuerId, discipline, distanceDiscipline, distance)
                .Include(pt => pt.License.Person)
                .Include(pt => pt.License.Club)
                .Include(pt => pt.Venue);
            query = selectors.OfType<IPersonTimeSelector>().Aggregate(query, (current, selector) => selector.Query(expert, current));
            var times = await query.Where(pt => !toTime.HasValue || pt.Time < toTime).OrderBy(pt => pt.Time).ToListAsync();
            times = times.Distinct(PersonTime.LicenseComparer).Take(count ?? int.MaxValue).ToList();

            var rankedPeople = new List<RankedPersonTime>(times.Count);
            var previousTime = new TimeSpan?();
            for (var i = 0; i < times.Count; i++)
            {
                var personTime = times[i];
                var time = personTime.Time.Truncate(expert.DefaultClassificationPrecision);
                rankedPeople.Add(new RankedPersonTime(i + 1, personTime, time == previousTime));
                previousTime = time;
            }
            return rankedPeople;
        }

        public async Task<IList<RankedPersonPoints>> GetRankingAsync(string licenseIssuerId, string discipline, string distanceDiscipline, IEnumerable<int> distances,
            int? classificationWeight, decimal? maxPoints, int? count, params IHistoricalTimeSelector[] selectors)
        {
            var expert = calculatorManager.Find(discipline);
            if (expert == null)
                throw new InvalidDisciplineException();

            var distanceTimes = new List<IReadOnlyList<PersonTime>>();
            foreach (var distance in distances)
            {
                var query = Query(licenseIssuerId, discipline, distanceDiscipline, distance)
                    .Include(pt => pt.License.Person)
                    .Include(pt => pt.License.Club)
                    .Include(pt => pt.Venue);
                query = selectors.OfType<IPersonTimeSelector>().Aggregate(query, (current, selector) => selector.Query(expert, current));
                var times = await query.OrderBy(pt => pt.Time).ToListAsync();
                times = times.Distinct(PersonTime.LicenseComparer).ToList();
                distanceTimes.Add(times);
            }

            var licenses = distanceTimes.SelectMany(pt => pt)
                .GroupBy(pt => pt.License, (license, times) =>
                {
                    var cache = times.OrderBy(t => t.Distance).ToList();
                    return new
                    {
                        License = license,
                        Times = cache,
                        Points = CalculatePoints(cache, distanceTimes.Count, classificationWeight ?? expert.DefaultClassificationWeight, expert.DefaultClassificationPrecision)
                    };
                }, PersonLicense.KeyComparer)
                .Where(l => l.Points.HasValue && (!maxPoints.HasValue || l.Points.Value < maxPoints.Value))
                .OrderBy(l => l.Points.Value)
                .Take(count ?? int.MaxValue)
                .ToList();

            var rankedPeople = new List<RankedPersonPoints>();
            var previousPoints = new decimal?();
            for (var i = 0; i < licenses.Count; i++)
            {
                var license = licenses[i];
                var points = license.Points.Value;
                rankedPeople.Add(new RankedPersonPoints(i + 1, license.License, points, license.Times, previousPoints == points));
                previousPoints = points;
            }
            return rankedPeople;
        }

        private decimal? CalculatePoints(IEnumerable<PersonTime> times, int requiredCount, int classificationWeight, TimeSpan classificationPrecision)
        {
            var points = 0M;
            var count = 0;
            foreach (var time in times)
            {
                var expert = distanceExpertManager.Find(time.DistanceDiscipline);
                if (expert == null)
                    return null;

                if (!expert.Calculator.CanCalculatePoints(time.Distance, classificationWeight, classificationPrecision, time.Time))
                    return null;

                points += expert.Calculator.CalculatePoints(time.Distance, classificationWeight, classificationPrecision, time.Time);
                count++;
            }
            return count == requiredCount ? points : new decimal?();
        }

        ~PersonTimesWorkflow()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                    context.Dispose();

                isDisposed = true;
            }
        }
    }
}