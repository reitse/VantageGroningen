using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Properties;

namespace Emando.Vantage.Components
{
    [Serializable]
    public class TransponderBagNotFoundException : Exception
    {
        public TransponderBagNotFoundException() : this(Resources.TransponderBagNotFound)
        {
        }

        public TransponderBagNotFoundException(string message) : base(message)
        {
        }

        public TransponderBagNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TransponderBagNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}