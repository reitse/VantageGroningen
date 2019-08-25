using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Competitions.Properties;

namespace Emando.Vantage.Components.Competitions
{
    [Serializable]
    public class InvalidCompetitionResultsStatusException : Exception
    {
        public InvalidCompetitionResultsStatusException() : this(Resources.InvalidCompetitionResultsState)
        {
        }

        public InvalidCompetitionResultsStatusException(string message) : base(message)
        {
        }

        public InvalidCompetitionResultsStatusException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidCompetitionResultsStatusException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}