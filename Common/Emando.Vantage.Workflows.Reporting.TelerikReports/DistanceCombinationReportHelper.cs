using System;
using Emando.Vantage.Entities.Competitions;
using Telerik.Reporting;

namespace Emando.Vantage.Components.Reporting.TelerikReports
{
    public static class DistanceCombinationReportHelper
    {
        public static void SetParameters(this Report report, DistanceCombination distanceCombination)
        {
            report.ReportParameters.Add("DistanceCombinationName", ReportParameterType.String, distanceCombination.Name);
            report.ReportParameters.Add("DistanceCombinationNumber", ReportParameterType.Integer, distanceCombination.Number);
            report.ReportParameters.Add("DistanceCombinationStarts", ReportParameterType.DateTime, distanceCombination.Starts);
            report.SetParameters(distanceCombination.Competition);
        }
    }
}