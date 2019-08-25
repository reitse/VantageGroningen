using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities")]
    public class VenueTrack : IVenueTrack
    {
        [Key, Column(Order = 0)]
        [StringLength(50)]
        [DataMember]
        public string VenueCode { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(100)]
        [DataMember]
        public string VenueDiscipline { get; set; }

        public virtual Venue Venue { get; set; }

        [Key, Column(Order = 2)]
        [DataMember]
        public decimal Length { get; set; }
    }
}