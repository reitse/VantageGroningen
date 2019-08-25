using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Competitions;
using Emando.Vantage.Components;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;
using Emando.Vantage.Workflows.Competitions.Properties;

namespace Emando.Vantage.Workflows.Competitions
{
    public class RecordTimesWorkflow : IDisposable
    {
        private readonly ICompetitionContext context;
        private bool isDisposed;

        public RecordTimesWorkflow(ICompetitionContext context)
        {
            this.context = context;
        }

        public IQueryable<RecordTime> RecordTimes => context.RecordTimes;

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        public async Task<IList<RecordTime>> GetRecordsBrokeAsync(IDisciplineCalculator calculator, Distance distance, PersonCompetitor competitor, TimeSpan time)
        {
            if (distance.Competition == null)
                throw new ArgumentNullException(nameof(distance), Resources.DistanceCompetitionIsNull);
            if (competitor.Person == null)
                throw new ArgumentNullException(nameof(competitor), Resources.PersonCompetitorPersonIsNull);

            var season = calculator.Season(distance.Competition.Starts);
            var age = calculator.SeasonAge(season, competitor.Person.BirthDate);

            return await (from rt in context.RecordTimes
                          where rt.LicenseIssuerId == distance.Competition.LicenseIssuerId
                              && rt.Discipline == distance.Competition.Discipline
                              && rt.DistanceDiscipline == distance.Discipline
                              && rt.Distance == distance.Value
                              && rt.Gender == competitor.Person.Gender
                              && ((rt.Type == RecordType.National || rt.Type == RecordType.World)
                                || (rt.Type == RecordType.Track && rt.VenueCode == distance.VenueCode)
                                || (rt.Type == RecordType.TrackAge && rt.VenueCode == distance.VenueCode && rt.FromAge <= age && rt.ToAge >= age))
                              && time < rt.Time
                          select rt).ToListAsync();
        }

        public async Task AddAsync(RecordTime time)
        {
            if (time.Type != RecordType.TrackAge)
            {
                time.FromAge = int.MinValue;
                time.ToAge = int.MaxValue;
            }

            context.RecordTimes.Add(time);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new NumberCollissionException(LogMessages.RecordTimeAlreadyExists, e);
            }
        }

        public Task RemoveAsync(RecordTime time)
        {
            context.RecordTimes.Remove(time);
            return context.SaveChangesAsync();
        }

        public Task UpdateAsync(RecordTime time)
        {
            if (time.Type != RecordType.TrackAge)
            {
                time.FromAge = int.MinValue;
                time.ToAge = int.MaxValue;
            }
            return context.SaveChangesAsync();
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

        ~RecordTimesWorkflow()
        {
            Dispose(false);
        }
    }
}