using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Competitions;
using Emando.Vantage.Entities.Competitions;
using Emando.Vantage.Workflows.Competitions.Reporting;
using Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting.Properties;
using Emando.Vantage.Workflows.Reporting;
using Emando.Vantage.Workflows.Reporting.TelerikReports;
using Telerik.Reporting;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    public class CompetitorListReportLoaderBase<T, TCompetitor> : IDistanceCombinationReportLoader
        where T : Report, ICompetitorListReportWithOptionalColumn, new()
        where TCompetitor : CompetitorBase
    {
        private readonly Func<DistanceCombinationsWorkflow> workflowFactory;

        protected CompetitorListReportLoaderBase(Func<DistanceCombinationsWorkflow> workflowFactory)
        {
            this.workflowFactory = workflowFactory;
        }

        public async Task<ILoadedReport> LoadAsync(Guid competitionId, Guid distanceCombinationId, OptionalReportColumns optionalColumns)
        {
            using (var workflow = workflowFactory())
            {
                var report = await LoadAsync(workflow, competitionId, distanceCombinationId, optionalColumns);
                return new TelerikLoadedReport(report);
            }
        }

        internal static async Task<T> LoadAsync(DistanceCombinationsWorkflow workflow, Guid competitionId, Guid distanceCombinationId,
            OptionalReportColumns optionalColumns)
        {
            var report = new T();

            var distanceCombination = await workflow
                .Combinations(competitionId)
                .Include(dc => dc.Competition.Venue)
                .Include(dc => dc.Competition.ReportTemplate.Logos)
                .FirstOrDefaultAsync(dc => dc.Id == distanceCombinationId);
            if (distanceCombination == null)
                return null;

            report.SetParameters(distanceCombination);

            var competitors = await workflow.Competitors(competitionId, distanceCombinationId)
                .Include(c => c.Competitor)
                .Where(c => c.Status == DistanceCombinationCompetitorStatus.Confirmed && c.Competitor is TCompetitor)
                .ToListAsync();

            if (typeof(TeamCompetitor).IsAssignableFrom(typeof(TCompetitor)))
                await workflow.Competitors(competitionId, distanceCombinationId)
                    .Select(c => c.Competitor)
                    .OfType<TeamCompetitor>()
                    .Include(tc => tc.Members.Select(m => m.Member))
                    .LoadAsync();

            report.Competitors = competitors.OrderBy(c => c.Reserve).ThenBy(c => c.Competitor.StartNumber);

            report.ReportParameters["OptionalColumnHeader"].Value = Resources.ResourceManager.GetString($"OptionalColumn_{(int)optionalColumns}") ?? "";
            switch (optionalColumns)
            {
                case OptionalReportColumns.HomeVenueCode:
                    report.ReportParameters["OptionalColumnField"].Value = "Competitor.VenueCode";
                    break;
                case OptionalReportColumns.NationalityCode:
                    report.ReportParameters["OptionalColumnField"].Value = "Competitor.NationalityCode";
                    break;
                case OptionalReportColumns.ClubShortName:
                    report.ReportParameters["OptionalColumnField"].Value = "Competitor.ClubShortName";
                    break;
                case OptionalReportColumns.LicenseKey:
                    report.ReportParameters["OptionalColumnField"].Value = "Competitor.LicenseKey";
                    break;
                default:
                    report.OptionalFieldValue = null;
                    break;
            }

            return report;
        }
    }
}