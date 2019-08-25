using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.Events
{
    public class LastRaceSpeedChangedEvent : RacePassingEventBase
    {
        public LastRaceSpeedChangedEvent(Distance distance, RacePassingState passing) : base(distance, passing)
        {
        }
    }
}