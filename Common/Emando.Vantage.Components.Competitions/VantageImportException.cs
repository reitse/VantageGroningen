using System;
using System.Runtime.Serialization;

namespace Emando.Vantage.Components.Competitions
{
    [Serializable]
    public class VantageImportException : Exception
    {
        public VantageImportException()
        {
        }

        public VantageImportException(string message) : base(message)
        {
        }

        public VantageImportException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VantageImportException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}