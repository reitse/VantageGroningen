using System;

namespace Emando.Vantage.Data.MylapsX2
{
    public abstract class X2EventBase
    {
        protected X2EventBase(string applianceName, string applianceInstanceName, long applianceEventId, string how, long @where, DateTime when, DateTime sent,
            DateTime received, bool isResend = false)
        {
            ApplianceName = applianceName;
            ApplianceInstanceName = applianceInstanceName;
            ApplianceEventId = applianceEventId;
            How = how;
            Where = @where;
            When = when;
            Sent = sent;
            Received = received;
            IsResend = isResend;
        }

        public string ApplianceName { get; }

        public string ApplianceInstanceName { get; }

        public long ApplianceEventId { get; }

        public string How { get; }

        public long Where { get; }

        public DateTime Sent { get; }

        public DateTime Received { get; }

        public bool IsResend { get; }

        public DateTime When { get; }
    }
}