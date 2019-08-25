using System.Linq;
using System.Collections.Generic;
using Emando.Vantage.Workflows.Reporting;
using Telerik.Reporting;
using Telerik.Reporting.Drawing;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    [DisciplineReport(typeof(CompetitorGroupPointsReportLoader), "SpeedSkating.LongTrack", "CompetitorGroupPoints", 220)]
    public partial class CompetitorGroupPointsRankingReport : Report
    {
        private static readonly Unit groupWidth = Unit.Cm(0.6);

        public CompetitorGroupPointsRankingReport()
        {
            InitializeComponent();
        }

        public void AddGroups(IEnumerable<CompetitorGroup> groups)
        {
            groupsDataSource.DataSource = groups;

            var i = table.ColumnGroups.Count;
            foreach (var distance in groups.SelectMany(g => g.Races).Select(r => r.Key.Distance.Number).Distinct().OrderBy(n => n))
            {
                table.Body.Columns.Add(new TableBodyColumn(groupWidth));

                var header = new TextBox();
                header.Size = new SizeU(groupWidth, Unit.Cm(0.6));
                header.Style.BorderStyle.Bottom = BorderType.Solid;
                header.Style.Font.Bold = true;
                header.Style.TextAlign = HorizontalAlign.Center;
                header.Value = distance.ToString();
                table.ColumnGroups.Add(new TableGroup
                {
                    ReportItem = header
                });

                var cell = new TextBox();
                cell.Size = new SizeU(groupWidth, Unit.Cm(0.6));
                cell.Style.VerticalAlign = VerticalAlign.Middle;
                cell.Style.TextAlign = HorizontalAlign.Center;
                cell.Format = "{0:#}";
                cell.Value = string.Format("= Global.PointsAtDistance(Fields.Races, {0})", distance);
                table.Body.SetCellContent(0, i, cell);

                i++;
            }
        }
    }
}