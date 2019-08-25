using System;
using System.Collections.Generic;

namespace Emando.Vantage.Components.Competitions
{
    public class TimeSpanPrecisionComparer : IComparer<TimeSpan>, IEqualityComparer<TimeSpan>
    {
        private readonly TimeSpan precision;

        public TimeSpanPrecisionComparer(TimeSpan precision)
        {
            this.precision = precision;
        }

        #region IComparer<TimeSpan> Members

        public int Compare(TimeSpan x, TimeSpan y)
        {
            return x.Truncate(precision).CompareTo(y.Truncate(precision));
        }

        #endregion

        #region IEqualityComparer<TimeSpan> Members

        public bool Equals(TimeSpan x, TimeSpan y)
        {
            return x.Truncate(precision).Equals(y.Truncate(precision));
        }

        public int GetHashCode(TimeSpan obj)
        {
            return obj.Truncate(precision).GetHashCode();
        }

        #endregion
    }
}