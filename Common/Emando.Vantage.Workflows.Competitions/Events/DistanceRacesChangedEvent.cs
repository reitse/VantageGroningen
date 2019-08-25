using System.Collections.Generic;
using System.Linq;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.Events
{
    public class DistanceRacesChangedEvent : DistanceEventBase
    {
        public DistanceRacesChangedEvent(Distance distance, IEnumerable<Race> races) : base(distance)
        {
            Races = races.ToList();
        }

        public IList<Race> Races { get; set; }
    }
}