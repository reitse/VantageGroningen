using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.Events
{
    public class CompetitionResultsUnofficialEvent : CompetitionEventBase
    {
        public CompetitionResultsUnofficialEvent(Competition competition) : base(competition.Id)
        {
        }
    }
}