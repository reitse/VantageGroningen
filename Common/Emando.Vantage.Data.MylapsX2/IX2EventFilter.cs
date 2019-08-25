using System;

namespace Emando.Vantage.Data.MylapsX2
{
    public interface IX2EventFilter
    {
        X2EventBase FilterAuxiliaryEvent(long id, long channel, DateTime time, DateTime sent, DateTime received, bool isResend);

        X2EventBase FilterTransponderPassing(long id, long loopId, long transponderId, double strength, DateTime time, DateTime sent, DateTime received, bool isResend);
    }
}