using System.Collections.Generic;
using Emando.Vantage.Workflows.Reporting;
using Telerik.Reporting;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    [DisciplineReport(typeof(DrawReportLoader<TeamDrawReport>), "SpeedSkating.LongTrack.PairsDistance.TeamPursuit", "Draw", 110)]
    [DisciplineReport(typeof(DrawReportLoader<TeamDrawReport>), "SpeedSkating.LongTrack.PairsDistance.TeamSprint", "Draw", 110)]
    public partial class TeamDrawReport : Report, IPairsDrawReportWithOptionalColumn
    {
        public TeamDrawReport()
        {
            InitializeComponent();
        }

        public IEnumerable<Pair> Pairs
        {
            get { return pairsDataSource.DataSource as IEnumerable<Pair>; }
            set { pairsDataSource.DataSource = value; }
        }

        public string InnerOptionalFieldValue
        {
            get { return InnerOptionalFieldTextBox.Value; }
            set { InnerOptionalFieldTextBox.Value = value; }
        }

        public string OuterOptionalFieldValue
        {
            get { return OuterOptionalFieldTextBox.Value; }
            set { OuterOptionalFieldTextBox.Value = value; }
        }
    }
}