using System;

namespace Emando.Vantage.Data.RMonitor
{
    public class RMonitorRecord
    {
        public RMonitorRecord(DateTime received)
        {
            Received = received;
        }

        public DateTime Received { get; private set; }
    }
}