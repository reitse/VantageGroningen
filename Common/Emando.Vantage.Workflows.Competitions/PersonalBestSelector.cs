using System;
using System.Linq;
using Emando.Vantage.Components;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class PersonalBestSelector : IPersonTimeSelector
    {
        #region IPersonTimeSelector Members

        public IQueryable<PersonTime> Query(IDisciplineCalculator calculator, IQueryable<PersonTime> times, DateTime? reference = null)
        {
            return times;
        }

        public string ToShortString()
        {
            return null;
        }

        #endregion
    }
}