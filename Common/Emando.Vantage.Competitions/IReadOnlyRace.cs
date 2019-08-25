using System;

namespace Emando.Vantage.Competitions
{
    public interface IReadOnlyRace
    {
        ICompetitor Competitor { get; }

        int Heat { get; }

        int Round { get; }

        int Lane { get; }

        int Color { get; }

        TimeSpan? SeasonBest { get; }

        TimeSpan? PersonalBest { get; }
    }
}