using System;
using System.Linq;
using Emando.Vantage.Components;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class CountrySeasonBestSelector : SeasonTimeSelectorBase
    {
        private readonly string countryCode;

        public CountrySeasonBestSelector(DateTime from, DateTime to, string countryCode) : base(from, to)
        {
            this.countryCode = countryCode;
        }

        public override IQueryable<PersonTime> Query(IDisciplineCalculator calculator, IQueryable<PersonTime> times, DateTime? reference)
        {
            return from pt in base.Query(calculator, times, reference)
                   where pt.Venue.Address.CountryCode == countryCode
                   select pt;
        }

        public override string ToShortString()
        {
            return string.Format("{0}-{1}", base.ToShortString(), countryCode);
        }
    }
}