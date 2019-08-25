using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    public class DistancePoints
    {
        [Key, Column(Order = 0)]
        [DataMember]
        public Guid DistancePointsTableId { get; set; }

        public virtual DistancePointsTable DistancePointsTable { get; set; }

        [Key, Column(Order = 1)]
        [DataMember]
        public int Ranking { get; set; }
        [Key, Column(Order = 2)]
        [StringLength(100)]
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public decimal Points { get; set; }
    }
}