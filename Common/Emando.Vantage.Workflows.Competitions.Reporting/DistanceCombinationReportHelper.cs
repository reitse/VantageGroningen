using Emando.Vantage.Entities.Competitions;
using Telerik.Reporting;

namespace Emando.Vantage.Workflows.Competitions.Reporting
{
    public static class DistanceCombinationReportHelper
    {
        public static void SetParameters(this Report report, DistanceCombination distanceCombination)
        {
            report.ReportParameters.Add("DistanceCombinationName", ReportParameterType.String, distanceCombination.Name);
            report.ReportParameters.Add("DistanceCombinationNumber", ReportParameterType.Integer, distanceCombination.Number);
            report.SetParameters(distanceCombination.Competition);
        }
    }
}