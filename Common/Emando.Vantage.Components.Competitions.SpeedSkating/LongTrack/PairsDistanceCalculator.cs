using System;
using System.Collections.Generic;
using System.Linq;
using Emando.Vantage.Competitions;
using Emando.Vantage.Competitions.SpeedSkating.LongTrack;

namespace Emando.Vantage.Components.Competitions.SpeedSkating.LongTrack
{
    public abstract class PairsDistanceCalculator : DistanceDisciplineCalculator
    {
        public override TimeSpan? DefaultLapTimePrecision => TimeSpan.FromMilliseconds(100);

        public override decimal? Speed(IEnumerable<IReadOnlyRacePassing> passings, IReadOnlyRacePassing passing, decimal passed)
        {
            return passings.Speed(passing.PresentationSource, passed, passing.When, 2);
        }

        public static PairsRaceColors Colors(IDistance distance, int pair)
        {
            switch (distance.StartMode)
            {
                case DistanceStartMode.SingleHeat:
                    return PairsRaceColors.WhiteRed;
                case DistanceStartMode.MultipleHeats:
                    return (pair - distance.FirstHeat) % 2 == 0 ? PairsRaceColors.WhiteRed : PairsRaceColors.YellowBlue;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override bool CanCalculatePoints(int distanceLength, int classificationWeight, TimeSpan classificationPrecision, TimeSpan time)
        {
            return classificationWeight > 0;
        }

        public override decimal CalculatePoints(int distanceLength, int classificationWeight, TimeSpan classificationPrecision, TimeSpan time)
        {
            if (!CanCalculatePoints(distanceLength, classificationWeight, classificationPrecision, time))
                throw new InvalidOperationException();

            var factor = distanceLength / (decimal)classificationWeight;
            var milliseconds = time.Truncate(classificationPrecision).Ticks / (decimal)TimeSpan.TicksPerMillisecond / factor;
            
            return (long)milliseconds / 1000M;
        }

        public override TimeSpan? PointsToDistanceTime(int distance, int classificationWeight, decimal points)
        {
            var factor = classificationWeight > 0 ? distance / classificationWeight : 1;
            var seconds = points * factor;
            return TimeSpan.FromTicks((long)(seconds * TimeSpan.TicksPerSecond));
        }

        public override IEnumerable<CalculatedLap> GetEstimatedLapTimes(IDistance distance, IRace race, double ratio = 1, bool requireTargetTime = false)
        {
            var targetTime = race.SeasonBest ?? race.PersonalBest;
            if (!targetTime.HasValue && requireTargetTime)
                yield break;

            DistanceScheme scheme;
            if (!TryGetDistanceScheme(distance, out scheme))
                yield break;

            var lapTimes = new List<double>
            {
                scheme.Opening
            };
            var laps = Laps(distance);
            for (var lap = 2; lap <= laps; lap++)
                lapTimes.Add(scheme.Base + (lap - 2) * scheme.Decline);

            ratio *= targetTime?.TotalSeconds / lapTimes.Sum() ?? (race.Competitor.Gender == Gender.Male ? 1 : 1.05);

            var time = 0d;
            var times = new List<TimeSpan?>();
            foreach (var lapTime in lapTimes)
            {
                time += lapTime;
                times.Add(TimeSpan.FromSeconds(time * ratio));
            }

            foreach (var lap in CalculateLaps(distance, times))
                yield return lap;
        }

        public override IEnumerable<CalculatedLap> AdjustEstimatedLapTimes(IDistance distance, IRace race, IReadOnlyList<ICalculatedLap> current,
            IReadOnlyList<IReadOnlyLap> actual)
        {
            if (current.Count == 0 && actual.Count > 0)
                current = GetEstimatedLapTimes(distance, race).ToList();

            var ratio = 1d;
            var laps = new List<TimeSpan?>();
            for (var i = 0; i < current.Count; i++)
            {
                var previousTime = current[i];
                if (i < actual.Count)
                {
                    var actualTime = actual[i];
                    ratio = actualTime.Time.TotalSeconds / previousTime.Time.TotalSeconds * 0.7 + ratio * 0.3;
                    laps.Add(actualTime.Time);
                }
                else
                    laps.Add(TimeSpan.FromSeconds(previousTime.Time.TotalSeconds * ratio));
            }
            return CalculateLaps(distance, laps);
        }

        protected abstract bool TryGetDistanceScheme(IDistance distance, out DistanceScheme scheme);

        #region Nested type: DistanceScheme

        protected struct DistanceScheme
            {
            public DistanceScheme(double opening, double @base = 0, double decline = 0)
            {
                Opening = opening;
                Base = @base;
                Decline = decline;
            }

            public double Opening { get; }

            public double Base { get; }

            public double Decline { get; }
        }

        #endregion
    }
}