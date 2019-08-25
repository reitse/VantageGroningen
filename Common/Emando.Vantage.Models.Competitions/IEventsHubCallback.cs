using Emando.Vantage.Models.Events;

namespace Emando.Vantage.Models.Competitions
{
    public interface IEventsHubCallback
    {
        void HandleEvent(EventViewModelBase @event);
    }
}