using System;
using System.Runtime.Serialization;
using Emando.Vantage.Api.Client.Properties;

namespace Emando.Vantage.Api.Client
{
    [Serializable]
    public class ApiResourceNotFoundException : ApiClientException
    {
        public ApiResourceNotFoundException() : this(Resources.ApiResourceNotFound)
        {
        }

        public ApiResourceNotFoundException(string message) : base(message)
        {
        }

        public ApiResourceNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ApiResourceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}