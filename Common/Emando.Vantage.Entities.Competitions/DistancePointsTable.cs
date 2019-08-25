using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    public class DistancePointsTable
    {
        [Key]
        [DataMember]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public virtual ICollection<DistancePoints> Points { get; set; }
    }
}