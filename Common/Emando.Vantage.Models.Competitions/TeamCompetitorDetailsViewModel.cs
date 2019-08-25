namespace Emando.Vantage.Models.Competitions
{
    public class TeamCompetitorDetailsViewModel : CompetitorDetailsViewModel
    {
        public string Name { get; set; }

        public TeamCompetitorMemberViewModel[] Members { get; set; }
    }
}