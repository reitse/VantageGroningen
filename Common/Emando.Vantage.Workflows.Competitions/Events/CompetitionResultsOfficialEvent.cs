using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.Events
{
    public class CompetitionResultsOfficialEvent : CompetitionEventBase
    {
        public CompetitionResultsOfficialEvent(Competition competition) : base(competition.Id)
        {
        }
    }
}