using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Emando.Vantage.Data.MylapsX2.Properties;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.Objects;

namespace Emando.Vantage.Data.MylapsX2
{
    public class X2Client
    {
        private SDK client;
        private TaskCompletionSource<bool> connected;
        private TaskCompletionSource<bool> disconnected;
        private CancellationTokenSource disconnectedTokenSource;
        private bool isConnected;
        private bool isDisposed;
        private Task processMessagesTask;
        private readonly ILog log;

        public X2Client()
        {
            log = LogManager.GetLogger(GetType());
        }

        internal MTA Device { get; private set; }

        public string Host => Device.GetHostname();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public event EventHandler Connected;

        public event EventHandler Disconnected;

        public async Task ConnectAsync(string host, string applicationName, string userName, string password, CancellationToken cancellationToken)
        {
            if (client != null)
                throw new InvalidOperationException();

            log.Debug(l => l(LogMessages.Connecting, userName, host));

            connected = new TaskCompletionSource<bool>();
            disconnected = new TaskCompletionSource<bool>();

            client = SDK.CreateSDK(applicationName);
            Device = client.CreateMTA();
            Device.NotifyConnectHandlers = NotifyConnectHandler;
            Device.NotifyConnectionStateHandlers = NotifyConnectionStateHandler;
            Device.Connect(host, userName, password);

            disconnectedTokenSource = new CancellationTokenSource();
            var disconnectedToken = disconnectedTokenSource.Token;
            processMessagesTask = Task.Run(() => ProcessMessages(disconnectedToken), cancellationToken);

            try
            {
                await connected.Task;
            }
            catch (Exception e)
            {
                log.Error(l => l(LogMessages.ConnectFailed), e);
                disconnected.TrySetResult(true);
                DisconnectAsync();
                throw;
            }
        }

        public async Task DisconnectAsync()
        {
            if (client != null)
            {
                log.Debug(l => l(LogMessages.Disconnecting, Host));

                Device.Disconnect();
                await disconnected.Task;

                disconnectedTokenSource.Cancel();

                try
                {
                    await processMessagesTask;
                }
                catch (Exception e)
                {
                    if (!(e is OperationCanceledException))
                        log.Warn(l => l(LogMessages.ProcessMessagesTaskFailed), e);
                }
                processMessagesTask = null;

                connected = null;
                disconnected = null;

                Device.Dispose();
                Device = null;
                client.Dispose();
                client = null;
            }
        }

        internal event EventHandler ProcessingMessages;

        protected virtual void OnProcessingMessages()
        {
            var handler = ProcessingMessages;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        private void NotifyConnectHandler(bool newValue, MTA sender)
        {
            isConnected = newValue;
            if (isConnected)
            {
                log.Info(l => l(LogMessages.Connected, Host));
                OnConnected();
            }
            else
            {
                log.Info(l => l(LogMessages.Disconnected));
                OnDisconnected();
            }

            if (isConnected)
                Task.Run(() => connected.TrySetResult(true));
            else
                Task.Run(() => disconnected.TrySetResult(true));
        }

        private void NotifyConnectionStateHandler(CONNECTIONSTATE state, MTA sender)
        {
            log.Debug(l => l(LogMessages.ConnectionStateChanged, state));

            switch (state)
            {
                case CONNECTIONSTATE.csConnectFailed:
                    Task.Run(() => connected.TrySetException(new X2ConnectFailedException()));
                    break;
                case CONNECTIONSTATE.csAuthenticationFailed:
                    Task.Run(() => connected.TrySetException(new X2ConnectFailedException(Resources.AuthenticationFailed)));
                    break;
            }
        }

        protected virtual void OnConnected()
        {
            var handler = Connected;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnDisconnected()
        {
            var handler = Disconnected;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        ~X2Client()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                    DisconnectAsync().Wait();

                isDisposed = true;
            }
        }

        private void ProcessMessages(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                OnProcessingMessages();
                client.ProcessMessageQueue(true, TimeSpan.FromMilliseconds(100));
            }
        }
    }
}