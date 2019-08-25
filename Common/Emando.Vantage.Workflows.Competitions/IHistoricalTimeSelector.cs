using System;
using System.Linq;
using Emando.Vantage.Competitions;
using Emando.Vantage.Components.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public interface IHistoricalTimeSelector
    {
        string ToShortString();
    }

    public interface IHistoricalTimeSelector<T> : IHistoricalTimeSelector
        where T : IPersonLicenseTime
    {
        IQueryable<T> Query(IDisciplineCalculator calculator, IQueryable<T> times, DateTime? reference = null);
    }
}