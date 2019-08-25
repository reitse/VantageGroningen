using System.Collections.Generic;
using Emando.Vantage.Workflows.Reporting;
using Telerik.Reporting;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    [DisciplineReport(typeof(DistanceResultReportLoader), "SpeedSkating.LongTrack.PairsDistance", "Result", 210)]
    [DisciplineReport(typeof(DistanceResultReportBookLoader), "SpeedSkating.LongTrack", "Results", 210)]
    public partial class DistanceResultReport : Report
    {
        public DistanceResultReport()
        {
            InitializeComponent();
        }

        public IEnumerable<RankedRace> Races
        {
            get { return racesDataSource.DataSource as IEnumerable<RankedRace>; }
            set { racesDataSource.DataSource = value; }
        }
    }
}