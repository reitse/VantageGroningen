using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Competitions.Properties;

namespace Emando.Vantage.Components.Competitions
{
    [Serializable]
    public class DrawModeNotSupportedException : NotSupportedException
    {
        public DrawModeNotSupportedException() : this(Resources.DrawModeNotSupported)
        {
        }

        public DrawModeNotSupportedException(string message) : base(message)
        {
        }

        public DrawModeNotSupportedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DrawModeNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}