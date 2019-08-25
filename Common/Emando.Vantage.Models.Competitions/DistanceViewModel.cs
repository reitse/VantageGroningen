using System;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Models.Competitions
{
    public class DistanceViewModel : IDistance
    {
        public Guid Id { get; set; }

        public Guid CompetitionId { get; set; }

        public string Discipline { get; set; }

        public int Number { get; set; }

        public decimal TrackLength { get; set; }

        public int Value { get; set; }

        public DistanceValueQuantity ValueQuantity { get; set; }

        public TimeSpan ClassificationPrecision { get; set; }

        public string Name { get; set; }

        public Guid? DistancePointsTableId { get; set; }

        public DateTime? Starts { get; set; }

        public DateTime? LastRaceCommitted { get; set; }

        public int Rounds { get; set; }

        public DistanceStartMode StartMode { get; set; }

        public string Starter { get; set; }

        public string Referee1 { get; set; }

        public string Referee2 { get; set; }

        public int FirstHeat { get; set; }

        public bool ContinuousNumbering { get; set; }

        public WeatherViewModel StartWeather { get; set; }

        public WeatherViewModel EndWeather { get; set; }

        public string VenueCode { get; set; }

        public string VenueDiscipline { get; set; }

        public DistanceDistanceCombinationViewModel[] Combinations { get; set; }
    }
}