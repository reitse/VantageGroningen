using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Emando.Vantage.Server.Entities.Appliances.MylapsX2
{
    [DataContract(Namespace = "http://emandovantage.com/2014/10/Appliances/MYLAPSX2")]
    public class X2Instance
    {
        [Key, Column(Order = 0)]
        [Required]
        public string VenueCode { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(100)]
        [Required]
        public string Discipline { get; set; }

        [Key, Column(Order = 2)]
        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [DataMember]
        public string Host { get; set; }

        [DataMember]
        [StringLength(100)]
        public string UserName { get; set; }

        [DataMember]
        [StringLength(100)]
        public string Password { get; set; }
    }
}