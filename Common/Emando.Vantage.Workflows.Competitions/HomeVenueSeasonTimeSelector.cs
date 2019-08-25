using System;
using System.Linq;
using Emando.Vantage.Components;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class HomeVenueSeasonTimeSelector : SeasonTimeSelectorBase
    {
        public HomeVenueSeasonTimeSelector(DateTime from, DateTime to) : base(from, to)
        {
        }

        public override IQueryable<PersonTime> Query(IDisciplineCalculator calculator, IQueryable<PersonTime> personTimes, DateTime? reference)
        {
            return from pt in base.Query(calculator, personTimes, reference)
                   where pt.License.VenueCode == pt.VenueCode
                   select pt;
        }
    }
}