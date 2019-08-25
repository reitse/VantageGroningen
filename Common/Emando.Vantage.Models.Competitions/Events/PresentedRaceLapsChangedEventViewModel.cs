using System.Collections.Generic;

namespace Emando.Vantage.Models.Competitions.Events
{
    public class PresentedRaceLapsChangedEventViewModel : RaceEventViewModelBase
    {
        public List<CalculatedLapViewModel> Laps { get; set; }
    }
}