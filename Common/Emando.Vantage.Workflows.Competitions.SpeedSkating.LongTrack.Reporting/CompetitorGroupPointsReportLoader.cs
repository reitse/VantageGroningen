using Emando.Vantage.Workflows.Competitions.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emando.Vantage.Workflows.Reporting;
using Telerik.Reporting;
using System.Data.Entity;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting.Properties;
using Emando.Vantage.Workflows.Reporting.TelerikReports;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    public partial class CompetitorGroupPointsReportLoader : ICompetitionReportLoader
    {
        private readonly Func<RacesWorkflow> workflowFactory;
        private readonly IDistanceDisciplineExpertManager expertManager;

        public interface ICompetitorGroupComparer : IEqualityComparer<CompetitorBase>
        {
            CompetitorGroup Group(CompetitorBase key, IEnumerable<RankedRace> races);
        }

        public CompetitorGroupPointsReportLoader(Func<RacesWorkflow> workflowFactory, IDistanceDisciplineExpertManager expertManager)
        {
            this.workflowFactory = workflowFactory;
            this.expertManager = expertManager;
        }

        public async Task<ILoadedReport> LoadAsync(Guid competitionId, OptionalReportColumns optionalColumns)
        {
            var book = new ReportBook();
            using (var workflow = workflowFactory())
            {
                var competition = await workflow.Competitions.Include(c => c.Venue).Include(c => c.ReportTemplate.Logos).FirstOrDefaultAsync(c => c.Id == competitionId);
                if (competition == null)
                    throw new CompetitionNotFoundException();

                book.DocumentName = string.Format(Resources.CompetitorGroupTitle, competition.Name);

                ICompetitorGroupComparer comparer;
                switch (optionalColumns)
                {
                    case OptionalReportColumns.HomeVenueCode:
                        comparer = HomeVenueComparer.Default;
                        break;
                    case OptionalReportColumns.NationalityCode:
                        comparer = NationalityComparer.Default;
                        break;
                    case OptionalReportColumns.ClubShortName:
                        comparer = ClubComparer.Default;
                        break;
                    default:
                        comparer = LicenseKeyComparer.Default;
                        break;
                }

                var races = new List<RankedRace>();
                foreach (var distance in await workflow.Distances.Where(d => d.CompetitionId == competitionId).OrderBy(d => d.Number).ToListAsync())
                {
                    var distanceResult = await workflow.GetDistanceResultAsync(distance);
                    races.AddRange(distanceResult);
                }

                var groups = races.GroupBy(r => r.Race.Competitor, comparer.Group, comparer).SortAndRank().Where(g => g.TotalPoints > 0).ToList();

                book.Reports.Add(CreateRankingReport(competition, groups));
            }

            return new TelerikLoadedReport(book);
        }

        private Report CreateRankingReport(Competition competition, IList<CompetitorGroup> groups)
        {
            var report = new CompetitorGroupPointsRankingReport();
            report.AddGroups(groups);
            report.SetParameters(competition);
            return report;
        }
    }
}
