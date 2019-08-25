using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.Events
{
    public class CompetitionChangedEvent : CompetitionEventBase
    {
        public Competition Competition { get; set; }

        public CompetitionChangedEvent(Competition competition) : base(competition.Id)
        {
            Competition = competition;
        }
    }
}