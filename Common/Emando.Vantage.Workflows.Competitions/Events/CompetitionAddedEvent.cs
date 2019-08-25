using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.Events
{
    public class CompetitionAddedEvent : CompetitionEventBase
    {
        public Competition Competition { get; set; }

        public CompetitionAddedEvent(Competition competition) : base(competition.Id)
        {
            Competition = competition;
        }
    }
}