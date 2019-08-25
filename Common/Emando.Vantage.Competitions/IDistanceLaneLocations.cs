using System.Collections.Generic;

namespace Emando.Vantage.Competitions
{
    public interface IDistanceLaneLocations
    {
        long Start { get; }

        decimal StartOffset { get; }

        IList<long> Finishes { get; }
    }
}