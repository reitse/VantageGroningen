using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities
{
    [DataContract(Namespace = "http://emandovantage.com/2016/10/Entities")]
    public class ClubVenue
    {
        [Key, Column(Order = 0)]
        [Index]
        [DataMember]
        public string ClubCountryCode { get; set; }

        [Key, Column(Order = 1)]
        [Range(0, int.MaxValue)]
        [DataMember]
        public int ClubCode { get; set; }

        public virtual Club Club { get; set; }

        [StringLength(50)]
        [DataMember]
        public string VenueCode { get; set; }

        [Key, Column(Order = 2)]
        [StringLength(100)]
        [DataMember]
        public string VenueDiscipline { get; set; }
    }
}