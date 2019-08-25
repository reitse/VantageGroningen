using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Competitions.Properties;

namespace Emando.Vantage.Components.Competitions
{
    [Serializable]
    public class CompetitionNotFoundException : Exception
    {
        public CompetitionNotFoundException() : this(Resources.CompetitionNotFound)
        {
        }

        public CompetitionNotFoundException(string message) : base(message)
        {
        }

        public CompetitionNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CompetitionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}