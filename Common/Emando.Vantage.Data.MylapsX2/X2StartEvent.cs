using System;

namespace Emando.Vantage.Data.MylapsX2
{
    public class X2StartEvent : X2EventBase
    {
        public X2StartEvent(string applianceName, string applianceInstanceName, long applianceEventId, string how, long @where, DateTime when, DateTime sent, DateTime received, bool isResend = false) : base(applianceName, applianceInstanceName, applianceEventId, how, @where, when, sent, received, isResend)
        {
        }
    }
}
