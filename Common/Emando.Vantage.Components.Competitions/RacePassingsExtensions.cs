using System;
using System.Collections.Generic;
using System.Linq;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Components.Competitions
{
    public static class RacePassingsExtensions
    {
        public static IEnumerable<T> How<T>(this IEnumerable<T> passings, string how)
            where T : IHaveRacePassingKey
        {
            return from p in passings
                   where p.PresentationSource.How == how
                   orderby p.Time
                   select p;
        }

        public static RacePassingComparison CompareTo(this IReadOnlyRacePassing passing, IEnumerable<IReadOnlyPassing> reference, TimeSpan precision, decimal delta = 20M)
        {
            return CompareTo(passing, reference, precision, TimeSpan.Zero, delta);
        }

        public static RacePassingComparison CompareTo(this IReadOnlyRacePassing passing, IEnumerable<IReadOnlyPassing> reference, TimeSpan precision, TimeSpan gapOffset, decimal delta = 20M)
        {
            if (passing == null || passing.Passed == null || reference == null)
                return null;

            var nearest = (from r in reference
                           let passed = r.Passed
                           where passed != null
                           let d = Math.Abs(passed.Value - passing.Passed.Value)
                           where d < delta
                           orderby d
                           select new
                           {
                               Passed = passed.Value,
                               r.Time
                           }).FirstOrDefault();
            if (nearest == null)
                return null;

            var factor = nearest.Passed / passing.Passed.Value;
            var time = TimeSpan.FromTicks((long)(passing.Time.Ticks * factor));
            var gap = time.Truncate(precision) - nearest.Time.Truncate(precision) + gapOffset.Truncate(precision);

            return new RacePassingComparison(passing.RaceId, passing.InstanceName, passing.PresentationSource, passing.When, passing.Time, gap);
        }
    }
}