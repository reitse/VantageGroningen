using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Server.Services.Competitions
{
    [ServiceContract(Namespace = "http://emandovantage.com/2014/02/Server/Competitions", SessionMode = SessionMode.Required, CallbackContract = typeof(ITrackDistanceEvents))]
    public interface ITrackDistanceTimingWorkflowService : IDistanceTimingWorkflowService
    {
        [OperationContract(IsInitiating = false)]
        Task<bool> InsertLapAsync(Guid raceId, PresentationSource presentationSource, TimeSpan time);

        [OperationContract(IsInitiating = false)]
        Task<bool> InsertMissedLapAsync(Guid raceId);

        [OperationContract(IsInitiating = false)]
        Task<bool> UpdateLapAsync(Guid raceId, PresentationSource presentationSource, TimeSpan time, TimeSpan newTime);

        [OperationContract(IsInitiating = false)]
        Task<bool> PresentLapAsync(Guid raceId, PresentationSource presentationSource, TimeSpan time);

        [OperationContract(IsInitiating = false)]
        Task<bool> SwapPresentedLapAsync(Guid raceId, PresentationSource oldPresentationSource, TimeSpan oldTime, PresentationSource newPresentationSource, TimeSpan newTime);

        [OperationContract(IsInitiating = false)]
        Task<bool> DeleteLapAsync(Guid raceId, PresentationSource presentationSource, TimeSpan time);

        [OperationContract(IsInitiating = false)]
        Task<bool> MoveLapAsync(Guid sourceId, PresentationSource sourcePresentationSource, TimeSpan sourceTime, Guid destinationId);

        [OperationContract(IsInitiating = false)]
        Task<bool> ResetToLapIndexAsync(Guid raceId, int index, TimeInvalidReason? timeInvalidReason = null);

        [OperationContract(IsInitiating = false)]
        Task UpdateTimeInfoAsync(Guid raceId, TimeInfo timeInfo);
    }
}