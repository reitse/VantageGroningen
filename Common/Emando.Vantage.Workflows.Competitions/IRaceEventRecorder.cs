using Emando.Vantage.Components;
using Emando.Vantage.Workflows.Competitions.Events;

namespace Emando.Vantage.Workflows.Competitions
{
    public interface IRaceEventRecorder : IEventRecorder
    {
        void RecordRaceEvent(RaceEventBase e);
    }
}