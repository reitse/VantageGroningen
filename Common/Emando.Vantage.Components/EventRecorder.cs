using System;
using System.Reactive.Subjects;
using Emando.Vantage.Events;

namespace Emando.Vantage.Components
{
    public class EventRecorder : IEventRecorder, IEventSource, IDisposable
    {
        private readonly Subject<EventBase> events = new Subject<EventBase>();
        private bool isDisposed;

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region IEventRecorder Members

        public void RecordEvent(EventBase e)
        {
            events.OnNext(e);
        }

        #endregion

        #region IEventSource Members

        public IDisposable Subscribe(IObserver<EventBase> observer)
        {
            return events.Subscribe(observer);
        }

        #endregion

        ~EventRecorder()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                    events.Dispose();

                isDisposed = true;
            }
        }
    }
}