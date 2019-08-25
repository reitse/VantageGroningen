using System.Linq;
using System.Collections.Generic;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    public class CompetitorGroup
    {
        public CompetitorGroup(string name, IEnumerable<RankedRace> races)
        {
            Name = name;
            Races = (from r in races
                     where r.TotalPoints > 0
                     orderby r.Race.Distance.Number, r.Race.Heat, r.Race.Lane
                     select r).ToDictionary(r => r.Race, r => r.TotalPoints);
            TotalPoints = races.Sum(r => r.TotalPoints);
        }

        public string Name { get; }

        public IDictionary<Race, decimal> Races { get; }

        public decimal TotalPoints { get; }

        public int? Ranking { get; internal set; }

        public bool SameRankingAsPrevious { get; internal set; }
    }

    public static class CompetitorGroupExtensions
    {
        public static IList<CompetitorGroup> SortAndRank(this IEnumerable<CompetitorGroup> groups)
        {
            var result = groups.OrderByDescending(g => g.TotalPoints).ToList();
            var previousPoints = new decimal?();
            for (var i = 0; i < result.Count; i++)
            {
                var group = result[i];
                group.Ranking = i + 1;
                group.SameRankingAsPrevious = group.TotalPoints == previousPoints;
                previousPoints = group.TotalPoints;
            }
            return result;
        }
    }
}
