using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models
{
    public class VantageUserCompetitionRightBindingModel
    {
        [Required]
        [StringLength(100)]
        public string LicenseIssuerId { get; set; }

        [Required]
        [StringLength(100)]
        public string Discipline { get; set; }

        [Range(0, int.MaxValue)]
        public int CompetitionClass { get; set; }

        [StringLength(100)]
        public string Value { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}