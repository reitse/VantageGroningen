using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Properties;

namespace Emando.Vantage.Components
{
    [Serializable]
    public class HomeVenueFilterException : Exception
    {
        public HomeVenueFilterException() : this(Resources.HomeVenueMatchFailed)
        {
        }

        public HomeVenueFilterException(string message) : base(message)
        {
        }

        public HomeVenueFilterException(string filter, string venueCode) : this(string.Format(Resources.HomeVenueMatchFailedFormat, venueCode, filter))
        {
        }

        public HomeVenueFilterException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HomeVenueFilterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}