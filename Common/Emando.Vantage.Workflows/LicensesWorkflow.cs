using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Components;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities;
using Emando.Vantage.Workflows.Events;

namespace Emando.Vantage.Workflows
{
    public class LicensesWorkflow : IDisposable
    {
        private readonly IVantageContext context;
        private readonly IDisciplineCalculatorManager calculatorManager;
        private readonly IEventRecorder recorder;
        private bool isDisposed;

        public LicensesWorkflow(IVantageContext context, IDisciplineCalculatorManager calculatorManager, IEventRecorder recorder)
        {
            this.context = context;
            this.calculatorManager = calculatorManager;
            this.recorder = recorder;
        }

        public IQueryable<LicenseIssuer> Issuers => context.LicenseIssuers;

        public IQueryable<Club> Clubs => context.Clubs;

        public IQueryable<Person> People => context.Persons;

        public IQueryable<PersonLicense> PersonLicenses => context.PersonLicenses;

        public IQueryable<PersonLicensePrice> PersonLicensePrices => context.PersonLicensePrices;

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                    context.Dispose();

                isDisposed = true;
            }
        }

        ~LicensesWorkflow()
        {
            Dispose(false);
        }

        public async Task<PersonLicensePrice> GetPersonLicensePriceAsync(string issuerId, string discipline, Gender gender, int age, PersonLicenseFlags flags = PersonLicenseFlags.None)
        {
            flags &= PersonLicenseFlags.PriceFlags;
            return await (from p in context.PersonLicensePrices
                          where p.LicenseIssuerId == issuerId
                              && p.Discipline == discipline
                              && (p.Flags & PersonLicenseFlags.PriceFlags) == flags
                              && p.FromAge <= age
                              && p.ToAge >= age
                          select p).FirstOrDefaultAsync();
        }

        public async Task<PersonCategory> GetDefaultCategoryAsync(string issuerId, string discipline, Gender gender, int age)
        {
            return await (from c in context.PersonCategories
                          where c.LicenseIssuerId == issuerId
                              && c.Discipline == discipline
                              && c.Gender == gender
                              && age >= c.FromAge && age <= c.ToAge
                              && (c.Flags & PersonCategoryFlags.PerformanceDependent) == 0
                          select c).FirstOrDefaultAsync();
        }

        public Task<PersonCategory> GetDefaultCategoryAsync(string issuerId, string discipline, Gender gender, DateTime birthDate, DateTime? reference = null)
        {
            var expert = calculatorManager.Find(discipline);
            if (expert == null)
                throw new InvalidDisciplineException();

            var season = reference.HasValue ? expert.Season(reference.Value) : expert.CurrentSeason;
            var age = expert.SeasonAge(season, birthDate);
            return GetDefaultCategoryAsync(issuerId, discipline, gender, age);
        }

        public async Task<PersonLicense> AddNewLicenseAsync(string issuerId, string discipline, string key, Person person, int? season, DateTime? validFrom, DateTime? validTo,
            PersonLicenseFlags flags, string category = null, int? number = null, string sponsor = null, Club club = null, string venueCode = null,
            string transponder1 = null, string transponder2 = null)
        {
            var expert = calculatorManager.Find(discipline);
            if (expert == null)
                throw new InvalidDisciplineException();

            season = season ?? expert.CurrentSeason;
            validFrom = validFrom ?? expert.SeasonStarts(season.Value);
            validTo = validTo ?? expert.SeasonEnds(season.Value);

            if (category == null)
            {
                var age = expert.SeasonAge(season.Value, person.BirthDate);
                var personCategory = await GetDefaultCategoryAsync(issuerId, discipline, person.Gender, age);
                category = personCategory?.Code;
            }

            if (venueCode == null && club != null)
            {
                await context.LoadAsync(club, c => c.Venues, c => c.VenueDiscipline == discipline);
                venueCode = club.Venues?.FirstOrDefault(v => v.VenueDiscipline == discipline)?.VenueCode;
            }

            var license = new PersonLicense
            {
                IssuerId = issuerId,
                Discipline = discipline,
                Key = key,
                Season = season.Value,
                ValidFrom = validFrom.Value,
                ValidTo = validTo.Value,
                Sponsor = sponsor,
                Club = club,
                Person = person,
                Category = category,
                Number = number,
                Flags = flags,
                VenueCode = venueCode,
                Transponder1 = transponder1,
                Transponder2 = transponder2
            };

            context.PersonLicenses.Add(license);
            await context.SaveChangesAsync();

            recorder.RecordEvent(new PersonLicenseChangedEvent(license));
            return license;
        }

        public async Task<PersonLicense> AddNewTemporaryLicenseAsync(string issuerId, string discipline, Person person, int season, DateTime validFrom,
            DateTime validTo, PersonLicenseFlags flags = PersonLicenseFlags.None)
        {
            var expert = calculatorManager.Find(discipline);
            if (expert == null)
                throw new InvalidDisciplineException();

            flags |= PersonLicenseFlags.TemporaryLicense;

            var key = await GenerateTemporaryLicenseKeyAsync(issuerId);
            return await AddNewLicenseAsync(issuerId, discipline, key, person, season, validFrom, validTo, flags);
        }

        private async Task<string> GenerateTemporaryLicenseKeyAsync(string issuerId)
        {
            var issuer = await context.LicenseIssuers.FirstOrDefaultAsync(l => l.Id == issuerId);
            if (issuer == null)
                return null;

            return await GenerateLicenseKeyAsync(issuerId, PersonLicenseFlags.TemporaryLicense, issuer.TemporaryKeyPrefix, issuer.TemporaryKeyFrom);
        }

        public async Task<PersonLicense> AddNewDisposableLicenseAsync(string issuerId, string discipline, Person person, DateTime valid,
            PersonLicenseFlags flags = PersonLicenseFlags.None, Club club = null, string venueCode = null)
        {
            var expert = calculatorManager.Find(discipline);
            if (expert == null)
                throw new InvalidDisciplineException();

            flags |= PersonLicenseFlags.DisposableLicense;

            var key = await GenerateDisposableLicenseKeyAsync(issuerId);
            var season = expert.Season(valid);
            return await AddNewLicenseAsync(issuerId, discipline, key, person, season, valid.Date, valid.Date.AddDays(1), flags, club: club, venueCode: venueCode);
        }

        private async Task<string> GenerateDisposableLicenseKeyAsync(string issuerId)
        {
            var issuer = await context.LicenseIssuers.FirstOrDefaultAsync(l => l.Id == issuerId);
            if (issuer == null)
                return null;

            return await GenerateLicenseKeyAsync(issuerId, PersonLicenseFlags.DisposableLicense, issuer.DisposableKeyPrefix, issuer.DisposableKeyFrom);
        }

        private async Task<string> GenerateLicenseKeyAsync(string issuerId, PersonLicenseFlags flags, string keyPrefix, int keyFrom)
        {
            var previousKey = await (from l in context.PersonLicenses
                                     where l.IssuerId == issuerId && (l.Flags & flags) == flags
                                     orderby l.Key descending
                                     select l.Key).FirstOrDefaultAsync();

            var prefix = keyPrefix ?? "";
            int previousKeyNumber;
            var nextKeyNumber = previousKey != null && previousKey.StartsWith(prefix) && int.TryParse(previousKey.Substring(prefix.Length), out previousKeyNumber)
                ? Math.Max(keyFrom, previousKeyNumber + 1)
                : keyFrom;
            return $"{prefix}{nextKeyNumber}";
        }

        public async Task DeletePersonLicenseAsync(PersonLicense license)
        {
            context.PersonLicenses.Remove(license);
            await context.SaveChangesAsync();
        }

        public async Task<PersonLicense> UpdatePersonLicenseAsync(PersonLicense license, PersonLicenseExpertise expertise, int? season = null,
            DateTime? validFrom = null, DateTime? validTo = null, string sponsor = null, Club club = null, PersonLicenseFlags flags = PersonLicenseFlags.None,
            string category = null, int? number = null, string venueCode = null, string transponder1 = null, string transponder2 = null)
        {
            var expert = calculatorManager.Find(license.Discipline);
            if (expert == null)
                throw new InvalidDisciplineException();

            season = season ?? expert.CurrentSeason;

            using (var transaction = context.UseOrBeginTransaction(IsolationLevel.RepeatableRead))
                try
                {
                    if (license.Season != season.Value)
                    {
                        license.Flags = flags;
                        license.Season = season.Value;
                        license.ValidFrom = validFrom ?? expert.SeasonStarts(season.Value);
                        license.ValidTo = validTo ?? expert.SeasonEnds(season.Value);
                        license.Sponsor = sponsor;
                        license.Club = club;

                        if (category == null)
                        {
                            var age = expert.SeasonAge(season.Value, license.Person.BirthDate);
                            var personCategory = await GetDefaultCategoryAsync(license.IssuerId, license.Discipline, license.Person.Gender, age);
                            license.Category = personCategory?.Code;
                        }
                        else
                            license.Category = category;

                        if (expertise.HasFlag(PersonLicenseExpertise.Number))
                            license.Number = number;

                        if (venueCode != null)
                            license.VenueCode = venueCode;

                        license.Transponder1 = transponder1;
                        license.Transponder2 = transponder2;
                    }
                    else
                    {
                        license.Flags = flags;
                        if (expertise.HasFlag(PersonLicenseExpertise.Validity))
                        {
                            license.ValidFrom = validFrom ?? expert.SeasonStarts(season.Value);
                            license.ValidTo = validTo ?? expert.SeasonEnds(season.Value);
                        }
                        if (expertise.HasFlag(PersonLicenseExpertise.Sponsor))
                            license.Sponsor = sponsor;
                        if (expertise.HasFlag(PersonLicenseExpertise.Club))
                            license.Club = club;
                        if (expertise.HasFlag(PersonLicenseExpertise.Category))
                            if (category == null)
                            {
                                var age = expert.SeasonAge(season.Value, license.Person.BirthDate);
                                var personCategory = await GetDefaultCategoryAsync(license.IssuerId, license.Discipline, license.Person.Gender, age);
                                license.Category = personCategory?.Code;
                            }
                            else
                                license.Category = category;
                        if (expertise.HasFlag(PersonLicenseExpertise.Number))
                            license.Number = number;
                        if (expertise.HasFlag(PersonLicenseExpertise.Venue))
                            license.VenueCode = venueCode;
                        if (expertise.HasFlag(PersonLicenseExpertise.Transponders))
                        {
                            license.Transponder1 = transponder1;
                            license.Transponder2 = transponder2;
                        }
                    }

                    await context.SaveChangesAsync();
                    transaction.Commit();
                    recorder.RecordEvent(new PersonLicenseChangedEvent(license));
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

            return license;
        }

        public IQueryable<PersonLicenseVenueSubscription> VenueSubscriptions(string venueCode, string venueDiscipline)
        {
            return from pl in context.PersonLicenseVenueSubscriptions
                   where pl.VenueCode == venueCode && pl.VenueDiscipline == venueDiscipline
                   select pl;
        }

        public IQueryable<PersonLicenseVenueClass> VenueClasses(string venueCode, string venueDiscipline)
        {
            return from pl in context.PersonLicenseVenueClasses
                   where pl.VenueCode == venueCode && pl.VenueDiscipline == venueDiscipline
                   select pl;
        }

        public async Task ResetVenueSubscriptionsAsync(string issuerId, string discipline, ICollection<string> keys, string venueCode, string venueDiscipline, int? season)
        {
            var calculator = calculatorManager.Find(discipline);
            if (calculator == null)
                throw new InvalidDisciplineException();

            season = season ?? calculator.CurrentSeason;
            var validFrom = calculator.SeasonStarts(season.Value);
            var validTo = calculator.SeasonEnds(season.Value);

            using (var transaction = context.BeginTransaction(IsolationLevel.Serializable))
                try
                {
                    var venue = await context.Venues.FirstOrDefaultAsync(v => v.Code == venueCode && v.Discipline == venueDiscipline);
                    if (venue == null)
                        throw new VenueNotFoundException();

                    if (!discipline.StartsWith(venue.Discipline))
                        throw new InvalidDisciplineException();

                    var existing = await context.PersonLicenseVenueSubscriptions.Where(s => s.VenueCode == venueCode && s.VenueDiscipline == venueDiscipline).ToListAsync();
                    foreach (var subscription in existing)
                    {
                        context.PersonLicenseVenueSubscriptions.Remove(subscription);
                        await context.SaveChangesAsync();
                    }

                    foreach (var key in keys)
                    {
                        var subscription = new PersonLicenseVenueSubscription
                        {
                            LicenseIssuerId = issuerId,
                            LicenseDiscipline = discipline,
                            LicenseKey = key,
                            ValidFrom = validFrom,
                            ValidTo = validTo,
                            Issued = DateTime.UtcNow,
                            Venue = venue
                        };
                        context.PersonLicenseVenueSubscriptions.Add(subscription);
                        try
                        {
                            await context.SaveChangesAsync();
                        }
                        catch (Exception e)
                        {
                            throw new PersonLicenseNotFoundException(issuerId, discipline, key, e);
                        }
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
        }

        public async Task ResetVenueClassesAsync(string issuerId, string discipline, ICollection<KeyValuePair<string, int>> keyClasses, string venueCode,
            string venueDiscipline, int? season)
        {
            var calculator = calculatorManager.Find(discipline);
            if (calculator == null)
                throw new InvalidDisciplineException();

            season = season ?? calculator.CurrentSeason;
            var validFrom = calculator.SeasonStarts(season.Value);
            var validTo = calculator.SeasonEnds(season.Value);

            using (var transaction = context.BeginTransaction(IsolationLevel.Serializable))
                try
                {
                    var venue = await context.Venues.FirstOrDefaultAsync(v => v.Code == venueCode && v.Discipline == venueDiscipline);
                    if (venue == null)
                        throw new VenueNotFoundException();

                    if (!discipline.StartsWith(venue.Discipline))
                        throw new InvalidDisciplineException();

                    var existing = await context.PersonLicenseVenueClasses.Where(s => s.VenueCode == venueCode && s.VenueDiscipline == venueDiscipline).ToListAsync();
                    foreach (var venueClass in existing)
                    {
                        context.PersonLicenseVenueClasses.Remove(venueClass);
                        await context.SaveChangesAsync();
                    }

                    foreach (var keyClass in keyClasses)
                    {
                        var venueClass = new PersonLicenseVenueClass
                        {
                            LicenseIssuerId = issuerId,
                            LicenseDiscipline = discipline,
                            LicenseKey = keyClass.Key,
                            Class = keyClass.Value,
                            ValidFrom = validFrom,
                            ValidTo = validTo,
                            Issued = DateTime.UtcNow,
                            Venue = venue
                        };
                        context.PersonLicenseVenueClasses.Add(venueClass);
                        try
                        {
                            await context.SaveChangesAsync();
                        }
                        catch (Exception e)
                        {
                            throw new PersonLicenseNotFoundException(issuerId, discipline, keyClass.Key, e);
                        }
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
        }

        public async Task<bool> HasVenueSubscriptionAsync(PersonLicense license, string venueCode, string venueDiscipline, DateTime? reference = null)
        {
            reference = reference ?? DateTime.UtcNow;
            return await context.PersonLicenseVenueSubscriptions.AnyAsync(s => s.LicenseIssuerId == license.IssuerId
                && s.LicenseDiscipline == license.Discipline
                && s.LicenseKey == license.Key
                && s.VenueCode == venueCode
                && s.VenueDiscipline == venueDiscipline
                && s.ValidFrom <= reference.Value
                && s.ValidTo > reference.Value);
        }

        public async Task<int?> GetPersonLicenseVenueClassAsync(PersonLicense license, string venueCode, string venueDiscipline, DateTime? reference = null)
        {
            reference = reference ?? DateTime.UtcNow;
            return (await context.PersonLicenseVenueClasses
                .Where(c => c.LicenseIssuerId == license.IssuerId
                    && c.LicenseDiscipline == license.Discipline
                    && c.LicenseKey == license.Key
                    && c.VenueCode == venueCode
                    && c.VenueDiscipline == venueDiscipline
                    && c.ValidFrom <= reference.Value
                    && c.ValidTo > reference.Value)
                .Select(c => new {
                    c.Class
                }).FirstOrDefaultAsync())?.Class;
        }
    }
}