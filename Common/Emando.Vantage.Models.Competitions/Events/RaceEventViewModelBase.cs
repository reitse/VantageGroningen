using System;

namespace Emando.Vantage.Models.Competitions.Events
{
    public class RaceEventViewModelBase : HeatEventViewModelBase
    {
        public Guid RaceId { get; set; }
    }
}