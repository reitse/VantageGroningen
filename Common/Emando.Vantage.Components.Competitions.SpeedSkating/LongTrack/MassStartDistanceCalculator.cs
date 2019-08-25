using System;
using System.Collections.Generic;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Components.Competitions.SpeedSkating.LongTrack
{
    public class MassStartDistanceCalculator : DistanceDisciplineCalculator
    {
        static MassStartDistanceCalculator()
        {
        }

        public static MassStartDistanceCalculator Default { get; } = new MassStartDistanceCalculator();

        public override HeatStartEvent HeatStartEvent => HeatStartEvent.FirstPassing;

        public override int Laps(IDistance distance)
        {
            return distance.Value + 1;
        }

        public override decimal RoundsToGo(IDistance distance, int lap)
        {
            return Math.Max(0, Laps(distance) - lap);
        }

        public override decimal? PassedLength(IDistance distance, int startLane, IEnumerable<IReadOnlyRacePassing> passings, IReadOnlyRacePassing passing,
            IVenueDistance source)
        {
            IDistanceLaneLocations laneLocations;
            if (!source.Locations.TryGetValue(0, out laneLocations))
                return null;

            return passings.PassedLength(passing.PresentationSource, passing.When, laneLocations, source.Segments);
        }

        public override int LapPassedLength(IDistance distance, int lap)
        {
            return (int)Math.Round(distance.TrackLength * lap);
        }

        public override int Length(IDistance distance)
        {
            return (int)Math.Round(distance.TrackLength * distance.Value);
        }
    }
}