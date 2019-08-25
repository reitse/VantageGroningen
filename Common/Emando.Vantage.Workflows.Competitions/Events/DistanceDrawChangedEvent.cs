using System.Collections.Generic;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.Events
{
    public class DistanceDrawChangedEvent : DistanceEventBase
    {
        public DistanceDrawChangedEvent(Distance distance, IList<Race> draw) : base(distance)
        {
            Draw = draw;
        }

        public IList<Race> Draw { get; private set; }
    }
}