using System;

namespace Emando.Vantage.Competitions
{
    public interface ICalculatedLap
    {
        TimeSpan Time { get; }

        int Index { get; }

        TimeSpan LapTime { get; }

        decimal Rounds { get; }

        decimal RoundsToGo { get; }

        int PassedLength { get; }

        int? Ranking { get; }
    }
}