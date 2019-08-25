using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class CompetitorListCreateModel
    {
        [Required, StringLength(100)]
        public string Name { get; set; }
    }
}