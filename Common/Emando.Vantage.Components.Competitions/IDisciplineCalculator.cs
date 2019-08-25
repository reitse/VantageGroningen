using System;

namespace Emando.Vantage.Components.Competitions
{
    public interface IDisciplineCalculator
    {
        int CurrentSeason { get; }

        int Season(DateTime reference);

        DateTime SeasonStarts(int season);

        DateTime SeasonEnds(int season);

        int SeasonAge(int season, DateTime birthDate);

        int DefaultClassificationWeight { get; }

        TimeSpan DefaultClassificationPrecision { get; }

        bool AutomaticStartNumbers { get; }

        int AutomaticStartNumberFrom { get; }

        PrimaryGroup PrimaryGroup { get; }

        int GroupByCategoryLength { get; }
    }

    public enum PrimaryGroup
    {
        Distances,
        DistanceCombinations
    }
}