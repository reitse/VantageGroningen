using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Competitions.Properties;

namespace Emando.Vantage.Components.Competitions
{
    [Serializable]
    public class DistanceNotFoundException : Exception
    {
        public DistanceNotFoundException() : this(Resources.DistanceNotFound)
        {
        }

        public DistanceNotFoundException(string message) : base(message)
        {
        }

        public DistanceNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DistanceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}