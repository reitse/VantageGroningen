using System;
using System.Linq;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class LicenseKeysTimeSelector : IPersonTimeSelector
    {
        private readonly string[] licenseKeys;

        public LicenseKeysTimeSelector(string[] licenseKeys)
        {
            this.licenseKeys = licenseKeys;
        }

        public IQueryable<PersonTime> Query(IDisciplineCalculator calculator, IQueryable<PersonTime> times, DateTime? reference = null)
        {
            return from pt in times
                   where licenseKeys.Contains(pt.LicenseKey)
                   select pt;
        }

        public override string ToString()
        {
            return string.Join(", ", licenseKeys);
        }

        public string ToShortString()
        {
            return null;
        }
    }
}