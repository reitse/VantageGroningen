using System.Collections.Generic;
using Emando.Vantage.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.Events
{
    public class PresentedRaceLapsChangedEvent : RaceEventBase
    {
        public PresentedRaceLapsChangedEvent(Distance distance, Race race, IList<CalculatedLap> laps) : base(distance, race)
        {
            Laps = laps;
        }

        public IList<CalculatedLap> Laps { get; set; }
    }
}