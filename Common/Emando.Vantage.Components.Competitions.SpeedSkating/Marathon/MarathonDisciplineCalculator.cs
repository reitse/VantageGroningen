using System;

namespace Emando.Vantage.Components.Competitions.SpeedSkating.Marathon
{
    public class MarathonDisciplineCalculator : IDisciplineCalculator
    {
        #region IDisciplineCalculator Members

        public int CurrentSeason => Season(DateTime.UtcNow);

        public int Season(DateTime reference)
        {
            return reference.Month <= 6 ? reference.Year - 1 : reference.Year;
        }

        public DateTime SeasonStarts(int season)
        {
            return new DateTime(season, 7, 1);
        }

        public DateTime SeasonEnds(int season)
        {
            return new DateTime(season + 1, 7, 1);
        }

        public int SeasonAge(int season, DateTime birthDate)
        {
            return Math.Max(0, birthDate.Age(SeasonStarts(season).AddDays(-1)));
        }

        public int DefaultClassificationWeight => 1;

        public TimeSpan DefaultClassificationPrecision => TimeSpan.FromMilliseconds(10);

        public bool AutomaticStartNumbers => true;

        public int AutomaticStartNumberFrom => 1;

        public PrimaryGroup PrimaryGroup => PrimaryGroup.Distances;

        public int GroupByCategoryLength => 2;

        #endregion
    }
}