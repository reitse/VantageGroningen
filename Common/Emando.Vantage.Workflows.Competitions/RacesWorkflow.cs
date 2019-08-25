using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Competitions;
using Emando.Vantage.Components;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;
using Emando.Vantage.Workflows.Competitions.Events;

namespace Emando.Vantage.Workflows.Competitions
{
    public class RacesWorkflow : IDisposable
    {
        private const string UserInstanceName = "User";
        private readonly ICompetitionContext context;
        private readonly IDistanceDisciplineExpertManager expertManager;
        private readonly IEventRecorder recorder;
        private bool isDisposed;

        public RacesWorkflow(ICompetitionContext context, IDistanceDisciplineExpertManager expertManager, IEventRecorder recorder)
        {
            this.context = context;
            this.expertManager = expertManager;
            this.recorder = recorder;

            context.ProxyCreationEnabled = false;
            context.LazyLoadingEnabled = false;
        }

        public IQueryable<Competition> Competitions => context.Competitions;

        public IQueryable<Distance> Distances => context.Distances;

        public IDistanceDisciplineExpert FindExpert(string discipline)
        {
            return expertManager.Find(discipline);
        }

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

        ~RacesWorkflow()
        {
            Dispose(false);
        }

        public IQueryable<Race> Races(Guid competitionId)
        {
            return from r in context.Races
                   where r.Competitor.List.CompetitionId == competitionId
                   select r;
        }

        public IQueryable<Race> Races(Guid competitionId, Guid distanceId)
        {
            return from r in context.Races
                   where r.Distance.CompetitionId == competitionId && r.DistanceId == distanceId
                   orderby r.Round, r.Heat, r.Lane
                   select r;
        }

        public IQueryable<Race> CompetitorRaces(Guid competitionId, Guid competitorId)
        {
            return from r in context.Races
                   where r.Distance.CompetitionId == competitionId && r.CompetitorId == competitorId
                   select r;
        }

        public IQueryable<DistanceCombination> DistanceCombinations(Guid competitionId)
        {
            return from dc in context.DistanceCombinations
                   where dc.CompetitionId == competitionId
                   orderby dc.Number
                   select dc;
        }

        public IQueryable<DistanceCombination> DistanceCombinations(Guid competitionId, Guid distanceId)
        {
            return from dc in context.DistanceCombinations
                   where dc.CompetitionId == competitionId && dc.Distances.Any(d => d.Id == distanceId)
                   orderby dc.Number
                   select dc;
        }

        public IQueryable<DistanceCombination> DistanceCombinations(Guid competitionId, Guid distanceId, Guid competitorId)
        {
            return from cdc in context.DistanceCombinationCompetitors
                   where cdc.DistanceCombination.CompetitionId == competitionId && cdc.CompetitorId == competitorId
                       && cdc.DistanceCombination.Distances.Any(d => d.Id == distanceId)
                   select cdc.DistanceCombination;
        }

        public async Task<IList<Race>> GetDistanceDrawAsync(Distance distance)
        {
            return await (from r in Races(distance.CompetitionId, distance.Id).Include(r => r.Competitor).Include(r => r.Results).Include(r => r.Times)
                          orderby r.Round, r.Heat, r.Lane
                          select r).ToListAsync();
        }

        public async Task<IList<RankedRace>> GetDistanceResultAsync(Distance distance)
        {
            var expert = expertManager.Find(distance.Discipline);
            if (expert == null)
                throw new InvalidDisciplineException();

            var races = await Races(distance.CompetitionId, distance.Id)
                .Include(r => r.Competitor)
                .Include(r => r.Results)
                .Include(r => r.Times).ToListAsync();

            races = races.Where(r => r.PresentedResult?.Status == RaceStatus.Done)
                .Where(r => !races.Any(or => or != r && or.CompetitorId == r.CompetitorId && or.PresentedTime?.Time < r.PresentedTime?.Time))
                .OrderBy(r => r.PresentedTime?.TimeInfo.HasFlag(TimeInfo.OutOfCompetition) == true)
                .ThenBy(r => r.PresentedResult.TimeInvalidReason, expert.TimeInvalidReasonComparer)
                .ThenBy(r => r.PresentedTime?.Time)
                .ToList();

            var points = distance.DistancePointsTableId.HasValue
                ? await context.DistancePoints
                    .Where(p => p.DistancePointsTable.Id == distance.DistancePointsTableId.Value && p.Type == "Finish")
                    .ToDictionaryAsync(p => p.Ranking, p => p.Points)
                : new Dictionary<int, decimal>();

            var rankedRaces = new List<RankedRace>();
            var previousTime = new TimeSpan?();
            for (var i = 0; i < races.Count; i++)
            {
                var race = races[i];
                var time = race.PresentedTime?.Time.Truncate(distance.ClassificationPrecision);
                var ranking = race.PresentedResult?.TimeInvalidReason == null && race.PresentedTime?.TimeInfo.HasFlag(TimeInfo.OutOfCompetition) != true
                    ? i + 1
                    : new int?();

                decimal totalPoints;
                if (!ranking.HasValue || !points.TryGetValue(ranking.Value, out totalPoints))
                    totalPoints = 0;

                rankedRaces.Add(new RankedRace(ranking, race, totalPoints, time == previousTime));
                previousTime = time;
            }
            return rankedRaces;
        }

        public async Task<IList<PointsRankedRace>> GetDistanceResultByLapPointsAsync(Distance distance)
        {
            var expert = expertManager.Find(distance.Discipline);
            if (expert == null)
                throw new InvalidDisciplineException();

            var races = await Races(distance.CompetitionId, distance.Id)
                .Include(r => r.Competitor)
                .Include(r => r.Results)
                .Include(r => r.Times)
                .Include(r => r.Laps).ToListAsync();

            var racesWithPoints = races.Where(r => r.PresentedResult?.Status == RaceStatus.Done)
                .Select(r =>
                {
                    var calculatedLaps = r.PresentedLaps?.Select((l, li) => new
                    {
                        Index = li,
                        l.Points
                    }).ToList();
                    var lapPoints = calculatedLaps?.Where(l => l.Points > 0).ToDictionary(l => l.Index, l => l.Points.Value) ?? new Dictionary<int, decimal>();
                    return new
                    {
                        Race = r,
                        LapCount = calculatedLaps?.Count ?? 0,
                        LapPoints = lapPoints,
                        TotalPoints = lapPoints.Values.Sum()
                    };
                })
                .OrderBy(r => r.Race.PresentedResult.TimeInvalidReason, expert.TimeInvalidReasonComparer)
                .ThenByDescending(r => r.LapCount)
                .ThenByDescending(r => r.TotalPoints)
                .ThenBy(r => r.Race.PresentedTime?.Time)
                .ToList();

            var rankedRaces = new List<PointsRankedRace>();
            var previousTime = new TimeSpan?();
            var previousPoints = new decimal?();
            var previousLapCount = 0;
            for (var i = 0; i < racesWithPoints.Count; i++)
            {
                var raceWithPoints = racesWithPoints[i];
                var race = raceWithPoints.Race;
                var ranking = i + 1;
                var time = race.PresentedResult.TimeInvalidReason == null
                    ? race.PresentedTime?.Time.Truncate(distance.ClassificationPrecision)
                    : race.PresentedLaps.LastOrDefault()?.Time.Truncate(distance.ClassificationPrecision);
                var points = race.PresentedResult.TimeInvalidReason == null
                    ? raceWithPoints.TotalPoints
                    : 0;

                var sameRankingAsPrevious = raceWithPoints.LapCount == previousLapCount
                    && points == previousPoints
                    && time == previousTime;
                rankedRaces.Add(new PointsRankedRace(ranking, race, raceWithPoints.LapPoints, raceWithPoints.TotalPoints, raceWithPoints.LapCount,
                    sameRankingAsPrevious));

                previousTime = time;
                previousPoints = points;
                previousLapCount = raceWithPoints.LapCount;
            }
            return rankedRaces;
        }

        public async Task PresentInstanceResultAsync(Race race, string instanceName)
        {
            Debug.Assert(race.Distance != null, "race.Distance != null");

            using (var transaction = context.BeginTransaction(IsolationLevel.RepeatableRead))
                try
                {
                    race.PresentedInstanceName = instanceName;
                    await context.SaveChangesAsync();

                    foreach (var combination in await DistanceCombinations(race.Distance.CompetitionId, race.DistanceId).ToListAsync())
                        recorder.RecordEvent(new DistanceCombinationClassificationChangedEvent(combination));

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
        }

        public async Task LoadPassingsAsync(Race race, string instanceName = null)
        {
            instanceName = instanceName ?? race.PresentedInstanceName;
            await context.LoadAsync(race, r => r.Passings, p => p.InstanceName == instanceName);
        }

        public async Task LoadTeamCompetitorMembersAsync(TeamCompetitor competitor)
        {
            var query = from tcm in context.TeamCompetitorMembers.Include(m => m.Member)
                        where tcm.TeamId == competitor.Id
                        select tcm;
            await query.LoadAsync();
        }

        public async Task PresentUserResultAsync(Race race, RaceStatus status, TimeInvalidReason? timeInvalidReason, TimeSpan? time, TimeInfo timeInfo,
            IList<TimeSpan> lapTimes)
        {
            Debug.Assert(race.Distance != null, "race.Distance != null");

            if (status == RaceStatus.Done && !timeInvalidReason.HasValue && !time.HasValue)
                throw new InvalidRaceResultException();

            using (var transaction = context.BeginTransaction(IsolationLevel.RepeatableRead))
                try
                {
                    foreach (var raceResult in race.Results.Where(r => r.InstanceName == UserInstanceName).ToList())
                        context.RaceResults.Remove(raceResult);
                    foreach (var raceTime in race.Times.Where(r => r.InstanceName == UserInstanceName).ToList())
                        context.RaceTimes.Remove(raceTime);
                    foreach (var raceLap in race.Laps.Where(l => l.InstanceName == UserInstanceName).ToList())
                        context.RaceLaps.Remove(raceLap);

                    race.PresentedInstanceName = UserInstanceName;

                    var result = new RaceResult
                    {
                        Race = race,
                        InstanceName = UserInstanceName,
                        Status = status,
                        TimeInvalidReason = timeInvalidReason
                    };

                    if (time.HasValue)
                    {
                        var raceTime = new RaceTime
                        {
                            Race = race,
                            PresentationSource = PresentationSource.User,
                            InstanceName = UserInstanceName,
                            Time = time.Value,
                            TimeInfo = timeInfo
                        };
                        race.Times.Add(raceTime);

                        await UpdatePointsAsync(result, raceTime);

                        if (lapTimes != null)
                        {
                            var now = DateTime.UtcNow;
                            foreach (var lapTime in lapTimes)
                                race.Laps.Add(new RaceLap
                                {
                                    Race = race,
                                    PresentationSource = PresentationSource.User,
                                    InstanceName = UserInstanceName,
                                    Time = lapTime,
                                    When = now - time.Value + lapTime,
                                    Flags = RaceEventFlags.Inserted | RaceEventFlags.Present
                                });
                        }
                    }
                    race.Results.Add(result);
                    race.Distance.LastRaceCommitted = DateTime.UtcNow;

                    await context.SaveChangesAsync();

                    foreach (var combination in await DistanceCombinations(race.Distance.CompetitionId, race.DistanceId).ToListAsync())
                        recorder.RecordEvent(new DistanceCombinationClassificationChangedEvent(combination));

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
        }

        public async Task UpdatePointsAsync(RaceResult result, RaceTime raceTime)
        {
            // TODO: The first DC is selected when a competitor is part of multiple distance combinations that use this distance. This is an unwanted edge case anyway
            var distance = result.Race.Distance;
            var distanceCombination = await DistanceCombinations(distance.CompetitionId, result.Race.DistanceId, result.Race.CompetitorId).FirstOrDefaultAsync();
            if (distanceCombination != null)
            {
                var expert = expertManager.Find(distance.Discipline);
                if (expert != null)
                {
                    var canCalculateRacePoints = expert.Calculator.CanCalculateRacePoints(distance, distanceCombination.ClassificationWeight,
                        distance.ClassificationPrecision, result, raceTime);
                    if (canCalculateRacePoints)
                    {
                        result.Points = expert.Calculator.CalculateRacePoints(distance, distanceCombination.ClassificationWeight, distance.ClassificationPrecision, result,
                            raceTime);
                        return;
                    }
                }
            }

            result.Points = null;
        }

        public async Task UpdateTranspondersAsync(Race race, IList<RaceTransponder> transponders)
        {
            foreach (var transponder in race.Transponders.ToList())
                race.Transponders.Remove(transponder);

            foreach (var transponder in transponders)
                race.Transponders.Add(transponder);

            await context.SaveChangesAsync();
        }

        public async Task<ICollection<Classification>> GetClassificationAsync(Guid competitionId, int classificationWeight, int categoryLength, int? behindDistance = null,
            params IRaceSelector[] selectors)
        {
            var query = Races(competitionId).Include(r => r.Distance).Include(r => r.Competitor).Include(r => r.Results).Include(r => r.Times);
            query = selectors.Aggregate(query, (current, selector) => selector.Query(current));
            var races = await query.ToListAsync();

            var competitors = races.Where(r => r.PresentedResult?.Status == RaceStatus.Done)
                .Select(r => r.Competitor)
                .Distinct()
                .ToDictionary(c => c, c => new ClassifiedCompetitor(c));

            IDistanceDisciplineExpert expert = null;

            var distances = races.GroupBy(r => r.Distance).OrderBy(r => r.Key.Number).ToList();
            for (int i = 0; i < distances.Count; i++)
            {
                var distance = distances[i];

                var distanceExpert = expertManager.Find(distance.Key.Discipline);
                if (distanceExpert == null)
                    throw new InvalidDisciplineException();

                if (expert == null)
                    expert = distanceExpert;
                else if (expert.GetType() != distanceExpert.GetType())
                    throw new InvalidDisciplineException();

                var distanceRaces = distance
                    .Where(r => r.PresentedResult?.Status == RaceStatus.Done && r.PresentedTime?.TimeInfo.HasFlag(TimeInfo.OutOfCompetition) != true)
                    .OrderBy(r => r, Race.PresentedTimeComparer).ToList();
                var previousTime = new TimeSpan?();
                var previousRanking = new int?();
                for (var j = 0; j < distanceRaces.Count; j++)
                {
                    var race = distanceRaces[j];
                    var time = race.PresentedTime?.Time.Truncate(distance.Key.ClassificationPrecision);
                    var ranking = time == previousTime ? previousRanking : j + 1;
                    previousTime = time;
                    previousRanking = ranking;

                    var competitor = competitors[race.Competitor];
                    if (expert.Calculator.CanCalculateRacePoints(distance.Key, classificationWeight, distance.Key.ClassificationPrecision, race.PresentedResult,
                        race.PresentedTime))
                    {
                        var points = expert.Calculator.CalculateRacePoints(distance.Key, classificationWeight, distance.Key.ClassificationPrecision, race.PresentedResult,
                            race.PresentedTime);
                        competitor.Points += points;
                        competitor.Races.Add(new ClassifiedRace(race, ranking));
                    }
                    else
                        competitor.Races.Add(new ClassifiedRace(race, null));
                }
                
                foreach (var competitor in competitors.Values)
                    if (competitor.Races.Count == i)
                        competitor.Races.Add(null);
            }

            var classifications = new List<Classification>();
            foreach (var categoryCompetitors in competitors.Values.GroupBy(c => c.Competitor.Category.Substring(0, categoryLength)).OrderBy(g => g.Key))
            {
                var rankedCompetitors = new List<ClassifiedCompetitor>();
                var offset = 0;
                foreach (var group in categoryCompetitors.GroupBy(c => c.Races.TakeWhile(r => r != null).Count()).OrderByDescending(g => g.Key))
                {
                    var firstPoints = new decimal?();
                    var previousPoints = new decimal?();
                    foreach (var valid in group.Where(c => c.AllValid).OrderBy(c => c.Points))
                    {
                        valid.Ranking = valid.Points != previousPoints ? offset + 1 : new int?();
                        valid.TimeBehind = behindDistance.HasValue && previousPoints.HasValue
                            ? expert.Calculator.PointsToDistanceTime(behindDistance.Value, classificationWeight, valid.Points - firstPoints.Value)
                            : null;

                        firstPoints = firstPoints ?? valid.Points;
                        previousPoints = valid.Points;
                        rankedCompetitors.Add(valid);
                        offset++;
                    }

                    foreach (var invalid in group.Where(c => !c.AllEmpty && !c.AllValid)
                        .OrderByDescending(c => c.InvalidSortGroup)
                        .ThenBy(c => c.Races.Select(r => r?.Race.PresentedResult?.TimeInvalidReason).ToArray(), TimeInvalidReasonClassificationComparer.Default)
                        .ThenBy(c => c.Points))
                    {
                        invalid.Ranking = null;
                        rankedCompetitors.Add(invalid);
                    }
                }

                classifications.Add(new Classification(distances.Select(d => d.Key).ToList(), rankedCompetitors, categoryCompetitors.Key));
            }

            return classifications;
        }
    }

    public class TimeInvalidReasonClassificationComparer : IComparer<IReadOnlyCollection<TimeInvalidReason?>>
    {
        public static TimeInvalidReasonClassificationComparer Default { get; } = new TimeInvalidReasonClassificationComparer();

        public int Compare(IReadOnlyCollection<TimeInvalidReason?> x, IReadOnlyCollection<TimeInvalidReason?> y)
        {
            var notFinishedX = x.Count(r => r == TimeInvalidReason.NotFinished);
            var notFinishedY = y.Count(r => r == TimeInvalidReason.NotFinished);
            if (notFinishedX != notFinishedY)
                return notFinishedX > notFinishedY ? -1 : 1;
            
            var disqualifiedX = x.Count(r => r == TimeInvalidReason.Disqualified);
            var disqualifiedY = y.Count(r => r == TimeInvalidReason.Disqualified);
            if (disqualifiedX != disqualifiedY)
                return disqualifiedX > disqualifiedY ? -1 : 1;

            var notStartedX = x.Count(r => r == TimeInvalidReason.NotStarted);
            var notStartedY = y.Count(r => r == TimeInvalidReason.NotStarted);
            if (notStartedX != notStartedY)
                return notStartedX > notStartedY ? -1 : 1;

            var withdrawnX = x.Count(r => r == TimeInvalidReason.Withdrawn);
            var withdrawnY = y.Count(r => r == TimeInvalidReason.Withdrawn);
            if (withdrawnX != withdrawnY)
                return withdrawnX > withdrawnY ? -1 : 1;

            return 0;
        }
    }
}