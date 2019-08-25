using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;
using Emando.Vantage.Workflows.Competitions.Reporting;
using Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting.Properties;
using Emando.Vantage.Workflows.Reporting;
using Emando.Vantage.Workflows.Reporting.TelerikReports;
using Telerik.Reporting;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    public class CompetitorListReportBookLoader : ICompetitionReportLoader
    {
        private readonly Func<DistanceCombinationsWorkflow> workflowFactory;

        public CompetitorListReportBookLoader(Func<DistanceCombinationsWorkflow> workflowFactory)
        {
            this.workflowFactory = workflowFactory;
        }

        #region ICompetitionReportLoader Members

        public async Task<ILoadedReport> LoadAsync(Guid competitionId, OptionalReportColumns optionalColumns)
        {
            var book = new ReportBook();
            using (var workflow = workflowFactory())
            {
                var competition = await workflow.Competitions.FirstOrDefaultAsync(c => c.Id == competitionId);
                if (competition == null)
                    throw new CompetitionNotFoundException();

                book.DocumentName = string.Format(Resources.CompetitorListTitle, competition.Name);

                var combinations = await workflow.Combinations(competitionId).OrderBy(dc => dc.Number).ToListAsync();
                foreach (var combination in combinations)
                {
                    var query = workflow.Competitors(competitionId, combination.Id).Select(c => c.Competitor);

                    var people = await query.OfType<PersonCompetitor>().CountAsync();
                    if (people > 0)
                    {
                        var report = await CompetitorListReportLoaderBase<CompetitorListReport, PersonCompetitor>.LoadAsync(workflow, competitionId, combination.Id, optionalColumns);
                        book.Reports.Add(report);
                    }

                    var teams = await query.OfType<TeamCompetitor>().CountAsync();
                    if (teams > 0)
                    {
                        var report = await CompetitorListReportLoaderBase<TeamCompetitorListReport, TeamCompetitor>.LoadAsync(workflow, competitionId, combination.Id, optionalColumns);
                        book.Reports.Add(report);
                    }
                }
            }

            return new TelerikLoadedReport(book);
        }

        #endregion
    }
}