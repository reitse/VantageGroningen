using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Emando.Vantage
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities")]
    public class Address : IAddress
    {
        [DataMember]
        [StringLength(100)]
        public string Line1 { get; set; }

        [DataMember]
        [StringLength(100)]
        public string Line2 { get; set; }

        [DataMember]
        [StringLength(50)]
        public string StateOrProvince { get; set; }

        [DataMember]
        [StringLength(20)]
        public string PostalCode { get; set; }

        [DataMember]
        [StringLength(100)]
        public string City { get; set; }

        [StringLength(3, MinimumLength = 3)]
        [DataMember]
        public string CountryCode { get; set; }
    }
}