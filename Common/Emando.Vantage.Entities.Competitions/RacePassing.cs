﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    public class RacePassing : Passing, IReadOnlyRacePassing, IHaveRacePassingKey
    {
        [Key, Column(Order = 0), StringLength(50), DataMember]
        public string ApplianceName { get; set; }

        [Key, Column(Order = 1), StringLength(50), DataMember]
        public string ApplianceInstanceName { get; set; }

        [Key, Column(Order = 2), StringLength(50)]
        [Index("IX_RacePassings_RaceId_InstanceName", 1)]
        [DataMember]
        public string InstanceName { get; set; }

        [Key, Column(Order = 3), DataMember]
        [Index, Index("IX_RacePassings_RaceId_InstanceName", 0)]
        public Guid RaceId { get; set; }

        public virtual Race Race { get; set; }

        [Key, Column(Order = 4), StringLength(50), DataMember]
        public string How { get; set; }

        [Key, Column(Order = 5), DataMember]
        public DateTime When { get; set; }

        [DataMember]
        public RaceEventFlags Flags { get; set; }

        public PresentationSource PresentationSource => new PresentationSource(ApplianceName, ApplianceInstanceName, How);
    }
}