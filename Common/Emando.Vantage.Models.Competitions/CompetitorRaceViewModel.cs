using System;

namespace Emando.Vantage.Models.Competitions
{
    public class CompetitorRaceViewModel
    {
        public Guid Id { get; set; }

        public int DistanceNumber { get; set; }

        public string DistanceName { get; set; }

        public string DistanceDiscipline { get; set; }

        public RaceResultViewModel Result { get; set; }

        public RaceTimeViewModel Time { get; set; }
    }
}