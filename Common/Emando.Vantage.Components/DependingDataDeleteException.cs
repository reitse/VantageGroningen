using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Properties;

namespace Emando.Vantage.Components
{
    public class DependingDataDeleteException : Exception
    {
        public DependingDataDeleteException() : this(Resources.DependingDataDeleteFailed)
        {
        }

        public DependingDataDeleteException(string message) : base(message)
        {
        }

        public DependingDataDeleteException(Exception innerException) : this(Resources.DependingDataDeleteFailed, innerException)
        {
        }

        public DependingDataDeleteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DependingDataDeleteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}