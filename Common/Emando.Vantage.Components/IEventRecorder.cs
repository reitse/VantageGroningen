using Emando.Vantage.Events;

namespace Emando.Vantage.Components
{
    public interface IEventRecorder
    {
        void RecordEvent(EventBase e);
    }
}