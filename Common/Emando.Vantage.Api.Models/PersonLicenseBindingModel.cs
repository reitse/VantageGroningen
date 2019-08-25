using System;
using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models
{
    public class PersonLicenseBindingModel
    {
        public PersonBindingModel Person { get; set; }

        public int? Season { get; set; }

        public PersonLicenseFlags Flags { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        [StringLength(100)]
        public string Sponsor { get; set; }

        [StringLength(3, MinimumLength = 3)]
        public string ClubCountryCode { get; set; }

        [Range(0, int.MaxValue)]
        public int? ClubCode { get; set; }

        public PersonLicenseExpertise Expertise { get; set; }

        [StringLength(20)]
        public string Category { get; set; }

        [Range(0, int.MaxValue)]
        public int? Number { get; set; }

        [StringLength(20)]
        public string LegNumber { get; set; }

        [StringLength(50)]
        public string VenueCode { get; set; }

        [StringLength(50)]
        public string Transponder1 { get; set; }

        [StringLength(50)]
        public string Transponder2 { get; set; }
    }
}