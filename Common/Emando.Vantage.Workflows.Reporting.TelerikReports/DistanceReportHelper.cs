using System;
using Emando.Vantage.Entities.Competitions;
using Telerik.Reporting;

namespace Emando.Vantage.Components.Reporting.TelerikReports
{
    public static class DistanceReportHelper
    {
        public static void SetParameters(this Report report, Distance distance)
        {
            report.ReportParameters.Add("DistanceName", ReportParameterType.String, distance.Name);
            report.ReportParameters.Add("DistanceNumber", ReportParameterType.Integer, distance.Number);
            report.ReportParameters.Add("DistanceStarts", ReportParameterType.DateTime, distance.Starts);
            report.ReportParameters.Add("TimeDigits", ReportParameterType.Integer, distance.ClassificationPrecision >= TimeSpan.FromMilliseconds(10) ? 2 : 3);
            report.SetParameters(distance.Competition);
        }
    }
}