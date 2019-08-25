namespace Emando.Vantage.Events
{
    public class HeartbeatEvent : EventBase
    {
        public HeartbeatEvent(long time)
        {
            Time = time;
        }

        public long Time { get; private set; }
    }
}