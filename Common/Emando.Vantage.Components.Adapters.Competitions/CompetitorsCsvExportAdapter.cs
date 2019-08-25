using System;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Emando.Vantage.Competitions;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Components.Adapters.Competitions
{
    [Adapter("Competitors (CSV)", 100)]
    public class CompetitorsCsvExportAdapter : ICompetitionExportAdapter
    {
        private static readonly Encoding Encoding = Encoding.GetEncoding(1252);
        private readonly Func<ICompetitionContext> contextFactory;

        public CompetitorsCsvExportAdapter(Func<ICompetitionContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        #region ICompetitionExportAdapter Members

        public string FileExtension => ".csv";

        public string MediaType => "text/csv";

        public async Task ExportAsync(Guid competitionId, Stream stream, CultureInfo culture)
        {
            var configuration = new CsvConfiguration
            {
                CultureInfo = culture,
            };

            using (var context = contextFactory())
            using (var writer = new StreamWriter(stream, Encoding))
            using (var csv = new CsvWriter(writer, configuration))
            {
                var competitors = await (from c in context.Competitors.OfType<PersonCompetitor>().Include(c => c.Person)
                                         where c.List.CompetitionId == competitionId && c.Status == CompetitorStatus.Confirmed
                                         orderby c.List.SortOrder, c.List.Name, c.StartNumber
                                         select c).ToListAsync();

                // Keep this in sync with RegistrantsCsvExportAdapter
                var projection = (from c in competitors
                                  select new
                                  {
                                      c.Category,
                                      c.StartNumber,
                                      c.LicenseKey,
                                      c.Name.Initials,
                                      c.Name.FirstName,
                                      c.Name.PrefixedSurname,
                                      c.FullName,
                                      c.ShortName,
                                      BirthDate = c.Person.BirthDate.ToString("d", culture),
                                      Gender = c.Person.Gender.ToLetter(),
                                      c.Person.Address.City,
                                      c.NationalityCode,
                                      c.ClubCode,
                                      c.ClubFullName,
                                      c.Sponsor,
                                      c.Transponder1,
                                      c.Transponder2
                                  }).ToList();

                csv.WriteHeader(projection.GetType().GetGenericArguments()[0]);
                foreach (var competitor in projection)
                    csv.WriteRecord(competitor);
            }
        }

        #endregion
    }
}