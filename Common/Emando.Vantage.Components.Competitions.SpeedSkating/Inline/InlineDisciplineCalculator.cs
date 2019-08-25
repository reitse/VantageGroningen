using System;

namespace Emando.Vantage.Components.Competitions.SpeedSkating.Inline
{
    public class InlineDisciplineCalculator : IDisciplineCalculator
    {
        #region IDisciplineCalculator Members

        public int CurrentSeason => Season(DateTime.UtcNow);

        public int Season(DateTime reference)
        {
            return reference.Year;
        }

        public DateTime SeasonStarts(int season)
        {
            return new DateTime(season, 1, 1);
        }

        public DateTime SeasonEnds(int season)
        {
            return new DateTime(season + 1, 1, 1);
        }

        public int SeasonAge(int season, DateTime birthDate)
        {
            return Math.Max(0, season - birthDate.Year);
        }

        public int DefaultClassificationWeight => 1;

        public TimeSpan DefaultClassificationPrecision => TimeSpan.FromMilliseconds(10);

        public bool AutomaticStartNumbers => false;

        public int AutomaticStartNumberFrom => 1000;

        public PrimaryGroup PrimaryGroup => PrimaryGroup.DistanceCombinations;

        public int GroupByCategoryLength => 3;

        #endregion
    }
}