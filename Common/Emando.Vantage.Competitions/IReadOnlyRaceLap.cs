using System;

namespace Emando.Vantage.Competitions
{
    public interface IReadOnlyRaceLap : IReadOnlyLap
    {
        Guid RaceId { get; }

        string InstanceName { get; }

        PresentationSource PresentationSource { get; }

        DateTime When { get; }

        RaceEventFlags Flags { get; }

        int? FixedIndex { get; }

        int? FixedRanking { get; }
    }
}