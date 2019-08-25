using System;

namespace Emando.Vantage.Competitions
{
    public interface IHaveRacePassingKey
    {
        Guid RaceId { get; }

        string InstanceName { get; }

        PresentationSource PresentationSource { get; }

        DateTime When { get; }

        TimeSpan Time { get; }

        RaceEventFlags Flags { get; }
    }
}