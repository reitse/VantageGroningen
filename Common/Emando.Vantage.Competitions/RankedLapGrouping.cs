using System.Collections.Generic;
using System.Linq;

namespace Emando.Vantage.Competitions
{
    internal class RankedLapGrouping<T> : List<T>, IGrouping<int, T>
        where T : IReadOnlyRaceLap
    {
        public RankedLapGrouping(int ranking, IEnumerable<T> laps) : base(laps)
        {
            this.Key = ranking;
        }

        #region IGrouping<int,T> Members

        public int Key { get; }

        #endregion
    }
}