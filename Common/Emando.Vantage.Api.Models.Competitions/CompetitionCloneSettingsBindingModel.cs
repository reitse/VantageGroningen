using System;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class CompetitionCloneSettingsBindingModel
    {
        public bool CloneVenue { get; set; }

        public bool CloneSerie { get; set; }

        public string Name { get; set; }

        public DateTime Starts { get; set; }

        public bool CloneDistances { get; set; }

        public DistanceCloneSettingsBindingModel DistanceCloneSettings { get; set; }

        public bool CloneDistanceCombinations { get; set; }

        public DistanceCombinationCloneSettingsBindingModel DistanceCombinationCloneSettings { get; set; }
    }
}