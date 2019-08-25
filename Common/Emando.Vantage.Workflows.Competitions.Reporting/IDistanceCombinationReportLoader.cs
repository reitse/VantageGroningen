using System;
using System.Threading.Tasks;
using Emando.Vantage.Workflows.Reporting;

namespace Emando.Vantage.Workflows.Competitions.Reporting
{
    public interface IDistanceCombinationReportLoader : IReportLoader
    {
        Task<ILoadedReport> LoadAsync(Guid competitionId, Guid distanceCombinationId, OptionalReportColumns optionalColumns);
    }
}