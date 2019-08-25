using System;
using System.Runtime.Serialization;

namespace Emando.Vantage.Api.Client
{
    [Serializable]
    public class ApiClientException : ApiException
    {
        public ApiClientException()
        {
        }

        public ApiClientException(string message) : base(message)
        {
        }

        public ApiClientException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ApiClientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}