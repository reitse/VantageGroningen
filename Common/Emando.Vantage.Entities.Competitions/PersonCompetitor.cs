using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    public class PersonCompetitor : CompetitorBase
    {
        [Required]
        [DataMember]
        public Name Name { get; set; }

        [DataMember]
        public Guid PersonId { get; set; }

        public virtual Person Person { get; set; }

        public virtual ICollection<TeamCompetitorMember> TeamMemberships { get; set; }

        public override string FullName => Name.ToString();

        public override string ToString()
        {
            return FullName;
        }

        public static PersonCompetitor FromLicense(PersonLicense license)
        {
            return new PersonCompetitor
            {
                LicenseDiscipline = license.Discipline,
                LicenseKey = license.Key,
                LicenseFlags = license.Flags,
                Person = license.Person,
                Name = license.Person.Name,
                Gender = license.Person.Gender,
                Category = license.Category,
                ClubCountryCode = license.Club?.CountryCode,
                ClubCode = license.Club?.Code,
                ClubFullName = license.Club?.FullName,
                ClubShortName = license.Club?.ShortName,
                LegNumber = license.LegNumber,
                NationalityCode = license.Person.NationalityCode,
                VenueCode = license.VenueCode
            };
        }
    }
}