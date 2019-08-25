using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Properties;

namespace Emando.Vantage.Components
{
    public class ClassFilterException : Exception
    {
        public ClassFilterException() : this(Resources.ClassMatchFailed)
        {
        }

        public ClassFilterException(string classFilter, int? @class) : this(string.Format(Resources.ClassMatchFailedFormat, classFilter, @class))
        {
        }

        public ClassFilterException(string message) : base(message)
        {
        }

        public ClassFilterException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ClassFilterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}