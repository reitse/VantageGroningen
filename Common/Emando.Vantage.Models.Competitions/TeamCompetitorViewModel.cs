namespace Emando.Vantage.Models.Competitions
{
    public class TeamCompetitorViewModel : CompetitorViewModel
    {
        public string Name { get; set; }

        public TeamCompetitorMemberViewModel[] Members { get; set; }
    }
}