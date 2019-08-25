using System;
using System.Collections.Generic;
using System.Linq;
using Emando.Vantage.Workflows.Reporting;
using Telerik.Reporting;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    [DisciplineReport(typeof(ClassificationReportLoader), "SpeedSkating.LongTrack", "Classification")]
    public partial class ClassificationReport : Report
    {
        public ClassificationReport()
        {
            InitializeComponent();
        }

        public IEnumerable<ClassifiedCompetitor> Competitors
        {
            get { return competitorsDataSource.DataSource as IEnumerable<ClassifiedCompetitor>; }
            set { competitorsDataSource.DataSource = value; }
        }

        public static bool HasFirst(IList<ClassifiedRace> races)
        {
            return GetFirst(races) != null;
        }

        public static ClassifiedRace GetFirst(IList<ClassifiedRace> races)
        {
            return races.ElementAtOrDefault(0);
        }

        public static bool HasSecond(IList<ClassifiedRace> races)
        {
            return GetSecond(races) != null;
        }

        public static ClassifiedRace GetSecond(IList<ClassifiedRace> races)
        {
            return races.ElementAtOrDefault(1);
        }

        public static bool HasThird(IList<ClassifiedRace> races)
        {
            return GetThird(races) != null;
        }

        public static ClassifiedRace GetThird(IList<ClassifiedRace> races)
        {
            return races.ElementAtOrDefault(2);
        }

        public static bool HasFourth(IList<ClassifiedRace> races)
        {
            return GetFourth(races) != null;
        }

        public static ClassifiedRace GetFourth(IList<ClassifiedRace> races)
        {
            return races.ElementAtOrDefault(3);
        }

        public static long GetPrecision(ClassifiedRace race)
        {
            return race?.Race.Distance.ClassificationPrecision == TimeSpan.FromMilliseconds(10) ? 2 : 3;
        }
    }
}