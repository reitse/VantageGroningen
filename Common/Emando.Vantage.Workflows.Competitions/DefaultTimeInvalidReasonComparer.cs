using System.Collections.Generic;
using Emando.Vantage.Competitions;
using Emando.Vantage.Components.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class DefaultTimeInvalidReasonComparer : IComparer<TimeInvalidReason?>
    {
        private static IDictionary<TimeInvalidReason, int> order = new Dictionary<TimeInvalidReason, int>
        {
            { TimeInvalidReason.NotFinished, 1 },
            { TimeInvalidReason.Disqualified, 2 },
            { TimeInvalidReason.NotStarted, 3 },
            { TimeInvalidReason.Withdrawn, 4 },
            { TimeInvalidReason.Unknown, 5 }
        };

        public static DefaultTimeInvalidReasonComparer Default { get; } = new DefaultTimeInvalidReasonComparer();

        public virtual int Compare(TimeInvalidReason? x, TimeInvalidReason? y)
        {
            if (x == null && y == null)
                return 0;
            if (x == null)
                return -1;
            if (y == null)
                return 1;
            return order[x.Value].CompareTo(order[y.Value]);
        }
    }
}