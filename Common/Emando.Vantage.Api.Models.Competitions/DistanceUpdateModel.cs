using System;
using System.ComponentModel.DataAnnotations;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class DistanceUpdateModel
    {
        [Range(1, int.MaxValue)]
        public int Number { get; set; }

        public decimal TrackLength { get; set; }

        [Range(0, int.MaxValue)]
        public int Value { get; set; }

        public DistanceValueQuantity ValueQuantity { get; set; }

        public DistanceStartMode StartMode { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        public Guid? DistancePointsTableId { get; set; }

        [Range(1, int.MaxValue)]
        public int Rounds { get; set; }

        public TimeSpan ClassificationPrecision { get; set; }

        public DateTime? Starts { get; set; }

        [StringLength(200)]
        public string Starter { get; set; }

        [StringLength(200)]
        public string Referee1 { get; set; }

        [StringLength(200)]
        public string Referee2 { get; set; }

        [StringLength(50)]
        public string VenueCode { get; set; }

        [StringLength(100)]
        public string VenueDiscipline { get; set; }
        
        [Required]
        public WeatherBindingModel StartWeather { get; set; }

        [Required]
        public WeatherBindingModel EndWeather { get; set; }
    }
}