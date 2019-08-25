using System;
using System.Linq;
using Emando.Vantage.Components;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class VenueSeasonBestSelector : SeasonTimeSelectorBase
    {
        private readonly string venueCode;

        public VenueSeasonBestSelector(DateTime from, DateTime to, string venueCode) : base(from, to)
        {
            this.venueCode = venueCode;
        }

        public override IQueryable<PersonTime> Query(IDisciplineCalculator calculator, IQueryable<PersonTime> times, DateTime? reference)
        {
            return from pt in base.Query(calculator, times, reference)
                   where pt.VenueCode == venueCode
                   select pt;
        }

        public override string ToShortString()
        {
            return string.Format("{0}-{1}", base.ToShortString(), venueCode);
        }
    }
}