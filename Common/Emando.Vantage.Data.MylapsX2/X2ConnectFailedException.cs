using System;
using System.Runtime.Serialization;
using Emando.Vantage.Data.MylapsX2.Properties;

namespace Emando.Vantage.Data.MylapsX2
{
    [Serializable]
    public class X2ConnectFailedException : Exception
    {
        public X2ConnectFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public X2ConnectFailedException(string message) : base(message)
        {
        }

        public X2ConnectFailedException() : this(Resources.ConnectFailed)
        {
        }

        protected X2ConnectFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}