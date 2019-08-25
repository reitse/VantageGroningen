using System;
using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class TeamCompetitorListCreateModel : CompetitorListCreateModel
    {
        [Required]
        public Guid PersonsId { get; set; }
    }
}