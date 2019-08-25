using System;
using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class TeamCompetitorListUpdateModel : CompetitorListUpdateModel
    {
        [Required]
        public Guid PersonsId { get; set; }
    }
}