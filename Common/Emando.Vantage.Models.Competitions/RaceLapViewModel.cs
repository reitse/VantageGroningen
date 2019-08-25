using System;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Models.Competitions
{
    public class RaceLapViewModel : IReadOnlyRaceLap, IHaveRacePassingKey
    {
        public Guid RaceId { get; set; }

        public string InstanceName { get; set; }

        public PresentationSource PresentationSource { get; set; }

        public DateTime When { get; set; }

        public TimeSpan Time { get; set; }

        public RaceEventFlags Flags { get; set; }

        public decimal? Points { get; set; }

        public int? FixedIndex { get; set; }

        public int? FixedRanking { get; set; }
    }
}