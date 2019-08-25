using System;

namespace Emando.Vantage.Models.Competitions
{
    public class RefreshDistanceDrawMessageViewModel : MessageViewModelBase
    {
        public Guid CompetitionId { get; set; }

        public Guid DistanceId { get; set; }
    }
}