using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Competitions.SpeedSkating.LongTrack;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Components.Competitions.SpeedSkating.LongTrack;
using Emando.Vantage.Entities.Competitions;
using Emando.Vantage.Workflows.Competitions.Reporting;
using Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting.Properties;
using Emando.Vantage.Workflows.Reporting;
using Emando.Vantage.Workflows.Reporting.TelerikReports;
using Telerik.Reporting;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    public class DrawReportLoader<T> : IDistanceReportLoader
        where T : Report, IPairsDrawReport, new()
    {
        private readonly Func<ICompetitionContext> contextFactory;

        public DrawReportLoader(Func<ICompetitionContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        async Task<ILoadedReport> IDistanceReportLoader.LoadAsync(Guid competitionId, Guid distanceId, OptionalReportColumns optionalColumns)
        {
            using (var context = contextFactory())
            {
                var report = await LoadAsync(context, competitionId, distanceId, optionalColumns);
                return new TelerikLoadedReport(report);
            }
        }

        public static async Task<T> LoadAsync(ICompetitionContext context, Guid competitionId, Guid distanceId, OptionalReportColumns optionalColumns)
        {
            var report = new T();

            var distance = await context.Distances
                .Include(d => d.Competition.Venue)
                .Include(d => d.Competition.ReportTemplate.Logos)
                .FirstOrDefaultAsync(d => d.Id == distanceId);
            if (distance == null)
                throw new DistanceNotFoundException();

            report.SetParameters(distance);
            report.ReportParameters["OptionalColumnHeader"].Value = Resources.ResourceManager.GetString($"OptionalColumn_{(int)optionalColumns}") ?? "";

            var reportOptionalColumns = report as IPairsDrawReportWithOptionalColumn;
            if (reportOptionalColumns != null)
                switch (optionalColumns)
                {
                    case OptionalReportColumns.HomeVenueCode:
                        report.ReportParameters["InnerOptionalColumnField"].Value = "Inner.Competitor.VenueCode";
                        report.ReportParameters["OuterOptionalColumnField"].Value = "Outer.Competitor.VenueCode";
                        break;
                    case OptionalReportColumns.NationalityCode:
                        report.ReportParameters["InnerOptionalColumnField"].Value = "Inner.Competitor.NationalityCode";
                        report.ReportParameters["OuterOptionalColumnField"].Value = "Outer.Competitor.NationalityCode";
                        break;
                    case OptionalReportColumns.ClubShortName:
                        report.ReportParameters["InnerOptionalColumnField"].Value = "Inner.Competitor.ClubShortName";
                        report.ReportParameters["OuterOptionalColumnField"].Value = "Outer.Competitor.ClubShortName";
                        break;
                    case OptionalReportColumns.LicenseKey:
                        report.ReportParameters["InnerOptionalColumnField"].Value = "Inner.Competitor.LicenseKey";
                        report.ReportParameters["OuterOptionalColumnField"].Value = "Outer.Competitor.LicenseKey";
                        break;
                    case OptionalReportColumns.SeasonBest:
                        reportOptionalColumns.InnerOptionalFieldValue = "= SpeedSkating.LongTrack.FormatTime(Fields.Inner.SeasonBest, Parameters.TimeDigits.Value)";
                        reportOptionalColumns.OuterOptionalFieldValue = "= SpeedSkating.LongTrack.FormatTime(Fields.Outer.SeasonBest, Parameters.TimeDigits.Value)";
                        break;
                    default:
                        report.ReportParameters["OptionalColumnHeader"].Value = string.Empty;
                        reportOptionalColumns.InnerOptionalFieldValue = null;
                        reportOptionalColumns.OuterOptionalFieldValue = null;
                        break;
                }

            var races = await context.Races
                .Include(r => r.Competitor)
                .Include(r => r.Results)
                .Include(r => r.Times)
                .Where(r => r.DistanceId == distanceId)
                .ToListAsync();

            await context.Competitors.OfType<TeamCompetitor>()
                .Include(t => t.Members.Select(m => m.Member))
                .Where(tc => tc.Races.Any(r => r.DistanceId == distanceId))
                .LoadAsync();

            var maxPair = races.Select(r => r.Heat).DefaultIfEmpty(0).Max();
            var pairs = new List<Pair>();
            for (var pair = distance.FirstHeat; pair <= maxPair; pair++)
            {
                var colors = PairsDistanceCalculator.Colors(distance, pair);
                var innerRace = races.SingleOrDefault(r => r.Heat == pair && r.Lane == 0);
                var innerRaceColor = (int)colors.ToLaneColor(Lane.Inner);
                var outerRace = races.SingleOrDefault(r => r.Heat == pair && r.Lane == 1);
                var outerRaceColor = (int)colors.ToLaneColor(Lane.Outer);

                pairs.Add(new Pair(pair, innerRace, innerRaceColor, null, outerRace, outerRaceColor, null));
            }

            report.Pairs = pairs;
            return report;
        }
    }
}