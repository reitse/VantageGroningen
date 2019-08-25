using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Emando.Vantage.Workflows.Reporting;

namespace Emando.Vantage.Workflows.Competitions.Reporting
{
    public interface IRankingReportLoader : IReportLoader
    {
        Task<ILoadedReport> LoadAsync(string licenseIssuerId, string discipline, string distanceDiscipline, int[][] distanceValues, TimeSpan? toTime, decimal? maxPoints,
            int? count, IEnumerable<IHistoricalTimeSelector[]> selectorSets, OptionalReportColumns optionalColumns, int? classificationWeight);
    }
}