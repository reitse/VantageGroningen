using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Emando.Vantage.Server.Entities.Appliances.MylapsX2
{
    [DataContract(Namespace = "http://emandovantage.com/2014/10/Appliances/MYLAPSX2")]
    public class X2AuxiliaryChannel
    {
        [Key, Column(Order = 0)]
        [DataMember]
        [Index("IX_X2AuxiliaryChannels_VenueId_Discipline", 0)]
        public string VenueCode { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(100)]
        [DataMember]
        [Index("IX_X2AuxiliaryChannels_VenueId_Discipline", 1)]
        public string Discipline { get; set; }

        [Key, Column(Order = 2)]
        [DataMember]
        public int Distance { get; set; }

        [Key, Column(Order = 3)]
        [DataMember]
        public long Channel { get; set; }

        [DataMember]
        public bool IsStart { get; set; }

        [Required]
        [StringLength(100)]
        [DataMember]
        public string Device { get; set; }

        [DataMember]
        public long Where { get; set; }

        [DataMember]
        public long? What { get; set; }
    }
}