using System;

namespace Emando.Vantage.Models.Competitions
{
    public class RaceDetailsViewModel
    {
        public Guid Id { get; set; }

        public int Heat { get; set; }

        public Guid DistanceId { get; set; }

        public int Lane { get; set; }

        public int Color { get; set; }

        public CompetitorViewModel Competitor { get; set; }

        public string PresentedInstanceName { get; set; }

        public RaceResultViewModel[] Results { get; set; }

        public RaceTimeViewModel[] Times { get; set; }

        public InstanceRaceLapsViewModel[] Laps { get; set; }

        public RaceTransponderViewModel[] Transponders { get; set; }
    }
}