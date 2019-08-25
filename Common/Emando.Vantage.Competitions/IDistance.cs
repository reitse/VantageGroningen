using System;

namespace Emando.Vantage.Competitions
{
    public interface IDistance
    {
        int Number { get; }

        string Discipline { get; }

        int Rounds { get; }

        int FirstHeat { get; }

        DistanceStartMode StartMode { get; }

        decimal TrackLength { get; }

        int Value { get; }

        string Name { get; }

        DistanceValueQuantity ValueQuantity { get; }

        TimeSpan ClassificationPrecision { get; }
    }
}