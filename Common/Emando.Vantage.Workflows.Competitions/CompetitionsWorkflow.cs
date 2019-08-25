using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Competitions;
using Emando.Vantage.Components;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities;
using Emando.Vantage.Entities.Competitions;
using Emando.Vantage.Workflows.Competitions.Events;
using Emando.Vantage.Workflows.Competitions.Properties;

namespace Emando.Vantage.Workflows.Competitions
{
    public class CompetitionsWorkflow : IDisposable
    {
        private readonly IDisciplineCalculatorManager calculatorManager;
        private readonly ICompetitionContext context;
        private readonly IDistanceDisciplineExpertManager distanceExpertManager;
        private readonly IEventRecorder recorder;
        private bool isDisposed;

        public CompetitionsWorkflow(ICompetitionContext context, IDisciplineCalculatorManager calculatorManager, IDistanceDisciplineExpertManager distanceExpertManager,
            IEventRecorder recorder)
        {
            this.context = context;
            this.calculatorManager = calculatorManager;
            this.recorder = recorder;
            this.distanceExpertManager = distanceExpertManager;

            DistancesWorkflow = new DistancesWorkflow(context, calculatorManager, distanceExpertManager, recorder);
            DistanceCombinationsWorkflow = new DistanceCombinationsWorkflow(context, recorder);
            LicensesWorkflow = new LicensesWorkflow(context, calculatorManager, recorder);
            RacesWorkflow = new RacesWorkflow(context, distanceExpertManager, recorder);
        }

        private LicensesWorkflow LicensesWorkflow { get; }

        public DistanceCombinationsWorkflow DistanceCombinationsWorkflow { get; }

        public DistancesWorkflow DistancesWorkflow { get; }

        public RacesWorkflow RacesWorkflow { get; }

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
                {
                    DistancesWorkflow.Dispose();
                    DistanceCombinationsWorkflow.Dispose();
                    LicensesWorkflow.Dispose();
                    RacesWorkflow.Dispose();
                    context.Dispose();
                }

                isDisposed = true;
            }
        }

        ~CompetitionsWorkflow()
        {
            Dispose(false);
        }

        #region Licenses

        public async Task<PersonLicense> GetPersonLicenseAsync(Competition competition, string key)
        {
            return await (from pl in context.PersonLicenses.Include(pl => pl.Person).Include(pl => pl.Club)
                          where pl.IssuerId == competition.LicenseIssuerId
                              && competition.Discipline.StartsWith(pl.Discipline)
                              && pl.Key == key
                          select pl).FirstOrDefaultAsync();
        }

        public async Task<PersonLicense> GetValidPersonLicenseAsync(Competition competition, string key)
        {
            return await (from pl in context.PersonLicenses.Include(pl => pl.Person).Include(pl => pl.Club)
                          where pl.IssuerId == competition.LicenseIssuerId
                              && competition.Discipline.StartsWith(pl.Discipline)
                              && pl.Key == key
                              && pl.ValidFrom <= competition.Starts.Date
                              && pl.ValidTo > competition.Starts.Date
                          select pl).FirstOrDefaultAsync();
        }

        public async Task<PersonLicense> GetValidPersonLicenseAsync(Competition competition, Guid personId)
        {
            return await (from pl in context.PersonLicenses.Include(pl => pl.Person).Include(pl => pl.Club)
                          where pl.IssuerId == competition.LicenseIssuerId
                              && competition.Discipline.StartsWith(pl.Discipline)
                              && pl.PersonId == personId
                              && pl.ValidFrom <= competition.Starts.Date
                              && pl.ValidTo > competition.Starts.Date
                          select pl).FirstOrDefaultAsync();
        }

        public bool IsLicenseExpired(PersonLicense license, Competition competition)
        {
            return license.ValidTo < competition.Starts || license.ValidFrom >= competition.Starts;
        }

        public async Task<PersonLicensePrice> GetCompetitionLicenseRenewalPriceAsync(Competition competition, PersonLicense license)
        {
            if (!IsLicenseExpired(license, competition) && !license.Flags.HasFlag(PersonLicenseFlags.TemporaryLicense))
                return null;

            var expert = calculatorManager.Find(competition.Discipline);
            if (expert == null)
                throw new InvalidDisciplineException();

            var age = license.Flags.HasFlag(PersonLicenseFlags.TemporaryLicense)
                ? license.Person.BirthDate.Age()
                : expert.SeasonAge(expert.Season(competition.Starts), license.Person.BirthDate);
            return await LicensesWorkflow.GetPersonLicensePriceAsync(competition.LicenseIssuerId, competition.Discipline, license.Person.Gender, age, license.Flags);
        }

        public async Task<PersonLicensePrice> GetCompetititonTemporaryLicensePriceAsync(Competition competition, Gender gender, DateTime birthDate)
        {
            var expert = calculatorManager.Find(competition.Discipline);
            if (expert == null)
                throw new InvalidDisciplineException();

            var age = birthDate.Age();
            return await LicensesWorkflow.GetPersonLicensePriceAsync(competition.LicenseIssuerId, competition.Discipline, gender, age, PersonLicenseFlags.TemporaryLicense);
        }

        #endregion

        #region Series

        public IQueryable<CompetitionSerie> Series => context.CompetitionSeries;

        public async Task AddSerieAsync(CompetitionSerie serie)
        {
            serie.Id = Guid.NewGuid();
            context.CompetitionSeries.Add(serie);
            await context.SaveChangesAsync();
        }

        public async Task UpdateSerieAsync(CompetitionSerie serie)
        {
            await context.SaveChangesAsync();
        }

        public async Task DeleteSerieAsync(CompetitionSerie serie)
        {
            context.CompetitionSeries.Remove(serie);
            await context.SaveChangesAsync();
        }

        #endregion

        #region Competitions

        public IQueryable<Competition> Competitions => context.Competitions;

        public IQueryable<PersonTime> PersonTimes => context.PersonTimes;

        public IQueryable<PersonCategory> PersonCategories => context.PersonCategories;

        public IQueryable<Venue> Venues => context.Venues;

        public async Task AddCompetitionAsync(Competition competition)
        {
            await EnsureValidSerieAsync(competition);

            if (competition.VenueCode != null)
            {
                var venue = await context.Venues.FirstOrDefaultAsync(v => v.Code == competition.VenueCode && competition.Discipline.StartsWith(v.Discipline));
                competition.Venue = venue ?? throw new VenueNotFoundException();
            }

            competition.Id = Guid.NewGuid();
            context.Competitions.Add(competition);
            await context.SaveChangesAsync();

            recorder.RecordEvent(new CompetitionAddedEvent(competition));
        }

        public async Task UpdateCompetitionAsync(Competition competition)
        {
            await EnsureValidSerieAsync(competition);

            if (competition.VenueCode != null)
            {
                await context.LoadAsync(competition, c => c.Distances);
                foreach (var distance in competition.Distances.Where(d => d.VenueCode == null))
                {
                    distance.VenueCode = competition.VenueCode;
                    distance.VenueDiscipline = competition.Discipline;
                }
            }

            await context.SaveChangesAsync();
            recorder.RecordEvent(new CompetitionChangedEvent(competition));
        }

        private async Task EnsureValidSerieAsync(Competition competition)
        {
            if (!competition.SerieId.HasValue)
                return;

            var serie = await Series.FirstOrDefaultAsync(s => s.Id == competition.SerieId.Value);
            if (serie == null)
                throw new CompetitionSerieNotFoundException();

            if (!competition.Discipline.StartsWith(serie.Discipline))
                throw new InvalidDisciplineException();

            competition.Serie = serie;
        }

        public async Task DeleteCompetitionAsync(Competition competition)
        {
            context.Competitions.Remove(competition);
            await context.SaveChangesAsync();
            recorder.RecordEvent(new CompetitionDeletedEvent(competition));
        }

        public async Task MakeResultsUnofficialAsync(Competition competition)
        {
            if (competition.ResultsStatus == CompetitionResultsStatus.Official)
            {
                var times = await (from pt in context.PersonTimes
                                   where pt.CompetitionId == competition.Id
                                   select pt).ToListAsync();
                foreach (var time in times)
                    context.PersonTimes.Remove(time);
            }

            competition.ResultsStatus = CompetitionResultsStatus.Unofficial;
            competition.MadeOfficial = null;

            await context.SaveChangesAsync();
            recorder.RecordEvent(new CompetitionResultsUnofficialEvent(competition));
        }

        public async Task MakeResultsOfficialAsync(Competition competition)
        {
            if (competition.ResultsStatus != CompetitionResultsStatus.Unofficial)
                throw new InvalidCompetitionResultsStatusException();

            competition.ResultsStatus = CompetitionResultsStatus.Official;
            competition.MadeOfficial = DateTime.UtcNow;

            foreach (var personTime in
                (from race in await context.Races.Include(r => r.Distance)
                    .Include(r => r.Competitor)
                    .Include(r => r.Results)
                    .Include(r => r.Times)
                    .Where(r => r.Distance.CompetitionId == competition.Id && r.Distance.Competition.Class > CompetitionClasses.Test && r.Distance.VenueCode != null)
                    .ToListAsync()
                 where race.PresentedTime != null
                 let personCompetitor = race.Competitor as PersonCompetitor
                 where personCompetitor?.LicenseKey != null && personCompetitor.LicenseFlags.HasFlag(PersonLicenseFlags.CompetitionLicense)
                 let discipline = personCompetitor.LicenseDiscipline ?? competition.Discipline
                 let expert = distanceExpertManager.Find(race.Distance.Discipline)
                 where expert != null
                 select new PersonTime
                 {
                     CompetitionId = competition.Id,
                     Date = race.Distance.Starts.GetValueOrDefault(competition.Starts).Date,
                     Discipline = competition.Discipline,
                     DistanceDiscipline = race.Distance.Discipline,
                     Distance = expert.Calculator.Length(race.Distance),
                     LicenseIssuerId = competition.LicenseIssuerId,
                     LicenseDiscipline = discipline,
                     LicenseKey = personCompetitor.LicenseKey,
                     VenueCode = race.Distance.VenueCode,
                     NationalityCode = personCompetitor.NationalityCode,
                     Time = race.PresentedTime.Time,
                     Source = "Vantage"
                 }).Distinct())
            {
                var existing = await context.PersonTimes.FirstOrDefaultAsync(pt => pt.LicenseIssuerId == personTime.LicenseIssuerId
                    && pt.LicenseDiscipline == personTime.LicenseDiscipline
                    && pt.LicenseKey == personTime.LicenseKey
                    && pt.VenueCode == personTime.VenueCode
                    && pt.Discipline == personTime.Discipline
                    && pt.DistanceDiscipline == personTime.DistanceDiscipline
                    && pt.Distance == personTime.Distance
                    && pt.Date == personTime.Date
                    && pt.Time == personTime.Time);
                if (existing != null)
                {
                    existing.CompetitionId = competition.Id;
                    existing.NationalityCode = personTime.NationalityCode;
                    existing.Source = "Vantage";
                }
                else
                    context.PersonTimes.Add(personTime);
            }

            await context.SaveChangesAsync();
            recorder.RecordEvent(new CompetitionResultsOfficialEvent(competition));
        }

        public async Task<Competition> CloneCompetitionAsync(Competition current, CompetitionCloneSettings settings)
        {
            using (var transaction = context.BeginTransaction(IsolationLevel.RepeatableRead))
                try
                {
                    var shift = settings.Starts - current.Starts;
                    var clone = new Competition
                    {
                        SerieId = settings.CloneSerie ? current.SerieId : new Guid?(),
                        VenueCode = settings.CloneVenue ? current.VenueCode : null,
                        Discipline = current.Discipline,
                        Location = current.Location,
                        LocationFlags = current.LocationFlags,
                        Extra = current.Extra,
                        Sponsor = current.Sponsor,
                        Name = settings.Name ?? current.Name,
                        Class = current.Class,
                        LicenseIssuerId = current.LicenseIssuerId,
                        Starts = settings.Starts,
                        Ends = current.Ends + shift,
                        TimeZone = current.TimeZone,
                        Culture = current.Culture
                    };
                    await AddCompetitionAsync(clone);

                    var clonedDistances = new Dictionary<Guid, Distance>();
                    if (settings.CloneDistances)
                        foreach (var currentDistance in await DistancesWorkflow.Distances(current.Id).ToListAsync())
                        {
                            var clonedDistance = await DistancesWorkflow.CloneDistanceAsync(currentDistance, settings.DistanceCloneSettings, settings.CloneVenue, clone.Id,
                                shift);
                            clonedDistances.Add(currentDistance.Id, clonedDistance);
                        }

                    if (settings.CloneDistanceCombinations)
                        foreach (var currentDistanceCombination in await DistanceCombinationsWorkflow.Combinations(current.Id).Include(c => c.Distances).ToListAsync())
                        {
                            var clonedDistanceCombination =
                                await
                                    DistanceCombinationsWorkflow.CloneDistanceCombinationAsync(currentDistanceCombination, settings.DistanceCombinationCloneSettings, clone.Id,
                                        shift);
                            if (settings.CloneDistances)
                            {
                                clonedDistanceCombination.Distances = new Collection<Distance>();
                                await
                                    DistanceCombinationsWorkflow.UpdateCombinationDistancesAsync(clonedDistanceCombination,
                                        currentDistanceCombination.Distances.Select(d => clonedDistances[d.Id].Id).ToArray());
                            }
                        }

                    transaction.Commit();
                    return clone;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
        }

        #endregion

        #region Competitor Lists

        public IQueryable<CompetitorListBase> CompetitorLists(Guid competitionId)
        {
            return from c in context.CompetitorLists
                   where c.CompetitionId == competitionId
                   select c;
        }

        public async Task AddCompetitorListAsync(CompetitorListBase competitorList)
        {
            competitorList.Id = Guid.NewGuid();
            context.CompetitorLists.Add(competitorList);
            await context.SaveChangesAsync();
        }

        public async Task UpdateCompetitorListAsync(CompetitorListBase competitorList)
        {
            await context.SaveChangesAsync();
        }

        public async Task DeleteCompetitorListAsync(CompetitorListBase competitorList)
        {
            context.CompetitorLists.Remove(competitorList);
            await context.SaveChangesAsync();
        }

        public async Task<ICollection<CompetitorBase>> ClearCompetitorsAsync(Guid listId)
        {
            var competitors = await (from c in context.Competitors
                                     where c.ListId == listId
                                     select c).ToListAsync();

            foreach (var competitor in competitors)
                context.Competitors.Remove(competitor);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DependingDataDeleteException(e);
            }
            return competitors;
        }

        public async Task RenumberCompetitorsAsync(Guid listId, int from, int add)
        {
            if (add == 0)
                throw new ArgumentOutOfRangeException(nameof(add));

            using (var transaction = context.BeginTransaction(IsolationLevel.RepeatableRead))
            {
                var query = from c in context.Competitors
                            where c.ListId == listId && c.StartNumber >= @from
                            select c;

                query = add > 0 ? query.OrderByDescending(c => c.StartNumber) : query.OrderBy(c => c.StartNumber);

                try
                {
                    foreach (var competitor in await query.ToListAsync())
                    {
                        competitor.StartNumber += add;
                        Validator.ValidateObject(competitor, new ValidationContext(competitor), true);

                        try
                        {
                            await context.SaveChangesAsync();
                        }
                        catch (DbUpdateException e)
                        {
                            throw new NumberCollissionException(string.Format(Resources.StartNumberCollission, competitor.StartNumber), e);
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
        }

        #endregion

        #region Competitors

        private async Task AddCompetitorAsync(CompetitorBase competitor)
        {
            competitor.Id = Guid.NewGuid();
            competitor.Added = DateTime.UtcNow;
            context.Competitors.Add(competitor);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                // TODO: Check if the number indeed already exists
                throw new NumberCollissionException(string.Format(Resources.StartNumberCollission, competitor.StartNumber), e);
            }
        }

        public async Task AddCompetitorAsync(Competition competition, PersonCompetitor competitor)
        {
            var expert = calculatorManager.Find(competition.Discipline);
            if (expert == null)
                throw new InvalidDisciplineException();

            using (var transaction = context.UseOrBeginTransaction(IsolationLevel.RepeatableRead))
                try
                {
                    if (competitor.StartNumber == 0 || expert.AutomaticStartNumbers)
                    {
                        var highest = await (from d in context.Competitors
                                             where d.ListId == competitor.ListId
                                             select d.StartNumber).DefaultIfEmpty(0).MaxAsync();
                        competitor.StartNumber = Math.Max(expert.AutomaticStartNumberFrom, highest + 1);
                    }

                    competitor.EntityId = competitor.Person?.Id ?? competitor.PersonId;

                    await AddCompetitorAsync(competitor);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
        }

        public async Task AddCompetitorAsync(Competition competition, TeamCompetitor competitor, Guid[] members)
        {
            using (var transaction = context.BeginTransaction(IsolationLevel.RepeatableRead))
                try
                {
                    competitor.StartNumber = competitor.StartNumber > 0
                        ? competitor.StartNumber
                        : await (from d in context.Competitors
                                 where d.ListId == competitor.ListId
                                 select d.StartNumber).DefaultIfEmpty(0).MaxAsync() + 1;

                    competitor.EntityId = Guid.NewGuid();
                    competitor.Members = new List<TeamCompetitorMember>();
                    await AddCompetitorAsync(competitor);

                    if (members != null)
                        await UpdateTeamCompetitorMembersAsync(competitor, competition.Id, members);

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
        }

        public async Task<PersonCompetitor> AddNewCompetitorAsync(Competition competition, Guid listId, PersonLicense license, CompetitorStatus status, CompetitorSource source)
        {
            await context.LoadAsync(license, l => l.Club);

            var @class = await LicensesWorkflow.GetPersonLicenseVenueClassAsync(license, competition.VenueCode, competition.Discipline, competition.Starts);

            var competitor = new PersonCompetitor
            {
                ListId = listId,
                Person = license.Person,
                Name = license.Person.Name,
                ShortName = new string(license.Person.Name.PrefixedSurname.Take(50).ToArray()),
                LicenseDiscipline = license.Discipline,
                LicenseKey = license.Key,
                LicenseFlags = license.Flags,
                Gender = license.Person.Gender,
                Status = status,
                Category = license.Category,
                Class = @class,
                Sponsor = license.Sponsor,
                ClubCountryCode = license.ClubCountryCode,
                ClubCode = license.ClubCode,
                ClubShortName = license.Club?.ShortName,
                ClubFullName = license.Club?.FullName,
                From = license.Person.Address.City,
                StartNumber = license.Number ?? 0,
                NationalityCode = license.Person.NationalityCode,
                VenueCode = license.VenueCode,
                Transponder1 = license.Transponder1,
                Transponder2 = license.Transponder2,
                Source = source
            };
            await AddCompetitorAsync(competition, competitor);
            return competitor;
        }

        public async Task UpdateCompetitorAsync(CompetitorBase competitor)
        {
            if (competitor.ClubCode.HasValue)
            {
                var club = await context.Clubs.FirstOrDefaultAsync(c => c.Code == competitor.ClubCode && c.CountryCode == competitor.ClubCountryCode);
                competitor.ClubShortName = club?.ShortName;
                competitor.ClubFullName = club?.FullName;
            }

            await context.SaveChangesAsync();
        }

        public async Task UpdateCompetitorsByLicenseAsync(PersonLicense license)
        {
            var expert = calculatorManager.Find(license.Discipline);
            if (expert == null)
                throw new InvalidDisciplineException();

            using (var transaction = context.BeginTransaction(IsolationLevel.RepeatableRead))
                try
                {
                    context.PersonLicenses.Attach(license);
                    await context.LoadAsync(license, l => l.Club);

                    var competitors = await (from c in Competitors(license.IssuerId, license.Discipline, license.Key)
                                             where c.List.Competition.Starts > DateTime.UtcNow
                                             select c).ToListAsync();
                    foreach (var competitor in competitors)
                    {
                        competitor.Sponsor = license.Sponsor;
                        competitor.ClubCountryCode = license.ClubCountryCode;
                        competitor.ClubCode = license.ClubCode;
                        competitor.ClubShortName = license.Club?.ShortName;
                        competitor.ClubFullName = license.Club?.FullName;
                        competitor.Category = license.Category;
                        competitor.StartNumber = !expert.AutomaticStartNumbers && license.Number.HasValue ? license.Number.Value : competitor.StartNumber;
                        competitor.Transponder1 = license.Transponder1;
                        competitor.Transponder2 = license.Transponder2;
                        await UpdateCompetitorAsync(competitor);
                    }
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
        }

        public async Task ConfirmCompetitorAsync(CompetitorBase competitor, params Guid[] combinations)
        {
            competitor.Status = CompetitorStatus.Confirmed;

            var competitors = await (from dcc in context.DistanceCombinationCompetitors
                                     where combinations.Contains(dcc.DistanceCombinationId) && dcc.CompetitorId == competitor.Id
                                     select dcc).ToListAsync();
            foreach (var combinationCompetitor in competitors)
                combinationCompetitor.Status = DistanceCombinationCompetitorStatus.Confirmed;

            await context.SaveChangesAsync();
        }

        public async Task WithdrawCompetitorAsync(CompetitorBase competitor, params Guid[] combinations)
        {
            var competitors = await (from dcc in context.DistanceCombinationCompetitors
                                     where dcc.CompetitorId == competitor.Id && combinations.Contains(dcc.DistanceCombinationId)
                                     select dcc).ToListAsync();
            foreach (var combinationCompetitor in competitors)
                combinationCompetitor.Status = DistanceCombinationCompetitorStatus.Withdrawn;

            var races = await context.Races.Where(r => r.CompetitorId == competitor.Id).ToListAsync();
            foreach (var race in races)
                context.Races.Remove(race);

            await context.SaveChangesAsync();
        }

        public async Task DeleteCompetitorAsync(CompetitorBase competitor)
        {
            var races = await context.Races.Where(r => r.CompetitorId == competitor.Id).ToListAsync();
            foreach (var race in races)
                context.Races.Remove(race);

            context.Competitors.Remove(competitor);
            await context.SaveChangesAsync();
        }

        public IQueryable<PersonCompetitor> Competitors(string licenseIssuerId, string discipline, string key)
        {
            return from c in context.Competitors.OfType<PersonCompetitor>()
                   where c.List.Competition.LicenseIssuerId == licenseIssuerId && discipline.StartsWith(c.List.Competition.Discipline) && c.LicenseKey == key
                   select c;
        }

        public IQueryable<CompetitorBase> Competitors(Guid competitionId)
        {
            return from p in context.Competitors
                   where p.List.CompetitionId == competitionId
                   select p;
        }

        public async Task LoadTeamCompetitorMembersAsync(TeamCompetitor competitor)
        {
            var query = from tcm in context.TeamCompetitorMembers.Include(m => m.Member)
                        where tcm.TeamId == competitor.Id
                        select tcm;
            await query.LoadAsync();
        }

        public async Task UpdateTeamCompetitorMembersAsync(TeamCompetitor competitor, Guid competitionId, Guid[] members)
        {
            using (var transaction = context.UseOrBeginTransaction(IsolationLevel.RepeatableRead))
                try
                {
                    competitor.Members.Clear();
                    await context.SaveChangesAsync();

                    foreach (var personCompetitor in await (from c in Competitors(competitionId).OfType<PersonCompetitor>()
                                                            where members.Contains(c.Id)
                                                            select c).ToListAsync())
                        competitor.Members.Add(new TeamCompetitorMember
                        {
                            Team = competitor,
                            Member = personCompetitor,
                            Order = Array.IndexOf(members, personCompetitor.Id)
                        });

                    await context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
        }

        #endregion
    }
}