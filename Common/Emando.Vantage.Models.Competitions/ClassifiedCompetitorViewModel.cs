using System;

namespace Emando.Vantage.Models.Competitions
{
    public class ClassifiedCompetitorViewModel
    {
        public int? Ranking { get; set; }

        public CompetitorViewModel Competitor { get; set; }

        public int RacesCount { get; set; }

        public decimal Points { get; set; }

        public TimeSpan? TimeBehind { get; set; }
    }
}