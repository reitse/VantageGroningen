namespace Emando.Vantage.Models.Competitions
{
    public class RaceLapGroupViewModel
    {
        public RaceLapViewModel Presented { get; set; }

        public RaceLapViewModel[] NotPresented { get; set; }
    }
}