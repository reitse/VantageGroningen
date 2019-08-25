using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Workflows.Competitions.Reporting;
using Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting.Properties;
using Emando.Vantage.Workflows.Reporting;
using Emando.Vantage.Workflows.Reporting.TelerikReports;
using Telerik.Reporting;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    public class DistanceResultReportBookLoader : ICompetitionReportLoader
    {
        private readonly IDistanceDisciplineExpertManager expertManager;
        private readonly Func<RacesWorkflow> workflowFactory;

        public DistanceResultReportBookLoader(Func<RacesWorkflow> workflowFactory, IDistanceDisciplineExpertManager expertManager)
        {
            this.workflowFactory = workflowFactory;
            this.expertManager = expertManager;
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

                book.DocumentName = string.Format(Resources.ResultTitle, competition.Name);

                foreach (var distance in await workflow.Distances.Where(d => d.CompetitionId == competitionId).OrderBy(d => d.Number).ToListAsync())
                    if (distance.Discipline.StartsWith("SpeedSkating.LongTrack.PairsDistance"))
                    {
                        var result = await DistanceResultReportLoader.LoadAsync(workflow, competitionId, distance.Id, optionalColumns);
                        if (result.Races.Count() == 0)
                            continue;

                        book.Reports.Add(result);

                        var details = await DistanceDetailedResultReportLoader.LoadAsync(workflow, competitionId, distance.Id, expertManager, optionalColumns);
                        book.Reports.Add(details);
                    }
                    else if (distance.Discipline.StartsWith("SpeedSkating.LongTrack.MassStartDistance"))
                    {
                        var result = await MassStartDistanceResultReportLoader.LoadAsync(workflow, competitionId, distance.Id, optionalColumns);
                        if (result.Races.Count() == 0)
                            continue;

                        book.Reports.Add(result);
                    }
            }

            return new TelerikLoadedReport(book);
        }

        #endregion
    }
}