using System;
using System.Threading.Tasks;
using Emando.Vantage.Components.IO;

namespace Emando.Vantage.Server.Services.IO.Client
{
    public class IOClient : IIOService, IDisposable
    {
        private bool isDisposed;
        private readonly IOTcpServiceChannel channel;

        public IOClient(string name)
        {
            channel = new IOTcpServiceChannel(name);
            channel.Update += (s, e) => OnUpdate(new ChannelUpdateEventArgs(e.Id, e.Value));
        }

        public bool IsConnected
        {
            get { return channel.IsConnected; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region IIOService Members

        public Task SetAsync(int id, object value)
        {
            return channel.SetAsync(id, value);
        }

        #endregion

        public event ChannelUpdateEventHandler Update;

        protected virtual void OnUpdate(ChannelUpdateEventArgs e)
        {
            var handler = Update;
            if (handler != null)
                handler(this, e);
        }

        ~IOClient()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                    channel.Dispose();

                isDisposed = true;
            }
        }

        public async Task ConnectAsync(string host)
        {
            await channel.ConnectAsync(host);
        }
    }
}