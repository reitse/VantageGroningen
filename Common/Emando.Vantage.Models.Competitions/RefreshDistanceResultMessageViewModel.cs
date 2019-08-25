using System;

namespace Emando.Vantage.Models.Competitions
{
    public class RefreshDistanceResultMessageViewModel : MessageViewModelBase
    {
        public Guid CompetitionId { get; set; }

        public Guid DistanceId { get; set; }
    }
}