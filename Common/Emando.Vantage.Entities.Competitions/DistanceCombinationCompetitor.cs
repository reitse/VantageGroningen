using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    public class DistanceCombinationCompetitor
    {
        [Key, Column(Order = 0), DataMember]
        public Guid DistanceCombinationId { get; set; }

        public virtual DistanceCombination DistanceCombination { get; set; }

        [Key, Column(Order = 1), DataMember]
        public Guid CompetitorId { get; set; }

        public virtual CompetitorBase Competitor { get; set; }

        [DataMember, Range(1, int.MaxValue)]
        public int? Reserve { get; set; }

        [DataMember]
        public DistanceCombinationCompetitorStatus Status { get; set; }
    }
}