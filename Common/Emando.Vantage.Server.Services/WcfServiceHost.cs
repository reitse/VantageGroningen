using System;
using System.ServiceModel;

namespace Emando.Vantage.Server.Services
{
    public class WcfServiceHost : IServiceHost
    {
        private bool isDisposed;
        private readonly ServiceHost serviceHost;

        public WcfServiceHost(Type serviceType, params Uri[] baseAddresses)
        {
            serviceHost = new ServiceHost(serviceType, baseAddresses);
        }

        public WcfServiceHost(object singletonInstance, params Uri[] baseAddresses)
        {
            serviceHost = new ServiceHost(singletonInstance, baseAddresses);
        }

        protected WcfServiceHost(ServiceHost serviceHost)
        {
            this.serviceHost = serviceHost;
        }

        public static WcfServiceHost Wrap(ServiceHost serviceHost)
        {
            return new WcfServiceHost(serviceHost);
        }

        ~WcfServiceHost()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                    serviceHost.Close();

                isDisposed = true;
            }
        }

        #region IServiceHost Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Open()
        {
            serviceHost.Open();
        }

        public void Close()
        {
            serviceHost.Close();
        }

        public Uri BaseAddress
        {
            get { return serviceHost.Description.Endpoints[0].ListenUri; }
        }

        #endregion
    }
}