using System.Collections.Generic;
using System.Linq;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Components.Competitions
{
    public static class DistanceCombinationsExtensions
    {
        public static IEnumerable<T> Matches<T>(this IEnumerable<T> combinations, string category, int? @class)
            where T : IDistanceCombination
        {
            return from dc in combinations
                   where CategoryFilter.IsMatch(dc.CategoryFilter, category) && ClassFilter.IsMatch(dc.ClassFilter, @class)
                   select dc;
        }
    }
}