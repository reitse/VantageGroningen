using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace Emando.Vantage.Components.Adapters
{
    [Adapter("Licensees (CSV)", 100)]
    public class LicenseesCsvExportAdapter : ILicenseesExportAdapter
    {
        private static readonly Encoding Encoding = Encoding.GetEncoding(1252);
        private readonly Func<IVantageContext> contextFactory;

        public LicenseesCsvExportAdapter(Func<IVantageContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        #region ILicenseesExportAdapter Members

        public string FileExtension => ".csv";

        public string MediaType => "text/csv";

        public Task SaveToStreamAsync(string issuerId, string discipline, string category, bool includeDetails, Stream stream)
        {
            using (var context = contextFactory())
            using (var writer = new StreamWriter(stream, Encoding))
            using (var csv = new CsvWriter(writer))
            {
                csv.Configuration.Delimiter = ";";

                var query = from pl in context.PersonLicenses.Include(l => l.Person).Include(l => l.Club)
                            where (pl.Flags & PersonLicenseFlags.DisposableLicense) != PersonLicenseFlags.DisposableLicense
                            select pl;

                if (issuerId != null)
                {
                    query = query.Where(l => l.IssuerId == issuerId);
                    if (discipline != null)
                    {
                        query = query.Where(l => l.Discipline == discipline);
                        if (category != null)
                            query = query.Where(l => l.Category == category);
                    }
                }

                var count = 0;
                foreach (var l in query.OrderBy(l => l.Person.Name.Surname).ThenBy(l => l.Person.Name.FirstName))
                {
                    var record = new
                    {
                        l.IssuerId,
                        l.Discipline,
                        l.Category,
                        l.Number,
                        l.Key,
                        l.Flags,
                        l.ValidFrom,
                        l.ValidTo,
                        l.Person.Name.FirstName,
                        l.Person.Name.SurnamePrefix,
                        l.Person.Name.Surname,
                        Name = l.Person.Name.ToString(),
                        BirthDate = l.Person.BirthDate.ToString("d"),
                        Gender = l.Person.Gender.ToLetter(),
                        AddressLine1 = includeDetails ? l.Person.Address.Line1 : null,
                        AddressLine2 = includeDetails ? l.Person.Address.Line2 : null,
                        PostalCode = includeDetails ? l.Person.Address.PostalCode : null,
                        AddressCity = l.Person.Address.City,
                        Phone = includeDetails ? l.Person.Phone : null,
                        Email = includeDetails ? l.Person.Email : null,
                        l.Person.NationalityCode,
                        l.ClubCode,
                        ClubFullName = l.Club?.FullName,
                        l.Sponsor,
                        Venue = l.VenueCode,
                        l.Transponder1,
                        l.Transponder2,
                        Iban = includeDetails ? l.Person.Iban : null
                    };

                    if (count == 0)
                        csv.WriteHeader(record.GetType());

                    csv.WriteRecord(record);
                    count++;
                }

                return Task.FromResult<object>(null);
            }
        }

        #endregion
    }
}