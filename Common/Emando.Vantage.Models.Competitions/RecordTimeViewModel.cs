using System;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Models.Competitions
{
    public class RecordTimeViewModel : IRecordTime
    {
        public string LicenseIssuerId { get; set; }

        public RecordType Type { get; set; }

        public string Name { get; set; }

        public Gender Gender { get; set; }

        public int FromAge { get; set; }

        public int ToAge { get; set; }

        public string VenueCode { get; set; }

        public string VenueName { get; set; }

        public string VenueAddressCity { get; set; }

        public string Discipline { get; set; }

        public string DistanceDiscipline { get; set; }

        public int Distance { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public string NationalityCode { get; set; }
    }
}