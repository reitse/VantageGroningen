using System.Collections.Generic;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.Events
{
    public class DistanceResultChangedEvent : DistanceEventBase
    {
        public DistanceResultChangedEvent(Distance distance, IList<RankedRace> result) : base(distance)
        {
            Result = result;
        }

        public IList<RankedRace> Result { get; private set; }
    }
}