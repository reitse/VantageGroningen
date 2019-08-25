using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Emando.Vantage.Server.Entities.Appliances.MylapsX2
{
    public class X2VenueSegment : IVenueSegment
    {
        [Key, Column(Order = 0)]
        [Index]
        [DataMember]
        public string VenueCode { get; set; }

        #region IVenueSegment Members

        [Key, Column(Order = 1)]
        [DataMember]
        public long From { get; set; }

        [Key, Column(Order = 2)]
        [DataMember]
        public long To { get; set; }

        [Key, Column(Order = 3)]
        [DataMember]
        public int Lane { get; set; }

        [DataMember]
        public decimal Length { get; set; }

        [DataMember]
        public VenueSegmentFlags Flags { get; set; }

        #endregion
    }
}