using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class RankedPersonTime
    {
        public RankedPersonTime(int ranking, PersonTime time, bool sameRankingAsPrevious = false)
        {
            Ranking = ranking;
            Time = time;
            SameRankingAsPrevious = sameRankingAsPrevious;
        }

        public int Ranking { get; }

        public PersonTime Time { get; }

        public bool SameRankingAsPrevious { get; }
    }
}