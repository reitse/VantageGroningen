using System;

namespace Emando.Vantage.Models
{
    public class PersonLicenseVenueClassViewModel
    {
        public PersonLicenseRestrictedDetailsViewModel License { get; set; }

        public int Class { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public DateTime Issued { get; set; }
    }
}