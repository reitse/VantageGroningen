using System;

namespace Emando.Vantage.Models.Competitions
{
    public class RaceLapPointsViewModel
    {
        public Guid RaceId { get; set; }

        public int? Index { get; set; }

        public int? Ranking { get; set; }

        public decimal? Points { get; set; }

        public decimal? TotalPoints { get; set; }

        public TimeSpan Time { get; set; }
    }
}