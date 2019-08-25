using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Properties;

namespace Emando.Vantage.Components
{
    [Serializable]
    public class PersonNotFoundException : Exception
    {
        public PersonNotFoundException() : this(Resources.PersonNotFound)
        {
        }

        public PersonNotFoundException(string message) : base(message)
        {
        }

        public PersonNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PersonNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}