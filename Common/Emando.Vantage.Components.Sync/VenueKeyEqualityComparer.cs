using System.Collections.Generic;

namespace Emando.Vantage.Components.Sync
{
    public class VenueKeyEqualityComparer : IEqualityComparer<IVenue>
    {
        public static VenueKeyEqualityComparer Default { get; } = new VenueKeyEqualityComparer();

        public bool Equals(IVenue x, IVenue y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (ReferenceEquals(x, null))
                return false;
            if (ReferenceEquals(y, null))
                return false;
            return string.Equals(x.Code, y.Code) && string.Equals(x.Discipline, y.Discipline);
        }

        public int GetHashCode(IVenue obj)
        {
            unchecked
            {
                return ((obj.Code?.GetHashCode() ?? 0) * 397) ^ (obj.Discipline?.GetHashCode() ?? 0);
            }
        }
    }
}
