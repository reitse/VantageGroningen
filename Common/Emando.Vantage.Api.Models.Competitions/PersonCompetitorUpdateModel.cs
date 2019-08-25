using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class PersonCompetitorUpdateModel : CompetitorUpdateModel
    {
        [Required]
        public NameBindingModel Name { get; set; }
    }
}