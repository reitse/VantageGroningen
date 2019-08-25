using System;
using System.Text;
using Emando.Vantage.Entities.Competitions;
using Telerik.Reporting;

namespace Emando.Vantage.Workflows.Competitions.Reporting
{
    public static class DistanceReportHelper
    {
        public static void SetParameters(this Report report, Distance distance)
        {
            report.ReportParameters.Add("DistanceName", ReportParameterType.String, distance.Name);
            report.ReportParameters.Add("DistanceNumber", ReportParameterType.Integer, distance.Number);
            report.ReportParameters.Add("DistanceStarts", ReportParameterType.DateTime, distance.Starts).AllowNull = true;
            report.ReportParameters.Add("DistanceLastRaceCommitted", ReportParameterType.DateTime, distance.LastRaceCommitted).AllowNull = true;
            report.ReportParameters.Add("DistanceStarter", ReportParameterType.String, distance.Starter).AllowNull = true;
            report.ReportParameters.Add("DistanceReferee1", ReportParameterType.String, distance.Referee1).AllowNull = true;
            report.ReportParameters.Add("DistanceReferee2", ReportParameterType.String, distance.Referee2).AllowNull = true;
            report.ReportParameters.Add("TimeDigits", ReportParameterType.Integer, distance.ClassificationPrecision >= TimeSpan.FromMilliseconds(10) ? 2 : 3);
            report.SetParameters(distance.Competition);
        }
    }
}