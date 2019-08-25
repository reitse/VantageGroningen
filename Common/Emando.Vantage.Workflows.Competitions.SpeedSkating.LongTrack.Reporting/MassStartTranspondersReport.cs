using System.Collections.Generic;
using System.Linq;
using Emando.Vantage.Entities.Competitions;
using Emando.Vantage.Workflows.Reporting;
using Telerik.Reporting;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    [DisciplineReport(typeof(MassStartTranspondersReportLoader), "SpeedSkating.LongTrack.MassStartDistance", "Transponders", 300)]
    public partial class MassStartTranspondersReport : Report
    {
        public MassStartTranspondersReport()
        {
            InitializeComponent();
        }

        public IEnumerable<Race> Races
        {
            get { return racesDataSource.DataSource as IEnumerable<Race>; }
            set { racesDataSource.DataSource = value; }
        }

        public static string FormatTransponders(IEnumerable<RaceTransponder> transponders)
        {
            var set = transponders.FirstOrDefault(t => t.Set.HasValue);
            return $"{set?.Set}";
        }
    }
}