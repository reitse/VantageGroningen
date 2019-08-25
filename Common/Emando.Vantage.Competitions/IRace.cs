using System;

namespace Emando.Vantage.Competitions
{
    public interface IRace
    {
        Guid Id { get; }

        ICompetitor Competitor { get; }

        int Round { get; set; }

        int Heat { get; set; }

        int Lane { get; set; }

        int Color { get; set; }

        TimeSpan? SeasonBest { get; }

        TimeSpan? PersonalBest { get; }
    }
}