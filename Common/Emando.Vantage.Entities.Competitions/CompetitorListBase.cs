using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    [KnownType(typeof(PersonCompetitorList))]
    [KnownType(typeof(TeamCompetitorList))]
    public abstract class CompetitorListBase
    {
        [Key]
        [DataMember]
        public Guid Id { get; set; }

        [Index("UK_CompetitorLists_CompetitionId_Name", Order = 0, IsUnique = true)]
        [DataMember]
        public Guid CompetitionId { get; set; }

        public virtual Competition Competition { get; set; }

        [DataMember]
        public virtual ICollection<CompetitorBase> Competitors { get; set; }

        [Required]
        [StringLength(100)]
        [Index("UK_CompetitorLists_CompetitionId_Name", Order = 1, IsUnique = true)]
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int SortOrder { get; set; }

        public string TypeName => GetType().Name;
    }
}