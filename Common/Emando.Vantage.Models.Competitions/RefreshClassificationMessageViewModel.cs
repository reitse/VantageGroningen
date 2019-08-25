using System;

namespace Emando.Vantage.Models.Competitions
{
    public class RefreshClassificationMessageViewModel : MessageViewModelBase
    {
        public Guid CompetitionId { get; set; }

        public Guid DistanceCombinationId { get; set; }

        public int ClassificationWeight { get; set; }

        public int CategoryLength { get; set; }

        public int? BehindDistance { get; set; }

        public int Index { get; set; }
    }
}