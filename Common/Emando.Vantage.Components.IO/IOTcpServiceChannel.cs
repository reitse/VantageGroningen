using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Emando.Vantage.Components.IO
{
    public class IOTcpServiceChannel : IOTcpClient, IIOServiceChannel
    {
        private readonly string name;

        public IOTcpServiceChannel(string name)
        {
            this.name = name;
        }

        #region IIOServiceChannel Members

        public async Task SetAsync(int id, object value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            string type;
            if (!Types.TryGetValue(value.GetType(), out type))
                return;

            await WriteMessageAsync("set", new Dictionary<string, object>
            {
                { "id", id },
                { "type", type },
                { "value", value }
            });
        }

        #endregion

        public event ChannelUpdateEventHandler Update;

        protected virtual void OnUpdate(ChannelUpdateEventArgs e)
        {
            var handler = Update;
            if (handler != null)
                handler(this, e);
        }

        public override async Task HandshakeAsync()
        {
            await WriteMessageAsync("hello", new Dictionary<string, object>
            {
                { "name", name }
            });
        }

        protected override void HandleMessage(string command, IDictionary<string, string> parameters)
        {
            switch (command.ToLowerInvariant())
            {
                case "update":
                    int id = Int32.Parse(parameters["id"]);
                    Func<string, object> converter;
                    if (Converters.TryGetValue(parameters["type"].ToLowerInvariant(), out converter))
                        OnUpdate(new ChannelUpdateEventArgs(id, converter(parameters["value"])));
                    break;
            }
        }
    }
}