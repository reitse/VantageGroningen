using System;
using System.Collections.Generic;
using System.Linq;
using Emando.Vantage.Workflows.Competitions.Reporting;
using Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting.Properties;
using Telerik.Reporting;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    public class RankingReportLoader : RankingReportLoaderBase
    {
        public RankingReportLoader(Func<PersonTimesWorkflow> personTimesWorkflowFactory) : base(personTimesWorkflowFactory)
        {
        }

        protected override Report CreateTimeReport(int distance, IList<RankedPersonTime> people, OptionalReportColumns optionalColumns)
        {
            if (people.Count == 0)
                return null;

            var report = new TimeRankingReport
            {
                RankedPeople = people
            };
            report.ReportParameters["Distance"].Value = distance;
            report.ReportParameters["OptionalColumnHeader"].Value = Resources.ResourceManager.GetString($"OptionalColumn_{(int)optionalColumns}") ?? "";

            switch (optionalColumns)
            {
                case OptionalReportColumns.HomeVenueCode:
                    report.ReportParameters["OptionalColumnField"].Value = "Time.License.VenueCode";
                    break;
                case OptionalReportColumns.NationalityCode:
                    report.ReportParameters["OptionalColumnField"].Value = "Time.License.Person.NationalityCode";
                    break;
                case OptionalReportColumns.ClubShortName:
                    report.ReportParameters["OptionalColumnField"].Value = "Time.License.Club.ShortName";
                    break;
                case OptionalReportColumns.LicenseKey:
                    report.ReportParameters["OptionalColumnField"].Value = "Time.License.Key";
                    break;
                default:
                    report.OptionalFieldTextBox.Value = null;
                    break;
            }
            return report;
        }

        protected override Report CreatePointsReport(IList<int> distances, IList<RankedPersonPoints> people, OptionalReportColumns optionalColumns)
        {
            if (people.Count == 0)
                return null;

            var report = new PointsRankingReport
            {
                RankedPeople = people
            };
            report.ReportParameters["Distances"].Value = string.Join(" + ", distances);
            for (var i = 0; i < 4; i++)
                report.ReportParameters[$"Distance{i + 1}"].Value = distances.Select(d => new int?(d)).ElementAtOrDefault(i);
            report.ReportParameters["OptionalColumnHeader"].Value = Resources.ResourceManager.GetString($"OptionalColumn_{(int)optionalColumns}") ?? "";

            switch (optionalColumns)
            {
                case OptionalReportColumns.HomeVenueCode:
                    report.ReportParameters["OptionalColumnField"].Value = "License.VenueCode";
                    break;
                case OptionalReportColumns.NationalityCode:
                    report.ReportParameters["OptionalColumnField"].Value = "License.Person.NationalityCode";
                    break;
                case OptionalReportColumns.ClubShortName:
                    report.ReportParameters["OptionalColumnField"].Value = "License.Club.ShortName";
                    break;
                case OptionalReportColumns.LicenseKey:
                    report.ReportParameters["OptionalColumnField"].Value = "License.Key";
                    break;
                default:
                    report.OptionalFieldTextBox.Value = null;
                    break;
            }
            return report;
        }
    }
}