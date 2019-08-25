using System;

namespace Emando.Vantage.Models.Competitions
{
    public class DistanceCombinationViewModel
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public string Name { get; set; }

        public string ClassFilter { get; set; }

        public string CategoryFilter { get; set; }

        public int ClassificationWeight { get; set; }

        public DistanceCombinationDistanceViewModel[] Distances { get; set; }

        public DateTime? Starts { get; set; }

        public int CompetitorsTotal { get; set; }

        public int CompetitorsPending { get; set; }

        public int CompetitorsConfirmed { get; set; }
        
        public int CompetitorsWithdrawn { get; set; }
    }
}