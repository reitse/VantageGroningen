using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Components;
using Emando.Vantage.Workflows.Competitions.Reporting.Properties;
using Emando.Vantage.Workflows.Reporting;
using Emando.Vantage.Workflows.Reporting.TelerikReports;
using Telerik.Reporting;

namespace Emando.Vantage.Workflows.Competitions.Reporting
{
    public abstract class ClassificationReportLoaderBase : IClassificationReportLoader
    {
        private readonly Func<RacesWorkflow> racesWorkflowFactory;

        protected ClassificationReportLoaderBase(Func<RacesWorkflow> racesWorkflowFactory)
        {
            this.racesWorkflowFactory = racesWorkflowFactory;
        }

        #region IClassificationReportLoader Members

        public async Task<ILoadedReport> LoadAsync(Guid competitionId, int classificationWeight, int categoryLength, int? differenceDistance, Guid[] distanceIdentifiers,
            bool groupByDistanceCombinations, OptionalReportColumns optionalColumns)
        {
            var book = new ReportBook();
            using (var workflow = racesWorkflowFactory())
            {
                var competition = await workflow.Competitions.Include(c => c.Venue).Include(c => c.ReportTemplate.Logos).FirstOrDefaultAsync(c => c.Id == competitionId);
                if (competition == null)
                    return null;

                var distances = await workflow.Distances.Include(d => d.Combinations).Where(d => d.CompetitionId == competitionId && distanceIdentifiers.Contains(d.Id))
                    .OrderBy(d => d.Number).ToArrayAsync();

                book.DocumentName = string.Format(Resources.ClassificationTitle, competition.Name,
                    groupByDistanceCombinations
                        ? Resources.DistanceCombinations
                        : string.Join("-", distances.Select(d => d.Value)));

                var selectorSets = new List<IRaceSelector[]>();
                if (groupByDistanceCombinations)
                {
                    var distanceCombinations = distances.SelectMany(d => d.Combinations).Distinct();
                    selectorSets.Add(distanceCombinations.Select(dc => (IRaceSelector)new RaceDistanceCombinationSelector(dc)).ToArray());
                }
                else
                    selectorSets.Add(new IRaceSelector[] { new RaceDistancesSelector(distances) });

                foreach (var selectors in selectorSets.CartesianProduct().Select(p => p.ToArray()))
                {
                    var classifications = await workflow.GetClassificationAsync(competitionId, classificationWeight, categoryLength, differenceDistance, selectors);
                    foreach (var classification in classifications)
                    {
                        if (classification.Distances.Count == 0)
                            continue;

                        var filters = selectors.Select(s => s.ToString());
                        if (classification.Category != string.Empty)
                            filters = filters.Concat(new[] { classification.Category });

                        var report = CreateReport(classification, differenceDistance, optionalColumns);
                        report.SetParameters(competition);
                        report.ReportParameters.Add("Filters", ReportParameterType.String, string.Join(" ", filters));
                        book.Reports.Add(report);
                    }
                }
            }

            return book.Reports.Count != 0 ? new TelerikLoadedReport(book) : null;
        }

        #endregion

        protected abstract Report CreateReport(Classification distances, int? differenceDistance, OptionalReportColumns optionalColumns);
    }
}