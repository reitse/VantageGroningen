using System.Collections.Generic;

namespace Emando.Vantage.Components.Sync
{
    public sealed class PersonLicenseKeyEqualityComparer : IEqualityComparer<IPersonLicense>
    {
        public static PersonLicenseKeyEqualityComparer Default { get; } = new PersonLicenseKeyEqualityComparer();

        public bool Equals(IPersonLicense x, IPersonLicense y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (ReferenceEquals(x, null))
                return false;
            if (ReferenceEquals(y, null))
                return false;
            return string.Equals(x.IssuerId, y.IssuerId) && string.Equals(x.Discipline, y.Discipline) && string.Equals(x.Key, y.Key);
        }

        public int GetHashCode(IPersonLicense obj)
        {
            unchecked
            {
                var hashCode = obj.IssuerId?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (obj.Discipline?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (obj.Key?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}