namespace Emando.Vantage.Models.Competitions
{
    public class CompetitionStructureViewModel : CompetitionViewModel
    {
        public DistanceViewModel[] Distances { get; set; }

        public DistanceCombinationViewModel[] DistanceCombinations { get; set; }
    }
}