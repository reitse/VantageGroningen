using System.Runtime.Serialization;

namespace Emando.Vantage.Models.Competitions
{
    [KnownType(typeof(DistanceRankingTimeViewModel))]
    [KnownType(typeof(DistancesRankingPointsViewModel))]
    public class RankingViewModelBase
    {
        public string[] Selectors { get; set; }
    }
}