using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    public class PersonTime : IPersonLicenseTime, IEquatable<PersonTime>
    {
        public static IEqualityComparer<PersonTime> LicenseComparer { get; } = new LicenseEqualityComparer();

        [Key, Column(Order = 0)]
        [StringLength(50)]
        [DataMember]
        public string LicenseIssuerId { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(100)]
        [DataMember]
        public string LicenseDiscipline { get; set; }

        [Key, Column(Order = 2)]
        [StringLength(100)]
        [DataMember]
        public string LicenseKey { get; set; }

        [Index]
        [Required]
        [StringLength(50)]
        public string Source { get; set; }

        public virtual PersonLicense License { get; set; }

        public virtual Venue Venue { get; set; }

        #region IEquatable<PersonTime> Members

        public bool Equals(PersonTime other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return string.Equals(LicenseIssuerId, other.LicenseIssuerId) && string.Equals(LicenseDiscipline, other.LicenseDiscipline)
                && string.Equals(LicenseKey, other.LicenseKey) && string.Equals(VenueCode, other.VenueCode) && string.Equals(Discipline, other.Discipline)
                && string.Equals(DistanceDiscipline, other.DistanceDiscipline) && Distance == other.Distance && Date.Equals(other.Date) && Time.Equals(other.Time);
        }

        #endregion

        #region IPersonLicenseTime Members

        [Key, Column(Order = 3)]
        [DataMember]
        public string VenueCode { get; set; }

        [Key, Column(Order = 4)]
        [DataMember]
        public string Discipline { get; set; }

        [Key, Column(Order = 5)]
        [StringLength(100)]
        public string DistanceDiscipline { get; set; }

        [Key, Column(Order = 6)]
        [DataMember]
        public int Distance { get; set; }

        [DataMember]
        [Key, Column(Order = 7)]
        public DateTime Date { get; set; }

        [DataMember]
        [Key, Column(Order = 8)]
        public TimeSpan Time { get; set; }

        [StringLength(3)]
        [Index]
        [DataMember]
        public string NationalityCode { get; set; }

        [Index]
        [DataMember]
        public Guid? CompetitionId { get; set; }

        #endregion

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((PersonTime)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = LicenseIssuerId?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (LicenseDiscipline?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (LicenseKey?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (VenueCode?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Discipline?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (DistanceDiscipline?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ Distance;
                hashCode = (hashCode * 397) ^ Date.GetHashCode();
                hashCode = (hashCode * 397) ^ Time.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(PersonTime left, PersonTime right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PersonTime left, PersonTime right)
        {
            return !Equals(left, right);
        }

        #region Nested type: LicenseEqualityComparer

        private sealed class LicenseEqualityComparer : IEqualityComparer<PersonTime>
        {
            #region IEqualityComparer<PersonTime> Members

            public bool Equals(PersonTime x, PersonTime y)
            {
                if (ReferenceEquals(x, y))
                    return true;
                if (ReferenceEquals(x, null))
                    return false;
                if (ReferenceEquals(y, null))
                    return false;
                if (x.GetType() != y.GetType())
                    return false;
                return string.Equals(x.LicenseIssuerId, y.LicenseIssuerId) && string.Equals(x.LicenseDiscipline, y.LicenseDiscipline)
                    && string.Equals(x.LicenseKey, y.LicenseKey);
            }

            public int GetHashCode(PersonTime obj)
            {
                unchecked
                {
                    var hashCode = obj.LicenseIssuerId?.GetHashCode() ?? 0;
                    hashCode = (hashCode * 397) ^ (obj.LicenseDiscipline?.GetHashCode() ?? 0);
                    hashCode = (hashCode * 397) ^ (obj.LicenseKey?.GetHashCode() ?? 0);
                    return hashCode;
                }
            }

            #endregion
        }

        #endregion
    }
}