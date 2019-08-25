using System.Collections.Generic;

namespace Emando.Vantage.Components.Adapters
{
    public interface IAdapterManager
    {
        IEnumerable<AdapterRegistration> GetRegistrations<T>() where T : IAdapter;

        bool IsRegistered<T>(string name) where T : IAdapter;

        T Create<T>(string name) where T : IAdapter;
    }
}