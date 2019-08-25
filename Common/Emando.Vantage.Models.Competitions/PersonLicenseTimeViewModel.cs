using System;

namespace Emando.Vantage.Models.Competitions
{
    public class PersonLicenseTimeViewModel
    {
        public string LicenseIssuerId { get; set; }

        public string LicenseDiscipline { get; set; }

        public string LicenseKey { get; set; }

        public NameViewModel LicensePersonName { get; set; }

        public string LicenseCategory { get; set; }

        public string LicenseClubFullName { get; set; }

        public string LicenseVenueCode { get; set; }

        public string VenueCode { get; set; }

        public string VenueName { get; set; }

        public string VenueAddressCity { get; set; }

        public int Distance { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public string NationalityCode { get; set; }
    }
}