using System;

namespace Emando.Vantage.Competitions
{
    public interface IReadOnlyPassing
    {
        TimeSpan Time { get; }

        decimal? Passed { get; }

        decimal? Speed { get; }

        long Where { get; }
    }
}