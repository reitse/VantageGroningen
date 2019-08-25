using System;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Emando.Vantage.Competitions;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Components.Adapters.Competitions.Mylaps
{
    [Adapter("MYLAPS Orbits Groups and Runs File", 210)]
    public class MylapsOrbitsExportAdapter : ICompetitionExportAdapter
    {
        private readonly IDisciplineCalculatorManager calculatorManager;
        private readonly IDistanceDisciplineCalculatorManager distanceCalculatorManager;
        private readonly Func<ICompetitionContext> contextFactory;

        public MylapsOrbitsExportAdapter(Func<ICompetitionContext> contextFactory, IDisciplineCalculatorManager calculatorManager,
            IDistanceDisciplineCalculatorManager distanceCalculatorManager)
        {
            this.contextFactory = contextFactory;
            this.distanceCalculatorManager = distanceCalculatorManager;
            this.calculatorManager = calculatorManager;
        }

        #region ICompetitionExportAdapter Members

        public string FileExtension => ".xml";

        public string MediaType => "text/xml";

        public async Task ExportAsync(Guid competitionId, Stream stream, CultureInfo culture)
        {
            using (var context = contextFactory())
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {
                var competition = await context.Competitions.Where(c => c.Id == competitionId).FirstOrDefaultAsync();
                if (competition == null)
                    return;

                var calculator = calculatorManager.Find(competition.Discipline);
                if (calculator == null)
                    return;

                switch (calculator.PrimaryGroup)
                {
                    case PrimaryGroup.Distances:
                        await ExportDistancesAsync(competition, writer, culture, context);
                        break;
                    case PrimaryGroup.DistanceCombinations:
                        await ExportDistanceCombinationsAsync(competition, writer, culture, context);
                        break;
                }
            }
        }

        public static DateTime InTimeZone(DateTime input, string timeZoneId)
        {
            if (timeZoneId == null)
                return input;

            TimeZoneInfo timeZone;
            try
            {
                timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            }
            catch (TimeZoneNotFoundException)
            {
                return input;
            }
            catch (InvalidTimeZoneException)
            {
                return input;
            }

            if (input.Kind == DateTimeKind.Local)
                input = input.ToUniversalTime();

            return TimeZoneInfo.ConvertTimeFromUtc(input, timeZone);
        }

        private async Task ExportDistancesAsync(Competition competition, TextWriter writer, CultureInfo culture, ICompetitionContext context)
        {
            var distances = await context.Distances
                .Include(d => d.Races.Select(r => r.Competitor))
                .Include(d => d.Races.Select(r => r.Transponders.Select(t => t.Transponder)))
                .Where(d => d.CompetitionId == competition.Id)
                .ToListAsync();

            var groups = new XElement("groups");
            foreach (var distance in distances.OrderBy(dc => dc.Number))
            {
                var calculator = distanceCalculatorManager.Get(distance.Discipline);

                var group = new XElement("group",
                    new XAttribute("name", $"{distance.Number:00}. {distance.Name}"),
                    new XElement("competitors", 
                    from r in distance.Races
                    let personCompetitor = r.Competitor as PersonCompetitor
                    where personCompetitor != null
                    orderby personCompetitor.StartNumber
                    select new XElement("competitor",
                        new XAttribute("no", personCompetitor.StartNumber),
                        new XAttribute("driverregistration", personCompetitor.LicenseKey),
                        new XAttribute("registration", personCompetitor.LicenseKey),
                        new XAttribute("class", personCompetitor.Category),
                        new XAttribute("firstname", personCompetitor.Name.FirstName ?? ""),
                        new XAttribute("lastname", personCompetitor.Name.PrefixedSurname ?? ""),
                        new XAttribute("transponders", string.Join(",", r.Transponders.Select(t => t.Transponder.Label))),
                        new XAttribute("additional1", personCompetitor.NationalityCode),
                        new XAttribute("additional2", personCompetitor.Sponsor ?? ""),
                        new XAttribute("additional3", personCompetitor.ShortName),
                        new XAttribute("additional4", personCompetitor.From ?? ""),
                        new XAttribute("additional5", personCompetitor.ClubFullName ?? ""))));

                var starts = InTimeZone(distance.Starts ?? competition.Starts, competition.TimeZone);

                var runs = new XElement("runs");
                foreach (var heat in distance.Races.GroupBy(r => new { r.Round, r.Heat }).OrderBy(g => g.Key.Round).ThenBy(g => g.Key.Heat))
                {
                    var minimumLapTime = calculator.MininumLapTime(distance);
                    var run = new XElement("run",
                        new XAttribute("name", $"{heat.Key.Round:00}.{heat.Key.Heat:000}"),
                        new XAttribute("shortname", $"{heat.Key.Round:00}.{heat.Key.Heat:000}"),
                        new XAttribute("date", starts.ToString("yyyy-MM-dd")),
                        new XAttribute("time", starts.ToString("HH:mm:ss")),
                        new XAttribute("type", "race"),
                        new XAttribute("countfirst", "lap"),
                        new XAttribute("minimumlaptime", minimumLapTime.ToString(@"h\:mm\:ss\.fff")),
                        new XAttribute("copygroupcompetitor", true),
                        new XElement("competitors",
                            from r in heat
                            let personCompetitor = r.Competitor as PersonCompetitor
                            where personCompetitor != null
                            orderby r.Lane
                            select new XElement("competitor",
                                new XAttribute("no", personCompetitor.StartNumber),
                                new XAttribute("driverregistration", personCompetitor.LicenseKey),
                                new XAttribute("registration", personCompetitor.LicenseKey),
                                new XAttribute("class", personCompetitor.Category),
                                new XAttribute("firstname", personCompetitor.Name.FirstName ?? ""),
                                new XAttribute("lastname", personCompetitor.Name.PrefixedSurname ?? ""),
                                new XAttribute("transponders", string.Join(",", r.Transponders.Select(t => t.Transponder.Label))),
                                new XAttribute("additional1", personCompetitor.NationalityCode),
                                new XAttribute("additional2", personCompetitor.Sponsor ?? ""),
                                new XAttribute("additional3", personCompetitor.ShortName),
                                new XAttribute("additional4", personCompetitor.From ?? ""),
                                new XAttribute("additional5", personCompetitor.ClubFullName ?? ""))));

                    switch (calculator.HeatStartEvent)
                    {
                        case HeatStartEvent.Start:
                            run.Add(new XAttribute("startmethod", "flag"));
                            break;
                        case HeatStartEvent.FirstPassing:
                            run.Add(new XAttribute("startmethod", "firstpassing"));
                            break;
                    }

                    switch (distance.ValueQuantity)
                    {
                        case DistanceValueQuantity.Length:
                        case DistanceValueQuantity.Count:
                            run.Add(new XAttribute("autofinishmethod", "individualonlaps"));
                            run.Add(new XAttribute("autofinishlaps", calculator.Laps(distance)));
                            break;
                    }

                    runs.Add(run);
                }

                group.Add(runs);
                groups.Add(group);
            }

            var document = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), groups);
            document.Save(writer);
        }

        private async Task ExportDistanceCombinationsAsync(Competition competition, TextWriter writer, CultureInfo culture, ICompetitionContext context)
        {
            var distanceCombinations = await (from dc in context.DistanceCombinations
                                              where dc.CompetitionId == competition.Id
                                              let competitors = from dcc in dc.Competitors
                                                                where dcc.Status == DistanceCombinationCompetitorStatus.Confirmed
                                                                select dcc.Competitor
                                              select new
                                              {
                                                  dc.Number,
                                                  dc.Name,
                                                  dc.Competition.Starts,
                                                  Competitors = competitors.OfType<PersonCompetitor>(),
                                                  dc.Distances
                                              }).ToListAsync();

            var groups = new XElement("groups");
            foreach (var distanceCombination in distanceCombinations.OrderBy(dc => dc.Number))
            {
                var group = new XElement("group", new XAttribute("name", $"{distanceCombination.Number:00}. {distanceCombination.Name}"), new XElement("competitors", from c in distanceCombination.Competitors.OrderBy(c => c.StartNumber)
                                                                                                                                                                      select new XElement("competitor", new XAttribute("no", c.StartNumber), new XAttribute("driverregistration", c.LicenseKey), new XAttribute("registration", c.LicenseKey), new XAttribute("class", c.Category), new XAttribute("firstname", c.Name.FirstName ?? ""), new XAttribute("lastname", c.Name.PrefixedSurname ?? ""), new XAttribute("transponders", string.Join(", ", new[] { c.Transponder1, c.Transponder2 }.Where(t => !string.IsNullOrWhiteSpace(t)))), new XAttribute("additional1", c.NationalityCode), new XAttribute("additional2", c.Sponsor ?? ""), new XAttribute("additional3", c.ShortName), new XAttribute("additional4", c.From ?? ""), new XAttribute("additional5", c.ClubFullName ?? ""))));

                var runs = new XElement("runs");
                foreach (var distance in distanceCombination.Distances.OrderBy(d => d.Number))
                {
                    var calculator = distanceCalculatorManager.Get(distance.Discipline);
                    var starts = distance.Starts ?? distanceCombination.Starts;
                    var minimumLapTime = calculator.MininumLapTime(distance);
                    for (var round = 1; round <= distance.Rounds; round++)
                    {
                        var heatCount = calculator.HeatsInRound(distance, round);
                        for (var heat = 1; heat <= heatCount; heat++)
                        {
                            var run = new XElement("run", new XAttribute("name", $"{distance.Number:00}. {calculator.DistanceHeatName(distance, round, heat, heatCount, culture)}"), new XAttribute("shortname", calculator.HeatName(distance, round, heat, culture)), new XAttribute("date", starts.ToString("yyyy-MM-dd")), new XAttribute("time", starts.ToString("HH:mm:ss")), new XAttribute("type", "race"), new XAttribute("countfirst", "lap"), new XAttribute("minimumlaptime", minimumLapTime.ToString(@"h\:mm\:ss\.fff")));

                            switch (calculator.HeatStartEvent)
                            {
                                case HeatStartEvent.Start:
                                    run.Add(new XAttribute("startmethod", "flag"));
                                    break;
                                case HeatStartEvent.FirstPassing:
                                    run.Add(new XAttribute("startmethod", "firstpassing"));
                                    break;
                            }

                            switch (distance.ValueQuantity)
                            {
                                case DistanceValueQuantity.Length:
                                case DistanceValueQuantity.Count:
                                    run.Add(new XAttribute("autofinishmethod", "individualonlaps"));
                                    run.Add(new XAttribute("autofinishlaps", calculator.Laps(distance)));
                                    break;
                            }

                            runs.Add(run);
                        }
                        starts += calculator.EstimatedRoundDuration(distance, round);
                    }
                }

                @group.Add(runs);
                groups.Add(@group);
            }

            var document = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), groups);
            document.Save(writer);
        }

        #endregion
    }
}