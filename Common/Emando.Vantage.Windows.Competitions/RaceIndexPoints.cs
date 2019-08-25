using System;

namespace Emando.Vantage.Windows.Competitions
{
    public struct RaceIndexPoints
    {
        public RaceIndexPoints(TimeSpan time, decimal points)
        {
            this.Time = time;
            this.Points = points;
        }

        public TimeSpan Time { get; }

        public decimal Points { get; }
    }
}