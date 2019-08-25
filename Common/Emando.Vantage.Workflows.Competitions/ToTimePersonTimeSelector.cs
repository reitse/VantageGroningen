using System;
using System.Linq;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class ToTimePersonTimeSelector : IPersonTimeSelector
    {
        private readonly TimeSpan time;

        public ToTimePersonTimeSelector(TimeSpan time)
        {
            this.time = time;
        }

        #region IPersonTimeSelector Members

        public IQueryable<PersonTime> Query(IDisciplineCalculator calculator, IQueryable<PersonTime> times, DateTime? reference = null)
        {
            return from pt in times
                   where pt.Time < time
                   select pt;
        }

        #endregion

        public override string ToString()
        {
            return $"Time: < {time}";
        }

        public string ToShortString()
        {
            return string.Format("L{0:0}", time.TotalMilliseconds);
        }
    }
}