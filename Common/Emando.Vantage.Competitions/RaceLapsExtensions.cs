using System;
using System.Collections.Generic;
using System.Linq;

namespace Emando.Vantage.Competitions
{
    public static class RaceLapsExtensions
    {
        public static T FindPresented<T>(this IEnumerable<T> laps, TimeSpan time) where T : IHaveRacePassingKey
        {
            return laps.Find(time, RaceEventFlags.Present);
        }

        public static T Find<T>(this IEnumerable<T> laps, TimeSpan time, RaceEventFlags flagMask = RaceEventFlags.None, PresentationSource? presentationSource = null)
            where T : IHaveRacePassingKey
        {
            return (from l in laps
                    where (!presentationSource.HasValue || l.PresentationSource == presentationSource.Value)
                        && l.Flags.HasFlag(flagMask)
                        && !l.Flags.HasFlag(RaceEventFlags.Deleted)
                        && l.Time == time
                    select l).FirstOrDefault();
        }

        public static T Find<T>(this IEnumerable<T> laps, DateTime when, TimeSpan delta, RaceEventFlags flags = RaceEventFlags.None,
            PresentationSource? presentationSource = null) where T : IHaveRacePassingKey
        {
            return (from l in laps
                    where (!presentationSource.HasValue || l.PresentationSource == presentationSource.Value)
                        && l.Flags.HasFlag(flags)
                        && !l.Flags.HasFlag(RaceEventFlags.Deleted)
                    let d = (l.When - when).Duration()
                    where d <= delta
                    orderby d
                    select l).FirstOrDefault();
        }

        public static IReadOnlyList<T> Presented<T>(this IEnumerable<T> laps) where T : IReadOnlyRaceLap
        {
            var query = from l in laps
                        where !l.Flags.HasFlag(RaceEventFlags.Deleted) && l.Flags.HasFlag(RaceEventFlags.Present)
                        orderby l.FixedIndex, l.Time
                        select l;

            var result = new List<T>();
            foreach (var lap in query)
                if (lap.FixedIndex.HasValue)
                {
                    while (result.Count <= lap.FixedIndex.Value)
                        result.Add(default(T));
                    result[lap.FixedIndex.Value] = lap;
                }
                else
                    result.Add(lap);
            return result.AsReadOnly();
        }

        public static IReadOnlyList<IGrouping<T, T>> GroupByPresented<T>(this IEnumerable<T> laps) where T : IReadOnlyRaceLap
        {
            var query = from l in laps
                        where !l.Flags.HasFlag(RaceEventFlags.Deleted)
                        orderby l.FixedIndex, l.Time
                        select l;

            var cache = query.ToList();
            var groups = new List<RaceLapGroup<T>>();
            foreach (var lap in cache.Where(l => l.Flags.HasFlag(RaceEventFlags.Present)))
                if (lap.FixedIndex.HasValue)
                {
                    while (groups.Count <= lap.FixedIndex.Value)
                        groups.Add(new RaceLapGroup<T>(default(T)));
                    var group = groups[lap.FixedIndex.Value];
                    if (!Equals(group.Key, default(T)))
                        group.Add(group.Key);
                    group.Key = lap;
                }
                else
                    groups.Add(new RaceLapGroup<T>(lap));

            foreach (var notPresentedGroup in cache.Where(l => !l.Flags.HasFlag(RaceEventFlags.Present)).GroupBy(l => l.PresentationSource))
            {
                var lastIndex = -1;
                foreach (var lap in notPresentedGroup)
                {
                    var group = (from g in groups.Skip(lastIndex + 1)
                                 where !Equals(g.Key, default(T)) && !g.HasPresentationSource(notPresentedGroup.Key)
                                 let diff = (g.Key.Time - lap.Time).Duration()
                                 orderby diff
                                 select g).FirstOrDefault();
                    if (group == null)
                    {
                        group = new RaceLapGroup<T>(default(T));
                        groups.Add(group);
                    }
                    group.Add(lap);
                    lastIndex = groups.IndexOf(group);
                }
            }

            return groups;
        }

        public static IReadOnlyList<IGrouping<int, T>> GroupByRanking<T>(this IEnumerable<T> laps)
            where T : IReadOnlyRaceLap
        {
            return GroupByRanking(laps, EqualityComparer<TimeSpan>.Default);
        }

        public static IReadOnlyList<IGrouping<int, T>> GroupByRanking<T>(this IEnumerable<T> laps, IEqualityComparer<TimeSpan> comparer)
            where T : IReadOnlyRaceLap
        {
            var cache = laps.Where(l => !l.Flags.HasFlag(RaceEventFlags.Deleted) && l.Flags.HasFlag(RaceEventFlags.Present)).ToList();
            var times = cache.Where(l => !l.FixedRanking.HasValue).GroupBy(l => l.Time, comparer).OrderBy(l => l.Key).ToList();
            var fixedRankings = cache.Where(l => l.FixedRanking.HasValue).ToLookup(l => l.FixedRanking.Value);

            var result = new List<IGrouping<int, T>>();
            var previousRanking = 0;
            var ranking = 1;
            foreach (var time in times)
            {
                var group = new RankedLapGrouping<T>(ranking, time);
                for (var i = previousRanking + 1; i <= ranking; i++)
                    group.AddRange(fixedRankings[i]);
                result.Add(group);
                previousRanking = ranking;
                ranking += group.Count;
            }

            return result;
        }
    }
}