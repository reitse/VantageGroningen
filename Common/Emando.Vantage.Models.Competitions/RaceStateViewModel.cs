using System;
using System.Collections.Generic;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Models.Competitions
{
    public class RaceStateViewModel : IRaceState<RacePassingViewModel, RaceLapViewModel>
    {
        public RaceViewModel Race { get; set; }

        public RaceStartViewModel Start { get; set; }

        public RacePassingViewModel[] Passings { get; set; }

        public RaceLapViewModel[] Laps { get; set; }

        public CalculatedLapViewModel[] EstimatedLaps { get; set; }

        Guid IRaceState<RacePassingViewModel, RaceLapViewModel>.RaceId => Race.Id;

        IEnumerable<RacePassingViewModel> IRaceState<RacePassingViewModel, RaceLapViewModel>.Passings => Passings;

        IEnumerable<RaceLapViewModel> IRaceState<RacePassingViewModel, RaceLapViewModel>.Laps => Laps;
    }
}