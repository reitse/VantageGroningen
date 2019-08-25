using System;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Entities.Competitions
{
    public struct RaceStateLap : IReadOnlyRaceLap
    {
        public RaceState Race { get; }

        public RaceLap Lap { get; }

        public RaceStateLap(RaceState race, RaceLap lap)
        {
            Race = race;
            Lap = lap;
        }

        public TimeSpan Time => Lap.Time;

        public decimal? Points => Lap.Points;

        public Guid RaceId => Lap.RaceId;

        public string InstanceName => Lap.InstanceName;

        public PresentationSource PresentationSource => Lap.PresentationSource;

        public DateTime When => Lap.When;

        public RaceEventFlags Flags => Lap.Flags;

        public int? FixedIndex => Lap.FixedIndex;

        public int? FixedRanking => Lap.FixedRanking;
    }
}