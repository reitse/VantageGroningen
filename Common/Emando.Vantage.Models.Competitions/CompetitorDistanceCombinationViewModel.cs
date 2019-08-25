using Emando.Vantage.Competitions;

namespace Emando.Vantage.Models.Competitions
{
    public class CompetitorDistanceCombinationViewModel
    {
        public DistanceCombinationViewModel DistanceCombination { get; set; }

        public int? Reserve { get; set; }

        public DistanceCombinationCompetitorStatus Status { get; set; }
    }
}