using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    [KnownType(typeof(PersonCompetitor))]
    [KnownType(typeof(TeamCompetitor))]
    public abstract class CompetitorBase : ICompetitor
    {
        [Key]
        [DataMember]
        public Guid Id { get; set; }

        [Index("UK_Competitors_ListId_StartNumber", Order = 0, IsUnique = true)]
        [Index("UK_Competitors_ListId_EntityId", Order = 0, IsUnique = true)]
        [DataMember]
        public Guid ListId { get; set; }

        [Range(1, int.MaxValue)]
        [Index("UK_Competitors_ListId_StartNumber", Order = 1, IsUnique = true)]
        [DataMember]
        public int StartNumber { get; set; }

        [Index("UK_Competitors_ListId_EntityId", Order = 1, IsUnique = true)]
        [DataMember]
        public Guid EntityId { get; set; }

        [StringLength(20)]
        [DataMember]
        public string LegNumber { get; set; }

        public virtual CompetitorListBase List { get; set; }

        [DataMember]
        public virtual ICollection<DistanceCombinationCompetitor> DistanceCombinations { get; set; }

        [Required]
        [StringLength(50)]
        [DataMember]
        public string ShortName { get; set; }

        [Required]
        [DataMember]
        public string NationalityCode { get; set; }

        public virtual Country Nationality { get; set; }

        [StringLength(100), DataMember]
        public string LicenseDiscipline { get; set; }

        [StringLength(100), DataMember]
        public string LicenseKey { get; set; }

        [DataMember]
        public PersonLicenseFlags LicenseFlags { get; set; }

        [StringLength(100), DataMember]
        public string Category { get; set; }

        [Range(0, int.MaxValue), DataMember]
        public int? Class { get; set; }

        [StringLength(100)]
        [DataMember]
        public string Sponsor { get; set; }

        [DataMember]
        public string ClubCountryCode { get; set; }

        [DataMember]
        public int? ClubCode { get; set; }

        [StringLength(20)]
        [DataMember]
        public string ClubShortName { get; set; }

        [StringLength(100)]
        [DataMember]
        public string ClubFullName { get; set; }

        [StringLength(100)]
        [DataMember]
        public string From { get; set; }

        [StringLength(50)]
        [DataMember]
        public string VenueCode { get; set; }

        [DataMember]
        public CompetitorStatus Status { get; set; }

        [StringLength(50)]
        [DataMember]
        public string Transponder1 { get; set; }

        [StringLength(50)]
        [DataMember]
        public string Transponder2 { get; set; }

        [DataMember]
        public DateTime Added { get; set; }

        [DataMember]
        public CompetitorSource Source { get; set; }

        public virtual ICollection<Race> Races { get; set; }

        public abstract string FullName { get; }

        public string TypeName => GetType().Name;

        #region ICompetitor Members

        [DataMember]
        public Gender Gender { get; set; }

        #endregion
    }
}