using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Emando.Vantage.Components
{
    public static class CategoryFilter
    {
        public static bool IsMatch(string filter, string category)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return true;

            if (category == null)
                return false;

            var filters = filter.Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return filters.Any(f =>
            {
                var pattern = "^" + Regex.Escape(f).Replace(@"\*", ".*").Replace(@"\?", ".") + "$";
                return Regex.IsMatch(category, pattern, RegexOptions.IgnoreCase);
            });
        }

        public static void EnsureMatch(string filter, string category)
        {
            if (!IsMatch(filter, category))
                throw new CategoryFilterException(filter, category);
        }
    }
}