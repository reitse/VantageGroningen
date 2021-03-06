﻿using System;
using System.Threading.Tasks;
using Emando.Vantage.Workflows.Reporting;

namespace Emando.Vantage.Workflows.Competitions.Reporting
{
    public interface ICompetitionReportLoader : IReportLoader
    {
        Task<ILoadedReport> LoadAsync(Guid competitionId, OptionalReportColumns optionalColumns);
    }
}