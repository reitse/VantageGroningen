using System;

namespace Emando.Vantage.Components.Competitions
{
    public static class TimeSpanExtensions
    {
        public static TimeSpan Truncate(this TimeSpan time, TimeSpan precision)
        {
            return precision == TimeSpan.Zero ? time : TimeSpan.FromTicks(time.Ticks - (time.Ticks % precision.Ticks));
        }

        public static DateTime Truncate(this DateTime time, TimeSpan precision)
        {
            return precision == TimeSpan.Zero ? time : new DateTime(time.Ticks - (time.Ticks % precision.Ticks));
        }
    }
}