using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities")]
    public class PersonLicenseVenueSubscription
    {
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

        public virtual PersonLicense License { get; set; }

        [Key, Column(Order = 3)]
        [StringLength(50)]
        [DataMember]
        public string VenueCode { get; set; }

        [Key, Column(Order = 4)]
        [StringLength(100)]
        [DataMember]
        public string VenueDiscipline { get; set; }

        public virtual Venue Venue { get; set; }

        public DateTime Issued { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }
    }
}