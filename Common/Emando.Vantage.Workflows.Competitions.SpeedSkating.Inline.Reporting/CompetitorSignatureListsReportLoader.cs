using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Competitions;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;
using Emando.Vantage.Workflows.Competitions.Reporting;
using Emando.Vantage.Workflows.Competitions.SpeedSkating.Inline.Reporting.Properties;
using Emando.Vantage.Workflows.Reporting;
using Emando.Vantage.Workflows.Reporting.TelerikReports;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.Inline.Reporting
{
    public class CompetitorSignatureListsReportLoader : ICompetitionReportLoader
    {
        private readonly Func<ICompetitionContext> contextFactory;

        public CompetitorSignatureListsReportLoader(Func<ICompetitionContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        #region ICompetitionReportLoader Members

        public async Task<ILoadedReport> LoadAsync(Guid competitionId, OptionalReportColumns optionalColumns)
        {
            var report = new CompetitorSignatureListsReport();
            using (var context = contextFactory())
            {
                var competition = await (from c in context.Competitions
                                         where c.Id == competitionId
                                         select new
                                         {
                                             c.Name,
                                             c.Culture
                                         }).SingleAsync();
                
                report.CompetitionName = competition.Name;
                report.DocumentName = string.Format(Resources.CompetitorSignatureListTitle, competition.Name);
                report.Culture = CultureInfo.GetCultureInfo(competition.Culture ?? "");

                report.DistanceCombinations = (await (from dc in context.DistanceCombinations
                                                    where dc.CompetitionId == competitionId
                                                    orderby dc.Number
                                                    select new
                                                    {
                                                        dc.Number,
                                                        dc.Name,
                                                        Competitors = from c in dc.Competitors
                                                                    let pc = c.Competitor as PersonCompetitor
                                                                    where pc != null && c.Status == DistanceCombinationCompetitorStatus.Confirmed
                                                                    orderby c.Reserve, pc.StartNumber
                                                                    select new
                                                                    {
                                                                        pc.StartNumber,
                                                                        pc.From,
                                                                        pc.Name,
                                                                        pc.Category,
                                                                        pc.LicenseKey,
                                                                        c.Reserve,
                                                                        pc.Status,
                                                                        pc.Transponder1,
                                                                        pc.Sponsor
                                                                    }
                                                    }).ToListAsync()).Where(dc => dc.Competitors.Any());
            }

            return new TelerikLoadedReport(report);
        }

        #endregion
    }
}