using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Competitions;
using Emando.Vantage.Competitions.SpeedSkating.LongTrack;
using Emando.Vantage.Components;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Components.Competitions.SpeedSkating.LongTrack;
using Emando.Vantage.Entities;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack
{
    public abstract class PairsDistanceDisciplineExpertBase : LongTrackDistanceDisciplineExpertBase
    {
        public override bool FixedLanes => true;

        public override bool LastLapIsAlwaysFinal => true;

        public override IReadOnlyList<DrawCompetitor> SortDrawCompetitors(Distance distance, IEnumerable<DrawCompetitor> competitors, DistanceDrawSettings settings,
            IReadOnlyList<PersonCategory> categories)
        {
            switch (settings.GroupMode)
            {
                case DistanceDrawGroupMode.Category:
                    return competitors.OrderByDescending(c => categories.FirstOrDefault(ca => ca.Code == c.Competitor.Category)?.FromAge ?? int.MaxValue)
                        .ThenByDescending(c => c.Competitor.Category.Substring(0, 1))
                        .ToList()
                        .AsReadOnly();
                case DistanceDrawGroupMode.Time:
                    return competitors.OrderBy(c => c.Time?.Time ?? TimeSpan.MaxValue).ToList().AsReadOnly();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override async Task<IReadOnlyDictionary<int, IReadOnlyCollection<Race>>> FillCompetitorsInHeatsAsync(Distance distance,
            IReadOnlyCollection<Guid> distanceCombinations, int round, IReadOnlyList<IReadOnlyList<CompetitorBase>> competitorGroups, ICompetitionContext context,
            DistanceDrawSettings settings)
        {
            var competitors = competitorGroups.SelectMany(g => g).ToList();
            if (!settings.ReverseGroups)
                competitors.Reverse();

            var pairCount = (competitors.Count + 1) / 2;
            var hasFillPair = distance.StartMode == DistanceStartMode.MultipleHeats && (pairCount % 2) == 1;
            if (hasFillPair)
                pairCount++;

            var firstHeat = distance.Races.Max(r => new int?(r.Heat)) + 1 ?? distance.FirstHeat;

            switch (settings.Mode)
            {
                case DistanceDrawMode.Random:
                    return FillHeatsByCompetitorsOrder(firstHeat, pairCount, hasFillPair, competitors, settings.ReverseFilling, settings.Spreading);

                case DistanceDrawMode.SwitchLanesOrRandom:
                    if (distanceCombinations.Count != 1)
                        throw new DrawModeNotSupportedException();

                    var distanceCombinationId = distanceCombinations.Single();
                    var previousDistance = await (from d in context.Distances
                                                  where d.CompetitionId == distance.CompetitionId && d.Number < distance.Number
                                                    && d.Combinations.Any(dc => dc.Id == distanceCombinationId)
                                                  orderby d.Number descending
                                                  select new
                                                  {
                                                      d.Id,
                                                      FirstPair = d.FirstHeat
                                                  }).FirstOrDefaultAsync();
                    if (previousDistance == null)
                        return FillHeatsByCompetitorsOrder(firstHeat, pairCount, hasFillPair, competitors, settings.ReverseFilling, settings.Spreading);

                    var competitorLookup = competitors.ToDictionary(c => c.Id);
                    var previousPairs = await (from r in context.Races
                                               where r.DistanceId == previousDistance.Id
                                               group r by r.Heat
                                               into heat
                                               select new
                                               {
                                                   Pair = heat.Key,
                                                   Races = from r in heat
                                                           select new
                                                           {
                                                               r.Lane,
                                                               r.CompetitorId
                                                           }
                                               }).ToListAsync();

                    var seed = previousPairs.ToDictionary(p => p.Pair - previousDistance.FirstPair + firstHeat,
                        p => p.Races.Where(r => competitorLookup.ContainsKey(r.CompetitorId)).ToDictionary(r => (r.Lane + 1) % 2, r => competitorLookup[r.CompetitorId]));

                    return FillHeatsByFixedLanes(firstHeat, pairCount, seed);

                default:
                    throw new DrawModeNotSupportedException();
            }
        }

        private static IReadOnlyDictionary<int, IReadOnlyCollection<Race>> FillHeatsByCompetitorsOrder(int firstPair, int pairCount, bool hasFillPair,
            IReadOnlyCollection<CompetitorBase> competitors, bool reverse = false, DistanceDrawSpreading spreading = DistanceDrawSpreading.None)
        {
            var pairs = new Dictionary<int, List<Race>>();
            var count = 0;
            for (var pair = 1; pair <= pairCount; pair++)
            {
                IEnumerable<CompetitorBase> heatCompetitors;
                if (!reverse && pair == 1 && (competitors.Count % 2) == 1)
                    heatCompetitors = competitors.Take(1);
                else if (hasFillPair && (!reverse && pair == 2 || reverse && pair == pairCount - 1))
                    heatCompetitors = Enumerable.Empty<CompetitorBase>();
                else
                    heatCompetitors = competitors.Skip(count).Take(2);

                var races = new List<Race>();
                var lane = 0;
                foreach (var competitor in heatCompetitors)
                {
                    races.Add(new Race
                    {
                        Lane = lane,
                        Competitor = competitor
                    });
                    lane++;
                }

                pairs.Add(firstPair + pair - 1, races);
                count += races.Count;
            }

            SpreadPairs(pairs, spreading);

            return new ReadOnlyDictionary<int, IReadOnlyCollection<Race>>(pairs.ToDictionary(p => p.Key, p => (IReadOnlyCollection<Race>)p.Value.AsReadOnly()));
        }

        private static void SpreadPairs(IDictionary<int, List<Race>> pairs, DistanceDrawSpreading spreading)
        {
            if (pairs.Count == 0)
                return;

            Func<Race, Race, bool> equal;
            switch (spreading)
            {
                case DistanceDrawSpreading.None:
                    return;
                case DistanceDrawSpreading.Clubs:
                    equal = (x, y) => x.Competitor.ClubCode == y.Competitor.ClubCode;
                    break;
                case DistanceDrawSpreading.Nationalities:
                    equal = (x, y) => x.Competitor.NationalityCode == y.Competitor.NationalityCode;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(spreading), spreading, null);
            }

            var lowPair = pairs.Keys.Min();
            var highPair = pairs.Keys.Max();
            foreach (var pair in pairs)
            {
                if (pair.Value.Count < 2 || !equal(pair.Value[0], pair.Value[1]))
                    continue;

                for (var i = -1; pair.Key + i >= lowPair && pair.Key + i <= highPair; i = i * -1 - Math.Max(0, Math.Sign(i)))
                {
                    var otherPair = pairs[pair.Key + i];
                    if (otherPair.Count > 0 && TrySwap(pair.Value, otherPair, equal))
                        break;
                }
            }
        }

        private static bool TrySwap(IList<Race> pair, IList<Race> otherPair, Func<Race, Race, bool> equalityComparer)
        {
            if (otherPair.All(otherRace => !pair.Any(race => equalityComparer(otherRace, race))))
            {
                var t = pair[0];
                pair[0] = otherPair[0];
                otherPair[0] = t;
                return true;
            }
            return false;
        }

        private static IReadOnlyDictionary<int, IReadOnlyCollection<Race>> FillHeatsByFixedLanes(int firstPair, int pairCount,
            IReadOnlyDictionary<int, Dictionary<int, CompetitorBase>> seed)
        {
            var pairs = new Dictionary<int, IReadOnlyCollection<Race>>();
            for (var pair = firstPair; pair <= pairCount + firstPair; pair++)
            {
                var races = new List<Race>();

                Dictionary<int, CompetitorBase> pairSeed;
                if (seed.TryGetValue(pair, out pairSeed))
                    for (var lane = 0; lane <= 1; lane++)
                    {
                        CompetitorBase competitor;
                        if (!pairSeed.TryGetValue(lane, out competitor))
                            continue;

                        races.Add(new Race
                        {
                            Lane = lane,
                            Competitor = competitor
                        });
                    }

                pairs.Add(pair, races);
            }
            return pairs;
        }

        public override Task DrawCompetitorGroupAsync(Distance distance, IReadOnlyCollection<Guid> distanceCombinations, int round, IList<CompetitorBase> competitors,
            ICompetitionContext context, DistanceDrawSettings settings)
        {
            switch (settings.Mode)
            {
                case DistanceDrawMode.Random:
                case DistanceDrawMode.SwitchLanesOrRandom:
                    competitors.Shuffle();
                    return Task.FromResult<object>(null);

                default:
                    throw new DrawModeNotSupportedException();
            }
        }

        public override void OnAddingRace(Distance distance, Race race)
        {
            UpdateRaceColor(distance, race);
        }

        public override void OnUpdatingDistance(Distance distance, Race race)
        {
            UpdateRaceColor(distance, race);
        }

        public override void OnMovingRace(Distance distance, Race race)
        {
            UpdateRaceColor(distance, race);
        }

        private static void UpdateRaceColor(IDistance distance, IRace race)
        {
            race.Color = (int)PairsDistanceCalculator.Colors(distance, race.Heat).ToLaneColor((Lane)race.Lane);
        }

        public override void OnCopyingRace(Distance distance, Race original, Race copy, RacesCopySettings settings)
        {
            if (settings.SwitchLanes)
            {
                var originalLane = (Lane)original.Lane;
                copy.Lane = (int)originalLane.Opposite();
                UpdateRaceColor(distance, copy);
            }
        }
    }
}