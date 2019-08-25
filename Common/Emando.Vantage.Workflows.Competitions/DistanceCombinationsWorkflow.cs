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
    public class DistanceCombinationsWorkflow : IDisposable
    {
        private readonly ICompetitionContext context;
        private readonly IEventRecorder recorder;
        private bool isDisposed;

        public DistanceCombinationsWorkflow(ICompetitionContext context, IEventRecorder recorder)
        {
            this.context = context;
            this.recorder = recorder;
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

        public IQueryable<Competition> Competitions => context.Competitions;
        
        public IQueryable<DistanceCombination> Combinations(Guid competitionId)
        {
            return from dc in context.DistanceCombinations
                   where dc.CompetitionId == competitionId
                   orderby dc.Number
                   select dc;
        }

        public IQueryable<CompetitorBase> Competitors(Guid competitionId)
        {
            return from c in context.Competitors
                   where c.List.CompetitionId == competitionId
                   select c;
        }

        public IQueryable<Race> Races(Guid competitionId, Guid combinationId, Guid competitorId)
        {
            return from r in context.Races.Include(r => r.Results).Include(r => r.Times)
                   where r.Distance.CompetitionId == competitionId && r.CompetitorId == competitorId && r.Distance.Combinations.Any(c => c.Id == combinationId)
                   orderby r.Distance.Number
                   select r;
        }

        public async Task AddCombinationAsync(DistanceCombination combination)
        {
            combination.Id = Guid.NewGuid();
            context.DistanceCombinations.Add(combination);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCombinationAsync(DistanceCombination combination)
        {
            context.DistanceCombinations.Remove(combination);
            await context.SaveChangesAsync();
        }

        public async Task UpdateCombinationDistancesAsync(DistanceCombination current, ICollection<Guid> distances)
        {
            using (var transaction = context.UseOrBeginTransaction(IsolationLevel.RepeatableRead))
                try
                {
                    current.Distances.Clear();
                    foreach (var distance in from d in context.Distances
                                             where d.CompetitionId == current.CompetitionId && distances.Contains(d.Id)
                                             select d)
                        current.Distances.Add(distance);

                    await context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
        }

        public async Task<DistanceCombination> CloneDistanceCombinationAsync(DistanceCombination current, DistanceCombinationCloneSettings settings, Guid? competitionId = null, TimeSpan? shiftTimes = null)
        {
            competitionId = competitionId ?? current.CompetitionId;

            int number;
            if (competitionId != current.CompetitionId)
                number = current.Number;
            else
                number = await Combinations(competitionId.Value).Select(d => d.Number).DefaultIfEmpty(0).MaxAsync() + 1;

            var clone = new DistanceCombination
            {
                CompetitionId = competitionId.Value,
                Number = number,
                Name = current.Name,
                ClassFilter = current.ClassFilter,
                CategoryFilter = current.CategoryFilter,
                ClassificationWeight = current.ClassificationWeight,
                Starts = current.Starts + (shiftTimes ?? TimeSpan.Zero)
            };
            await AddCombinationAsync(clone);
            return clone;
        }

        public IQueryable<DistanceCombinationCompetitor> Competitors(Guid competitionId, Guid combinationId)
        {
            return from c in context.DistanceCombinationCompetitors
                   where c.DistanceCombination.CompetitionId == competitionId && c.DistanceCombinationId == combinationId
                   select c;
        }

        public async Task UpdateDistanceCombinationCompetitorAsync(DistanceCombinationCompetitor competitor)
        {
            await context.SaveChangesAsync();
        }

        public IQueryable<DistanceCombinationCompetitor> Combinations(Guid competitionId, Guid competitorId)
        {
            return from c in context.DistanceCombinationCompetitors
                   where c.DistanceCombination.CompetitionId == competitionId && c.CompetitorId == competitorId
                   select c;
        }

        public async Task<DistanceCombinationCompetitor> AddNewCompetitorAsync(Guid competitorId, Guid distanceCombinationId, int? reserve,
            DistanceCombinationCompetitorStatus status)
        {
            using (var transaction = context.UseOrBeginTransaction(IsolationLevel.RepeatableRead))
                try
                {
                    var competitor = await context.Competitors.FirstOrDefaultAsync(c => c.Id == competitorId);
                    if (competitor == null)
                        throw new CompetitorNotFoundException();

                    var combination = await context.DistanceCombinations.FirstOrDefaultAsync(c => c.Id == distanceCombinationId);
                    if (combination == null)
                        throw new DistanceCombinationNotFoundException();

                    CategoryFilter.EnsureMatch(combination.CategoryFilter, competitor.Category);
                    ClassFilter.EnsureMatch(combination.ClassFilter, competitor.Class);

                    var combinationCompetitor = new DistanceCombinationCompetitor
                    {
                        DistanceCombination = combination,
                        Competitor = competitor,
                        Reserve = reserve,
                        Status = status
                    };

                    context.DistanceCombinationCompetitors.Add(combinationCompetitor);
                    await context.SaveChangesAsync();

                    recorder.RecordEvent(new DistanceCombinationClassificationChangedEvent(combination));

                    transaction.Commit();
                    return combinationCompetitor;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
        }

        public async Task UpdateCompetitorAsync(Guid competitionId, Guid competitorId, IReadOnlyCollection<DistanceCombinationCompetitor> competitorCombinations)
        {
            using (var transaction = context.BeginTransaction(IsolationLevel.RepeatableRead))
                try
                {
                    var competitor = await context.Competitors.FirstOrDefaultAsync(c => c.Id == competitorId);
                    if (competitor == null)
                        throw new CompetitorNotFoundException();

                    foreach (var combination in await Combinations(competitionId, competitorId).ToListAsync())
                        context.DistanceCombinationCompetitors.Remove(combination);

                    var combinations = (from c in competitorCombinations
                                        join dc in await Combinations(competitionId).ToListAsync() on c.DistanceCombinationId equals dc.Id
                                        select new
                                        {
                                            Competitor = c,
                                            Combination = dc
                                        }).ToList();

                    foreach (var c in combinations)
                    {
                        CategoryFilter.EnsureMatch(c.Combination.CategoryFilter, competitor.Category);
                        ClassFilter.EnsureMatch(c.Combination.ClassFilter, competitor.Class);

                        c.Competitor.Competitor = competitor;
                        context.DistanceCombinationCompetitors.Add(c.Competitor);

                        if (c.Competitor.Status == DistanceCombinationCompetitorStatus.Withdrawn)
                        {
                            var competitorRaces = await (from r in context.Races
                                                         where r.Distance.Combinations.Any(dc => dc.Id == c.Combination.Id) && r.CompetitorId == c.Competitor.CompetitorId
                                                         select r).ToListAsync();
                            foreach (var race in competitorRaces)
                                context.Races.Remove(race);
                        }
                    }

                    if (competitorCombinations.Any(cc => cc.Status == DistanceCombinationCompetitorStatus.Confirmed || cc.Status == DistanceCombinationCompetitorStatus.Withdrawn))
                        competitor.Status = CompetitorStatus.Confirmed;
                    else if (competitorCombinations.All(cc => cc.Status == DistanceCombinationCompetitorStatus.Pending))
                        competitor.Status = CompetitorStatus.Pending;

                    await context.SaveChangesAsync();
                    
                    foreach (var combination in combinations.Select(c => c.Combination))
                        recorder.RecordEvent(new DistanceCombinationClassificationChangedEvent(combination));

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
        }

        public async Task DeleteCompetitorAsync(DistanceCombinationCompetitor combinationCompetitor)
        {
            Debug.Assert(combinationCompetitor.DistanceCombination != null, "combinationCompetitor.DistanceCombination != null");

            using (var transaction = context.BeginTransaction(IsolationLevel.RepeatableRead))
                try
                {
                    context.DistanceCombinationCompetitors.Remove(combinationCompetitor);
                    await context.SaveChangesAsync();
                    
                    recorder.RecordEvent(new DistanceCombinationClassificationChangedEvent(combinationCompetitor.DistanceCombination));

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