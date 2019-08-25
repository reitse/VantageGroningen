using System;
using Emando.Vantage.Events;

namespace Emando.Vantage.Workflows.Competitions.Events
{
    public abstract class CompetitionEventBase : EventBase
    {
        public Guid CompetitionId { get; private set; }

        protected CompetitionEventBase(Guid competitionId)
        {
            CompetitionId = competitionId;
        }
    }
}