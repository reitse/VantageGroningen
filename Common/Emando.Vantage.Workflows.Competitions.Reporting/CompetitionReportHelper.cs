using System;
using Emando.Vantage.Entities.Competitions;
using Telerik.Reporting;

namespace Emando.Vantage.Workflows.Competitions.Reporting
{
    public static class CompetitionReportHelper
    {
        public static void SetParameters(this Report report, Competition competition)
        {
            report.ReportParameters.Add("CompetitionName", ReportParameterType.String, competition.Name);
            report.ReportParameters.Add("CompetitionStarts", ReportParameterType.DateTime, competition.Starts);
            report.ReportParameters.Add("CompetitionEnds", ReportParameterType.DateTime, competition.Ends);
            report.ReportParameters.Add("VenueName", ReportParameterType.String, competition.Venue?.Name ?? "");
            report.ReportParameters.Add("VenueCity", ReportParameterType.String, competition.Venue?.Address.City ?? competition.Location ?? "");
            report.ReportParameters.Add("TimeZone", ReportParameterType.String, competition.TimeZone ?? TimeZoneInfo.Local.Id);

            if (competition.ReportTemplate != null)
                foreach (var logo in competition.ReportTemplate.Logos)
                {
                    string name = $"Logo_{logo.Name}";
                    if (!report.ReportParameters.Contains(name))
                        continue;

                    report.ReportParameters[name].Value = Convert.ToBase64String(logo.Image);
                }
        }
    }
}