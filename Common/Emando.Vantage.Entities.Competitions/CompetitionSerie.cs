using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    public class CompetitionSerie
    {
        [Key]
        [DataMember]
        public Guid Id { get; set; }

        public virtual ICollection<Competition> Competitions { get; set; }

        [Required]
        [StringLength(20)]
        [DataMember]
        public string LicenseIssuerId { get; set; }

        public virtual LicenseIssuer LicenseIssuer { get; set; }

        [Required]
        [StringLength(100)]
        [DataMember]
        public string Discipline { get; set; }

        [Range(1, int.MaxValue)]
        public int Season { get; set; }

        [Required]
        [StringLength(100)]
        [DataMember]
        public string Name { get; set; }
    }
}