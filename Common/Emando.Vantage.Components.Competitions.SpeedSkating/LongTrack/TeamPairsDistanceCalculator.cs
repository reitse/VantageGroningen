using System;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Components.Competitions.SpeedSkating.LongTrack
{
    public class TeamPairsDistanceCalculator : PairsDistanceCalculator
    {
        static TeamPairsDistanceCalculator()
        {
        }

        public static TeamPairsDistanceCalculator Default { get; } = new TeamPairsDistanceCalculator();

        protected override bool TryGetDistanceScheme(IDistance distance, out DistanceScheme scheme)
        {
            scheme = new DistanceScheme(17.5, 13.5, 0.1);
            return true;
        }

        public override int Laps(IDistance distance)
        {
            return distance.Value * 2;
        }

        public override decimal Rounds(IDistance distance, int lap)
        {
            return lap / 2M;
        }

        public override decimal RoundsToGo(IDistance distance, int lap)
        {
            return Math.Max(0, Laps(distance) - lap) / 2M;
        }

        public override int LapPassedLength(IDistance distance, int lap)
        {
            return (int)Math.Round(distance.TrackLength / 2 * lap);
        }

        public override int Length(IDistance distance)
        {
            return (int)Math.Round(distance.TrackLength * distance.Value);
        }
    }
}