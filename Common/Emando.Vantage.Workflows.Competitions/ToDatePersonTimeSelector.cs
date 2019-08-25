using System;
using System.Linq;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class ToDatePersonTimeSelector : IPersonTimeSelector
    {
        private readonly DateTime to;

        public ToDatePersonTimeSelector(DateTime to)
        {
            this.to = to.Date;
        }

        #region IPersonTimeSelector Members

        public IQueryable<PersonTime> Query(IDisciplineCalculator calculator, IQueryable<PersonTime> times, DateTime? reference = null)
        {
            return from pt in times
                   where pt.Date <= to
                   select pt;
        }

        #endregion

        public override string ToString()
        {
            return to.ToString("d");
        }

        public string ToShortString()
        {
            return to.ToString("yyyyMMdd");
        }
    }
}