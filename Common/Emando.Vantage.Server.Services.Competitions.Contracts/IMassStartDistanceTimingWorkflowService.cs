using System.ServiceModel;
using System.Threading.Tasks;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Server.Services.Competitions
{
    [ServiceContract(Namespace = "http://emandovantage.com/2014/02/Server/Competitions", SessionMode = SessionMode.Required,
        CallbackContract = typeof(IMassStartDistanceEvents))]
    public interface IMassStartDistanceTimingWorkflowServiceBase : ITrackDistanceTimingWorkflowService
    {
        [OperationContract(IsInitiating = false)]
        Task<bool> AddLapIndexPointsAsync(Heat heat, int index, string type);

        [OperationContract(IsInitiating = false)]
        Task<bool> RemoveLapIndexPointsAsync(Heat heat, int index);
    }
}