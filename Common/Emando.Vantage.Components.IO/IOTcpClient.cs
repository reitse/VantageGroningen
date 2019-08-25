using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Emando.Vantage.Components.IO
{
    public abstract class IOTcpClient
    {
        private const int Port = 52010;
        protected static readonly IDictionary<Type, string> Types = new Dictionary<Type, string>
        {
            { typeof(int), "int32" },
            { typeof(string), "string" }
        };
        protected static readonly IDictionary<string, Func<string, object>> Converters = new Dictionary<string, Func<string, object>>
        {
            { "int32", s => Int32.Parse(s) },
            { "string", WebUtility.UrlDecode }
        };
        private TcpClient connectedClient;
        private bool isDisposed;
        private bool isDisposing;
        private StreamReader reader;
        private Stream stream;
        private StreamWriter writer;

        protected IOTcpClient()
        {
        }

        protected IOTcpClient(TcpClient client)
        {
            Connect(client);
        }

        public bool IsConnected
        {
            get { return connectedClient != null; }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public event EventHandler Disconnected;

        protected virtual void OnDisconnected()
        {
            var handler = Disconnected;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        ~IOTcpClient()
        {
            Dispose(false);
        }

        public async Task ConnectAsync(string host)
        {
            var client = new TcpClient();
            await client.ConnectAsync(host, Port);
            Connect(client);
            await HandshakeAsync();
            BeginHandleMessage();
        }

        private void Connect(TcpClient client)
        {
            connectedClient = client;
            stream = client.GetStream();
            writer = new StreamWriter(stream, Encoding.UTF8)
            {
                AutoFlush = true
            };
            reader = new StreamReader(stream, Encoding.UTF8);
        }

        public abstract Task HandshakeAsync();

        public void BeginHandleMessage()
        {
            ReadMessageAsync().ContinueWith(t =>
            {
                if (t.Exception != null)
                    Disconnect();
                else if (t.IsCompleted)
                {
                    if (t.Result == null)
                    {
                        Disconnect();
                        return;
                    }

                    try
                    {
                        HandleMessage(t.Result);
                        BeginHandleMessage();
                    }
                    catch
                    {
                        Disconnect();
                    }
                }
            });
        }

        private void Disconnect()
        {
            if (!isDisposed && !isDisposing)
            {
                Dispose();
                OnDisconnected();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                isDisposing = true;
                if (disposing)
                {
                    if (writer != null)
                        writer.Dispose();
                    if (reader != null)
                        reader.Dispose();
                    if (stream != null)
                        stream.Dispose();
                    if (connectedClient != null)
                        connectedClient.Close();
                }

                isDisposed = true;
            }
        }

        private void HandleMessage(string message)
        {
            IDictionary<string, string> parameters;
            string command = DecodeMessage(message, out parameters);
            HandleMessage(command, parameters);
        }

        protected async Task<string> ReadMessageAsync()
        {
            return await reader.ReadLineAsync();
        }

        protected abstract void HandleMessage(string command, IDictionary<string, string> parameters);

        private static string EncodeMessage(string command, IDictionary<string, object> parameters)
        {
            string message = command;
            if (parameters.Any())
            {
                var queryString = from pair in parameters
                                  let key = pair.Key.ToLowerInvariant()
                                  let value = (pair.Value ?? "").ToString()
                                  select String.Format("{0}={1}", key, value);
                message += String.Format("?{0}", String.Join("&", queryString));
            }
            return message;
        }

        protected static string DecodeMessage(string message, out IDictionary<string, string> parameters)
        {
            parameters = new Dictionary<string, string>();
            var parts = message.Split(new[] { '?' }, 2);
            if (parts.Length == 2)
                foreach (string parameter in parts[1].Split('&'))
                {
                    var p = parameter.Split(new[] { '=' }, 2);
                    parameters.Add(p.ElementAt(0).ToLowerInvariant(), p.ElementAtOrDefault(1) ?? "");
                }

            return parts[0].ToLowerInvariant();
        }

        protected void WriteMessage(string command, IDictionary<string, object> parameters)
        {
            writer.WriteLine(EncodeMessage(command, parameters));
        }

        protected async Task WriteMessageAsync(string command, IDictionary<string, object> parameters)
        {
            await writer.WriteLineAsync(EncodeMessage(command, parameters));
        }
    }
}