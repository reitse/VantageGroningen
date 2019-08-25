using System;
using System.Linq;
using Emando.Vantage.Components;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public abstract class SeasonTimeSelectorBase : IPersonTimeSelector
    {
        protected SeasonTimeSelectorBase(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }

        public DateTime From { get; }

        public DateTime To { get; }

        public virtual string ToShortString()
        {
            return string.Format("{0}-{1}", From.ToString("yyyyMMdd"), To.ToString("yyyyMMdd"));
        }

        #region IPersonTimeSelector Members

        public virtual IQueryable<PersonTime> Query(IDisciplineCalculator calculator, IQueryable<PersonTime> times, DateTime? reference)
        {
            return from pt in times
                   where pt.Date >= From && pt.Date < To
                   select pt;
        }

        #endregion
    }
}