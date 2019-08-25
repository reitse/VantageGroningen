using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Emando.Vantage.Server.Entities.Appliances.MylapsX2
{
    [DataContract(Namespace = "http://emandovantage.com/2014/10/Appliances/MYLAPSX2")]
    public class X2VenueLaneLocations
    {
        [Key, Column(Order = 0)]
        [Index("IX_X2VenueLoops_VenueId_Discipline_Distance", 0)]
        [DataMember]
        public string VenueCode { get; set; }

        [Key, Column(Order = 1)]
        [Index("IX_X2VenueLoops_VenueId_Discipline_Distance", 1)]
        [StringLength(100)]
        [DataMember]
        public string Discipline { get; set; }

        [Key, Column(Order = 2)]
        [Index("IX_X2VenueLoops_VenueId_Discipline_Distance", 2)]
        [DataMember]
        public int Distance { get; set; }

        [Key, Column(Order = 3)]
        [DataMember]
        public int StartLane { get; set; }

        [Key, Column(Order = 4)]
        [DataMember]
        public long Start { get; set; }

        [DataMember]
        public decimal StartOffset { get; set; }

        [Key, Column(Order = 5)]
        [DataMember]
        public long Finish { get; set; }
    }
}