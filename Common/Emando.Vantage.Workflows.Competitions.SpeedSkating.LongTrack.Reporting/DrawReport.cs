using System.Collections.Generic;
using Emando.Vantage.Workflows.Reporting;
using Telerik.Reporting;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    [DisciplineReport(typeof(DrawReportLoader<DrawReport>), "SpeedSkating.LongTrack.PairsDistance.Individual", "Draw", 120)]
    [DisciplineReport(typeof(DrawReportBookLoader), "SpeedSkating.LongTrack", "Draws", 120)]
    public partial class DrawReport : Report, IPairsDrawReportWithOptionalColumn
    {
        public DrawReport()
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