using System;

namespace Emando.Vantage.Models.Competitions.Events
{
    public class HeatStartedEventViewModel : HeatEventViewModelBase
    {
        public DateTime Started { get; set; }

        public TimeSpan Clock { get; set; }
    }
}