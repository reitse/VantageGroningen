using System.Collections.Generic;

namespace Emando.Vantage.Components.Sync
{
    public sealed class ClubKeyEqualityComparer : IEqualityComparer<IClub>
    {
        public static ClubKeyEqualityComparer Default { get; } = new ClubKeyEqualityComparer();

        #region IEqualityComparer<IClub> Members

        public bool Equals(IClub x, IClub y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (ReferenceEquals(x, null))
                return false;
            if (ReferenceEquals(y, null))
                return false;
            return string.Equals(x.CountryCode, y.CountryCode) && x.Code == y.Code;
        }

        public int GetHashCode(IClub obj)
        {
            unchecked
            {
                return ((obj.CountryCode?.GetHashCode() ?? 0) * 397) ^ obj.Code;
            }
        }

        #endregion
    }
}