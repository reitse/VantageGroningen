using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Properties;

namespace Emando.Vantage.Components
{
    [Serializable]
    public class InvalidDisciplineException : Exception
    {
        public InvalidDisciplineException() : this(Resources.InvalidDiscipline)
        {
        }

        public InvalidDisciplineException(string message) : base(message)
        {
        }

        public InvalidDisciplineException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidDisciplineException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}