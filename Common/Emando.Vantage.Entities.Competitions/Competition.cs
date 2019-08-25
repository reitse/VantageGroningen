using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    public class Competition : ICompetition
    {
        [Key, DataMember]
        public Guid Id { get; set; }

        [DataMember]
        [StringLength(50)]
        public string ProviderKey { get; set; }

        [DataMember]
        public Guid? SerieId { get; set; }

        public virtual CompetitionSerie Serie { get; set; }

        [StringLength(50)]
        [DataMember]
        public string VenueCode { get; set; }

        [Required]
        [StringLength(100)]
        [DataMember]
        public string Discipline { get; set; }

        public virtual Venue Venue { get; set; }

        [DataMember]
        [StringLength(200)]
        public string Location { get; set; }

        public CompetitionLocationFlags LocationFlags { get; set; }

        [DataMember]
        public string Extra { get; set; }

        [StringLength(100)]
        [DataMember]
        public string Sponsor { get; set; }

        [Required, StringLength(100)]
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Class { get; set; }

        [Required, StringLength(50), DataMember]
        public string LicenseIssuerId { get; set; }

        public virtual LicenseIssuer LicenseIssuer { get; set; }

        [DataMember]
        public DateTime Starts { get; set; }

        [DataMember]
        public DateTime? Ends { get; set; }

        [DataMember]
        [StringLength(10)]
        public string Culture { get; set; }

        [DataMember]
        [StringLength(50)]
        public string TimeZone { get; set; }

        [DataMember]
        public CompetitionResultsStatus ResultsStatus { get; set; }

        [DataMember]
        public DateTime? MadeOfficial { get; set; }

        [StringLength(100)]
        public string ReportTemplateName { get; set; }

        [DataMember]
        public virtual ReportTemplate ReportTemplate { get; set; }

        [DataMember(Order = 10)]
        public virtual ICollection<Distance> Distances { get; set; }

        [DataMember(Order = 20)]
        public virtual ICollection<DistanceCombination> DistanceCombinations { get; set; }
        
        [DataMember(Order = 30)]
        public virtual ICollection<CompetitorListBase> CompetitorLists { get; set; }
    }
}