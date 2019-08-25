using System.Collections.Generic;

namespace Emando.Vantage.Models.Competitions.Events
{
    public class DistanceDrawChangedEventViewModel : DistanceEventViewModelBase
    {
        public List<RaceViewModel> Draw { get; set; }
    }
}