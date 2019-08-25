using System;
using System.Reactive.Linq;
using Emando.Vantage.Data.MylapsX2;

namespace Emando.Vantage.Components.Diagnostics.MylapsX2
{
    internal class X2TransponderEventSource : IObservable<ITransponderScan>, IX2EventFilter, IDisposable
    {
        private readonly X2EventSource eventSource;
        private bool isDisposed;

        public X2TransponderEventSource(X2Client client)
        {
            eventSource = new X2EventSource(client, this);
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region IObservable<ITransponderScan> Members

        public IDisposable Subscribe(IObserver<ITransponderScan> observer)
        {
            return eventSource.OfType<X2PassingEvent>().Select(e => new X2TransponderScan(e)).Subscribe(observer);
        }

        #endregion

        #region IX2EventFilter Members

        public X2EventBase FilterAuxiliaryEvent(long id, long channel, DateTime time, DateTime sent, DateTime received, bool isResend)
        {
            return null;
        }

        public X2EventBase FilterTransponderPassing(long id, long loopId, long transponderId, double strength, DateTime time, DateTime sent, DateTime received, bool isResend)
        {
            return new X2PassingEvent("MYLAPS X2", "Scanner", id, "Transponder", loopId, time, sent, received, isResend, transponderId, strength);
        }

        #endregion

        public void Start()
        {
            eventSource.Connect(null);
        }

        public void Stop()
        {
            eventSource.Disconnect();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                    eventSource.Dispose();

                isDisposed = true;
            }
        }

        ~X2TransponderEventSource()
        {
            Dispose(false);
        }
    }
}