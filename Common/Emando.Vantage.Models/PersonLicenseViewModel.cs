using System;

namespace Emando.Vantage.Models
{
    public class PersonLicenseViewModel : IPersonLicense
    {
        public string Discipline { get; set; }

        public string IssuerId { get; set; }

        public string Key { get; set; }

        public Guid PersonId { get; set; }

        int? IPersonLicense.ClubCode => Club?.Code;

        public PersonLicenseFlags Flags { get; set; }

        public int Season { get; set; }

        public string Sponsor { get; set; }

        string IPersonLicense.ClubCountryCode => Club?.CountryCode;

        public ClubViewModel Club { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public string Category { get; set; }

        public int? Number { get; set; }

        public string LegNumber { get; set; }

        public string VenueCode { get; set; }

        public string Transponder1 { get; set; }

        public string Transponder2 { get; set; }
    }
}