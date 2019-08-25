using System.Collections.Generic;
using Emando.Vantage.Workflows.Reporting;
using Telerik.Reporting;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    [DisciplineReport(typeof(RankingReportLoader), "SpeedSkating.LongTrack", "Ranking")]
    public partial class TimeRankingReport : Report
    {
        public TimeRankingReport()
        {
            InitializeComponent();
        }

        public IEnumerable<RankedPersonTime> RankedPeople
        {
            get { return peopleDataSource.DataSource as IEnumerable<RankedPersonTime>; }
            set { peopleDataSource.DataSource = value; }
        }
    }
}