using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Competitions.Properties;

namespace Emando.Vantage.Components.Competitions
{
    [Serializable]
    public class RaceNotFoundException : Exception
    {
        public RaceNotFoundException() : this(Resources.RaceNotFound)
        {
        }

        public RaceNotFoundException(string message) : base(message)
        {
        }

        public RaceNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RaceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}