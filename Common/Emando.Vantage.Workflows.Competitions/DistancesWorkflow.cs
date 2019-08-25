using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Components;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities;
using Emando.Vantage.Entities.Competitions;
using Emando.Vantage.Workflows.Competitions.Events;
using Emando.Vantage.Workflows.Competitions.Properties;

namespace Emando.Vantage.Workflows.Competitions
{
    public class DistancesWorkflow : IDisposable
    {
        private readonly ICompetitionContext context;
        private readonly IDistanceDisciplineExpertManager distanceExpertManager;
        private readonly IDisciplineCalculatorManager calculatorManager;
        private readonly PersonTimesWorkflow personTimesWorkflow;
        private readonly IEventRecorder recorder;
        private bool isDisposed;

        public DistancesWorkflow(ICompetitionContext context, IDisciplineCalculatorManager calculatorManager, IDistanceDisciplineExpertManager distanceExpertManager,
            IEventRecorder recorder)
        {
            this.context = context;
            this.calculatorManager = calculatorManager;
            this.distanceExpertManager = distanceExpertManager;
            this.recorder = recorder;

            personTimesWorkflow = new PersonTimesWorkflow(context, calculatorManager, distanceExpertManager);
        }

        public IQueryable<Competition> Competitions => context.Competitions;

        public IQueryable<ValidDistance> ValidDistances => context.ValidDistances;

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        ~DistancesWorkflow()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    personTimesWorkflow.Dispose();
                    context.Dispose();
                }

                isDisposed = true;
            }
        }

        public IQueryable<Distance> Distances(Guid competitionId)
        {
            return from d in context.Distances
                   where d.CompetitionId == competitionId
                   orderby d.Number
                   select d;
        }

        public IQueryable<DistanceCombination> DistanceCombinations(Guid competitionId, Guid distanceId)
        {
            return from dc in context.DistanceCombinations
                   where dc.CompetitionId == competitionId && dc.Distances.Any(d => d.Id == distanceId)
                   orderby dc.Number
                   select dc;
        }

        public async Task AddDistanceAsync(Distance distance)
        {
            using (var transaction = context.UseOrBeginTransaction(IsolationLevel.RepeatableRead))
                try
                {
                    distance.Id = Guid.NewGuid();
                    context.Distances.Add(distance);

                    if (distance.ContinuousNumbering)
                        await ResetFirstHeatAsync(distance, true, null);

                    await context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
        }

        public async Task UpdateDistanceAsync(Distance distance)
        {
            await context.LoadAsync(distance, d => d.Races);
            foreach (var race in distance.Races)
                OnUpdatingDistance(distance, race);

            await context.SaveChangesAsync();
        }

        public async Task<Distance> CloneDistanceAsync(Distance current, DistanceCloneSettings settings, bool cloneVenue, Guid? competitionId = null, TimeSpan? shiftTimes = null)
        {
            competitionId = competitionId ?? current.CompetitionId;

            int number;
            if (competitionId != current.CompetitionId)
                number = current.Number;
            else
                number = await Distances(competitionId.Value).Select(d => d.Number).DefaultIfEmpty(0).MaxAsync() + 1;

            var clone = new Distance
            {
                CompetitionId = competitionId.Value,
                Discipline = current.Discipline,
                Number = number,
                TrackLength = current.TrackLength,
                VenueDiscipline = current.VenueDiscipline,
                VenueCode = cloneVenue ? current.VenueCode : null,
                Value = current.Value,
                ValueQuantity = current.ValueQuantity,
                ClassificationPrecision = current.ClassificationPrecision,
                Name = current.Name,
                Starts = current.Starts + (shiftTimes ?? TimeSpan.Zero),
                StartMode = current.StartMode,
                FirstHeat = current.FirstHeat,
                ContinuousNumbering = current.ContinuousNumbering
            };
            
            await AddDistanceAsync(clone);
            return clone;
        }

        private void OnUpdatingDistance(Distance distance, Race race)
        {
            var expert = distanceExpertManager.Find(distance.Discipline);
            expert?.OnUpdatingDistance(distance, race);
        }

        private async Task OnAddingRaceAsync(Distance distance, Race race)
        {
            await UpdateCompetitorBestTimesAsync(distance, race);

            var distanceExpert = distanceExpertManager.Find(distance.Discipline);
            distanceExpert?.OnAddingRace(distance, race);
        }

        private async Task UpdateCompetitorBestTimesAsync(Distance distance, Race race)
        {
            var personCompetitor = race.Competitor as PersonCompetitor;
            if (personCompetitor == null)
                return;

            await context.LoadAsync(distance, d => d.Competition);

            var expert = calculatorManager.Find(distance.Competition.Discipline);
            if (expert == null)
                return;

            var previousRaces = await (from r in context.Races.Include(r => r.Results).Include(r => r.Times)
                                       where r.Distance.CompetitionId == distance.CompetitionId
                                          && r.Distance.Number < distance.Number
                                          && r.Distance.Discipline == distance.Discipline
                                          && r.Distance.Value == distance.Value
                                          && r.Distance.ValueQuantity == distance.ValueQuantity
                                          && r.CompetitorId == race.Competitor.Id
                                       select r).ToListAsync();
            var previousFastest = (from t in previousRaces
                                   where t.PresentedTime != null
                                   orderby t.PresentedTime.Time
                                   select new TimeSpan?(t.PresentedTime.Time)).FirstOrDefault();

            var seasonStarts = expert.SeasonStarts(expert.CurrentSeason);
            var seasonEnds = expert.SeasonEnds(expert.CurrentSeason);
            var seasonBest = await personTimesWorkflow.FindHistoricalTimeAsync(distance.Competition.LicenseIssuerId, distance.Competition.Discipline, distance.Discipline,
                distance.Value, race.Competitor.LicenseKey, new SeasonBestSelector(seasonStarts, seasonEnds));
            race.SeasonBest = previousFastest != null && seasonBest != null
                ? (previousFastest.Value < seasonBest.Time ? previousFastest.Value : seasonBest?.Time)
                : (seasonBest?.Time ?? previousFastest);

            var personalBest = await personTimesWorkflow.FindHistoricalTimeAsync(distance.Competition.LicenseIssuerId, distance.Competition.Discipline, distance.Discipline,
                distance.Value, race.Competitor.LicenseKey, new PersonalBestSelector());
            race.PersonalBest = previousFastest != null && personalBest != null
                ? (previousFastest.Value < personalBest.Time ? previousFastest.Value : personalBest?.Time)
                : (personalBest?.Time ?? previousFastest);
        }

        private void OnMovingRace(Distance distance, Race race)
        {
            var expert = distanceExpertManager.Find(distance.Discipline);
            expert?.OnMovingRace(distance, race);
        }

        public async Task DeleteDistanceAsync(Distance distance)
        {
            try
            {
                context.Distances.Remove(distance);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DependingDataDeleteException(e);
            }
        }

        public IQueryable<Race> Races(Guid competitionId, Guid distanceId)
        {
            return from r in context.Races
                   where r.Competitor.List.CompetitionId == competitionId && r.DistanceId == distanceId
                   orderby r.Round, r.Heat, r.Lane
                   select r;
        }

        public IQueryable<Race> Races(Guid competitionId, Guid distanceId, int round)
        {
            return from r in context.Races
                   where r.Competitor.List.CompetitionId == competitionId && r.DistanceId == distanceId && r.Round == round
                   select r;
        }

        public IQueryable<CompetitorBase> DistanceCombinationsCompetitors(Guid competitionId, Guid distanceId)
        {
            return from dc in context.DistanceCombinations
                   where dc.CompetitionId == competitionId && dc.Distances.Any(d => d.Id == distanceId)
                   from c in dc.Competitors
                   select c.Competitor;
        }

        private async Task<IList<Distance>> ShiftHeatsAsync(Distance from, int round, int heat)
        {
            var distances = await (from d in context.Distances.Include(r => r.Races)
                                   where d.CompetitionId == @from.CompetitionId && d.Number > @from.Number
                                   orderby d.Number
                                   select d).ToListAsync();

            if (distances.Count == 0 || !distances[0].ContinuousNumbering)
                return new List<Distance>();

            var shift = heat - distances[0].FirstHeat + 1;
            if (shift == 0)
                return distances;

            var shiftedDistances = new List<Distance>();
            foreach (var distance in distances.TakeWhile(d => d.ContinuousNumbering))
            {
                var races = shift < 0
                    ? distance.Races.Where(r => r.Round == round).OrderBy(r => r.Heat)
                    : distance.Races.Where(r => r.Round == round).OrderByDescending(r => r.Heat);
                foreach (var race in races)
                {
                    race.Heat += shift;
                    Validator.ValidateObject(race, new ValidationContext(race), true);

                    try
                    {
                        await context.SaveChangesAsync();
                    }
                    catch (DbUpdateException e)
                    {
                        throw new NumberCollissionException(e);
                    }
                }

                distance.FirstHeat += shift;
                await context.SaveChangesAsync();
                shiftedDistances.Add(distance);
            }

            return shiftedDistances;
        }

        public async Task AddRacesToHeatAsync(Distance distance, int round, int heat, bool shiftHeats = true, params Race[] races)
        {
            var expert = distanceExpertManager.Find(distance.Discipline);
            if (expert == null)
                throw new InvalidDisciplineException();

            using (var transaction = context.UseOrBeginTransaction(IsolationLevel.RepeatableRead))
                try
                {
                    if (heat < distance.FirstHeat)
                        throw new ValidationException(string.Format(Resources.InvalidHeat, heat));

                    // TODO: Continuous numbering may become optional here
                    if (await Races(distance.CompetitionId, distance.Id, round).AnyAsync(r => r.Heat == heat))
                        await RenumberHeatsAsync(distance, round, false, distance.FirstHeat, heat, 1);

                    var lane = 1;
                    if (!expert.FixedLanes)
                        lane = await (from r in context.Races
                                      where r.DistanceId == distance.Id && r.Round == round && r.Heat == heat
                                      select r.Lane).DefaultIfEmpty(0).MaxAsync() + 1;

                    foreach (var race in races)
                    {
                        race.Id = Guid.NewGuid();
                        race.Distance = distance;
                        race.Round = round;
                        race.Heat = heat;
                        race.Lane = expert.FixedLanes ? race.Lane : lane;
                        await OnAddingRaceAsync(distance, race);

                        context.Races.Add(race);
                        await context.SaveChangesAsync();
                        lane++;
                    }

                    IList<Distance> shiftedDistances = null;
                    if (shiftHeats)
                        shiftedDistances = await ShiftHeatsAsync(distance, round, heat);

                    transaction.Commit();

                    recorder.RecordEvent(new DistanceRacesAddedEvent(distance, races));
                    if (shiftHeats)
                        foreach (var shiftedDistance in shiftedDistances)
                            recorder.RecordEvent(new DistanceRacesChangedEvent(shiftedDistance, shiftedDistance.Races));
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
        }

        public async Task<IReadOnlyCollection<Race>> UpdateRacesInHeatAsync(Distance distance, int round, int heat, params Race[] races)
        {
            using (var transaction = context.BeginTransaction(IsolationLevel.RepeatableRead))
                try
                {
                    var existingRaces = await (from r in context.Races.Include(r => r.Transponders)
                                               where r.DistanceId == distance.Id && r.Round == round && r.Heat == heat
                                               select r).ToListAsync();
                    var removedRaces = new List<Race>();

                    var newLaneRaces = races.ToDictionary(r => r.Lane);
                    var existingLaneRaces = existingRaces.ToDictionary(r => r.Lane);
                    var existingTransponders = existingRaces.SelectMany(r => r.Transponders).ToLookup(t => t.Race.CompetitorId);
                    var maxLane = existingLaneRaces.Keys.Union(newLaneRaces.Keys).Select(l => new int?(l)).Max();
                    for (var lane = 0; lane <= maxLane; lane++)
                    {
                        Race newRace;
                        newLaneRaces.TryGetValue(lane, out newRace);
                        Race existingRace;
                        existingLaneRaces.TryGetValue(lane, out existingRace);

                        if (existingRace != null && newRace != null && existingRace.CompetitorId == newRace.Competitor.Id)
                            continue;

                        if (existingRace != null)
                        {
                            context.Races.Remove(existingRace);
                            await context.SaveChangesAsync();
                            existingLaneRaces.Remove(lane);
                            removedRaces.Add(existingRace);
                        }

                        if (newRace != null)
                        {
                            newRace.Id = Guid.NewGuid();
                            newRace.Distance = distance;
                            newRace.Round = round;
                            newRace.Heat = heat;
                            newRace.Transponders = new List<RaceTransponder>();
                            foreach (var transponder in existingTransponders[newRace.Competitor.Id])
                                newRace.Transponders.Add(new RaceTransponder
                                {
                                    Type = transponder.Type,
                                    Code = transponder.Code,
                                    Set = transponder.Set,
                                    PersonId = transponder.PersonId
                                });

                            await OnAddingRaceAsync(distance, newRace);

                            context.Races.Add(newRace);
                            await context.SaveChangesAsync();
                            existingLaneRaces[lane] = newRace;
                        }
                    }

                    await context.SaveChangesAsync();

                    recorder.RecordEvent(new DistanceRacesDeletedEvent(distance, removedRaces));
                    recorder.RecordEvent(new DistanceRacesAddedEvent(distance, existingLaneRaces.Values));
                    foreach (var combination in await DistanceCombinations(distance.CompetitionId, distance.Id).ToListAsync())
                        recorder.RecordEvent(new DistanceCombinationClassificationChangedEvent(combination));

                    transaction.Commit();
                    return existingLaneRaces.Values.ToList();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
        }

        public async Task MoveHeatAsync(Distance distance, int sourceRound, int sourceHeat, int destinationRound, int destinationHeat)
        {
            if (destinationHeat < distance.FirstHeat)
                throw new ValidationException(string.Format(Resources.InvalidHeat, destinationHeat));

            using (var transaction = context.BeginTransaction(IsolationLevel.RepeatableRead))
                try
                {
                    var races = await (from r in Races(distance.CompetitionId, distance.Id, sourceRound)
                                       where r.Heat == sourceHeat
                                       select r).ToListAsync();

                    foreach (var race in races)
                    {
                        race.Round = destinationRound;
                        race.Heat = destinationHeat;
                        OnMovingRace(distance, race);
                        Validator.ValidateObject(race, new ValidationContext(race), true);
                    }

                    try
                    {
                        await context.SaveChangesAsync();
                    }
                    catch (DbUpdateException e)
                    {
                        throw new NumberCollissionException(string.Format(Resources.HeatCollission, destinationRound, destinationHeat), e);
                    }

                    var shiftedDistances = await ShiftHeatsAsync(distance, destinationRound, destinationHeat);
                    transaction.Commit();

                    recorder.RecordEvent(new DistanceRacesChangedEvent(distance, races));
                    foreach (var shiftedDistance in shiftedDistances)
                        recorder.RecordEvent(new DistanceRacesChangedEvent(shiftedDistance, shiftedDistance.Races));
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
        }

        public async Task<int> GetNextHeatAsync(Distance distance, int round)
        {
            return await (from r in context.Races
                          where r.DistanceId == distance.Id && r.Round == round
                          select r.Heat).DefaultIfEmpty(distance.FirstHeat - 1).MaxAsync() + 1;
        }

        public async Task<int> ResetFirstHeatAsync(Distance distance, bool continuousNumbering, int? firstHeat)
        {
            distance.ContinuousNumbering = continuousNumbering;
            if (!firstHeat.HasValue)
                firstHeat = await (from d in context.Distances
                                   where d.CompetitionId == distance.CompetitionId && d.Number < distance.Number && d.Races.Any()
                                   orderby d.Number descending
                                   select (int?)d.Races.Select(r => r.Heat).Max()).FirstOrDefaultAsync() + 1 ?? 1;

            var shift = firstHeat.Value - distance.FirstHeat;
            distance.FirstHeat = firstHeat.Value;
            await context.SaveChangesAsync();
            return shift;
        }

        public async Task RenumberHeatsAsync(Distance distance, int round, bool continuousNumbering, int? firstHeat, int from, int add)
        {
            using (var transaction = context.UseOrBeginTransaction(IsolationLevel.RepeatableRead))
                try
                {
                    var shift = await ResetFirstHeatAsync(distance, continuousNumbering, firstHeat);

                    var races = add + shift > 0
                        ? await Races(distance.CompetitionId, distance.Id, round).OrderByDescending(r => r.Heat).ToListAsync()
                        : await Races(distance.CompetitionId, distance.Id, round).OrderBy(r => r.Heat).ToListAsync();

                    foreach (var race in races)
                    {
                        race.Heat += shift;
                        if (race.Heat >= @from)
                            race.Heat += add;

                        OnMovingRace(distance, race);
                        Validator.ValidateObject(race, new ValidationContext(race), true);

                        if (race.Heat < distance.FirstHeat)
                            throw new ValidationException(string.Format(Resources.InvalidHeat, race.Heat));

                        try
                        {
                            await context.SaveChangesAsync();
                        }
                        catch (DbUpdateException e)
                        {
                            throw new NumberCollissionException(e);
                        }
                    }


                    IList<Distance> shiftedDistances = null;
                    if (continuousNumbering)
                        shiftedDistances = await ShiftHeatsAsync(distance, round, races.Max(r => r.Heat));

                    transaction.Commit();

                    recorder.RecordEvent(new DistanceRacesChangedEvent(distance, races));
                    if (continuousNumbering)
                        foreach (var shiftedDistance in shiftedDistances)
                            recorder.RecordEvent(new DistanceRacesChangedEvent(shiftedDistance, shiftedDistance.Races));
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
        }

        public async Task<IReadOnlyCollection<Race>> DeleteHeatAsync(Distance distance, int round, int heat)
        {
            using (var transaction = context.BeginTransaction(IsolationLevel.RepeatableRead))
                try
                {
                    var races = await (from r in Races(distance.CompetitionId, distance.Id, round)
                                       where r.Heat == heat
                                       select r).ToListAsync();

                    foreach (var race in races)
                        context.Races.Remove(race);

                    await context.SaveChangesAsync();

                    // TODO: This may be distance discipline specific
                    // TODO: Continuous numbering may become optional here
                    await RenumberHeatsAsync(distance, round, false, distance.FirstHeat, heat + 1, -1);
                    transaction.Commit();

                    recorder.RecordEvent(new DistanceRacesDeletedEvent(distance, races));
                    foreach (var combination in await DistanceCombinations(distance.CompetitionId, distance.Id).ToListAsync())
                        recorder.RecordEvent(new DistanceCombinationClassificationChangedEvent(combination));

                    return races;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
        }

        public async Task MoveRaceAsync(Distance distance, Race race, int round, int heat, int lane)
        {
            if (heat < distance.FirstHeat)
                throw new ValidationException(string.Format(Resources.InvalidHeat, heat));

            using (var transaction = context.BeginTransaction(IsolationLevel.RepeatableRead))
                try
                {
                    race.Round = round;
                    race.Heat = heat;
                    race.Lane = lane;
                    OnMovingRace(distance, race);
                    Validator.ValidateObject(race, new ValidationContext(race), true);

                    try
                    {
                        await context.SaveChangesAsync();
                    }
                    catch (DbUpdateException e)
                    {
                        throw new NumberCollissionException(e);
                    }

                    var shiftedDistances = await ShiftHeatsAsync(distance, round, heat);
                    transaction.Commit();

                    recorder.RecordEvent(new DistanceRacesChangedEvent(distance, new[] { race }));
                    foreach (var shiftedDistance in shiftedDistances)
                        recorder.RecordEvent(new DistanceRacesChangedEvent(shiftedDistance, shiftedDistance.Races));
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
        }

        public async Task DeleteRaceAsync(Distance distance, Race race)
        {
            context.Races.Remove(race);
            await context.SaveChangesAsync();

            recorder.RecordEvent(new DistanceRacesDeletedEvent(distance, new[] { race }));
        }

        public async Task DeleteRacesAsync(Distance distance, IReadOnlyCollection<Race> races)
        {
            foreach (var race in races)
                context.Races.Remove(race);
            await context.SaveChangesAsync();

            recorder.RecordEvent(new DistanceRacesDeletedEvent(distance, races));
        }

        public async Task CopyRacesAsync(Distance sourceDistance, Distance destinationDistance, RacesCopySettings settings)
        {
            Debug.Assert(sourceDistance.Races != null, "sourceDistance.Races != null");
            Debug.Assert(destinationDistance.Races != null, "destinationDistance.Races != null");

            var expert = distanceExpertManager.Find(destinationDistance.Discipline);

            using (var transaction = context.UseOrBeginTransaction(IsolationLevel.RepeatableRead))
                try
                {
                    var oldRaces = destinationDistance.Races.ToList();
                    foreach (var race in oldRaces)
                        context.Races.Remove(race);
                    await context.SaveChangesAsync();

                    var heatDiff = destinationDistance.FirstHeat - sourceDistance.FirstHeat;
                    foreach (var race in sourceDistance.Races)
                    {
                        var newRace = new Race
                        {
                            Id = Guid.NewGuid(),
                            Distance = destinationDistance,
                            Round = race.Round,
                            Heat = race.Heat + heatDiff,
                            Lane = race.Lane,
                            Color = race.Color,
                            Competitor = race.Competitor,
                            Transponders = settings.CopyTransponders
                                ? race.Transponders?.Select(rt => new RaceTransponder
                                {
                                    PersonId = rt.PersonId,
                                    Type = rt.Type,
                                    Code = rt.Code,
                                    Set = rt.Set
                                }).ToList()
                                : null
                        };
                        await UpdateCompetitorBestTimesAsync(destinationDistance, newRace);
                        expert?.OnCopyingRace(destinationDistance, race, newRace, settings);
                        destinationDistance.Races.Add(newRace);
                    }

                    await context.SaveChangesAsync();
                    transaction.Commit();

                    if (oldRaces.Any())
                        recorder.RecordEvent(new DistanceRacesDeletedEvent(destinationDistance, oldRaces));
                    recorder.RecordEvent(new DistanceRacesAddedEvent(destinationDistance, destinationDistance.Races));
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
        }

        public async Task AssignTranspondersAsync(Distance distance, params string[] bagNames)
        {
            Debug.Assert(distance.Competition != null, "distance.Competition != null");

            var expert = distanceExpertManager.Find(distance.Discipline);
            var transponderSetsPerRace = expert?.TransponderSetsPerRace ?? int.MaxValue;

            using (var transaction = context.UseOrBeginTransaction(IsolationLevel.RepeatableRead))
                try
                {
                    var sets = new Queue<TransponderSet>();
                    foreach (var bagName in bagNames)
                    {
                        var transponderSets = await (from s in context.TransponderBagSets
                                                      where s.LicenseIssuerId == distance.Competition.LicenseIssuerId && s.Discipline == distance.Competition.Discipline
                                                          && s.BagName == bagName
                                                      orderby s.SetNumber
                                                      select s.Set).Include(s => s.Transponders.Select(t => t.Transponder)).ToListAsync();
                        foreach (var set in transponderSets)
                            sets.Enqueue(set);
                    }

                    var races = await Races(distance.CompetitionId, distance.Id).Include(r => r.Competitor).Include(r => r.Transponders).ToListAsync();
                    await context.TeamCompetitorMembers.Include(tcm => tcm.Member).Where(tcm => tcm.Team.Races.Any(r => r.DistanceId == distance.Id)).LoadAsync();

                    foreach (var race in races)
                    {
                        race.Transponders.Clear();
                        await context.SaveChangesAsync();

                        var personCompetitor = race.Competitor as PersonCompetitor;
                        var people = new List<Guid>();
                        if (personCompetitor != null)
                            people.Add(personCompetitor.PersonId);
                        else
                        {
                            var teamCompetitor = race.Competitor as TeamCompetitor;
                            if (teamCompetitor?.Members != null)
                                people.AddRange(teamCompetitor.Members.OrderBy(m => m.Order).Select(m => m.Member.PersonId));
                        }

                        int transpondersPerRace = 0;
                        foreach (var personId in people)
                        {
                            transpondersPerRace++;
                            if (sets.Count == 0 || transpondersPerRace > transponderSetsPerRace)
                                break;

                            var set = sets.Dequeue();

                            foreach (var setTransponder in set.Transponders)
                                race.Transponders.Add(new RaceTransponder
                                {
                                    PersonId = personId,
                                    Transponder = setTransponder.Transponder,
                                    Set = set.Number
                                });
                        }
                        await context.SaveChangesAsync();
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
}