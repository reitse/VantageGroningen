using System;
using Emando.Vantage.Models.Events;

namespace Emando.Vantage.Api.Client.Competitions
{
    public interface IEventsClient : IObservable<EventViewModelBase>
    {
    }
}