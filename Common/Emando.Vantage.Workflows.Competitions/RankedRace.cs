using Emando.Vantage.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class RankedRace
    {
        public RankedRace(int? ranking, Race race, decimal totalPoints, bool sameRankingAsPrevious = false)
        {
            Ranking = ranking;
            Race = race;
            TotalPoints = totalPoints;
            SameRankingAsPrevious = sameRankingAsPrevious;
        }

        public int? Ranking { get; }

        public Race Race { get; }

        public bool SameRankingAsPrevious { get; }

        public decimal TotalPoints { get; }

        public bool ShowHeat => Race.PresentedResult?.TimeInvalidReason != TimeInvalidReason.Withdrawn;

        public bool ShowLane => Race.PresentedResult?.TimeInvalidReason != TimeInvalidReason.Withdrawn;
    }
}