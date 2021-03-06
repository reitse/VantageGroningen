using System;
using System.Linq;
using Emando.Vantage.Components;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class HomeVenuePersonTimeSelector : IPersonTimeSelector
    {
        private readonly string venueCode;

        public HomeVenuePersonTimeSelector(string venueCode)
        {
            this.venueCode = venueCode;
        }

        #region IPersonTimeSelector Members

        public IQueryable<PersonTime> Query(IDisciplineCalculator calculator, IQueryable<PersonTime> times, DateTime? reference = null)
        {
            return from pt in times
                   where pt.License.VenueCode == venueCode
                   select pt;
        }

        #endregion

        public override string ToString()
        {
            return $"Home Venue: {venueCode}";
        }

        public string ToShortString()
        {
            return venueCode;
        }
    }
}