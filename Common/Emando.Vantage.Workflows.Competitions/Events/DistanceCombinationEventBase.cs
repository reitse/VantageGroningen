using System;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.Events
{
    public class DistanceCombinationEventBase : CompetitionEventBase
    {
        public DistanceCombinationEventBase(DistanceCombination distanceCombination) : base(distanceCombination.CompetitionId)
        {
            if (distanceCombination == null)
                throw new ArgumentNullException(nameof(distanceCombination));

            DistanceCombination = distanceCombination;
        }

        public DistanceCombination DistanceCombination { get; }
    }
}