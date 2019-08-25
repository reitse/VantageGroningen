using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.Events
{
    public abstract class DistanceEventBase : CompetitionEventBase
    {
        public Distance Distance { get; private set; }

        protected DistanceEventBase(Distance distance) : base(distance.CompetitionId)
        {
            Distance = distance;
        }
    }
}