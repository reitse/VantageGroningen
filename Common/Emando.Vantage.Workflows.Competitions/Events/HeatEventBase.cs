using Emando.Vantage.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.Events
{
    public abstract class HeatEventBase : DistanceEventBase
    {
        protected HeatEventBase(Distance distance, Heat heat) : base(distance)
        {
            Heat = heat;
        }

        public Heat Heat { get; }
    }
}