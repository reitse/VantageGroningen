using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Competitions;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{

    public abstract class DistanceDisciplineExpertBase : IDistanceDisciplineExpert
    {
        #region IDistanceDisciplineExpert Members

        public abstract IDisciplineCalculator DisciplineCalculator { get; }

        public abstract IDistanceDisciplineCalculator Calculator { get; }

        public abstract bool FixedLanes { get; }

        public virtual IComparer<TimeInvalidReason?> TimeInvalidReasonComparer => DefaultTimeInvalidReasonComparer.Default;

        public abstract bool LastLapIsAlwaysFinal { get; }

        public virtual IQueryable<T> SelectCompetitorsForRound<T>(Distance distance, int round, IQueryable<T> competitors) where T : CompetitorBase
        {
            return competitors;
        }

        public virtual IReadOnlyList<DrawCompetitor> SortDrawCompetitors(Distance distance, IEnumerable<DrawCompetitor> competitors, DistanceDrawSettings settings,
            IReadOnlyList<PersonCategory> categories)
        {
            return competitors.ToList().AsReadOnly();
        }

        public virtual IReadOnlyList<IReadOnlyList<DrawCompetitor>> GroupDrawCompetitors(Distance distance, int round, IReadOnlyList<DrawCompetitor> competitors,
            DistanceDrawSettings settings)
        {
            var groups = new List<IReadOnlyList<DrawCompetitor>>();
            switch (settings.GroupMode)
            {
                case DistanceDrawGroupMode.Category:
                    groups.AddRange(competitors.GroupBy(c => c.Competitor.Category.Substring(0, settings.CategoryLength)).Select(g => g.ToList()));
                    break;

                case DistanceDrawGroupMode.Time:
                    var count = 0;
                    while (count < competitors.Count)
                    {
                        var group = competitors.Skip(count).Take(settings.GroupSize).ToList().AsReadOnly();
                        groups.Add(@group);
                        count += @group.Count;
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            return groups.AsReadOnly();
        }

        public virtual Task DrawCompetitorGroupAsync(Distance distance, IReadOnlyCollection<Guid> distanceCombinations, int round, IList<CompetitorBase> competitors, ICompetitionContext context, DistanceDrawSettings settings)
        {
            throw new DrawModeNotSupportedException();
        }

        public abstract Task<IReadOnlyDictionary<int, IReadOnlyCollection<Race>>> FillCompetitorsInHeatsAsync(Distance distance, IReadOnlyCollection<Guid> distanceCombinations, int round, IReadOnlyList<IReadOnlyList<CompetitorBase>> competitorGroups, ICompetitionContext context, DistanceDrawSettings settings);

        public virtual void OnUpdatingDistance(Distance distance, Race race)
        {
        }

        public virtual void OnMovingRace(Distance distance, Race race)
        {
        }

        public virtual void OnAddingRace(Distance distance, Race race)
        {
        }

        public virtual void OnCopyingRace(Distance distance, Race original, Race copy, RacesCopySettings settings)
        {
        }

        public virtual int TransponderSetsPerRace => int.MaxValue;

        #endregion
    }
}