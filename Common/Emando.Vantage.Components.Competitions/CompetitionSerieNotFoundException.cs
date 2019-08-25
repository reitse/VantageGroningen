using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Competitions.Properties;

namespace Emando.Vantage.Components.Competitions
{
    [Serializable]
    public class CompetitionSerieNotFoundException : Exception
    {
        public CompetitionSerieNotFoundException() : this(Resources.CompetitionSerieNotFound)
        {
        }

        public CompetitionSerieNotFoundException(string message) : base(message)
        {
        }

        public CompetitionSerieNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CompetitionSerieNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}