using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities;
using Emando.Vantage.Entities.Competitions;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public interface IDistanceDisciplineExpert
    {
        IDisciplineCalculator DisciplineCalculator { get; }

        IDistanceDisciplineCalculator Calculator { get; }

        bool FixedLanes { get; }

        IComparer<TimeInvalidReason?> TimeInvalidReasonComparer { get; }

        bool LastLapIsAlwaysFinal { get; }

        IQueryable<T> SelectCompetitorsForRound<T>(Distance distance, int round, IQueryable<T> competitors)
            where T : CompetitorBase;

        IReadOnlyList<DrawCompetitor> SortDrawCompetitors(Distance distance, IEnumerable<DrawCompetitor> competitors, DistanceDrawSettings settings,
            IReadOnlyList<PersonCategory> categories);

        IReadOnlyList<IReadOnlyList<DrawCompetitor>> GroupDrawCompetitors(Distance distance, int round, IReadOnlyList<DrawCompetitor> competitors,
            DistanceDrawSettings settings);

        Task DrawCompetitorGroupAsync(Distance distance, IReadOnlyCollection<Guid> distanceCombinations, int round, IList<CompetitorBase> competitors,
            ICompetitionContext context, DistanceDrawSettings settings);

        Task<IReadOnlyDictionary<int, IReadOnlyCollection<Race>>> FillCompetitorsInHeatsAsync(Distance distance, IReadOnlyCollection<Guid> distanceCombinations, int round,
            IReadOnlyList<IReadOnlyList<CompetitorBase>> competitorGroups, ICompetitionContext context, DistanceDrawSettings settings);

        void OnUpdatingDistance(Distance distance, Race race);

        void OnMovingRace(Distance distance, Race race);

        void OnAddingRace(Distance distance, Race race);

        void OnCopyingRace(Distance distance, Race original, Race copy, RacesCopySettings settings);

        int TransponderSetsPerRace { get; }
    }
}