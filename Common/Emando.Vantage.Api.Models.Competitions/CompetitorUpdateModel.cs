using System;
using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class CompetitorUpdateModel
    {
        [StringLength(20)]
        public string LegNumber { get; set; }

        [Required]
        public string NationalityCode { get; set; }

        public Guid ListId { get; set; }

        [StringLength(100)]
        public string Category { get; set; }

        [Range(0, int.MaxValue)]
        public int? Class { get; set; }

        [StringLength(50)]
        [Required]
        public string ShortName { get; set; }

        [StringLength(3, MinimumLength = 3)]
        public string ClubCountryCode { get; set; }

        [Range(0, int.MaxValue)]
        public int? ClubCode { get; set; }
    }
}