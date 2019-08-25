using System;
using System.Linq;
using Emando.Vantage.Components;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class ContinentPersonTimeSelector : IPersonTimeSelector
    {
        private readonly string continentCode;

        public ContinentPersonTimeSelector(string continentCode)
        {
            this.continentCode = continentCode;
        }

        #region IPersonTimeSelector Members

        public IQueryable<PersonTime> Query(IDisciplineCalculator calculator, IQueryable<PersonTime> times, DateTime? reference = null)
        {
            return from pt in times
                   where pt.Venue.ContinentCode == continentCode
                   select pt;
        }

        #endregion

        public override string ToString()
        {
            return $"Continent: {continentCode}";
        }

        public string ToShortString()
        {
            return continentCode;
        }
    }
}