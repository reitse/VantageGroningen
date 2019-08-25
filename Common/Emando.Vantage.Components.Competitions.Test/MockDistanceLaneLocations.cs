using System.Collections.Generic;
using Emando.Vantage.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Components.Competitions.Test
{
    public class MockDistanceLaneLocations : IDistanceLaneLocations
    {
        public MockDistanceLaneLocations(long start, decimal startOffset, IList<long> finishes)
        {
            Start = start;
            StartOffset = startOffset;
            Finishes = finishes;
        }

        public long Start { get; }

        public decimal StartOffset { get; }

        public IList<long> Finishes { get; }
    }
}