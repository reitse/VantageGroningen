using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Workflows.Competitions.Reporting;
using Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting.Properties;
using Emando.Vantage.Workflows.Reporting;
using Emando.Vantage.Workflows.Reporting.TelerikReports;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    public class DistanceResultReportLoader : IDistanceReportLoader
    {
        private readonly Func<RacesWorkflow> workflowFactory;

        public DistanceResultReportLoader(Func<RacesWorkflow> workflowFactory)
        {
            this.workflowFactory = workflowFactory;
        }

        #region IDistanceReportLoader Members

        public async Task<ILoadedReport> LoadAsync(Guid competitionId, Guid distanceId, OptionalReportColumns optionalColumns)
        {
            using (var workflow = workflowFactory())
            {
                var report = await LoadAsync(workflow, competitionId, distanceId, optionalColumns);
                return new TelerikLoadedReport(report);
            }
        }

        #endregion

        internal static async Task<DistanceResultReport> LoadAsync(RacesWorkflow workflow, Guid competitionId, Guid distanceId, OptionalReportColumns optionalColumns)
        {
            var distance =
                await workflow.Distances.Include(d => d.Competition.Venue).Include(d => d.Competition.ReportTemplate.Logos).FirstOrDefaultAsync(d => d.Id == distanceId);
            if (distance == null)
                throw new DistanceNotFoundException();

            var report = new DistanceResultReport();
            report.SetParameters(distance);
            report.ReportParameters["OptionalColumnHeader"].Value = Resources.ResourceManager.GetString($"OptionalColumn_{(int)optionalColumns}") ?? "";
            switch (optionalColumns)
            {
                case OptionalReportColumns.HomeVenueCode:
                    report.ReportParameters["OptionalColumnField"].Value = "Race.Competitor.VenueCode";
                    break;
                case OptionalReportColumns.NationalityCode:
                    report.ReportParameters["OptionalColumnField"].Value = "Race.Competitor.NationalityCode";
                    break;
                case OptionalReportColumns.ClubShortName:
                    report.ReportParameters["OptionalColumnField"].Value = "Race.Competitor.ClubShortName";
                    break;
                case OptionalReportColumns.LicenseKey:
                    report.ReportParameters["OptionalColumnField"].Value = "Race.Competitor.LicenseKey";
                    break;
                default:
                    report.OptionalFieldTextBox.Value = null;
                    break;
            }

            report.Races = await workflow.GetDistanceResultAsync(distance);
            return report;
        }
    }
}