using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    public class RaceLap : IReadOnlyActiveRaceLap, IHaveRacePassingKey
    {
        public virtual Race Race { get; set; }

        [Key, Column(Order = 1), StringLength(50), DataMember]
        public string ApplianceName { get; set; }

        [Key, Column(Order = 2), StringLength(50), DataMember]
        public string ApplianceInstanceName { get; set; }

        [Key, Column(Order = 4), StringLength(50), DataMember]
        public string How { get; set; }

        [DataMember]
        public int? Index { get; set; }

        [DataMember]
        public int? Ranking { get; set; }

        #region IReadOnlyRaceLap Members

        [Key, Column(Order = 0)]
        [Index, Index("IX_RaceLaps_RaceId_InstanceName", 0)]
        [DataMember]
        public Guid RaceId { get; set; }

        [Key, Column(Order = 3), Required, StringLength(50), DataMember]
        [Index("IX_RaceLaps_RaceId_InstanceName", 1)]
        public string InstanceName { get; set; }

        [DataMember]
        public DateTime When { get; set; }

        [Key, Column(Order = 5), DataMember]
        public TimeSpan Time { get; set; }

        [DataMember]
        public RaceEventFlags Flags { get; set; }

        [DataMember]
        public decimal? Points { get; set; }

        [DataMember]
        public int? FixedIndex { get; set; }

        [DataMember]
        public int? FixedRanking { get; set; }

        public PresentationSource PresentationSource
        {
            get { return new PresentationSource(ApplianceName, ApplianceInstanceName, How); }
            set
            {
                ApplianceName = value.ApplianceName;
                ApplianceInstanceName = value.ApplianceInstanceName;
                How = value.How;
            }
        }

        #endregion
    }
}