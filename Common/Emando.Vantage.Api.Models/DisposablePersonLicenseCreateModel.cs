using System;
using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models
{
    public class DisposablePersonLicenseCreateModel
    {
        [Required]
        public Name Name { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public AddressBindingModel Address { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string NationalityCode { get; set; }

        [StringLength(3, MinimumLength = 3)]
        public string ClubCountryCode { get; set; }

        [Range(0, int.MaxValue)]
        public int? ClubCode { get; set; }

        [StringLength(50)]
        public string VenueCode { get; set; }

        public DateTime Valid { get; set; }
    }
}