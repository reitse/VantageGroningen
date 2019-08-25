namespace Emando.Vantage.Api.Models.Competitions
{
    public class CompetitionReportsViewModel
    {
        public string[] Reports { get; set; }

        public DistanceReportsViewModel[] Distances { get; set; }
    }
}