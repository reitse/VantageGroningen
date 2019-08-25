using System.Collections.Generic;
using System.Linq;

namespace Emando.Vantage.Competitions
{
    public class RaceLapGroup<T> : List<T>, IGrouping<T, T>
        where T : IReadOnlyRaceLap
    {
        public RaceLapGroup(T key)
        {
            Key = key;
        }

        public RaceLapGroup(T key, IEnumerable<T> collection) : base(collection)
        {
            Key = key;
        }

        #region IGrouping<T,T> Members

        public T Key { get; set; }

        #endregion

        public bool HasPresentationSource(PresentationSource presentationSource)
        {
            return (Key != null && Key.PresentationSource == presentationSource)
                || Exists(l => l.PresentationSource == presentationSource);
        }
    }
}