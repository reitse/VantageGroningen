using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Microsoft.Practices.Unity;

namespace Emando.Vantage.Infrastructure.SignalR
{
    public class UnitySignalRDependencyResolver : DefaultDependencyResolver
    {
        private readonly IUnityContainer container;

        public UnitySignalRDependencyResolver(IUnityContainer container)
        {
            this.container = container;
        }

        public override object GetService(Type serviceType)
        {
            return container.IsRegistered(serviceType) ? container.Resolve(serviceType) : base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            return container.IsRegistered(serviceType) ? container.ResolveAll(serviceType) : base.GetServices(serviceType);
        }
    }
}