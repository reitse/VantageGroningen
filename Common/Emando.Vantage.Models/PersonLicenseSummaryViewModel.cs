using System;

namespace Emando.Vantage.Models
{
    public class PersonLicenseSummaryViewModel
    {
        public string Discipline { get; set; }

        public string IssuerId { get; set; }

        public string Key { get; set; }

        public Guid PersonId { get; set; }

        public PersonLicenseFlags Flags { get; set; }

        public int Season { get; set; }

        public string ClubFullName { get; set; }

        public string ClubCountryCode { get; set; }

        public int? ClubCode { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public string Category { get; set; }

        public int? Number { get; set; }

        public string LegNumber { get; set; }

        public Name PersonName { get; set; }

        public string PersonAddressCity { get; set; }

        public string PersonNationalityCode { get; set; }
    }
}