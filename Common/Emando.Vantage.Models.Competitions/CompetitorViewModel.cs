using System;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Models.Competitions
{
    public class CompetitorViewModel : ICompetitor
    {
        public Guid Id { get; set; }

        public Guid ListId { get; set; }

        public int StartNumber { get; set; }

        public string LegNumber { get; set; }

        public string NationalityCode { get; set; }

        public string LicenseDiscipline { get; set; }

        public string LicenseKey { get; set; }

        public PersonLicenseFlags LicenseFlags { get; set; }

        public CompetitorStatus Status { get; set; }

        public string Category { get; set; }

        public int? Class { get; set; }

        public string Sponsor { get; set; }

        public string ClubCountryCode { get; set; }

        public int? ClubCode { get; set; }

        public string ClubShortName { get; set; }

        public string ClubFullName { get; set; }

        public string From { get; set; }

        public string Transponder1 { get; set; }

        public string Transponder2 { get; set; }

        public string FullName { get; set; }

        public string ShortName { get; set; }

        public Gender Gender { get; set; }

        public DateTime Added { get; set; }

        public CompetitorSource Source { get; set; }

        public string TypeName { get; set; }
    }
}