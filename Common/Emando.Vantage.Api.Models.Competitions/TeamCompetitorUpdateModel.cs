using System;
using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class TeamCompetitorUpdateModel : CompetitorUpdateModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid[] Members { get; set; }
    }
}