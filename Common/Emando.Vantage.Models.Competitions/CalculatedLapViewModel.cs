using System;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Models.Competitions
{
    public class CalculatedLapViewModel : ICalculatedLap
    {
        public TimeSpan Time { get; set; }

        public int Index { get; set; }

        public decimal Rounds { get; set; }

        public decimal RoundsToGo { get; set; }

        public int PassedLength { get; set; }

        public TimeSpan LapTime { get; set; }

        public int? Ranking { get; set; }
    }
}