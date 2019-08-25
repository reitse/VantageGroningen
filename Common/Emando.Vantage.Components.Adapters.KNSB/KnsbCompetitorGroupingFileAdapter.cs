using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Emando.Vantage.Competitions;
using Emando.Vantage.Components.Adapters.Competitions;
using Emando.Vantage.Components.Adapters.KNSB.Properties;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Components.Adapters.KNSB
{
    [Adapter("KNSB Competitor Groupings")]
    public class KnsbCompetitorGroupingFileAdapter : IPersonCompetitorsImportAdapter
    {
        private static readonly Encoding Encoding = Encoding.GetEncoding(1252);

        private readonly CsvConfiguration configuration = new CsvConfiguration
        {
            HasHeaderRecord = false,
            Delimiter = ";"
        };

        private readonly Func<ICompetitionContext> contextFactory;

        public KnsbCompetitorGroupingFileAdapter(Func<ICompetitionContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        #region IPersonCompetitorsImportAdapter Members

        public async Task<ICollection<PersonCompetitor>> LoadFromStreamAsync(Guid competitionId, Guid listId, Stream stream)
        {
            using (var context = contextFactory())
            {
                var distanceCombinations = await (from dc in context.DistanceCombinations
                                                  where dc.CompetitionId == competitionId
                                                  select dc).ToDictionaryAsync(dc => dc.Number, dc => dc.Id);

                using (var transaction = context.BeginTransaction(IsolationLevel.RepeatableRead))
                using (var reader = new StreamReader(stream, Encoding))
                using (var csv = new CsvReader(reader, configuration))
                    try
                    {
                        var competitors = new List<PersonCompetitor>();
                        while (csv.Read())
                        {
                            if (csv.CurrentRecord.Length < 3)
                                throw new FormatException(string.Format(Resources.TooFewFields, 8, csv.Row));

                            var key = csv.GetField(0);
                            var license = await context.PersonLicenses.Include(pl => pl.Club)
                                .Include(pl => pl.Person)
                                .FirstOrDefaultAsync(pl => pl.IssuerId == LongTrackLicenses.IssuerId && pl.Discipline == LongTrackLicenses.Discipline && pl.Key == key);
                            if (license == null)
                                throw new PersonNotFoundException(string.Format(Resources.PersonLicenseKeyNotFound, LongTrackLicenses.IssuerId, LongTrackLicenses.Discipline,
                                    key));

                            var combinations = csv.GetField(1).Split(new[] { '+' }, StringSplitOptions.RemoveEmptyEntries).Select(g => int.Parse(g.Trim()));

                            int startNumber;
                            if (!int.TryParse(csv.GetField(2), NumberStyles.None, CultureInfo.InvariantCulture, out startNumber))
                                throw new FormatException(string.Format(Resources.InvalidStartNumber, csv.GetField(2), csv.Row));

                            var category = csv.CurrentRecord.Length >= 4 ? csv.GetField(3) : license.Category;
                            var name = csv.CurrentRecord.Length >= 9 ? new Name(null, csv.GetField(6), csv.GetField(7), csv.GetField(8)) : license.Person.Name;
                            var shortName = csv.CurrentRecord.Length >= 5 ? csv.GetField(4) : name.ToInitialNameString();

                            var competitor = new PersonCompetitor
                            {
                                Id = Guid.NewGuid(),
                                EntityId = license.PersonId,
                                ListId = listId,
                                PersonId = license.PersonId,
                                Name = name,
                                ShortName = shortName,
                                LicenseDiscipline = LongTrackLicenses.Discipline,
                                LicenseKey = license.Key,
                                LicenseFlags = license.Flags,
                                Gender = license.Person.Gender,
                                Status = CompetitorStatus.Confirmed,
                                Category = category,
                                ClubCountryCode = license.Club?.CountryCode,
                                ClubCode = license.Club?.Code,
                                ClubShortName = license.Club?.ShortName,
                                ClubFullName = license.Club?.FullName,
                                From = license.Person.Address.City,
                                StartNumber = startNumber,
                                NationalityCode = license.Person.NationalityCode,
                                VenueCode = license.VenueCode,
                                Source = CompetitorSource.Manual,
                                Added = DateTime.UtcNow
                            };
                            context.Competitors.Add(competitor);

                            foreach (var combination in combinations.Where(combination => distanceCombinations.ContainsKey(combination)))
                                context.DistanceCombinationCompetitors.Add(new DistanceCombinationCompetitor
                                {
                                    Competitor = competitor,
                                    DistanceCombinationId = distanceCombinations[combination],
                                    Status = DistanceCombinationCompetitorStatus.Confirmed
                                });

                            competitors.Add(competitor);
                            await context.SaveChangesAsync();
                        }

                        transaction.Commit();
                        return competitors;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
            }
        }

        #endregion
    }
}