using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models
{
    public class ClubCreateModel
    {
        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string CountryCode { get; set; }

        [Range(0, int.MaxValue)]
        public int Code { get; set; }

        [StringLength(20)]
        public string ShortName { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
    }
}