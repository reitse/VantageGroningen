using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Competitions.SpeedSkating.LongTrack;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Components.Competitions.SpeedSkating.LongTrack;
using Emando.Vantage.Workflows.Competitions.Reporting;
using Emando.Vantage.Workflows.Reporting;
using Emando.Vantage.Workflows.Reporting.TelerikReports;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    public class RaceTranspondersReportLoader : IDistanceReportLoader
    {
        private readonly Func<ICompetitionContext> contextFactory;

        public RaceTranspondersReportLoader(Func<ICompetitionContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        #region IDistanceReportLoader Members

        async Task<ILoadedReport> IDistanceReportLoader.LoadAsync(Guid competitionId, Guid distanceId, OptionalReportColumns optionalColumns)
        {
            using (var context = contextFactory())
            {
                var report = await LoadAsync(context, competitionId, distanceId, optionalColumns);
                return new TelerikLoadedReport(report);
            }
        }

        #endregion

        internal static async Task<RaceTranspondersReport> LoadAsync(ICompetitionContext context, Guid competitionId, Guid distanceId, OptionalReportColumns optionalColumns)
        {
            var report = new RaceTranspondersReport();

            var distance = await context.Distances.Include(d => d.Competition.Venue).Include(d => d.Competition.ReportTemplate.Logos).FirstOrDefaultAsync(d => d.Id == distanceId);
            if (distance == null)
                throw new DistanceNotFoundException();

            report.SetParameters(distance);

            var races = await context.Races.Include(r => r.Competitor).Include(r => r.Transponders).Where(r => r.DistanceId == distanceId).ToListAsync();
            await (context.TeamCompetitorMembers
                .Include(tcm => tcm.Member)
                .Where(tcm => tcm.Team.DistanceCombinations.Any(dc => dc.DistanceCombination.Distances.Any(d => d.Id == distanceId))))
                .LoadAsync();

            var maxPair = races.Select(r => r.Heat).DefaultIfEmpty(0).Max();
            var pairs = new List<Pair>();
            for (var pair = distance.FirstHeat; pair <= maxPair; pair++)
            {
                var colors = PairsDistanceCalculator.Colors(distance, pair);
                var innerRace = races.SingleOrDefault(r => r.Heat == pair && r.Lane == 0);
                var innerRaceColor = (int)colors.ToLaneColor(Lane.Inner);
                var outerRace = races.SingleOrDefault(r => r.Heat == pair && r.Lane == 1);
                var outerRaceColor = (int)colors.ToLaneColor(Lane.Outer);

                pairs.Add(new Pair(pair, innerRace, innerRaceColor, null, outerRace, outerRaceColor, null));
            }

            report.Pairs = pairs;
            return report;
        }
    }
}