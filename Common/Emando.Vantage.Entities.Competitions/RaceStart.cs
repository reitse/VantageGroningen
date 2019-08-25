using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    public class RaceStart
    {
        [Key, Column(Order = 0), StringLength(50), DataMember]
        public string ApplianceName { get; set; }

        [Key, Column(Order = 1), StringLength(50), DataMember]
        public string ApplianceInstanceName { get; set; }

        [Key, Column(Order = 2), DataMember]
        public long ApplianceEventId { get; set; }

        [Key, Column(Order = 3), StringLength(50), DataMember]
        [Index("IX_RaceStarts_RaceId_InstanceName", 1)]
        public string InstanceName { get; set; }

        [Key, Column(Order = 4), DataMember]
        [Index, Index("IX_RaceStarts_RaceId_InstanceName", 0)]
        public Guid RaceId { get; set; }

        public virtual Race Race { get; set; }

        [Required, StringLength(50), DataMember]
        public string How { get; set; }

        [DataMember]
        public DateTime When { get; set; }

        [DataMember]
        public RaceEventFlags Flags { get; set; }

        public PresentationSource PresentationSource => new PresentationSource(ApplianceName, ApplianceInstanceName, How);
    }
}