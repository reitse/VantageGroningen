using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models.Competitions
{
    public abstract class CompetitorListUpdateModel
    {
        [Required, StringLength(100)]
        public string Name { get; set; }
    }
}