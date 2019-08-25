using System;
using System.Linq;
using Emando.Vantage.Components;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class HomeVenueDistrictSeasonBestSelector : SeasonTimeSelectorBase
    {
        private readonly string code;
        private readonly int level;

        public HomeVenueDistrictSeasonBestSelector(DateTime from, DateTime to, int level, string code) : base(from, to)
        {
            this.level = level;
            this.code = code;
        }

        public override IQueryable<PersonTime> Query(IDisciplineCalculator calculator, IQueryable<PersonTime> times, DateTime? reference)
        {
            return from pt in base.Query(calculator, times, reference)
                   where pt.License.Venue.Districts.Any(d => d.Level == level && d.Code == code)
                   select pt;
        }

        public override string ToShortString()
        {
            return string.Format("{0}-{1}", base.ToShortString(), code);
        }
    }
}