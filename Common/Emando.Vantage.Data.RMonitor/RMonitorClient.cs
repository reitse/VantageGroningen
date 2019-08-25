using System;
using System.IO;
using System.Net.Sockets;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;

namespace Emando.Vantage.Data.RMonitor
{
    public class RMonitorClient
    {
        private readonly ILog log;
        private bool isDisposed;
        private StreamReader reader;
        private readonly TcpClient client = new TcpClient();
        private readonly IConnectableObservable<RMonitorRecord> events;
        private readonly ISubject<RMonitorRecord> eventsSource = new Subject<RMonitorRecord>();
        private readonly IDisposable eventsSourceConnection;

        public RMonitorClient()
        {
            log = LogManager.GetLogger(GetType());
            events = eventsSource.Publish();
            eventsSourceConnection = events.Connect();
        }

        public IObservable<RMonitorRecord> Events
        {
            get { return events.AsObservable(); }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task ConnectAsync(string hostName, int port)
        {
            await client.ConnectAsync(hostName, port);
            reader = new StreamReader(client.GetStream(), Encoding.UTF7);
            ReadNextLine();
        }

        private void ReadNextLine()
        {
            reader.ReadLineAsync().ContinueWith(t =>
            {
                if (t.Exception != null)
                    eventsSource.OnError(t.Exception);
                else if (t.IsCanceled)
                    eventsSource.OnCompleted();
                else if (t.IsCompleted)
                {
                    if (t.Result == null)
                    {
                        eventsSource.OnCompleted();
                        return;
                    }

                    try
                    {
                        var record = RMonitorRecordParser.Parse(t.Result, DateTime.UtcNow);
                        eventsSource.OnNext(record);
                    }
                    catch (FormatException e)
                    {
                        log.Warn(l => l(LogMessages.RecordParseFailed, t.Result), e);
                    }
                    ReadNextLine();
                }
            });
        }

        public void Disconnect()
        {
            reader.Close();
            client.Close();
        }

        ~RMonitorClient()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    eventsSourceConnection.Dispose();
                    if (reader != null)
                        reader.Close();
                    client.Close();
                }

                isDisposed = true;
            }
        }
    }
}