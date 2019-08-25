using System;
using System.Threading.Tasks;
using Emando.Vantage.Workflows.Reporting;

namespace Emando.Vantage.Workflows.Competitions.Reporting
{
    public interface IClassificationReportLoader : IReportLoader
    {
        Task<ILoadedReport> LoadAsync(Guid competitionId, int classificationWeight, int categoryLength, int? behindDistance, Guid[] distanceIdentifiers,
            bool groupByDistanceCombinations, OptionalReportColumns optionalColumns);
    }
}