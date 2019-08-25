using System.Collections.Generic;
using System.ServiceModel;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Server.Services.Competitions
{
    [ServiceContract(Namespace = "http://emandovantage.com/2014/02/Server/Competitions")]
    public interface IMassStartDistanceEvents : ITrackDistanceEvents
    {
        [OperationContract(IsOneWay = true)]
        void LapIndexPointsChanged(Heat heat, int round, string type, Dictionary<int, decimal> points);
    }
}