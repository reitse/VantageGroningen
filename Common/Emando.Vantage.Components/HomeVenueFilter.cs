using System;
using System.Linq;

namespace Emando.Vantage.Components
{
    public static class HomeVenueFilter
    {
        public static bool IsMatch(string filter, string venueCode)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return true;

            if (venueCode == null)
                return false;

            var homeVenues = filter.Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return homeVenues.Contains(venueCode, StringComparer.OrdinalIgnoreCase);
        }

        public static void EnsureMatch(string filter, string venueCode)
        {
            if (!IsMatch(filter, venueCode))
                throw new HomeVenueFilterException(filter, venueCode);
        }
    }
}