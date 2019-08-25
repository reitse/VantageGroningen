using System.Collections.Generic;
using Emando.Vantage.Entities.Competitions;
using Emando.Vantage.Workflows.Reporting;
using Telerik.Reporting;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    [DisciplineReport(typeof(CompetitorListReportBookLoader), "SpeedSkating.LongTrack", "CompetitorList", 110)]
    public partial class CompetitorListReport : Report, ICompetitorListReportWithOptionalColumn
    {
        public CompetitorListReport()
        {
            InitializeComponent();
        }

        #region ICompetitorListReportWithOptionalColumn Members

        public IEnumerable<DistanceCombinationCompetitor> Competitors
        {
            get { return competitorsDataSource.DataSource as IEnumerable<DistanceCombinationCompetitor>; }
            set { competitorsDataSource.DataSource = value; }
        }

        public string OptionalFieldValue
        {
            get { return OptionalFieldTextBox.Value; }
            set { OptionalFieldTextBox.Value = value; }
        }

        #endregion
    }
}