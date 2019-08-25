using Emando.Vantage.Entities.Competitions;
using Telerik.Reporting;

namespace Emando.Vantage.Components.Reporting.TelerikReports
{
    public static class CompetitionReportHelper
    {
        public static void SetParameters(this Report report, Competition competition)
        {
            report.ReportParameters.Add("CompetitionName", ReportParameterType.String, competition.Name);
            report.ReportParameters.Add("CompetitionStarts", ReportParameterType.DateTime, competition.Starts);
            report.ReportParameters.Add("CompetitionEnds", ReportParameterType.DateTime, competition.Ends);
            report.ReportParameters.Add("VenueName", ReportParameterType.String, competition.Venue.Name);
            report.ReportParameters.Add("VenueCity", ReportParameterType.String, competition.Venue.Address.City);
        }
    }
}