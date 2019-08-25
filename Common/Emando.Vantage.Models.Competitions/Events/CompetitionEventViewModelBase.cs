using System;
using Emando.Vantage.Models.Events;

namespace Emando.Vantage.Models.Competitions.Events
{
    public class CompetitionEventViewModelBase : EventViewModelBase
    {
        public Guid CompetitionId { get; set; }
    }
}