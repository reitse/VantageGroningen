using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Emando.Vantage.Entities;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Server.Services.Competitions
{
    [ServiceContract(Namespace = "http://emandovantage.com/2014/02/Server/Competitions", SessionMode = SessionMode.Required,
        CallbackContract = typeof(ITimingInstanceEvents))]
    public interface ITimingInstanceService : IDisposable
    {
        [OperationContract(IsInitiating = true)]
        Task RegisterAsync(string name, string applicationName);

        [OperationContract(IsInitiating = false, IsTerminating = true)]
        Task UnregisterAsync();

        [OperationContract(IsInitiating = false)]
        Task<bool> TakePriorityAsync();

        [OperationContract(IsInitiating = false)]
        Task<bool> HandoverPriorityAsync();

        [OperationContract(IsInitiating = false)]
        Task<Venue[]> GetVenuesAsync();

        [OperationContract(IsInitiating = false)]
        Task<Competition[]> GetCompetitionsAsync(string venueCode, string discipline);

        [OperationContract(IsInitiating = false)]
        Task<Competition> SelectCompetitionAsync(Guid competitionId);

        [OperationContract(IsInitiating = false)]
        Task<string[]> GetAppliancesAsync();

        [OperationContract(IsInitiating = false)]
        Task ConnectApplianceAsync(string name, string instanceName, TimeSpan? resendWindow);

        [OperationContract(IsInitiating = false)]
        Task DisconnectApplianceAsync(string name, string instanceName);

        [OperationContract(IsInitiating = false)]
        Task PauseAsync();

        [OperationContract(IsInitiating = false)]
        Task ResumeAsync();
    }
}