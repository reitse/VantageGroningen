using Emando.Vantage.Competitions;

namespace Emando.Vantage.Models.Competitions
{
    public class DistanceDrawSettingsViewModel
    {
        public DistanceDrawGroupMode GroupMode { get; set; }

        public int CategoryLength { get; set; }

        public int GroupSize { get; set; }

        public bool ReverseGroups { get; set; }

        public DistanceDrawMode Mode { get; set; }

        public bool ReverseFilling { get; set; }

        public bool DeleteExisting { get; set; }

        public HistoricalTimeSelectorViewModel[] Selectors { get; set; }

        public DistanceDrawSpreading Spreading { get; set; }
    }
}