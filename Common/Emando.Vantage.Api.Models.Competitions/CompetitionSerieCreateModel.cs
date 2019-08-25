using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class CompetitionSerieCreateModel
    {
        [Required]
        [StringLength(100)]
        public string Discipline { get; set; }

        [Required]
        [StringLength(20)]
        public string LicenseIssuerId { get; set; }
        
        [Range(1, int.MaxValue)]
        public int Season { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}