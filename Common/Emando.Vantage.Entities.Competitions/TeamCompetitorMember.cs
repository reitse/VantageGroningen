using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    public class TeamCompetitorMember
    {
        [Key, Column(Order = 0)]
        [Index("UK_TeamCompetitorMembers_TeamId_Order", 0, IsUnique = true)]
        [DataMember]
        public Guid TeamId { get; set; }

        public virtual TeamCompetitor Team { get; set; }

        [Key, Column(Order = 1)]
        [DataMember]
        public Guid MemberId { get; set; }

        public virtual PersonCompetitor Member { get; set; }

        [Index("UK_TeamCompetitorMembers_TeamId_Order", 1, IsUnique = true)]
        [DataMember]
        public int Order { get; set; }

        [DataMember]
        public int? Reserve { get; set; }
    }
}