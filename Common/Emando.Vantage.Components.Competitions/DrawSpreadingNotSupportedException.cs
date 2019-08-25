using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Competitions.Properties;

namespace Emando.Vantage.Components.Competitions
{
    [Serializable]
    public class DrawSpreadingNotSupportedException : NotSupportedException
    {
        public DrawSpreadingNotSupportedException() : this(Resources.DrawSpreadingNotSupported)
        {
        }

        public DrawSpreadingNotSupportedException(string message) : base(message)
        {
        }

        public DrawSpreadingNotSupportedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DrawSpreadingNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}