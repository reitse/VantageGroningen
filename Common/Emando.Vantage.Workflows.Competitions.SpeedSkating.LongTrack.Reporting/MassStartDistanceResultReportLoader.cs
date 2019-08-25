using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Components.Competitions.SpeedSkating.LongTrack;
using Emando.Vantage.Workflows.Competitions.Reporting;
using Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting.Properties;
using Emando.Vantage.Workflows.Reporting;
using Emando.Vantage.Workflows.Reporting.TelerikReports;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    public class MassStartDistanceResultReportLoader : IDistanceReportLoader
    {
        private readonly Func<RacesWorkflow> workflowFactory;

        public MassStartDistanceResultReportLoader(Func<RacesWorkflow> workflowFactory)
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

        internal static async Task<MassStartDistanceResultReport> LoadAsync(RacesWorkflow workflow, Guid competitionId, Guid distanceId, OptionalReportColumns optionalColumns)
        {
            var distance =
                await workflow.Distances.Include(d => d.Competition.Venue).Include(d => d.Competition.ReportTemplate.Logos).FirstOrDefaultAsync(d => d.Id == distanceId);
            if (distance == null)
                throw new DistanceNotFoundException();

            var report = new MassStartDistanceResultReport();
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

            var races = await workflow.GetDistanceResultByLapPointsAsync(distance);
            var lapsWithPoints = races.SelectMany(r => r.LapPoints.Keys)
                .OrderBy(i => i)
                .Distinct()
                .Select(i => new
                {
                    Index = i,
                    RoundsToGo = MassStartDistanceCalculator.Default.RoundsToGo(distance, i + 1)
                })
                .ToList();

            report.Races = races;
            foreach (var i in new[] { 1, 2, 3, 4 })
            {
                report.ReportParameters[$"Round{i}Header"].Value = lapsWithPoints.ElementAtOrDefault(i - 1)?.RoundsToGo.ToString(CultureInfo.InvariantCulture);
                report.ReportParameters[$"Round{i}LapIndex"].Value = lapsWithPoints.ElementAtOrDefault(i - 1)?.Index;
            }

            return report;
        }
    }
}