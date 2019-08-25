using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Emando.Vantage.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Server.Services.Competitions
{
    [ServiceContract(Namespace = "http://emandovantage.com/2014/02/Server/Competitions", SessionMode = SessionMode.Required, CallbackContract = typeof(IDistanceEvents))]
    public interface IDistanceTimingWorkflowService
    {
        [OperationContract]
        Task ActivateAsync(string instanceName, Guid distanceId, DistanceTimingWorkflowServiceSettings settings);

        [OperationContract(IsInitiating = false, IsTerminating = true)]
        Task DeactivateAsync();

        [OperationContract(IsInitiating = false)]
        Task<PresentationSource[]> GetPresentationSourcesAsync();

        [OperationContract(IsInitiating = false)]
        Task SetPresentationSourceAsync(PresentationSource? presentationSource);

        [OperationContract(IsInitiating = false)]
        Task RecoverFromStartAsync(HeatStart start);

        [OperationContract(IsInitiating = false)]
        Task<Race[]> GetRacesAsync();

        [OperationContract(IsInitiating = false)]
        Task<Race> GetRaceAsync(Guid raceId);

        [OperationContract(IsInitiating = false)]
        Task ActivateHeatAsync(Heat heat);

        [OperationContract(IsInitiating = false)]
        Task DeactivateHeatAsync(Heat heat);

        [OperationContract(IsInitiating = false)]
        Task StartHeatAsync(Heat heat, HeatStart start);

        [OperationContract(IsInitiating = false)]
        Task ClearHeatAsync(Heat heat);

        [OperationContract(IsInitiating = false)]
        Task CommitHeatAsync(Heat heat);

        [OperationContract(IsInitiating = false)]
        Task UpdateTimeInvalidReasonAsync(Guid raceId, TimeInvalidReason? timeInvalidReason);
    }
}