using System;

namespace Emando.Vantage.Models.Competitions
{
    public class DistanceCombinationCompetitorsViewModel
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public string Name { get; set; }

        public string ClassFilter { get; set; }

        public string CategoryFilter { get; set; }

        public DistanceCombinationCompetitorViewModel[] Competitors { get; set; }
    }
}