using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Emando.Vantage.Competitions;
using Emando.Vantage.Competitions.SpeedSkating.LongTrack;
using Emando.Vantage.Components.Adapters.Competitions;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Components.Adapters.KNSB
{
    [Adapter("CovasXml", 20)]
    public class CovasXmlCompetitionExportAdapter : ICompetitionExportAdapter
    {
        private const int FileFormat = 2;
        private readonly IDistanceDisciplineCalculatorManager calculatorManager;
        private readonly Func<ICompetitionContext> contextFactory;

        public CovasXmlCompetitionExportAdapter(Func<ICompetitionContext> contextFactory, IDistanceDisciplineCalculatorManager calculatorManager)
        {
            this.contextFactory = contextFactory;
            this.calculatorManager = calculatorManager;
        }

        #region ICompetitionExportAdapter Members

        public string FileExtension => ".xml";

        public string MediaType => "text/xml";

        public async Task ExportAsync(Guid competitionId, Stream stream, CultureInfo culture)
        {
            using (var context = contextFactory())
            using (var streamWriter = new StreamWriter(stream, Encoding.UTF8))
            {
                var competition = await context.Competitions
                    .Include(c => c.Venue)
                    .Include(c => c.Distances.Select(d => d.Races.Select(r => r.Results)))
                    .Include(c => c.Distances.Select(d => d.Races.Select(r => r.Times)))
                    .FirstOrDefaultAsync(c => c.Id == competitionId);
                if (competition == null)
                    throw new CompetitionNotFoundException();

                var competitors = await context.Competitors.OfType<PersonCompetitor>().Include(pc => pc.Person)
                    .Where(c => c.List.CompetitionId == competitionId)
                    .ToListAsync();

                var xml = new XDocument(new XElement("competition",
                    new XAttribute("fileformat", FileFormat),
                    XInfo(competition),
                    XIcerinks(competition),
                    XCompetitors(competitors),
                    XDistances(competition, competitors)));
                xml.Save(streamWriter);
            }
        }

        #endregion

        private static XElement XInfo(Competition competition)
        {
            /* <info>
                 <code>GR15-0321C</code>
                 <name>YVG Clubkampioenschappen Jeugd t/m JunC</name>
                 <revision>0</revision>
                 <personidtype>knsb.natrefnr</personidtype>
                 <timeunit>millisecond</timeunit>
                 <lengthunit>meter</lengthunit>
                 <startdate>2015-03-21</startdate>
                 <icerinkref id="GR"/>
                 <source>Export from SARA</source>
               </info> */

            return new XElement("info",
                new XElement("code", competition.Id.ToString("N").Substring(0, 10)),
                new XElement("name", competition.Name.RemoveDiacritics()),
                new XElement("revision", 0),
                new XElement("personidtype", "knsb.natrefnr"),
                new XElement("timeunit", "millisecond"),
                new XElement("lengthunit", "meter"),
                new XElement("startdate", competition.Starts.ToString("yyyy-MM-dd")),
                new XElement("icerinkref", new XAttribute("id", competition.VenueCode ?? "")),
                new XElement("source", "Export from Vantage"));
        }

        private static XElement XIcerinks(Competition competition)
        {
            /* <icerinks>
                 <icerink id="GR">
                     <name>Sportcentrum Kardinge</name>
                     <city>Groningen</city>
                     <country>NED</country>
                 </icerink>
                 ...
               </icerinks> */

            var icerinks = new XElement("icerinks");
            if (competition.Venue != null)
                icerinks.Add(new XElement("icerink",
                    new XAttribute("id", competition.VenueCode),
                    new XElement("name", competition.Venue.Name.RemoveDiacritics()),
                    new XElement("city", competition.Venue.Address.City.RemoveDiacritics()),
                    new XElement("country", competition.Venue.Address.CountryCode)));
            return icerinks;
        }

        private static XElement XCompetitors(IEnumerable<CompetitorBase> competitors)
        {
            return new XElement("competitors",
                competitors.OfType<PersonCompetitor>().Select((c, i) =>
                    new XElement("competitor",
                        new XAttribute("id", i + 1),
                        new XElement("person",
                            new XAttribute("id", c.LicenseKey),
                            XName(c.Name),
                            new XElement("gender", AsGender(c.Gender)),
                            new XElement("city", c.From),
                            new XElement("dateofbirth", c.Person.BirthDate.ToString("yyyy-MM-dd")),
                            new XElement("nationality", c.NationalityCode),
                            new XElement("clubref", new XAttribute("code", c.ClubShortName ?? "INT"), c.ClubFullName ?? "Internationaal")),
                        new XElement("categoryref", new XAttribute("code", c.Category)),
                        new XElement("clubref", new XAttribute("id", c.ClubCode ?? 500)),
                        new XElement("class", c.Class ?? 0))));
        }

        private XElement XDistances(Competition competition, IList<PersonCompetitor> competitors)
        {
            /* <distances>
                 <distance id="1">
                   <length>1000</length>
                   <description>1000 meter</description>
                   <date>2015-03-29</date>
                   <icerinkref id="DH"/>
                   <timeinfo source="electronic" precision="2"/>
                   <races>
                     <race nr="1" track="inner">
                       <competitorref id="44"/>
                       <finaltime value="112200" points="56100"/>
                     </race>
                     ... */

            return new XElement("distances",
                from d in competition.Distances
                where d.Discipline.StartsWith("SpeedSkating.LongTrack.PairsDistance")
                orderby d.Number
                let calculator = calculatorManager.Get(d.Discipline)
                let precision = calculator.DefaultResultPrecision ?? TimeSpan.Zero
                let precisionDigits = precision == TimeSpan.FromTicks(10) ? 2 : 3
                select new XElement("distance",
                    new XAttribute("id", d.Number),
                    new XElement("length", calculator.Length(d)),
                    new XElement("description", d.Name),
                    new XElement("date", (d.Starts ?? competition.Starts).ToString("yyyy-MM-dd")),
                    new XElement("icerinkref", new XAttribute("id", competition.VenueCode ?? "")),
                    new XElement("timeinfo", new XAttribute("source", "electronic"), new XAttribute("precision", precisionDigits)),
                    new XElement("races",
                        from r in d.Races
                        where r.PresentedResult != null && r.PresentedResult.Status == RaceStatus.Done
                        orderby r.Round, r.Heat, r.Lane
                        let competitorId = competitors.IndexOf(r.Competitor as PersonCompetitor)
                        where competitorId > -1
                        select new XElement("race",
                            new XAttribute("nr", r.Heat),
                            new XAttribute("track", XTrack((Lane)r.Lane)),
                            new XElement("competitorref", new XAttribute("id", competitorId + 1)),
                            XFinalTime(r, precision),
                            XRaceAttributes(r)))));
        }

        private static XElement XFinalTime(Race race, TimeSpan timePrecision)
        {
            if (race.PresentedTime == null)
                return null;

            return new XElement("finaltime", new XAttribute("value", (int)race.PresentedTime.Time.Truncate(timePrecision).TotalMilliseconds));
        }

        private static IEnumerable<XElement> XRaceAttributes(Race race)
        {
            if (race.PresentedResult.TimeInvalidReason.HasValue)
            {
                string code;
                switch (race.PresentedResult.TimeInvalidReason.Value)
                {
                    case TimeInvalidReason.NotStarted:
                        code = "notstarted";
                        break;
                    case TimeInvalidReason.NotFinished:
                        code = "nofinish";
                        break;
                    case TimeInvalidReason.Disqualified:
                        code = "disqualified";
                        break;
                    case TimeInvalidReason.Withdrawn:
                        code = "withdrawn";
                        break;
                    default:
                        code = "nofinish";
                        break;
                }
                yield return new XElement("attribute", new XAttribute("value", code));
                yield break;
            }

            if (race.PresentedTime != null)
            {
                if (race.PresentedTime.TimeInfo.HasFlag(TimeInfo.Fall))
                    yield return new XElement("attribute", new XAttribute("value", "fall"));

                if (race.PresentedTime.TimeInfo.HasFlag(TimeInfo.Restart))
                    yield return new XElement("attribute", new XAttribute("value", "reskate"));

                if (race.PresentedTime.TimeInfo.HasFlag(TimeInfo.OutOfCompetition))
                    yield return new XElement("attribute", new XAttribute("value", "noranking"));
            }
        }

        private static XElement XName(Name name)
        {
            var element = new XElement("name");
            if (!string.IsNullOrEmpty(name.FirstName))
                element.Add(new XAttribute("first", name.FirstName.RemoveDiacritics()));
            if (!string.IsNullOrEmpty(name.SurnamePrefix))
                element.Add(new XAttribute("prefix", name.SurnamePrefix.RemoveDiacritics()));
            if (!string.IsNullOrEmpty(name.Surname))
                element.Add(new XAttribute("last", name.Surname.RemoveDiacritics()));
            return element;
        }

        private string XTrack(Lane lane)
        {
            switch (lane)
            {
                case Lane.Inner:
                    return "inner";
                case Lane.Outer:
                    return "outer";
                default:
                    throw new ArgumentOutOfRangeException(nameof(lane), lane, null);
            }
        }

        private static string AsGender(Gender gender)
        {
            switch (gender)
            {
                case Gender.Male:
                    return "male";
                case Gender.Female:
                    return "female";
                default:
                    throw new ArgumentOutOfRangeException(nameof(gender), gender, null);
            }
        }
    }
}