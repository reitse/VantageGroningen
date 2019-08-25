using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities
{
    [DataContract(Namespace = "http://emandovantage.com/2015/06/Entities")]
    public class VenueDistrict
    {
        [Key, Column(Order = 0)]
        [Range(0, int.MaxValue)]
        [DataMember]
        public int Level { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(50)]
        [DataMember]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        [DataMember]
        public string Label { get; set; }

        public virtual ICollection<Venue> Venues { get; set; }
    }
}