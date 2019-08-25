using Emando.Vantage.Workflows.Reporting;
using Telerik.Reporting;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.Inline.Reporting
{
    [DisciplineReport(typeof(CompetitorSignatureListsReportLoader), "SpeedSkating.Inline", "CompetitorSignatureLists")]
    public partial class CompetitorSignatureListsReport : Report
    {
        public CompetitorSignatureListsReport()
        {
            InitializeComponent();
        }

        public string CompetitionName
        {
            get { return ReportParameters["CompetitionName"].Value as string; }
            set { ReportParameters["CompetitionName"].Value = value; }
        }

        public object DistanceCombinations
        {
            get { return distanceCombinationsDataSource.DataSource; }
            set { distanceCombinationsDataSource.DataSource = value; }
        }
    }
}