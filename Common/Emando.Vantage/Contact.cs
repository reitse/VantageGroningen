using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Emando.Vantage
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities")]
    public class Contact
    {
        public Contact()
        {
            Name = new Name();
            Address = new Address();
        }

        [StringLength(100)]
        [DataMember]
        public string OrganizationName { get; set; }

        [DataMember]
        public Name Name { get; set; }

        [EmailAddress]
        [StringLength(100)]
        [DataMember]
        public string Email { get; set; }

        [StringLength(20)]
        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public Address Address { get; set; }

        [DataMember]
        public string Extra { get; set; }

        [StringLength(200)]
        [DataType(DataType.Url)]
        [DataMember]
        public string Url { get; set; }
    }
}