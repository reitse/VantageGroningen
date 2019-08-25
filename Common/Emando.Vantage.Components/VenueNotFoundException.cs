using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Properties;

namespace Emando.Vantage.Components
{
    [Serializable]
    public class VenueNotFoundException : Exception
    {
        public VenueNotFoundException() : this(Resources.VenueNotFound)
        {
        }

        public VenueNotFoundException(string message) : base(message)
        {
        }

        public VenueNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VenueNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}