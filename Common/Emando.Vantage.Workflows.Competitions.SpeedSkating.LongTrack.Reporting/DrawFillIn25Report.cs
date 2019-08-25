using System.Collections.Generic;
using Emando.Vantage.Workflows.Reporting;
using Telerik.Reporting;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    [DisciplineReport(typeof(DrawFillInReportLoader), "SpeedSkating.LongTrack.PairsDistance", "FillIn", 200)]
    [DisciplineReport(typeof(DrawFillInReportBookLoader), "SpeedSkating.LongTrack", "FillIns", 200)]
    public partial class DrawFillIn25Report : Report, IPairsDrawReport
    {
        public DrawFillIn25Report()
        {
            InitializeComponent();
        }

        public IEnumerable<Pair> Pairs
        {
            get { return pairsDataSource.DataSource as IEnumerable<Pair>; }
            set { pairsDataSource.DataSource = value; }
        }
    }
}