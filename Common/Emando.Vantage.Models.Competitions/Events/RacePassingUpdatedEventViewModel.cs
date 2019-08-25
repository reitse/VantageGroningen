using System;

namespace Emando.Vantage.Models.Competitions.Events
{
    public class RacePassingUpdatedEventViewModel : RaceEventViewModelBase
    {
        public RacePassingViewModel Passing { get; set; }

        public PresentationSource PresentationSource { get; set; }

        public TimeSpan OldTime { get; set; }
    }
}