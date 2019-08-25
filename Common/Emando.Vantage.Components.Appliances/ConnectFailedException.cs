using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Appliances.Properties;

namespace Emando.Vantage.Components.Appliances
{
    [Serializable]
    public class ConnectFailedException : Exception
    {
        public ConnectFailedException() : this(Resources.ConnectFailed)
        {
        }

        public ConnectFailedException(string message) : base(message)
        {
        }

        public ConnectFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConnectFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}