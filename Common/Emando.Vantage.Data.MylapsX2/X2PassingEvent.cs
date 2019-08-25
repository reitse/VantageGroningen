using System;

namespace Emando.Vantage.Data.MylapsX2
{
    public class X2PassingEvent : X2EventBase
    {
        public X2PassingEvent(string applianceName, string applianceInstanceName, long applianceEventId, string how, long @where, DateTime when, DateTime sent, DateTime received, bool isResend, object what, double? strength) : base(applianceName, applianceInstanceName, applianceEventId, how, @where, when, sent, received, isResend)
        {
            What = what;
            Strength = strength;
        }

        public object What { get; }

        public double? Strength { get; }
    }
}