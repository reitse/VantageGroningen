using System;
using System.Linq;

namespace Emando.Vantage.Components
{
    public static class ClubFilter
    {
        public static bool IsMatch(string filter, int? clubCode)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return true;

            if (clubCode == null)
                return false;

            var filters = filter.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            return filters.Select(f => f.Trim()).Any(f =>
            {
                var bounds = f.Split('-');
                if (bounds.Length == 1)
                    return bounds[0].Split(' ').Any(b =>
                    {
                        int value;
                        return int.TryParse(b.Trim(), out value) && clubCode == value;
                    });

                if (bounds.Length == 2)
                {
                    int lowerValue;
                    int upperValue;
                    return int.TryParse(bounds[0].Trim(), out lowerValue) && int.TryParse(bounds[1].Trim(), out upperValue)
                        && clubCode >= lowerValue && clubCode <= upperValue;
                }

                return false;
            });
        }

        public static void EnsureMatch(string filter, int? clubCode)
        {
            if (!IsMatch(filter, clubCode))
                throw new ClubFilterException(filter, clubCode);
        }
    }
}