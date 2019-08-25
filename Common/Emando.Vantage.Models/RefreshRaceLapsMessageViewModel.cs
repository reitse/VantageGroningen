using System;

namespace Emando.Vantage.Models
{
    public class RefreshRaceLapsMessageViewModel : MessageViewModelBase
    {
        public Guid CompetitionId { get; set; }

        public Guid DistanceId { get; set; }

        public Guid RaceId { get; set; }
    }
}