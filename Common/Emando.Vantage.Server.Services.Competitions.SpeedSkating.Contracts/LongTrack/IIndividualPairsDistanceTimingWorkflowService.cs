using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Emando.Vantage.Competitions.SpeedSkating.LongTrack;

namespace Emando.Vantage.Server.Services.Competitions.SpeedSkating.LongTrack
{
    [ServiceContract(Namespace = "http://emandovantage.com/2014/02/Server/Competitions/SpeedSkating/LongTrack", SessionMode = SessionMode.Required,
        CallbackContract = typeof(IIndividualPairsDistanceEvents))]
    public interface IIndividualPairsDistanceTimingWorkflowService : ITrackDistanceTimingWorkflowService
    {
        [OperationContract(IsInitiating = false)]
        Task<bool> EnqueueInLaneAsync(Guid raceId, Lane lane, int index);

        [OperationContract(IsInitiating = false)]
        Task SetPredictionEnabledAsync(bool enabled);

        [OperationContract(IsInitiating = false)]
        Task<bool> MoveToBenchAsync(Guid raceId);
    }
}