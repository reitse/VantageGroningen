using System.Collections.Generic;
using System.Linq;
using Emando.Vantage.Competitions;
using Emando.Vantage.Competitions.SpeedSkating.LongTrack;

namespace Emando.Vantage.Components.Competitions.SpeedSkating.LongTrack
{
    public class IndividualPairsDistanceCalculator : PairsDistanceCalculator
    {
        private static readonly IDictionary<int, IDictionary<int, DistanceScheme>> DistanceSchemes = new Dictionary<int, IDictionary<int, DistanceScheme>>
        {
            {
                333, new Dictionary<int, DistanceScheme>
                {
                    { 100, new DistanceScheme(12) },
                    { 167, new DistanceScheme(19) },
                    { 300, new DistanceScheme(30) },
                    { 333, new DistanceScheme(34) },
                    { 500, new DistanceScheme(19, 28) },
                    { 700, new DistanceScheme(6, 30, 2) },
                    { 1000, new DistanceScheme(34, 30, 2) },
                    { 1500, new DistanceScheme(19, 31, 1) },
                    { 3000, new DistanceScheme(32, 31, 0.5) },
                    { 5000, new DistanceScheme(32, 31, 0.5) },
                    { 10000, new DistanceScheme(32, 32, 0.5) }
                }
            },
            {
                400, new Dictionary<int, DistanceScheme>
                {
                    { 100, new DistanceScheme(12) },
                    { 300, new DistanceScheme(32) },
                    { 500, new DistanceScheme(12, 31) },
                    { 700, new DistanceScheme(32, 31) },
                    { 1000, new DistanceScheme(22, 32, 2.5) },
                    { 1500, new DistanceScheme(32, 32, 2) },
                    { 3000, new DistanceScheme(22, 33, 0.4) },
                    { 5000, new DistanceScheme(22, 33, 0.35) },
                    { 10000, new DistanceScheme(40, 34) }
                }
            }
        };

        static IndividualPairsDistanceCalculator()
        {
        }

        public static IndividualPairsDistanceCalculator Default { get; } = new IndividualPairsDistanceCalculator();

        protected override bool TryGetDistanceScheme(IDistance distance, out DistanceScheme scheme)
        {
            scheme = default(DistanceScheme);
            IDictionary<int, DistanceScheme> schemes;
            return DistanceSchemes.TryGetValue((int)distance.TrackLength, out schemes) && schemes.TryGetValue(distance.Value, out scheme);
        }

        public override decimal? PassedLength(IDistance distance, int startLane, IEnumerable<IReadOnlyRacePassing> passings, IReadOnlyRacePassing passing,
            IVenueDistance source)
        {
            IDistanceLaneLocations laneLocations;
            if (!source.Locations.TryGetValue(startLane, out laneLocations))
                return null;

            return passings.PassedLength(passing.PresentationSource, passing.When, laneLocations, source.Segments,
                (segments, i) => (int)ExpectedLane(distance, i, (Lane)startLane, segments));
        }

        public override int LapPassedLength(IDistance distance, int lap)
        {
            return Calculator.LapPassedLength(distance.TrackLength, Length(distance), lap);
        }

        public Lane ExpectedLane(IDistance distance, int lap, Lane startLane)
        {
            if (lap == 0)
                return startLane;

            var trackLength = distance.TrackLength;
            if (trackLength >= 333 && trackLength < 334)
                trackLength = 1000M / 3;

            var opening = distance.Value % trackLength;
            if (opening >= 1 && opening <= trackLength * 0.75M)
                lap--;

            return (Lane)((int)startLane ^ lap % 2);
        }

        public Lane ExpectedLane(IDistance distance, int lap, Lane startLane, IEnumerable<IVenueSegment> passedLapSegments)
        {
            if (passedLapSegments.Any(s => s.Flags.HasFlag(VenueSegmentFlags.LaneSwitch)))
                lap++;

            return ExpectedLane(distance, lap, startLane);
        }
    }
}