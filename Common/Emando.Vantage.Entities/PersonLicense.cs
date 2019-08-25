using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities")]
    public class PersonLicense : IPersonLicense
    {
        [Key, Column(Order = 0)]
        [StringLength(50)]
        [DataMember]
        public string IssuerId { get; set; }

        public virtual LicenseIssuer Issuer { get; set; }

        [Key, Column(Order = 2)]
        [StringLength(100)]
        [DataMember]
        public string Discipline { get; set; }
        
        [Key, Column(Order = 3)]
        [StringLength(100)]
        [DataMember]
        public string Key { get; set; }

        [DataMember]
        public Guid PersonId { get; set; }

        public virtual Person Person { get; set; }

        [Column(Order = 1)]
        [StringLength(50)]
        [DataMember]
        public string VenueCode { get; set; }

        public virtual Venue Venue { get; set; }

        [StringLength(100)]
        [DataMember]
        public string Sponsor { get; set; }

        [DataMember]
        public string ClubCountryCode { get; set; }

        [DataMember]
        public int? ClubCode { get; set; }

        public virtual Club Club { get; set; }

        [DataMember]
        public PersonLicenseFlags Flags { get; set; }
        
        [DataMember]
        public int Season { get; set; }

        [DataMember]
        public DateTime ValidFrom { get; set; }
        
        [DataMember]
        public DateTime ValidTo { get; set; }
        
        [StringLength(20)]
        [DataMember]
        public string Category { get; set; }
        
        [Range(0, int.MaxValue)]
        [DataMember]
        public int? Number { get; set; }

        [StringLength(20)]
        [DataMember]
        public string LegNumber { get; set; }

        [StringLength(50)]
        [DataMember]
        public string Transponder1 { get; set; }

        [StringLength(50)]
        [DataMember]
        public string Transponder2 { get; set; }

        public override string ToString()
        {
            return $"{IssuerId}/{Discipline}/{Key}";
        }

        private sealed class KeyEqualityComparer : IEqualityComparer<PersonLicense>
        {
            public bool Equals(PersonLicense x, PersonLicense y)
            {
                if (ReferenceEquals(x, y))
                    return true;
                if (ReferenceEquals(x, null))
                    return false;
                if (ReferenceEquals(y, null))
                    return false;
                if (x.GetType() != y.GetType())
                    return false;
                return string.Equals(x.IssuerId, y.IssuerId) && string.Equals(x.Discipline, y.Discipline) && string.Equals(x.Key, y.Key);
            }

            public int GetHashCode(PersonLicense obj)
            {
                unchecked
                {
                    var hashCode = obj.IssuerId?.GetHashCode() ?? 0;
                    hashCode = (hashCode * 397) ^ (obj.Discipline?.GetHashCode() ?? 0);
                    hashCode = (hashCode * 397) ^ (obj.Key?.GetHashCode() ?? 0);
                    return hashCode;
                }
            }
        }

        public static IEqualityComparer<PersonLicense> KeyComparer { get; } = new KeyEqualityComparer();
    }
}