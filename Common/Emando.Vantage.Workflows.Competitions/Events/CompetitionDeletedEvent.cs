using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.Events
{
    public class CompetitionDeletedEvent : CompetitionEventBase
    {
        public CompetitionDeletedEvent(Competition competition) : base(competition.Id)
        {
        }
    }
}