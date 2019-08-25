using System.Collections.Generic;

namespace Emando.Vantage.Components.Sync
{
    public sealed class PersonCategoryKeyEqualityComparer : IEqualityComparer<IPersonCategory>
    {
        public static IEqualityComparer<IPersonCategory> Default { get; } = new PersonCategoryKeyEqualityComparer();

        #region IEqualityComparer<IPersonCategory> Members

        public bool Equals(IPersonCategory x, IPersonCategory y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (ReferenceEquals(x, null))
                return false;
            if (ReferenceEquals(y, null))
                return false;
            return string.Equals(x.LicenseIssuerId, y.LicenseIssuerId) && string.Equals(x.Discipline, y.Discipline) && string.Equals(x.Code, y.Code);
        }

        public int GetHashCode(IPersonCategory obj)
        {
            unchecked
            {
                var hashCode = obj.LicenseIssuerId.GetHashCode();
                hashCode = (hashCode * 397) ^ obj.Discipline.GetHashCode();
                hashCode = (hashCode * 397) ^ obj.Code.GetHashCode();
                return hashCode;
            }
        }

        #endregion
    }
}