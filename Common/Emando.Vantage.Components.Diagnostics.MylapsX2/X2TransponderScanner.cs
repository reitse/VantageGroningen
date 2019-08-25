using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using Emando.Vantage.Data.MylapsX2;

namespace Emando.Vantage.Components.Diagnostics.MylapsX2
{
    public class X2TransponderScanner : ITransponderScanner
    {
        private readonly X2Client client = new X2Client();
        private X2TransponderEventSource eventSource;
        private bool isDisposed;
        
        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region ITransponderScanner Members

        public IDisposable Subscribe(IObserver<ITransponderScan> observer)
        {
            if (eventSource == null)
                throw new InvalidOperationException();

            return eventSource.Subscribe(observer);
        }

        #endregion

        public async Task ConnectAsync(string host, string applicationName, string userName, string password, CancellationToken cancellationToken)
        {
            await client.ConnectAsync(host, applicationName, userName, password, cancellationToken);
            eventSource = new X2TransponderEventSource(client);
        }

        public void Start()
        {
            eventSource.Start();
        }

        public void Stop()
        {
            eventSource.Stop();
        }

        public Task DisconnectAsync()
        {
            if (eventSource != null)
            {
                eventSource.Dispose();
                eventSource = null;
            }
            return client.DisconnectAsync();
        }

        ~X2TransponderScanner()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    eventSource?.Dispose();
                    client.Dispose();
                }

                isDisposed = true;
            }
        }
    }
}