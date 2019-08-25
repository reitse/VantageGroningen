using System;
using System.Linq;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class FromDatePersonTimeSelector : IPersonTimeSelector
    {
        private readonly DateTime from;

        public FromDatePersonTimeSelector(DateTime @from)
        {
            this.@from = @from.Date;
        }

        public IQueryable<PersonTime> Query(IDisciplineCalculator calculator, IQueryable<PersonTime> times, DateTime? reference = null)
        {
            return from pt in times
                   where pt.Date >= @from
                   select pt;
        }

        public override string ToString()
        {
            return from.ToString("d");
        }

        public string ToShortString()
        {
            return from.ToString("yyyyMMdd");
        }
    }
}