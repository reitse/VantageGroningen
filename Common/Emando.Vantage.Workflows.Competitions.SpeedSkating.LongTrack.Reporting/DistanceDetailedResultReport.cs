using System.Collections.Generic;
using Emando.Vantage.Workflows.Reporting;
using Telerik.Reporting;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    [DisciplineReport(typeof(DistanceDetailedResultReportLoader), "SpeedSkating.LongTrack.PairsDistance", "DetailedResult", 210)]
    public partial class DetailedResultReport : Report
    {
        public DetailedResultReport()
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