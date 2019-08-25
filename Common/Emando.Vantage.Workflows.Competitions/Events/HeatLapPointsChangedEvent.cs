using System.Collections.Generic;
using System.Linq;
using Emando.Vantage.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.Events
{
    public class HeatLapPointsChangedEvent : HeatEventBase
    {
        public HeatLapPointsChangedEvent(Distance distance, Heat heat, int index, decimal rounds, decimal roundsToGo, int passedLength,
            IReadOnlyList<RaceLapState> laps) : base(distance, heat)
        {
            Index = index;
            Rounds = rounds;
            RoundsToGo = roundsToGo;
            PassedLength = passedLength;
            Laps = laps;
        }

        public int Index { get; }

        public decimal Rounds { get; }

        public decimal RoundsToGo { get; }

        public int PassedLength { get; }

        public IReadOnlyList<RaceLapState> Laps { get; }
    }
}