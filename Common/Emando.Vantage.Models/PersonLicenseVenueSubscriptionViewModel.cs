using System;

namespace Emando.Vantage.Models
{
    public class PersonLicenseVenueSubscriptionViewModel
    {
        public PersonLicenseRestrictedDetailsViewModel License { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public DateTime Issued { get; set; }
    }
}