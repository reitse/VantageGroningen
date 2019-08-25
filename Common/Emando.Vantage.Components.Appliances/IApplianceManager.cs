using System.Collections.Generic;

namespace Emando.Vantage.Components.Appliances
{
    public interface IApplianceManager
    {
        IEnumerable<string> Appliances<T>() where T : IAppliance;

        T Create<T>(string name) where T : IAppliance;
    }
}