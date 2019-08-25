using System;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CsvHelper;
using Emando.Vantage.Competitions;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Components.Adapters.Competitions.FinishLynx
{
    [Adapter("FinishLynx Database", 220)]
    public class FinishLynxExportAdapter : ICompetitionExportAdapter
    {
#if DEBUG
        private const string PeopleFileName = "lynx.ppl.txt";
        private const string EventFileName = "lynx.evt.txt";
        private const string ScheduleFileName = "lynx.sch.txt";
#else
        private const string PeopleFileName = "lynx.ppl";
        private const string EventFileName = "lynx.evt";
        private const string ScheduleFileName = "lynx.sch";
#endif

        private static readonly Encoding Encoding = Encoding.GetEncoding(1252);

        private readonly Func<ICompetitionContext> contextFactory;
        private readonly IDistanceDisciplineCalculatorManager calculatorManager;

        public FinishLynxExportAdapter(Func<ICompetitionContext> contextFactory, IDistanceDisciplineCalculatorManager calculatorManager)
        {
            this.contextFactory = contextFactory;
            this.calculatorManager = calculatorManager;
        }

        #region ICompetitionExportAdapter Members

        public string FileExtension => ".zip";

        public string MediaType => "application/zip";

        public async Task ExportAsync(Guid competitionId, Stream stream, CultureInfo culture)
        {
            // Work-around as per https://connect.microsoft.com/VisualStudio/feedback/details/816411/ziparchive-shouldnt-read-the-position-of-non-seekable-streams
            // TODO: Check if this is still necessary in .NET Framework 4.6
            if (!stream.CanSeek)
                stream = new NonSeekableStreamWrapper(stream);

            using (var context = contextFactory())
            using (var archive = new ZipArchive(stream, ZipArchiveMode.Create))
            {
                var pattern = $"[{Regex.Escape(new string(Path.GetInvalidPathChars()))}]";
                var removeInvalidChars = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.CultureInvariant);
                
                //ReSharper disable once AccessToDisposedClosure
                await ExportPeopleAsync(context, competitionId,
                    folder => archive.CreateEntry($"{removeInvalidChars.Replace(folder, "")}/{PeopleFileName}").Open(), culture);

                using (var eventStream = archive.CreateEntry(EventFileName).Open())
                    await ExportEventAsync(context, competitionId, eventStream, culture);

                using (var scheduleStream = archive.CreateEntry(ScheduleFileName).Open())
                    await ExportScheduleAsync(context, competitionId, scheduleStream, culture);
            }
        }

        #endregion

        private static async Task ExportPeopleAsync(ICompetitionContext context, Guid competitionId, Func<string, Stream> streamFactory, CultureInfo culture)
        {
            var lists = await (from c in context.Competitors.OfType<PersonCompetitor>()
                               where c.List.CompetitionId == competitionId && c.Status == CompetitorStatus.Confirmed
                               group c by new
                               {
                                   c.ListId,
                                   c.List.SortOrder,
                                   c.List.Name
                               }
                               into g
                               select new
                               {
                                   g.Key.SortOrder,
                                   g.Key.Name,
                                   Competitors = from c in g
                                                 select new
                                                 {
                                                     c.Id,
                                                     c.StartNumber,
                                                     c.LicenseKey,
                                                     c.Name,
                                                     c.ClubFullName,
                                                     c.NationalityCode
                                                 }
                               }).ToListAsync();

            var projection = from l in lists
                             orderby l.SortOrder, l.Name
                             select new
                             {
                                 l.Name,
                                 Competitors = from c in l.Competitors
                                               orderby c.StartNumber
                                               select new
                                               {
                                                   c.StartNumber,
                                                   c.Name.PrefixedSurname,
                                                   c.Name.FirstName,
                                                   c.ClubFullName,
                                                   c.Id,
                                                   c.LicenseKey,
                                                   c.NationalityCode
                                               }
                             };

            foreach (var list in projection)
                using (var writer = new StreamWriter(streamFactory(list.Name), Encoding))
                using (var csv = new CsvWriter(writer))
                {
                    writer.WriteLine("; {0}", list.Name);
                    foreach (var competitor in list.Competitors)
                        csv.WriteRecord(competitor);
                }
        }

        private async Task ExportEventAsync(ICompetitionContext context, Guid competitionId, Stream stream, CultureInfo culture)
        {
            var distances = await (from d in context.Distances
                                   where d.CompetitionId == competitionId
                                   orderby d.Number
                                   select new
                                   {
                                       Value = d,
                                       Races = (from r in d.Races
                                                orderby r.Round, r.Heat, r.Lane
                                                select new
                                                {
                                                    r.Round,
                                                    r.Heat,
                                                    r.Color,
                                                    r.Competitor.StartNumber
                                                }).ToList(),
                                       CompetitorCount = (from dc in d.Combinations
                                                          from c in dc.Competitors
                                                          select c).Distinct().Count()
                                   }).ToListAsync();

            using (var writer = new StreamWriter(stream, Encoding))
            using (var csv = new CsvWriter(writer))
                foreach (var distance in distances)
                    for (var round = 1; round <= distance.Value.Rounds; round++)
                        foreach (var heat in distance.Races.Where(r => r.Round == round).GroupBy(r => r.Heat).ToList())
                        {
                            csv.WriteField(distance.Value.Number);
                            csv.WriteField(round);
                            csv.WriteField(heat.Key);
                            csv.WriteField(distance.Value.Name);
                            csv.NextRecord();

                            foreach (var race in heat)
                            {
                                csv.WriteField("");
                                csv.WriteField(race.StartNumber);
                                csv.WriteField(race.Color + 1);
                                csv.NextRecord();
                            }
                        }
        }

        private async Task ExportScheduleAsync(ICompetitionContext context, Guid competitionId, Stream stream, CultureInfo culture)
        {
            var distances = await (from d in context.Distances.Include(r => r.Races)
                                   where d.CompetitionId == competitionId
                                   orderby d.Number
                                   select new
                                   {
                                       d.Number,
                                       d.Rounds,
                                       Races = d.Races.Select(r => new
                                       {
                                           r.Round,
                                           r.Heat
                                       }).Distinct()
                                   }).ToListAsync();

            using (var writer = new StreamWriter(stream, Encoding))
            using (var csv = new CsvWriter(writer))
                foreach (var distance in distances)
                    for (var round = 1; round <= distance.Rounds; round++)
                        foreach (var race in distance.Races.Where(r => r.Round == round).OrderBy(r => r.Heat).ToList())
                        {
                            csv.WriteField(distance.Number);
                            csv.WriteField(round);
                            csv.WriteField(race.Heat);
                            csv.NextRecord();
                        }
        }
    }
}