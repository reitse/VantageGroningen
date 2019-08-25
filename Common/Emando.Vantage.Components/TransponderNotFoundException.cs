using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Properties;

namespace Emando.Vantage.Components
{
    [Serializable]
    public class TransponderNotFoundException : Exception
    {
        public TransponderNotFoundException() : this(Resources.TransponderNotFound)
        {
        }

        public TransponderNotFoundException(string message) : base(message)
        {
        }

        public TransponderNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TransponderNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}