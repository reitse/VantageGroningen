using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.Objects;

namespace Emando.Vantage.Data.MylapsX2
{
    public class X2EventSource : IObservable<X2EventBase>, IDisposable
    {
        private bool auxiliaryResendDataReceived;
        private EventData eventData;
        private bool isDisposed;
        private ConcurrentBag<X2EventBase> resendBuffer = new ConcurrentBag<X2EventBase>();
        private bool transponderResendDataReceived;
        private readonly X2Client client;
        private readonly Subject<X2EventBase> events = new Subject<X2EventBase>();
        private readonly IX2EventFilter filter;

        public X2EventSource(X2Client client, IX2EventFilter filter)
        {
            this.client = client;
            this.filter = filter;

            client.ProcessingMessages += OnProcessingMessages;
            client.Disconnected += OnDisconnected;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region IObservable<TimingEventBase> Members

        public IDisposable Subscribe(IObserver<X2EventBase> observer)
        {
            return events.Subscribe(observer);
        }

        #endregion

        private void OnDisconnected(object sender, EventArgs e)
        {
            events.Dispose();
        }

        public void Connect(TimeSpan? resendWindow)
        {
            eventData = resendWindow.HasValue
                ? client.Device.CreateEventDataLiveWithResend(client.Device.GetUTCTimeAsDateTime() - resendWindow.Value)
                : client.Device.CreateEventDataLive();
            eventData.PassingContainer.NotifyHandlers = NotifyPassing;
            eventData.AuxEventContainer.NotifyHandlers = NotifyAuxEvent;
            eventData.SubscribeToEventData(MTAEVENTDATA.mtaPassing, uint.MaxValue, false);
            eventData.SubscribeToEventData(MTAEVENTDATA.mtaAuxEvent, uint.MaxValue, false);
        }

        ~X2EventSource()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    Disconnect();
                    client.ProcessingMessages -= OnProcessingMessages;
                    client.Disconnected -= OnDisconnected;
                    events.Dispose();
                }

                isDisposed = true;
            }
        }

        public void Disconnect()
        {
            if (eventData != null)
            {
                eventData.PassingContainer.NotifyHandlers = null;
                eventData.AuxEventContainer.NotifyHandlers = null;
                eventData.UnsubscribeFromEventData(MTAEVENTDATA.mtaPassing);
                eventData.UnsubscribeFromEventData(MTAEVENTDATA.mtaAuxEvent);
                eventData.Dispose();
                eventData = null;
            }
        }

        private void OnProcessingMessages(object sender, EventArgs eventArgs)
        {
            if (resendBuffer != null && auxiliaryResendDataReceived && transponderResendDataReceived)
            {
                foreach (var @event in resendBuffer.OrderBy(e => e.When))
                    events.OnNext(@event);
                resendBuffer = null;
            }
        }

        private void NotifyPassing(MDP_NOTIFY_TYPE type, List<Passing> passings, EventData sender)
        {
            if (type == MDP_NOTIFY_TYPE.MDP_NOTIFY_SELECT)
                transponderResendDataReceived = true;
            else if (type != MDP_NOTIFY_TYPE.MDP_NOTIFY_INSERT)
                return;

            try
            {
                var sent = client.Device.GetUTCTimeAsDateTime();
                var received = DateTime.UtcNow;

                foreach (var passing in passings)
                {
                    var filteredEvent = filter.FilterTransponderPassing(passing.ID, passing.LoopID, passing.TransponderID, passing.GetStrength(), passing.UTCTimeAsDateTime, sent,
                        received, passing.IsResend() && !passing.IsDecoderResend());
                    if (filteredEvent != null)
                        ProcessEvent(filteredEvent);
                }
            }
            catch (Exception e)
            {
                //events.OnError(e);
                Disconnect();
            }
        }

        private void NotifyAuxEvent(MDP_NOTIFY_TYPE type, List<AuxEvent> auxEvents, EventData sender)
        {
            if (type == MDP_NOTIFY_TYPE.MDP_NOTIFY_SELECT)
                auxiliaryResendDataReceived = true;
            else if (type != MDP_NOTIFY_TYPE.MDP_NOTIFY_INSERT)
                return;

            try
            {
                var sent = client.Device.GetUTCTimeAsDateTime();
                var received = DateTime.UtcNow;

                foreach (var auxEvent in from e in auxEvents
                                         where e.IsInputChange() && e.IsEdgeRising()
                                         select e)
                {
                    var filteredEvent = filter.FilterAuxiliaryEvent(auxEvent.ID, auxEvent.GetChannel(), auxEvent.UTCTimeAsDateTime, sent, received,
                        auxEvent.IsResend() && !auxEvent.IsDecoderResend());
                    if (filteredEvent != null)
                        ProcessEvent(filteredEvent);
                }
            }
            catch (Exception e)
            {
                //events.OnError(e);
                Disconnect();
            }
        }

        private void ProcessEvent(X2EventBase filteredEvent)
        {
            if (resendBuffer != null)
                resendBuffer.Add(filteredEvent);
            else
                events.OnNext(filteredEvent);
        }
    }
}