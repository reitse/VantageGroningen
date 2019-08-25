using System;

namespace Emando.Vantage.Competitions
{
    public interface IReadOnlyLap
    {
        TimeSpan Time { get; }

        decimal? Points { get; }
    }
}