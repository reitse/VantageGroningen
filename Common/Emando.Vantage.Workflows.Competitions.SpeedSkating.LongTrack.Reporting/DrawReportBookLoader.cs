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
    public class DrawReportBookLoader : ICompetitionReportLoader
    {
        private readonly Func<ICompetitionContext> contextFactory;

        public DrawReportBookLoader(Func<ICompetitionContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        #region ICompetitionReportLoader Members

        public async Task<ILoadedReport> LoadAsync(Guid competitionId, OptionalReportColumns optionalColumns)
        {
            var book = new ReportBook();
            using (var context = contextFactory())
            {
                var competition = await context.Competitions.FirstOrDefaultAsync(c => c.Id == competitionId);
                if (competition == null)
                    throw new CompetitionNotFoundException();

                book.DocumentName = string.Format(Resources.DrawTitle, competition.Name);

                foreach (var distance in await context.Distances.Where(d => d.CompetitionId == competitionId).OrderBy(d => d.Number).ToListAsync())
                {
                    Report report = null;
                    switch (distance.Discipline)
                    {
                        case "SpeedSkating.LongTrack.PairsDistance.Individual":
                            report = await DrawReportLoader<DrawReport>.LoadAsync(context, competitionId, distance.Id, optionalColumns);
                            break;
                        case "SpeedSkating.LongTrack.PairsDistance.TeamPursuit":
                        case "SpeedSkating.LongTrack.PairsDistance.TeamSprint":
                            report = await DrawReportLoader<TeamDrawReport>.LoadAsync(context, competitionId, distance.Id, optionalColumns);
                            break;
                        case "SpeedSkating.LongTrack.MassStartDistance":
                            report = await MassStartDrawReportLoader.LoadAsync(context, competitionId, distance.Id, optionalColumns);
                            break;
                    }

                    var pairsDrawReport = report as IPairsDrawReport;
                    if (pairsDrawReport?.Pairs.Count() == 0)
                        continue;

                    book.Reports.Add(report);
                }
            }

            if (book.Reports.Count == 0)
                return null;

            return new TelerikLoadedReport(book);
        }

        #endregion
    }
}