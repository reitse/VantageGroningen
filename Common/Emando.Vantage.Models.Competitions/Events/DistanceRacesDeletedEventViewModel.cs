using System.Collections.Generic;

namespace Emando.Vantage.Models.Competitions.Events
{
    public class DistanceRacesDeletedEventViewModel : DistanceEventViewModelBase
    {
        public List<RaceChangeViewModel> Races { get; set; }
    }
}