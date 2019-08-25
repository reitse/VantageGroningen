using System;
using System.Globalization;
using System.Linq;
using Emando.Vantage.Workflows.Competitions.Reporting;
using Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting.Properties;
using Telerik.Reporting;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    public class ClassificationReportLoader : ClassificationReportLoaderBase
    {
        public ClassificationReportLoader(Func<RacesWorkflow> racesWorkflowFactory) : base(racesWorkflowFactory)
        {
        }

        protected override Report CreateReport(Classification classification, int? behindDistance, OptionalReportColumns optionalColumns)
        {
            var report = new ClassificationReport
            {
                Competitors = classification.Competitors
            };

            for (var i = 0; i < 4; i++)
            {
                var distance = classification.Distances.ElementAtOrDefault(i)?.Value;
                if (i == 3)
                    distance = distance ?? behindDistance;

                report.ReportParameters.Add($"Distance{i + 1}", ReportParameterType.String, distance?.ToString() ?? "");
            }

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
                    report.OptionalFieldTextBox.Value = null;
                    break;
            }

            return report;
        }
    }
}