namespace Emando.Vantage.Models.Competitions
{
    public class DistanceRankingTimeViewModel : RankingViewModelBase
    {
        public int Distance { get; set; }

        public RankedPersonTimeViewModel[] Times { get; set; }
    }
}