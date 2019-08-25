using System;
using System.Collections.Generic;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Components.Competitions.Sync
{
    public sealed class CompetitionProviderKeyEqualityComparer : IEqualityComparer<ICompetition>
    {
        public static IEqualityComparer<ICompetition> Default { get; } = new CompetitionProviderKeyEqualityComparer();

        #region IEqualityComparer<ICompetition> Members

        public bool Equals(ICompetition x, ICompetition y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (ReferenceEquals(x, null))
                return false;
            if (ReferenceEquals(y, null))
                return false;
            return string.Equals(x.ProviderKey, y.ProviderKey);
        }

        public int GetHashCode(ICompetition obj)
        {
            return obj.ProviderKey?.GetHashCode() ?? 0;
        }

        #endregion
    }
}