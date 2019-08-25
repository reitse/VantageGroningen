using System.ServiceModel;
using Emando.Vantage.Competitions.SpeedSkating.LongTrack;

namespace Emando.Vantage.Server.Services.Competitions.SpeedSkating.LongTrack
{
    [ServiceContract(Namespace = "http://emandovantage.com/2014/02/Server/Competitions/SpeedSkating/LongTrack")]
    public interface IIndividualPairsDistanceEvents : ITrackDistanceEvents
    {
        [OperationContract(IsOneWay = true)]
        void LaneQueueChanged(LaneQueueState state);

        [OperationContract(IsOneWay = true)]
        void LaneQueuePredictionEnabledChanged(bool value);
    }
}