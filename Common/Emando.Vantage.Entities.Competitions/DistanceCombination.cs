using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    public class DistanceCombination : IDistanceCombination
    {
        [Key]
        [DataMember]
        public Guid Id { get; set; }

        [Index("UK_DistanceCombinations_CompetitionId_Number", Order = 0, IsUnique = true, IsClustered = true)]
        [Index("UK_DistanceCombinations_CompetitionId_Name", Order = 0, IsUnique = true)]
        [DataMember]
        public Guid CompetitionId { get; set; }

        public virtual Competition Competition { get; set; }

        public virtual ICollection<DistanceCombinationCompetitor> Competitors { get; set; }

        [Required, StringLength(100)]
        [Index("UK_DistanceCombinations_CompetitionId_Name", Order = 1, IsUnique = true)]
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime? Starts { get; set; }

        [DataMember]
        public int ClassificationWeight { get; set; }

        [DataMember]
        public virtual ICollection<Distance> Distances { get; set; }

        #region IDistanceCombination Members

        [Range(0, int.MaxValue)]
        [Index("UK_DistanceCombinations_CompetitionId_Number", Order = 1, IsUnique = true, IsClustered = true)]
        [DataMember]
        public int Number { get; set; }

        [StringLength(100)]
        [DataMember]
        public string ClassFilter { get; set; }

        [Required, StringLength(200)]
        [DataMember]
        public string CategoryFilter { get; set; }

        #endregion

        public int? CompetitorsTotal => Competitors?.Count;

        public int? CompetitorsPending => Competitors?.Count(c => c.Status == DistanceCombinationCompetitorStatus.Pending);

        public int? CompetitorsWithdrawn => Competitors?.Count(c => c.Status == DistanceCombinationCompetitorStatus.Withdrawn);

        public int? CompetitorsConfirmed => Competitors?.Count(c => c.Status == DistanceCombinationCompetitorStatus.Confirmed);
    }
}