using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Emando.Vantage.Components.Adapters.KNSB.Properties;
using Emando.Vantage.Entities;

namespace Emando.Vantage.Components.Adapters.KNSB
{
    [Adapter("KNSB People Details")]
    public class KnsbPeopleDetailsFileAdapter : IPeopleImportAdapter
    {
        private static readonly Encoding Encoding = Encoding.GetEncoding(1252);
        private readonly Func<IVantageContext> contextFactory; 

        private readonly CsvConfiguration configuration = new CsvConfiguration
        {
            HasHeaderRecord = false
        };

        public KnsbPeopleDetailsFileAdapter(Func<IVantageContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        #region IPeopleAdapter Members

        public async Task<ICollection<Person>> LoadFromStreamAsync(Stream stream)
        {
            var persons = new List<Person>();
            using (var reader = new StreamReader(stream, Encoding))
            using (var csv = new CsvReader(reader, configuration))
            {
                while (csv.Read())
                {
                    if (csv.CurrentRecord.Length < 22)
                        throw new FormatException(string.Format(Resources.TooFewFields, 22, csv.Row));

                    string key = csv.GetField(15);

                    DateTime birthDate;
                    if (!DateTime.TryParseExact(csv.GetField(12), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate))
                        throw new FormatException(string.Format(Resources.InvalidBirthDate, csv.GetField(12), csv.Row));

                    Gender gender;
                    switch (csv.GetField(13))
                    {
                        case "M":
                            gender = Gender.Male;
                            break;
                        case "F":
                            gender = Gender.Female;
                            break;
                        default:
                            throw new FormatException(string.Format(Resources.InvalidGender, csv.GetField(13), csv.Row));
                    }

                    using (var context = contextFactory())
                    {
                        var person = await (from p in context.Persons.Include(p => p.Licenses)
                                            where p.Licenses.Any(pl => pl.IssuerId == LongTrackLicenses.IssuerId && pl.Discipline == LongTrackLicenses.Discipline && pl.Key == key)
                                            select p).FirstOrDefaultAsync();

                        if (person == null)
                        {
                            person = new Person
                            {
                                Id = Guid.NewGuid(),
                                Licenses = new Collection<PersonLicense>
                                {
                                    new PersonLicense
                                    {
                                        Discipline = LongTrackLicenses.Discipline,
                                        IssuerId = LongTrackLicenses.IssuerId,
                                        Key = key
                                    }
                                }
                            };
                            context.Persons.Add(person);
                        }

                        person.Name.FirstName = csv.GetField(4);
                        person.Name.SurnamePrefix = !string.IsNullOrWhiteSpace(csv.GetField(2)) ? csv.GetField(2) : null;
                        person.Name.Surname = csv.GetField(3);
                        person.BirthDate = birthDate;
                        person.Gender = gender;
                        person.NationalityCode = csv.GetField(17);

                        var license = person.Licenses.Single();
                        license.ValidFrom = LongTrackLicenses.ValidFrom(DateTime.Today);
                        license.ValidTo = LongTrackLicenses.ValidTo(DateTime.Today);
                        license.Category = LongTrackLicenses.Category(gender, birthDate, DateTime.Today);

                        persons.Add(person);

                        await context.SaveChangesAsync();
                    }
                }
            }

            return persons;
        }

        #endregion
    }
}