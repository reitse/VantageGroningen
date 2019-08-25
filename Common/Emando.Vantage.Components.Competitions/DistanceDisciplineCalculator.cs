using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Emando.Vantage.Competitions;
using Emando.Vantage.Components.Competitions.Properties;

namespace Emando.Vantage.Components.Competitions
{
    public class DistanceDisciplineCalculator : IDistanceDisciplineCalculator
    {
        #region IDistanceDisciplineCalculator Members

        public virtual HeatStartEvent HeatStartEvent => HeatStartEvent.Start;

        public virtual TimeSpan? DefaultResultPrecision => TimeSpan.FromMilliseconds(1);

        public virtual TimeSpan? DefaultLapTimePrecision => TimeSpan.FromMilliseconds(1);

        public virtual DistanceRoundScheme RoundScheme(IDistance distance)
        {
            return DistanceRoundScheme.SingleElimination;
        }

        public virtual int HeatsInRound(IDistance distance, int round)
        {
            var roundsToGo = distance.Rounds - round;
            return 2.Pow(roundsToGo);
        }

        public virtual string DistanceHeatName(IDistance distance, int round, int heat, int heatCount, CultureInfo culture)
        {
            if (distance.Rounds > 1)
            {
                var roundsToGo = distance.Rounds - round;
                var scheme = RoundScheme(distance);
                var format = roundsToGo > 0
                    ? $"{distance.Name}: {{0}} {heat}/{heatCount}"
                    : $"{distance.Name}: {{0}}";

                var roundName = Resources.ResourceManager.GetString($"{scheme}_RoundsToGo_{roundsToGo}", culture)
                    ?? Resources.ResourceManager.GetString($"{scheme}_RoundsToGo_Other", culture);
                return string.Format(format, roundName);
            }

            return distance.Name;
        }

        public virtual int ExpectedRacesInHeat(IDistance distance, int round, int heat, int competitorCount)
        {
            var heats = HeatsInRound(distance, 1);
            var races = (competitorCount + heats - 1) / heats;

            for (var r = 2; r <= round; r++)
            {
                competitorCount = 0;
                for (var h = 1; h <= heats; h++)
                    competitorCount += FirstCompetitorsToNextRound(distance, r, h, races);

                heats = HeatsInRound(distance, r);
                races = (competitorCount + heats - 1) / heats;
            }
            return races;
        }

        public virtual int FirstCompetitorsToNextRound(IDistance distance, int round, int heat, int races)
        {
            return races;
        }

        public virtual TimeSpan EstimatedRoundDuration(IDistance distance, int round)
        {
            return TimeSpan.Zero;
        }

        public virtual TimeSpan MininumLapTime(IDistance distance)
        {
            return TimeSpan.FromSeconds(5);
        }

        public virtual string HeatName(IDistance distance, int round, int heat, CultureInfo culture)
        {
            return string.Format(Resources.ResourceManager.GetString("HeatName", culture) ?? Resources.HeatName, heat);
        }

        public virtual int Laps(IDistance distance)
        {
            switch (distance.ValueQuantity)
            {
                case DistanceValueQuantity.Length:
                    return Calculator.Laps(distance.TrackLength, distance.Value);
                case DistanceValueQuantity.Count:
                    return distance.Value;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public virtual decimal Rounds(IDistance distance, int lap)
        {
            return lap;
        }

        public virtual decimal RoundsToGo(IDistance distance, int lap)
        {
            return Calculator.LapsToGo(distance.TrackLength, Length(distance), lap);
        }

        public virtual decimal? PassedLength(IDistance distance, int startLane, IEnumerable<IReadOnlyRacePassing> passings, IReadOnlyRacePassing passing,
            IVenueDistance source)
        {
            IDistanceLaneLocations laneLocations;
            if (!source.Locations.TryGetValue(startLane, out laneLocations))
                return null;

            return passings.PassedLength(passing.PresentationSource, passing.When, laneLocations, source.Segments);
        }

        public virtual decimal? Speed(IEnumerable<IReadOnlyRacePassing> passings, IReadOnlyRacePassing passing, decimal passed)
        {
            return passings.Speed(passing.PresentationSource, passed, passing.When, 1);
        }

        public virtual int LapPassedLength(IDistance distance, int lap)
        {
            return Calculator.LapPassedLength(distance.TrackLength, Length(distance), lap);
        }

        public virtual int Length(IDistance distance)
        {
            switch (distance.ValueQuantity)
            {
                case DistanceValueQuantity.Length:
                    return distance.Value;
                case DistanceValueQuantity.Count:
                    return (int)(distance.Value * distance.TrackLength);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public IEnumerable<CalculatedLap> CalculateLaps(IDistance distance, IEnumerable<IReadOnlyActiveRaceLap> laps)
        {
            return CalculateLaps(distance, laps.Select(l => new KeyValuePair<TimeSpan?, int?>(l?.Time, l?.Ranking)));
        } 

        public IEnumerable<CalculatedLap> CalculateLaps(IDistance distance, IEnumerable<TimeSpan?> laps)
        {
            return CalculateLaps(distance, laps.Select(l => new KeyValuePair<TimeSpan?, int?>(l, null)));
        }

        private IEnumerable<CalculatedLap> CalculateLaps(IDistance distance, IEnumerable<KeyValuePair<TimeSpan?, int?>> rankings)
        {
            var list = rankings.ToList();
            var previousTime = TimeSpan.Zero;
            var lapCount = Math.Min(list.Count, Laps(distance));
            for (var i = 0; i < lapCount; i++)
            {
                var lap = list[i];
                if (!lap.Key.HasValue)
                    continue;

                var lapTimePrecision = DefaultLapTimePrecision ?? TimeSpan.Zero;
                var lapTime = lap.Key.Value.Truncate(lapTimePrecision) - previousTime.Truncate(lapTimePrecision);
                var rounds = Rounds(distance, i + 1);
                var roundsToGo = RoundsToGo(distance, i + 1);
                var passedLength = LapPassedLength(distance, i + 1);
                previousTime = lap.Key.Value;

                yield return new CalculatedLap(lap.Key.Value, i, lapTime, rounds, roundsToGo, passedLength, lap.Value);
            }
        }

        public bool CanCalculateRacePoints(IDistance distance, int classificationWeight, TimeSpan classificationPrecision, IRaceResult result, IRaceTime time)
        {
            return result != null && result.TimeInvalidReason == null && time != null
                && CanCalculatePoints(Length(distance), classificationWeight, classificationPrecision, time.Time);
        }

        public decimal CalculateRacePoints(IDistance distance, int classificationWeight, TimeSpan classificationPrecision, IRaceResult result, IRaceTime time)
        {
            if (!CanCalculateRacePoints(distance, classificationWeight, classificationPrecision, result, time))
                throw new NotSupportedException();

            return CalculatePoints(Length(distance), classificationWeight, classificationPrecision, time.Time);
        }

        public virtual bool CanCalculatePoints(int distanceLength, int classificationWeight, TimeSpan classificationPrecision, TimeSpan time)
        {
            return false;
        }

        public virtual decimal CalculatePoints(int distanceLength, int classificationWeight, TimeSpan classificationPrecision, TimeSpan time)
        {
            throw new NotSupportedException();
        }

        public virtual IEnumerable<CalculatedLap> GetEstimatedLapTimes(IDistance distance, IRace race, double ratio = 1, bool requireTargetTime = false)
        {
            yield break;
        }

        public virtual TimeSpan? PointsToDistanceTime(int distance, int classificationWeight, decimal points)
        {
            return null;
        }

        public virtual IEnumerable<CalculatedLap> AdjustEstimatedLapTimes(IDistance distance, IRace race, IReadOnlyList<ICalculatedLap> current, IReadOnlyList<IReadOnlyLap> actual)
        {
            return CalculateLaps(distance, actual.Select(l => l?.Time));
        }

        #endregion
    }
}