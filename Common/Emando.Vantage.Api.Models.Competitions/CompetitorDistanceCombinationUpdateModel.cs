using System;
using System.ComponentModel.DataAnnotations;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class CompetitorDistanceCombinationUpdateModel
    {
        public Guid DistanceCombinationId { get; set; }

        [Range(1, int.MaxValue)]
        public int? Reserve { get; set; }

        public DistanceCombinationCompetitorStatus Status { get; set; }
    }
}