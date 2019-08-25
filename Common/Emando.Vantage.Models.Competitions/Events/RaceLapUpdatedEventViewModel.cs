using System;

namespace Emando.Vantage.Models.Competitions.Events
{
    public class RaceLapUpdatedEventViewModel : RaceLapEventViewModelBase
    {
        public PresentationSource PresentationSource { get; set; }

        public TimeSpan OldTime { get; set; }
    }
}