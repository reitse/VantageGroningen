using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.Events
{
    public class RacePassingEventBase : RaceEventBase
    {
        public RacePassingEventBase(Distance distance, RacePassingState passing)
            : base(distance, passing.Race)
        {
            Passing = passing;
        }

        public RacePassingState Passing { get; }
    }
}