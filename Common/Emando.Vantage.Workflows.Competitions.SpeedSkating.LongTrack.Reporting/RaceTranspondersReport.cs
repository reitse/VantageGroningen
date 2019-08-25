using System.Collections.Generic;
using System.Linq;
using Emando.Vantage.Entities.Competitions;
using Emando.Vantage.Workflows.Reporting;
using Telerik.Reporting;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    [DisciplineReport(typeof(RaceTranspondersReportLoader), "SpeedSkating.LongTrack.PairsDistance", "Transponders", 300)]
    public partial class RaceTranspondersReport : Report
    {
        public RaceTranspondersReport()
        {
            InitializeComponent();
        }

        public IEnumerable<Pair> Pairs
        {
            get { return pairsDataSource.DataSource as IEnumerable<Pair>; }
            set { pairsDataSource.DataSource = value; }
        }

        public static string FormatTransponders(Race race)
        {
            if (race == null)
                return null;

            var competitors = new List<PersonCompetitor>();
            var personCompetitor = race.Competitor as PersonCompetitor;
            if (personCompetitor != null)
                competitors.Add(personCompetitor);
            else
            {
                var teamCompetitor = race.Competitor as TeamCompetitor;
                if (teamCompetitor?.Members != null)
                    competitors.AddRange(teamCompetitor.Members.OrderBy(m => m.Order).Select(m => m.Member));
            }

            var competitorTransponders = competitors.Select(c => new
            {
                c.ShortName,
                Set = race.Transponders.Where(t => t.PersonId == c.PersonId).Select(t => t.Set).FirstOrDefault()
            }).Where(t => t.Set.HasValue).ToList();

            if (competitorTransponders.Count == 1)
                return $"{competitorTransponders[0].Set.Value}";

            return string.Join(", ", competitorTransponders.Select(t => $"<b>{t.Set}</b> ({t.ShortName})"));
        }
    }
}