using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Competitions.Properties;

namespace Emando.Vantage.Components.Competitions
{
    [Serializable]
    public class DistanceHasResultsException : Exception
    {
        public DistanceHasResultsException() : this(Resources.DistanceHasResults)
        {
        }

        public DistanceHasResultsException(string message) : base(message)
        {
        }

        public DistanceHasResultsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DistanceHasResultsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}