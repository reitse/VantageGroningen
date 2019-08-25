using System;

namespace Emando.Vantage.Models.Competitions
{
    public class RaceViewModel
    {
        public Guid Id { get; set; }

        public Guid DistanceId { get; set; }

        public CompetitorViewModel Competitor { get; set; }

        public int Round { get; set; }

        public int Heat { get; set; }

        public int Lane { get; set; }

        public int Color { get; set; }

        public TimeSpan? PersonalBest { get; set; }

        public TimeSpan? SeasonBest { get; set; }

        public RaceResultViewModel Result { get; set; }

        public RaceTimeViewModel Time { get; set; }

        public RaceLapViewModel[] Laps { get; set; }

        public RaceTransponderViewModel[] Transponders { get; set; }
    }
}