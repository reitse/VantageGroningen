using System;
using System.Linq;
using Emando.Vantage.Components;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class HomeVenueDistrictPersonTimeSelector : IPersonTimeSelector
    {
        private readonly string code;
        private readonly int level;

        public HomeVenueDistrictPersonTimeSelector(int level, string code)
        {
            this.level = level;
            this.code = code;
        }

        public override string ToString()
        {
            return $"District: {code}";
        }

        public string ToShortString()
        {
            return code;
        }

        #region IPersonTimeSelector Members

        public IQueryable<PersonTime> Query(IDisciplineCalculator calculator, IQueryable<PersonTime> times, DateTime? reference)
        {
            return from pt in times
                   where pt.License.Venue.Districts.Any(d => d.Level == level && d.Code == code)
                   select pt;
        }

        #endregion
    }
}