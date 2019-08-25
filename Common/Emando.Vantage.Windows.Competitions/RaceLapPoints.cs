using System.Collections.Generic;

namespace Emando.Vantage.Windows.Competitions
{
    public class RaceLapPoints
    {
        public RaceLapPoints(RaceLapIndex lap, string type, IDictionary<int, decimal> rankingPoints)
        {
            this.Type = type;
            this.RankingPoints = rankingPoints;
            this.Lap = lap;
        }

        public RaceLapIndex Lap { get; }

        public string Type { get; }

        public IDictionary<int, decimal> RankingPoints { get; }
    }
}