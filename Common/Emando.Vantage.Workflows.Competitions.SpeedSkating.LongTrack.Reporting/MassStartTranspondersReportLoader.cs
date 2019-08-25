using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Workflows.Competitions.Reporting;
using Emando.Vantage.Workflows.Reporting;
using Emando.Vantage.Workflows.Reporting.TelerikReports;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    public class MassStartTranspondersReportLoader : IDistanceReportLoader
    {
        private readonly Func<ICompetitionContext> contextFactory;

        public MassStartTranspondersReportLoader(Func<ICompetitionContext> contextFactory)
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

        internal static async Task<MassStartTranspondersReport> LoadAsync(ICompetitionContext context, Guid competitionId, Guid distanceId,
            OptionalReportColumns optionalColumns)
        {
            var report = new MassStartTranspondersReport();

            var distance = await context.Distances.Include(d => d.Competition.Venue).Include(d => d.Competition.ReportTemplate.Logos).FirstOrDefaultAsync(d => d.Id == distanceId);
            if (distance == null)
                throw new DistanceNotFoundException();

            report.SetParameters(distance);

            var races = await context.Races.Include(r => r.Competitor)
                .Include(r => r.Transponders)
                .Where(r => r.DistanceId == distanceId)
                .OrderBy(r => r.Round)
                .ThenBy(r => r.Heat)
                .ThenBy(r => r.Lane)
                .ToListAsync();
            report.Races = races;
            return report;
        }
    }
}