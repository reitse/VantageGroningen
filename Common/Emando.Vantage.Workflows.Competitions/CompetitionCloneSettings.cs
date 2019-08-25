using System;

namespace Emando.Vantage.Workflows.Competitions
{
    public struct CompetitionCloneSettings
    {
        public bool CloneVenue { get; set; }

        public bool CloneSerie { get; set; }

        public string Name { get; set; }

        public DateTime Starts { get; set; }

        public bool CloneDistances { get; set; }

        public DistanceCloneSettings DistanceCloneSettings { get; set; }

        public bool CloneDistanceCombinations { get; set; }

        public DistanceCombinationCloneSettings DistanceCombinationCloneSettings { get; set; }
    }
}