using System;
using System.Threading;
using System.Threading.Tasks;

namespace Emando.Vantage.Components.Appliances
{
    public interface IAppliance : IDisposable
    {
        string Name { get; }

        Task ConfigureAsync(string venueCode, string discipline, string instanceName, string applicationName, string applicationInstanceName);

        Task ConnectAsync(CancellationToken cancellationToken);

        Task DisconnectAsync();
    }
}