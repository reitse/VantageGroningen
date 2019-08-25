using System;

namespace Emando.Vantage.Server.Services.IO.Client
{
    public delegate void ChannelUpdateEventHandler(object sender, ChannelUpdateEventArgs e);

    public class ChannelUpdateEventArgs : EventArgs
    {
        private readonly int id;
        private readonly object value;

        public ChannelUpdateEventArgs(int id, object value)
        {
            this.id = id;
            this.value = value;
        }

        public int Id
        {
            get { return id; }
        }

        public object Value
        {
            get { return value; }
        }
    }
}