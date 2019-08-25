using System;
using System.Collections.Generic;
using System.Globalization;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Components.Competitions
{
    public interface IDistanceDisciplineCalculator
    {
        HeatStartEvent HeatStartEvent { get; }

        TimeSpan? DefaultResultPrecision { get; }

        TimeSpan? DefaultLapTimePrecision { get; }

        DistanceRoundScheme RoundScheme(IDistance distance);

        int HeatsInRound(IDistance distance, int round);

        string DistanceHeatName(IDistance distance, int round, int heat, int heatCount, CultureInfo culture);

        int ExpectedRacesInHeat(IDistance distance, int round, int heat, int competitorCount);

        int FirstCompetitorsToNextRound(IDistance distance, int round, int heat, int races);

        TimeSpan EstimatedRoundDuration(IDistance distance, int round);

        TimeSpan MininumLapTime(IDistance distance);

        string HeatName(IDistance distance, int round, int heat, CultureInfo culture);

        int Laps(IDistance distance);

        decimal Rounds(IDistance distance, int lap);

        decimal RoundsToGo(IDistance distance, int lap);

        decimal? PassedLength(IDistance distance, int startLane, IEnumerable<IReadOnlyRacePassing> passings, IReadOnlyRacePassing passing, IVenueDistance source);

        decimal? Speed(IEnumerable<IReadOnlyRacePassing> passings, IReadOnlyRacePassing passing, decimal passed);

        int LapPassedLength(IDistance distance, int lap);

        int Length(IDistance distance);

        bool CanCalculateRacePoints(IDistance distance, int classificationWeight, TimeSpan classificationPrecision, IRaceResult result, IRaceTime time);

        decimal CalculateRacePoints(IDistance distance, int classificationWeight, TimeSpan classificationPrecision, IRaceResult result, IRaceTime time);

        bool CanCalculatePoints(int distanceLength, int classificationWeight, TimeSpan classificationPrecision, TimeSpan time);

        decimal CalculatePoints(int distanceLength, int classificationWeight, TimeSpan classificationPrecision, TimeSpan time);

        IEnumerable<CalculatedLap> CalculateLaps(IDistance distance, IEnumerable<IReadOnlyActiveRaceLap> laps);
            
        IEnumerable<CalculatedLap> CalculateLaps(IDistance distance, IEnumerable<TimeSpan?> laps);

        IEnumerable<CalculatedLap> GetEstimatedLapTimes(IDistance distance, IRace race, double ratio = 1, bool requireTargetTime = false);

        TimeSpan? PointsToDistanceTime(int distance, int classificationWeight, decimal points);
        
        IEnumerable<CalculatedLap> AdjustEstimatedLapTimes(IDistance distance, IRace race, IReadOnlyList<ICalculatedLap> current, IReadOnlyList<IReadOnlyLap> actual);
    }
}