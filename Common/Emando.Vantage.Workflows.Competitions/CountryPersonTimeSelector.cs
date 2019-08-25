using System;
using System.Linq;
using Emando.Vantage.Components;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class CountryPersonTimeSelector : IPersonTimeSelector
    {
        private readonly string countryCode;

        public CountryPersonTimeSelector(string countryCode)
        {
            this.countryCode = countryCode;
        }

        #region IPersonTimeSelector Members

        public IQueryable<PersonTime> Query(IDisciplineCalculator calculator, IQueryable<PersonTime> times, DateTime? reference = null)
        {
            return from pt in times
                   where pt.Venue.Address.CountryCode == countryCode
                   select pt;
        }

        #endregion

        public override string ToString()
        {
            return $"Country: {countryCode}";
        }

        public string ToShortString()
        {
            return countryCode;
        }
    }
}