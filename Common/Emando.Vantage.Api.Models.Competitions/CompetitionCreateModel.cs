using System;
using System.ComponentModel.DataAnnotations;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class CompetitionCreateModel
    {
        [StringLength(50)]
        public string ProviderKey { get; set; }

        public Guid? SerieId { get; set; }

        [Required, StringLength(100)]
        public string Discipline { get; set; }
        
        [StringLength(50)]
        public string VenueCode { get; set; }
        
        [StringLength(100)]
        public string Sponsor { get; set; }
        
        [Required, StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(200)]
        public string Location { get; set; }

        public CompetitionLocationFlags LocationFlags { get; set; }

        public string Extra { get; set; }

        public int Class { get; set; }

        [Required, StringLength(50)]
        public string LicenseIssuerId { get; set; }

        [StringLength(100)]
        public string ReportTemplateName { get; set; }

        [StringLength(10)]
        public string Culture { get; set; }

        [StringLength(50)]
        public string TimeZone { get; set; }

        public DateTime Starts { get; set; }

        public DateTime? Ends { get; set; }
    }
}