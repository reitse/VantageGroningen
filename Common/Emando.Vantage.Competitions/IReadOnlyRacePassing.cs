using System;

namespace Emando.Vantage.Competitions
{
    public interface IReadOnlyRacePassing : IReadOnlyPassing
    {
        Guid RaceId { get; }

        string InstanceName { get; }

        DateTime When { get; }

        RaceEventFlags Flags { get; }

        PresentationSource PresentationSource { get; }
    }
}