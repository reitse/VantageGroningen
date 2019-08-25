namespace Emando.Vantage.Models.Competitions
{
    public class RankedRaceViewModel
    {
        public int? Ranking { get; set; }

        public RaceViewModel Race { get; set; }

        public bool SameRankingAsPrevious { get; set; }
    }
}