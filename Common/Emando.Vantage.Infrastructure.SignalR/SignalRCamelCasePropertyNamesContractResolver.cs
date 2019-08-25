using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace Emando.Vantage.Infrastructure.SignalR
{
    public class SignalRCamelCasePropertyNamesContractResolver : IContractResolver
    {
        private readonly IContractResolver camelCaseResolver = new CamelCasePropertyNamesContractResolver();
        private readonly IContractResolver defaultResolver = new DefaultContractResolver();

        public SignalRCamelCasePropertyNamesContractResolver()
        {
            ContractAssemblies = new List<Assembly>();
        }

        public IList<Assembly> ContractAssemblies { get; set; }

        public JsonContract ResolveContract(Type type)
        {
            if (ContractAssemblies != null && ContractAssemblies.Contains(type.Assembly))
                return camelCaseResolver.ResolveContract(type);

            return defaultResolver.ResolveContract(type);
        }
    }
}