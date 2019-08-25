using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.Events
{
    public abstract class RaceLapEventBase : RaceEventBase
    {
        protected RaceLapEventBase(Distance distance, RaceLapState lap) : base(distance, lap.Race)
        {
            Lap = lap;
        }

        public RaceLapState Lap { get; }
    }
}