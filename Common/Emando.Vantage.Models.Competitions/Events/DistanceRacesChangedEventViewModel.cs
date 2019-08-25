using System.Collections.Generic;

namespace Emando.Vantage.Models.Competitions.Events
{
    public class DistanceRacesChangedEventViewModel : DistanceEventViewModelBase
    {
        public List<RaceChangeViewModel> Races { get; set; }
    }
}