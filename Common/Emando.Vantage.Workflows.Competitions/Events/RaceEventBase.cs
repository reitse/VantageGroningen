using Emando.Vantage.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.Events
{
    public abstract class RaceEventBase : HeatEventBase
    {
        protected RaceEventBase(Distance distance, Race race) : base(distance, new Heat(race.Round, race.Heat))
        {
            Race = race;
        }

        public Race Race { get; }
    }
}