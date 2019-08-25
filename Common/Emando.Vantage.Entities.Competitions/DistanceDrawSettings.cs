using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2015/06/Entities/Competitions")]
    public class DistanceDrawSettings
    {
        [Key]
        [DataMember]
        public Guid CompetitionId { get; set; }

        public virtual Competition Competition { get; set; }

        [DataMember]
        public DistanceDrawGroupMode GroupMode { get; set; }

        [Range(0, int.MaxValue)]
        [DataMember]
        public int CategoryLength { get; set; }

        [Range(1, int.MaxValue)]
        [DataMember]
        public int GroupSize { get; set; }

        [DataMember]
        public bool DeleteExisting { get; set; }

        [DataMember]
        public bool ReverseGroups { get; set; }

        [DataMember]
        public DistanceDrawMode Mode { get; set; }

        [DataMember]
        public bool ReverseFilling { get; set; }

        [DataMember]
        public string Selectors { get; set; }

        [DataMember]
        public DistanceDrawSpreading Spreading { get; set; }
    }
}