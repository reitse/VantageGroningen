using System;
using System.Linq;

namespace Emando.Vantage.Components
{
    public static class ClassFilter
    {
        public static bool IsMatch(string filter, int? @class)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return true;

            if (@class == null)
                return false;

            var filters = filter.Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return filters.Any(f =>
            {
                int classFilter;
                return int.TryParse(f, out classFilter) && classFilter == @class;
            });
        }

        public static void EnsureMatch(string classFilter, int? @class)
        {
            if (!IsMatch(classFilter, @class))
                throw new ClassFilterException(classFilter, @class);
        }
    }
}