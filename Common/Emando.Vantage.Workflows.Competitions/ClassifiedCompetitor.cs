using System;
using System.Collections.Generic;
using System.Linq;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class ClassifiedCompetitor
    {
        public ClassifiedCompetitor(CompetitorBase competitor)
        {
            Competitor = competitor;
            Races = new List<ClassifiedRace>();
        }

        public int? Ranking { get; set; }

        public TimeSpan? TimeBehind { get; set; }

        public CompetitorBase Competitor { get; }

        public List<ClassifiedRace> Races { get; }

        public decimal Points { get; set; }

        internal bool AllValid => !AllEmpty && Races.TakeWhile(r => r != null).All(r => r.DistanceRanking != null);

        internal bool AllEmpty => Races.All(r => r == null);

        internal int InvalidSortGroup => Races.Count(r => r?.DistanceRanking != null);
    }
}