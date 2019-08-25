using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Competitions.Properties;

namespace Emando.Vantage.Components.Competitions
{
    public class PresentationSourceNotSupportedException : Exception
    {
        public PresentationSourceNotSupportedException() : this(Resources.PresentationSourceNotSupported)
        {
        }

        public PresentationSourceNotSupportedException(string message) : base(message)
        {
        }

        public PresentationSourceNotSupportedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PresentationSourceNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}