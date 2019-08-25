using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class PersonCompetitorCreateModel : CompetitorCreateModel
    {
        [Required]
        public Name Name { get; set; }
    }
}