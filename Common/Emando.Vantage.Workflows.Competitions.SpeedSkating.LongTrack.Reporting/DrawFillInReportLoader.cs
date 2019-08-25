using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Workflows.Competitions.Reporting;
using Emando.Vantage.Workflows.Reporting;
using Emando.Vantage.Workflows.Reporting.TelerikReports;
using Telerik.Reporting;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    public class DrawFillInReportLoader : IDistanceReportLoader
    {
        private readonly IDistanceDisciplineCalculatorManager calculatorManager;
        private readonly Func<ICompetitionContext> contextFactory;

        public DrawFillInReportLoader(Func<ICompetitionContext> contextFactory, IDistanceDisciplineCalculatorManager calculatorManager)
        {
            this.contextFactory = contextFactory;
            this.calculatorManager = calculatorManager;
        }

        #region IDistanceReportLoader Members

        public async Task<ILoadedReport> LoadAsync(Guid competitionId, Guid distanceId, OptionalReportColumns optionalColumns)
        {
            using (var context = contextFactory())
            {
                var distance = await context.Distances.FirstOrDefaultAsync(d => d.CompetitionId == competitionId && d.Id == distanceId);
                if (distance == null)
                    return null;

                var calculator = calculatorManager.Get(distance.Discipline);
                var length = calculator.Length(distance);
                var report = await LoadAsync(context, competitionId, distanceId, length, optionalColumns);
                return new TelerikLoadedReport(report);
            }
        }

        #endregion

        public static async Task<Report> LoadAsync(ICompetitionContext context, Guid competitionId, Guid distanceId, int length, OptionalReportColumns optionalColumns)
        {
            if (length <= 1500)
                return await DrawReportLoader<DrawFillIn4Report>.LoadAsync(context, competitionId, distanceId, optionalColumns);

            if (length <= 5000)
                return await DrawReportLoader<DrawFillIn13Report>.LoadAsync(context, competitionId, distanceId, optionalColumns);

            if (length <= 10000)
                return await DrawReportLoader<DrawFillIn25Report>.LoadAsync(context, competitionId, distanceId, optionalColumns);

            return null;
        }
    }
}