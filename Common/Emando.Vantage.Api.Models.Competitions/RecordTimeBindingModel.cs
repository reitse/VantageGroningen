using System;
using System.ComponentModel.DataAnnotations;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class RecordTimeBindingModel
    {
        [StringLength(50)]
        public string LicenseIssuerId { get; set; }

        public RecordType Type { get; set; }

        public Gender Gender { get; set; }

        public int FromAge { get; set; }

        public int ToAge { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public string VenueCode { get; set; }

        public string Discipline { get; set; }

        [StringLength(100)]
        public string DistanceDiscipline { get; set; }

        public int Distance { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        [StringLength(3)]
        public string NationalityCode { get; set; }
    }
}