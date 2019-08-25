using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Properties;

namespace Emando.Vantage.Components
{
    [Serializable]
    public class PersonCategoryNotFoundException : Exception
    {
        public PersonCategoryNotFoundException() : this(Resources.PersonCategoryNotFound)
        {
        }

        public PersonCategoryNotFoundException(string message) : base(message)
        {
        }

        public PersonCategoryNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PersonCategoryNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}