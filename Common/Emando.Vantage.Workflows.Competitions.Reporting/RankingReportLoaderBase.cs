using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Components;
using Emando.Vantage.Workflows.Reporting;
using Emando.Vantage.Workflows.Reporting.TelerikReports;
using Telerik.Reporting;

namespace Emando.Vantage.Workflows.Competitions.Reporting
{
    public abstract class RankingReportLoaderBase : IRankingReportLoader
    {
        private readonly Func<PersonTimesWorkflow> personTimesWorkflowFactory;

        protected RankingReportLoaderBase(Func<PersonTimesWorkflow> personTimesWorkflowFactory)
        {
            this.personTimesWorkflowFactory = personTimesWorkflowFactory;
        }

        #region IRankingReportLoader Members

        public async Task<ILoadedReport> LoadAsync(string licenseIssuerId, string discipline, string distanceDiscipline, int[][] distanceValues, TimeSpan? toTime,
            decimal? maxPoints, int? count, IEnumerable<IHistoricalTimeSelector[]> selectorSets, OptionalReportColumns optionalColumns, int? classificationWeight)
        {
            if (distanceValues == null)
                throw new ArgumentNullException(nameof(distanceValues));

            var book = new ReportBook();
            var i = 1;
            using (var workflow = personTimesWorkflowFactory())
                foreach (var distances in distanceValues)
                    foreach (var selectors in selectorSets.CartesianProduct().Select(p => p.ToArray()))
                    {
                        Report report = null;
                        if (distances.Length == 1)
                        {
                            var people = await workflow.GetRankingAsync(licenseIssuerId, discipline, distanceDiscipline, distances[0], toTime, count, selectors);
                            report = CreateTimeReport(distances[0], people, optionalColumns);
                        }
                        else if (distances.Length > 1)
                        {
                            var people = await workflow.GetRankingAsync(licenseIssuerId, discipline, distanceDiscipline, distances, classificationWeight, maxPoints, count, selectors);
                            report = CreatePointsReport(distances, people, optionalColumns);
                        }

                        if (report == null)
                            continue;

                        report.ReportParameters.Add("Filters", ReportParameterType.String, string.Join(", ", (IEnumerable<IHistoricalTimeSelector>)selectors));
                        report.DocumentName = string.Join("_", selectors.Select(s => s.ToShortString()));
                        book.Reports.Add(report);
                        i++;
                    }

            return book.Reports.Count != 0 ? new TelerikLoadedReport(book) : null;
        }

        #endregion

        protected abstract Report CreateTimeReport(int distance, IList<RankedPersonTime> people, OptionalReportColumns optionalColumns);

        protected abstract Report CreatePointsReport(IList<int> distances, IList<RankedPersonPoints> people, OptionalReportColumns optionalColumns);
    }
}