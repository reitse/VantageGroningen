using System;
using Emando.Vantage.Workflows.Competitions.Events;

namespace Emando.Vantage.Workflows.Competitions
{
    public interface IDistanceEventsSource : IObservable<DistanceEventBase>
    {
    }
}