using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Emando.Vantage.Components.IO.Properties;

namespace Emando.Vantage.Components.IO
{
    public class IOTcpClientChannel : IOTcpClient, IIOClientChannel
    {
        private readonly IIOEventPublisher events;

        public IOTcpClientChannel(TcpClient client, IIOEventPublisher events) : base(client)
        {
            this.events = events;
        }

        public string Name { get; private set; }

        #region IIOClientChannel Members

        public void Update(int id, object value)
        {
            string type;
            if (!Types.TryGetValue(value.GetType(), out type))
                return;

            WriteMessage("update", new Dictionary<string, object>
            {
                { "id", id },
                { "type", type },
                { "value", value }
            });
        }

        #endregion

        public override async Task HandshakeAsync()
        {
            IDictionary<string, string> parameters;
            string command = DecodeMessage(await ReadMessageAsync(), out parameters);
            if (command != "hello")
                throw new IOException(Resources.InvalidHandshakeMessage);
            Name = parameters["name"];
        }

        protected override void HandleMessage(string command, IDictionary<string, string> parameters)
        {
            switch (command)
            {
                case "set":
                    int id = Int32.Parse(parameters["id"]);
                    Func<string, object> converter;
                    if (Converters.TryGetValue(parameters["type"].ToLowerInvariant(), out converter))
                        events.Set(id, converter(parameters["value"]));
                    break;
            }
        }
    }
}