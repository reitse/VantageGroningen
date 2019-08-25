using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Emando.Vantage.Components.IO
{
    public class IOTcpListener
    {
        private readonly TcpListener listener;
        private readonly IIOEventPublisher publisher;

        public IOTcpListener(IPAddress localaddr, int port, IIOEventPublisher publisher)
        {
            this.publisher = publisher;
            listener = new TcpListener(localaddr, port);
        }

        public void Start()
        {
            listener.Start();
            BeginAcceptClient();
        }

        public void Stop()
        {
            listener.Stop();
        }

        private void BeginAcceptClient()
        {
            listener.AcceptTcpClientAsync().ContinueWith(t =>
            {
                var client = new IOTcpClientChannel(t.Result, publisher);
                client.HandshakeAsync().ContinueWith(h =>
                {
                    if (t.IsFaulted)
                    {
                        client.Dispose();
                        return;
                    }

                    if (t.IsCompleted)
                    {
                        client.Disconnected += ClientDisconnected;
                        client.BeginHandleMessage();
                        publisher.Subscribe(client, client.Name);
                    }
                });
                BeginAcceptClient();
            }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        private void ClientDisconnected(object sender, EventArgs e)
        {
            var client = (IOTcpClientChannel)sender;
            publisher.Unsubscribe(client);
        }
    }
}