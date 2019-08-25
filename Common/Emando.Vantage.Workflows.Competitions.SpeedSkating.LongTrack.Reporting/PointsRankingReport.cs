using System.Collections.Generic;
using Emando.Vantage.Workflows.Reporting;
using Telerik.Reporting;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    [DisciplineReport(typeof(RankingReportLoader), "SpeedSkating.LongTrack", "Ranking")]
    public partial class PointsRankingReport : Report
    {
        public PointsRankingReport()
        {
            InitializeComponent();
        }

        public IEnumerable<RankedPersonPoints> RankedPeople
        {
            get { return peopleDataSource.DataSource as IEnumerable<RankedPersonPoints>; }
            set { peopleDataSource.DataSource = value; }
        }
    }
}