using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Competitions.Properties;

namespace Emando.Vantage.Components.Competitions
{
    public class PresentationSourceNotFoundException : Exception
    {
        public PresentationSourceNotFoundException() : this(Resources.PresentationSourceNotFound)
        {
        }

        public PresentationSourceNotFoundException(string message) : base(message)
        {
        }

        public PresentationSourceNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PresentationSourceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}