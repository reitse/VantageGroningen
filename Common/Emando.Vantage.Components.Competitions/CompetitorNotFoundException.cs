using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Competitions.Properties;

namespace Emando.Vantage.Components.Competitions
{
    [Serializable]
    public class CompetitorNotFoundException : Exception
    {
        public CompetitorNotFoundException() : this(Resources.CompetitorNotFound)
        {
        }

        public CompetitorNotFoundException(string message) : base(message)
        {
        }

        public CompetitorNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CompetitorNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}