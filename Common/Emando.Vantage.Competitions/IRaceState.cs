using System;
using System.Collections.Generic;

namespace Emando.Vantage.Competitions
{
    public interface IRaceState<out TRacePassing, out TRaceLap>
        where TRacePassing : IReadOnlyRacePassing, IHaveRacePassingKey
        where TRaceLap : IReadOnlyRaceLap, IHaveRacePassingKey
    {
        Guid RaceId { get; }

        IEnumerable<TRacePassing> Passings { get; }
        
        IEnumerable<TRaceLap> Laps { get; }  
    }
}