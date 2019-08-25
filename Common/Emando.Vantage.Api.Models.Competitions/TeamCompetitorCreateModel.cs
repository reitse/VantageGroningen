using System;
using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class TeamCompetitorCreateModel : CompetitorCreateModel
    {
        [Required, StringLength(100)]
        public string Name { get; set; }

        public Gender Gender { get; set; }

        [Required]
        public Guid[] Members { get; set; }
    }
}