using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Competitions;
using Emando.Vantage.Components;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;
using Emando.Vantage.Workflows.Competitions.Properties;

namespace Emando.Vantage.Workflows.Competitions
{
    public class DistanceDrawingWorkflow : IDisposable
    {
        private readonly ICompetitionContext context;
        private readonly IDistanceDisciplineExpertManager distanceExpertManager;
        private readonly DistancesWorkflow distancesWorkflow;
        private readonly IDisciplineCalculatorManager calculatorManager;
        private readonly PersonTimesWorkflow personTimesWorkflow;
        private bool isDisposed;

        public DistanceDrawingWorkflow(ICompetitionContext context, IDisciplineCalculatorManager calculatorManager, IDistanceDisciplineExpertManager distanceExpertManager,
            IEventRecorder recorder)
        {
            this.context = context;
            this.calculatorManager = calculatorManager;
            this.distanceExpertManager = distanceExpertManager;

            personTimesWorkflow = new PersonTimesWorkflow(context, calculatorManager, distanceExpertManager);
            distancesWorkflow = new DistancesWorkflow(context, calculatorManager, distanceExpertManager, recorder);
        }

        public IQueryable<DistanceDrawSettings> Settings => context.DistanceDrawSettings;

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        public IQueryable<Distance> Distances(Guid competitionId)
        {
            return from d in context.Distances
                   where d.CompetitionId == competitionId
                   select d;
        }

        private IQueryable<CompetitorBase> ConfirmedCompetitors(Distance distance, IEnumerable<Guid> distanceCombinations, int round)
        {
            if (distance == null)
                throw new ArgumentNullException(nameof(distance));

            return (from dc in context.DistanceCombinations
                    where distanceCombinations.Contains(dc.Id) && dc.Distances.Any(d => d.Id == distance.Id)
                    from c in dc.Competitors
                    where c.Status == DistanceCombinationCompetitorStatus.Confirmed
                        && c.Reserve == null
                        && (!c.Competitor.Races.Any(r => r.DistanceId == distance.Id && r.Round == round
                            && r.Results.Any(rs => rs.InstanceName == r.PresentedInstanceName && rs.Status == RaceStatus.Done)))
                    select c.Competitor).Distinct();
        }

        public async Task<IReadOnlyList<IReadOnlyList<DrawCompetitor>>> GroupAsync(Distance distance, IEnumerable<Guid> distanceCombinations, int round,
            DistanceDrawSettings settings,
            params IHistoricalTimeSelector[] selectors)
        {
            if (distance == null)
                throw new ArgumentNullException(nameof(distance));
            if (distanceCombinations == null)
                throw new ArgumentNullException(nameof(distanceCombinations));
            if (round < 1)
                throw new ArgumentOutOfRangeException(nameof(round));

            Debug.Assert(distance.Competition != null);

            var distanceExpert = distanceExpertManager.Find(distance.Discipline);
            if (distanceExpert == null)
                throw new InvalidDisciplineException();

            var confirmedCompetitors = ConfirmedCompetitors(distance, distanceCombinations, round);
            var roundCompetitors = await distanceExpert.SelectCompetitorsForRound(distance, round, confirmedCompetitors).ToListAsync();

            var drawCompetitors = new List<DrawCompetitor>();
            switch (settings.GroupMode)
            {
                case DistanceDrawGroupMode.Category:
                    drawCompetitors.AddRange(roundCompetitors.Select(rc => new DrawCompetitor(rc, null)));
                    break;
                case DistanceDrawGroupMode.Time:
                    foreach (var competitor in roundCompetitors)
                    {
                        var personCompetitor = competitor as PersonCompetitor;
                        IPersonLicenseTime time = null;
                        if (personCompetitor != null)
                            time = await personTimesWorkflow.FindHistoricalTimeAsync(distance.Competition.LicenseIssuerId, distance.Competition.Discipline, distance.Discipline, distance.Value, competitor.LicenseKey, selectors);
                        drawCompetitors.Add(new DrawCompetitor(competitor, time));
                    }
                    break;
            }

            var categories = await context.PersonCategories
                .Where(c => c.LicenseIssuerId == distance.Competition.LicenseIssuerId && c.Discipline == distance.Competition.Discipline).ToListAsync();

            var sortedDrawCompetitors = distanceExpert.SortDrawCompetitors(distance, drawCompetitors, settings, categories).ToList();
            return distanceExpert.GroupDrawCompetitors(distance, round, sortedDrawCompetitors, settings);
        }

        public async Task<IReadOnlyDictionary<int, IReadOnlyCollection<Race>>> DrawAsync(Distance distance, IReadOnlyCollection<Guid> distanceCombinations, int round, IReadOnlyList<IReadOnlyList<Guid>> groups, DistanceDrawSettings settings)
        {
            if (distance == null)
                throw new ArgumentNullException(nameof(distance));
            if (round < 1)
                throw new ArgumentOutOfRangeException(nameof(round));

            Debug.Assert(distance.Competition != null);

            var distanceExpert = distanceExpertManager.Find(distance.Discipline);
            if (distanceExpert == null)
                throw new InvalidDisciplineException();

            using (var transaction = context.BeginTransaction(IsolationLevel.RepeatableRead))
                try
                {
                    if (settings.DeleteExisting)
                        await DeleteRacesAsync(distance, round);

                    var confirmedCompetitors = ConfirmedCompetitors(distance, distanceCombinations, round);
                    var roundCompetitors = await distanceExpert.SelectCompetitorsForRound(distance, round, confirmedCompetitors).ToDictionaryAsync(c => c.Id);

                    var competitorGroups = new List<IReadOnlyList<CompetitorBase>>();
                    foreach (var group in groups)
                    {
                        var competitors = new List<CompetitorBase>();
                        foreach (var competitorId in group)
                        {
                            CompetitorBase competitor;
                            if (!roundCompetitors.TryGetValue(competitorId, out competitor))
                                throw new CompetitorNotFoundException();

                            competitors.Add(competitor);
                        }

                        await distanceExpert.DrawCompetitorGroupAsync(distance, distanceCombinations, round, competitors, context, settings);
                        competitorGroups.Add(competitors.AsReadOnly());
                    }

                    await distancesWorkflow.ResetFirstHeatAsync(distance, distance.ContinuousNumbering, !distance.ContinuousNumbering
                        ? distance.FirstHeat
                        : new int?());
                    var races = await distanceExpert.FillCompetitorsInHeatsAsync(distance, distanceCombinations, round, competitorGroups, context, settings);

                    foreach (var heat in races.Keys)
                        await distancesWorkflow.AddRacesToHeatAsync(distance, round, heat, true, races[heat].ToArray());

                    await context.SaveChangesAsync();
                    transaction.Commit();

                    return races;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
        }

        private async Task DeleteRacesAsync(Distance distance, int round)
        {
            var races = await (from r in context.Races.Include(r => r.Results)
                               where r.DistanceId == distance.Id && r.Round == round
                               select r).ToListAsync();

            foreach (var race in races)
            {
                if (race.PresentedResult != null)
                    throw new DistanceHasResultsException(string.Format(Resources.DistanceHasResults, distance));

                context.Races.Remove(race);
            }

            await context.SaveChangesAsync();
        }

        public async Task<DistanceDrawSettings> GetSettingsOrDefaultAsync(Guid competitionId)
        {
            var settings = await Settings.FirstOrDefaultAsync(s => s.CompetitionId == competitionId);
            if (settings == null)
            {
                settings = new DistanceDrawSettings
                {
                    CompetitionId = competitionId
                };
                context.DistanceDrawSettings.Add(settings);
            }
            return settings;
        }

        public async Task UpdateSettingsAsync(DistanceDrawSettings settings)
        {
            await context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    personTimesWorkflow.Dispose();
                    context.Dispose();
                }

                isDisposed = true;
            }
        }

        ~DistanceDrawingWorkflow()
        {
            Dispose(false);
        }
    }
}