using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class CompetitorCreateModel
    {
        public string LicenseDiscipline { get; set; }

        public string LicenseKey { get; set; }

        public int StartNumber { get; set; }

        [StringLength(20)]
        public string LegNumber { get; set; }

        [StringLength(50)]
        [Required]
        public string ShortName { get; set; }

        [StringLength(3, MinimumLength = 3)]
        public string NationalityCode { get; set; }
        
        [StringLength(100)]
        public string Category { get; set; }
        
        [Range(0, int.MaxValue)]
        public int? Class { get; set; }

        [StringLength(3, MinimumLength = 3)]
        public string ClubCountryCode { get; set; }

        [Range(0, int.MaxValue)]
        public int? ClubCode { get; set; }
    }
}