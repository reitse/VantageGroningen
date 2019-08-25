using System.Collections.Generic;

namespace Emando.Vantage.Components.Sync
{
    public sealed class PersonIdEqualityComparer : IEqualityComparer<IPerson>
    {
        public static PersonIdEqualityComparer Default { get; } = new PersonIdEqualityComparer();

        #region IEqualityComparer<IPerson> Members

        public bool Equals(IPerson x, IPerson y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (ReferenceEquals(x, null))
                return false;
            if (ReferenceEquals(y, null))
                return false;
            return x.Id.Equals(y.Id);
        }

        public int GetHashCode(IPerson obj)
        {
            return obj.Id.GetHashCode();
        }

        #endregion
    }
}