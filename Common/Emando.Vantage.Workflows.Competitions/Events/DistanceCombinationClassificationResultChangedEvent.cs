using System.Collections.Generic;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.Events
{
    public class DistanceCombinationClassificationResultChangedEvent : DistanceCombinationEventBase
    {
        public DistanceCombinationClassificationResultChangedEvent(DistanceCombination distanceCombination, IList<ClassifiedCompetitor> result) : base(distanceCombination)
        {
            Result = result;
        }

        public IList<ClassifiedCompetitor> Result { get; private set; }
    }
}