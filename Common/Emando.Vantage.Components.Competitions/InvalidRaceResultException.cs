using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Competitions.Properties;

namespace Emando.Vantage.Components.Competitions
{
    public class InvalidRaceResultException : Exception
    {
        public InvalidRaceResultException() : this(Resources.InvalidRaceResult)
        {
        }

        public InvalidRaceResultException(string message) : base(message)
        {
        }

        public InvalidRaceResultException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidRaceResultException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}