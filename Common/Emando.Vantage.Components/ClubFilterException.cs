using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Properties;

namespace Emando.Vantage.Components
{
    [Serializable]
    public class ClubFilterException : Exception
    {
        public ClubFilterException() : this(Resources.ClubCodeMatchFailed)
        {
        }

        public ClubFilterException(string message) : base(message)
        {
        }

        public ClubFilterException(string filter, int? clubCode) : this(string.Format(Resources.ClubCodeMatchFailedFormat, clubCode, filter))
        {
        }

        public ClubFilterException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ClubFilterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}