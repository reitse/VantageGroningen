using System;
using System.Linq;
using Emando.Vantage.Components;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class ClubPersonTimeSelector : IPersonTimeSelector
    {
        private readonly ClubKey key;

        public ClubPersonTimeSelector(ClubKey key)
        {
            this.key = key;
        }

        public override string ToString()
        {
            return $"Club: {key}";
        }

        public string ToShortString()
        {
            return string.Format("{0}{1}", key.CountryCode, key.Code);
        }

        public IQueryable<PersonTime> Query(IDisciplineCalculator calculator, IQueryable<PersonTime> times, DateTime? reference = null)
        {
            return from pt in times
                   where pt.License.ClubCountryCode == key.CountryCode && pt.License.ClubCode == key.Code
                   select pt;
        }
    }
}