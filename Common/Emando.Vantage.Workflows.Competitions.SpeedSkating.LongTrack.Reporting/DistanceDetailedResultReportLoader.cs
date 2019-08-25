using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Emando.Vantage.Competitions;
using Emando.Vantage.Competitions.SpeedSkating.LongTrack;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Components.Competitions.SpeedSkating.LongTrack;
using Emando.Vantage.Workflows.Competitions.Reporting;
using Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting.Properties;
using Emando.Vantage.Workflows.Reporting;
using Emando.Vantage.Workflows.Reporting.TelerikReports;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    public class DistanceDetailedResultReportLoader : IDistanceReportLoader
    {
        private readonly Func<RacesWorkflow> workflowFactory;
        private readonly IDistanceDisciplineExpertManager expertManager;

        public DistanceDetailedResultReportLoader(Func<RacesWorkflow> workflowFactory, IDistanceDisciplineExpertManager expertManager)
        {
            this.workflowFactory = workflowFactory;
            this.expertManager = expertManager;
        }

        #region IDistanceReportLoader Members

        async Task<ILoadedReport> IDistanceReportLoader.LoadAsync(Guid competitionId, Guid distanceId, OptionalReportColumns optionalColumns)
        {
            using (var workflow = workflowFactory())
            {
                var report = await LoadAsync(workflow, competitionId, distanceId, expertManager, optionalColumns);
                return new TelerikLoadedReport(report);
            }
        }

        #endregion

        internal static async Task<DetailedResultReport> LoadAsync(RacesWorkflow workflow, Guid competitionId, Guid distanceId, IDistanceDisciplineExpertManager expertManager,
            OptionalReportColumns optionalColumns)
        {
            var distance = await workflow.Distances.Include(d => d.Competition.Venue).Include(d => d.Competition.ReportTemplate.Logos).FirstOrDefaultAsync(d => d.Id == distanceId);
            if (distance == null)
                throw new DistanceNotFoundException();

            var expert = expertManager.Find(distance.Discipline);
            if (expert == null)
                return null;

            var report = new DetailedResultReport();
            report.SetParameters(distance);
            report.ReportParameters["OptionalColumnHeader"].Value = Resources.ResourceManager.GetString($"OptionalColumn_{(int)optionalColumns}") ?? "";

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
                default:
                    report.InnerOptionalFieldTextBox.Value = null;
                    report.OuterOptionalFieldTextBox.Value = null;
                    break;
            }

            var races = await workflow.Races(competitionId).Include(r => r.Competitor)
                .Include(r => r.Results)
                .Include(r => r.Times)
                .Include(r => r.Laps)
                .Where(r => r.DistanceId == distanceId)
                .ToListAsync();
            races = races.Where(r => r.PresentedResult?.Status == RaceStatus.Done).ToList();

            var maxPair = races.Select(r => r.Heat).DefaultIfEmpty(0).Max();
            var pairs = new List<Pair>();
            for (var pair = distance.FirstHeat; pair <= maxPair; pair++)
            {
                var colors = PairsDistanceCalculator.Colors(distance, pair);
                var innerRace = races.SingleOrDefault(r => r.Heat == pair && r.Lane == 0);
                var innerRaceColor = (int)colors.ToLaneColor(Lane.Inner);
                var innerLaps = innerRace != null ? expert.Calculator.CalculateLaps(distance, innerRace.PresentedLaps.Select(t => t?.Time)) : null;
                var outerRace = races.SingleOrDefault(r => r.Heat == pair && r.Lane == 1);
                var outerRaceColor = (int)colors.ToLaneColor(Lane.Outer);
                var outerLaps = outerRace != null ? expert.Calculator.CalculateLaps(distance, outerRace.PresentedLaps.Select(t => t?.Time)) : null;

                pairs.Add(new Pair(pair, innerRace, innerRaceColor, innerLaps, outerRace, outerRaceColor, outerLaps));
            }

            report.Pairs = pairs;
            return report;
        }
    }
}