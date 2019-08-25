using System;
using System.Runtime.Serialization;
using Emando.Vantage.Components.Appliances.Properties;

namespace Emando.Vantage.Components.Appliances
{
    [Serializable]
    public class ApplianceNotConfiguredException : Exception
    {
        public ApplianceNotConfiguredException() : this(Resources.NotConfigured)
        {
        }

        public ApplianceNotConfiguredException(string message) : base(message)
        {
        }

        public ApplianceNotConfiguredException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ApplianceNotConfiguredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}