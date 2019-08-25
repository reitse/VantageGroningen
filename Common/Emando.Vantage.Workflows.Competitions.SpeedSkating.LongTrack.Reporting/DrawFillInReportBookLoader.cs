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
    public class DrawFillInReportBookLoader : ICompetitionReportLoader
    {
        private readonly Func<ICompetitionContext> contextFactory;
        private readonly IDistanceDisciplineCalculatorManager calculatorManager;

        public DrawFillInReportBookLoader(Func<ICompetitionContext> contextFactory, IDistanceDisciplineCalculatorManager calculatorManager)
        {
            this.contextFactory = contextFactory;
            this.calculatorManager = calculatorManager;
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

                book.DocumentName = string.Format(Resources.FillInTitle, competition.Name);

                foreach (var distance in await context.Distances.Where(d => d.CompetitionId == competitionId).OrderBy(d => d.Number).ToListAsync())
                {
                    var calculator = calculatorManager.Get(distance.Discipline);
                    var length = calculator.Length(distance);

                    var report = await DrawFillInReportLoader.LoadAsync(context, competitionId, distance.Id, length, optionalColumns);
                    var drawReport = report as IPairsDrawReport;
                    if (drawReport?.Pairs.Count() == 0)
                        continue;

                    book.Reports.Add(report);
                }
            }

            return new TelerikLoadedReport(book);
        }

        #endregion
    }
}