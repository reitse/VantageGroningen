using System.Collections.Generic;

namespace Emando.Vantage.Models.Competitions.Events
{
    public class DistanceResultChangedEventViewModel : DistanceEventViewModelBase
    {
        public List<RankedRaceViewModel> Result { get; set; }
    }
}