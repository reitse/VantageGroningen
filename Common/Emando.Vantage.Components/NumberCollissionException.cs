using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Properties;

namespace Emando.Vantage.Components
{
    [Serializable]
    public class NumberCollissionException : Exception
    {
        public NumberCollissionException() : this(Resources.NumberCollission)
        {
        }

        public NumberCollissionException(string message) : base(message)
        {
        }

        public NumberCollissionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public NumberCollissionException(Exception innerException) : this(Resources.NumberCollission, innerException)
        {
        }

        protected NumberCollissionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}