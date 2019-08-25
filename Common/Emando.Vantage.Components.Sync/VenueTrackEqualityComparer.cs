using System.Collections.Generic;

namespace Emando.Vantage.Components.Sync
{
    public class VenueTrackEqualityComparer : IEqualityComparer<IVenueTrack>
    {
        public static VenueTrackEqualityComparer Default { get; } = new VenueTrackEqualityComparer();

        public bool Equals(IVenueTrack x, IVenueTrack y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (ReferenceEquals(x, null))
                return false;
            if (ReferenceEquals(y, null))
                return false;
            return string.Equals(x.VenueCode, y.VenueCode) && string.Equals(x.VenueDiscipline, y.VenueDiscipline) && x.Length == y.Length;
        }

        public int GetHashCode(IVenueTrack obj)
        {
            unchecked
            {
                var hashCode = obj.VenueCode.GetHashCode();
                hashCode = (hashCode * 397) ^ obj.VenueDiscipline.GetHashCode();
                hashCode = (hashCode * 397) ^ obj.Length.GetHashCode();
                return hashCode;
            }
        }
    }
}
