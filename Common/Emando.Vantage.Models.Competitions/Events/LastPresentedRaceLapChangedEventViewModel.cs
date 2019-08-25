using System;

namespace Emando.Vantage.Models.Competitions.Events
{
    public class LastPresentedRaceLapChangedEventViewModel : RaceEventViewModelBase
    {
        public CalculatedLapViewModel Lap { get; set; }

        public TimeSpan? TimeDifference { get; set; }
    }
}