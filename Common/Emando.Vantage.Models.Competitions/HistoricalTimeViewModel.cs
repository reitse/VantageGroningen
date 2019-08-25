using System;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Models.Competitions
{
    public class HistoricalTimeViewModel : IPersonLicenseTime
    {
        public string LicenseIssuerId { get; set; }

        public string LicenseDiscipline { get; set;  }

        public string LicenseKey { get; set; }

        public string VenueCode { get; set; }

        public string Discipline { get; set; }

        public string DistanceDiscipline { get; set; }

        public int Distance { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public string NationalityCode { get; set; }

        public Guid? CompetitionId { get; set; }

        public string Source { get; set; }
    }
}