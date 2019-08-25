using System.Collections.Generic;

namespace Emando.Vantage.Models.Competitions.Events
{
    public class DistanceCombinationClassificationResultChangedEventViewModel : DistanceCombinationEventViewModelBase
    {
        public List<ClassifiedCompetitorViewModel> Result { get; set; }
    }
}