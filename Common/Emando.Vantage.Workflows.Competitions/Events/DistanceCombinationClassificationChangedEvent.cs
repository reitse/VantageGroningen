using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.Events
{
    public class DistanceCombinationClassificationChangedEvent : DistanceCombinationEventBase
    {
        public DistanceCombinationClassificationChangedEvent(DistanceCombination distanceCombination) : base(distanceCombination)
        {
        }
    }
}