using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Competitions.Properties;

namespace Emando.Vantage.Components.Competitions
{
    public class DistanceCombinationNotFoundException : Exception
    {
        public DistanceCombinationNotFoundException() : this(Resources.DistanceCombinationNotFound)
        {
        }

        public DistanceCombinationNotFoundException(string message) : base(message)
        {
        }

        public DistanceCombinationNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DistanceCombinationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}