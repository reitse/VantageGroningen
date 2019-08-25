using System;
using System.Collections.Generic;
using System.Linq;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Components.Competitions
{
    public delegate int LaneSelector(IEnumerable<IVenueSegment> passedLapSegments, int lap);

    public static class Calculator
    {
        public static int Laps(decimal trackLength, int length)
        {
            if (trackLength >= 333 && trackLength < 334)
                trackLength = 1000M / 3;

            int laps = (int)(length / trackLength);
            if (length % trackLength >= 1)
                laps++;
            return laps;
        }

        public static int LapsToGo(decimal trackLength, int length, int lap)
        {
            return Math.Max(0, Laps(trackLength, length) - lap);
        }

        public static int LapPassedLength(decimal trackLength, int length, int lap)
        {
            if (lap < 0)
                throw new ArgumentOutOfRangeException(nameof(lap));
            if (lap == 0)
                return 0;

            if (trackLength >= 333 && trackLength < 334)
                trackLength = 1000M / 3;
            var opening = length % trackLength;
            if (opening >= 1)
                lap--;
            return (int)Math.Min(Math.Round(opening + lap * trackLength), length);
        }

        public static decimal PassedLength(this IEnumerable<IReadOnlyRacePassing> passings, PresentationSource presentationSource, DateTime when,
            IDistanceLaneLocations laneLocations, IReadOnlyCollection<IVenueSegment> segments, LaneSelector laneSelector = null)
        {
            laneSelector = laneSelector ?? ((s, l) => 0);

            var lap = 0;
            int lane;
            IVenueSegment segment;
            var from = laneLocations.Start;
            var length = laneLocations.StartOffset;
            var passedLapSegments = new List<IVenueSegment>();

            foreach (var passing in (from p in passings
                                     where !p.Flags.HasFlag(RaceEventFlags.Deleted) && p.PresentationSource == presentationSource && p.When <= when
                                     orderby p.When
                                     select p).SkipWhile(p => p.Where == laneLocations.Start))
            {
                if (!segments.Any(s => s.To == passing.Where))
                    continue;

                do
                {
                    lane = laneSelector(passedLapSegments, lap);
                    segment = segments.FirstOrDefault(s => s.Lane == lane && s.From == from);
                    if (segment == null)
                        return 0;
                    if (laneLocations.Finishes.Contains(segment.To))
                    {
                        passedLapSegments.Clear();
                        lap++;
                    }
                    passedLapSegments.Add(segment);
                    length += segment.Length;
                    from = segment.To;
                } while (segment.To != passing.Where);
            }

            return length;
        }

        public static decimal? Speed(this IEnumerable<IReadOnlyRacePassing> passings, PresentationSource presentationSource, decimal passed, DateTime when, int segments)
        {
            if (segments < 1)
                throw new ArgumentOutOfRangeException(nameof(segments));

            var previous = (from p in passings
                            where !p.Flags.HasFlag(RaceEventFlags.Deleted) && p.PresentationSource == presentationSource && p.When < when
                            orderby p.When descending
                            select p).Skip(segments - 1).FirstOrDefault();
            if (previous == null)
                return null;

            var deltaPassed = passed - previous.Passed;
            var deltaTime = when - previous.When;

            if (deltaPassed < 0)
                return null;

            return deltaPassed / (decimal)deltaTime.TotalSeconds;
        }
    }
}