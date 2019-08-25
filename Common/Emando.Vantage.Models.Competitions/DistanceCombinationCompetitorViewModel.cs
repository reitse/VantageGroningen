using Emando.Vantage.Competitions;

namespace Emando.Vantage.Models.Competitions
{
    public class DistanceCombinationCompetitorViewModel
    {
        public CompetitorViewModel Competitor { get; set; }

        public int? Reserve { get; set; }

        public DistanceCombinationCompetitorStatus Status { get; set; }
    }
}