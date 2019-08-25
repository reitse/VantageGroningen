using System;
using Emando.Vantage.Events;

namespace Emando.Vantage.Components
{
    public interface IEventSource : IObservable<EventBase>
    {
    }
}