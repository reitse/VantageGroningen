using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class CompetitionSerieUpdateModel
    {
        [Range(1, int.MaxValue)]
        public int Season { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}