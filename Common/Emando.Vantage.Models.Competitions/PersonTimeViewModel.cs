using System;

namespace Emando.Vantage.Models.Competitions
{
    public class PersonTimeViewModel
    {
        public string VenueCode { get; set; }

        public string VenueName { get; set; }

        public string VenueAddressCity { get; set; }

        public int Distance { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public string NationalityCode { get; set; }
    }
}