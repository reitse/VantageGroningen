using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Properties;

namespace Emando.Vantage.Components
{
    [Serializable]
    public class ClubNotFoundException : Exception
    {
        public ClubNotFoundException() : this(Resources.ClubNotFound)
        {
        }

        public ClubNotFoundException(string message) : base(message)
        {
        }

        public ClubNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ClubNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}