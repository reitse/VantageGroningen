using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Properties;

namespace Emando.Vantage.Components
{
    [Serializable]
    public class CategoryFilterException : Exception
    {
        public CategoryFilterException() : this(Resources.CategoryMatchFailed)
        {
        }

        public CategoryFilterException(string message) : base(message)
        {
        }

        public CategoryFilterException(string filter, string category) : this(string.Format(Resources.CategoryMatchFailedFormat, filter, category))
        {
        }

        public CategoryFilterException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CategoryFilterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}