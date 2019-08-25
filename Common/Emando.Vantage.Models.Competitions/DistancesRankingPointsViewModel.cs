namespace Emando.Vantage.Models.Competitions
{
    public class DistancesRankingPointsViewModel : RankingViewModelBase
    {
        public int[] Distances { get; set; }
        
        public RankedPersonPointsViewModel[] Times { get; set; }
    }
}