using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using Common.Logging;
using Emando.Vantage.Api.Client.Competitions.Properties;
using Emando.Vantage.Models;
using Emando.Vantage.Models.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Emando.Vantage.Api.Client.Competitions
{
    public class TcpEventsClient : IEventsClient
    {
        private static readonly Encoding Encoding = new UTF8Encoding(false);
        private readonly ILog log;
        private readonly IObservable<EventViewModelBase> observable;

        public TcpEventsClient(string host, int port, string applicationName, string version, string instanceName)
        {
            log = LogManager.GetLogger(GetType());

            observable = Observable.Create<EventViewModelBase>(async (o, c) =>
            {
                var client = new TcpClient();
                log.Info(l => l(Resources.TcpEventsClientConnecting, host, port));
                await client.ConnectAsync(host, port);
                log.Info(l => l(Resources.TcpEventsClientConnected, host, port));

                var stream = client.GetStream();
                var writer = new StreamWriter(stream, Encoding)
                {
                    AutoFlush = true
                };
                Handshake(writer, applicationName, version, instanceName);

                var reader = new StreamReader(stream, Encoding);
                BeginRead(reader, o, c);
                return new CompositeDisposable(stream, client);
            });
        }

        #region IEventsClient Members

        public IDisposable Subscribe(IObserver<EventViewModelBase> observer)
        {
            return observable.Subscribe(observer);
        }

        #endregion

        private static void Handshake(TextWriter writer, string applicationName, string version, string instanceName)
        {
            var message = new HandshakeBindingModel
            {
                ApplicationName = applicationName,
                Version = version,
                InstanceName = instanceName
            };
            writer.WriteLine(JsonConvert.SerializeObject(message));
        }

        private void BeginRead(TextReader reader, IObserver<EventViewModelBase> observer, CancellationToken cancellationToken)
        {
            reader.ReadLineAsync().ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    Debug.Assert(t.Exception != null);
                    log.Error(l => l(Resources.TcpEventsClientReadFailed), t.Exception);
                    observer.OnError(t.Exception);
                }
                else if (t.IsCanceled)
                    observer.OnCompleted();
                else
                {
                    var token = JToken.Parse(t.Result);
                    EventViewModelBase @event;
                    if (JsonEventsDeserializer.TryDeserialize(token, out @event))
                    {
                        log.Debug(l => l(Resources.TcpEventsClientDeserializedEvent, @event));
                        observer.OnNext(@event);
                    }
                    else
                        log.Trace(l => l(Resources.TcpEventsClientDeserializationFailed, token));

                    BeginRead(reader, observer, cancellationToken);
                }
            }, cancellationToken);
        }
    }
}