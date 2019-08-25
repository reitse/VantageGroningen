using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities")]
    public class Person : IPerson
    {
        public Person()
        {
            Name = new Name();
            Address = new Address();
        }

        [Key]
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public virtual ICollection<PersonLicense> Licenses { get; set; }

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
        public Gender Gender { get; set; }

        [Required]
        [DataMember]
        public string NationalityCode { get; set; }

        public virtual Country Nationality { get; set; }

        [DataMember]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [DataMember]
        [StringLength(34)]
        public string Iban { get; set; }
    }
}