using System.ServiceModel;

namespace Emando.Vantage.Server.Services.Competitions.SpeedSkating.LongTrack
{
    [ServiceContract(Namespace = "http://emandovantage.com/2014/02/Server/Competitions/SpeedSkating/LongTrack")]
    public interface ITeamPursuitPairsDistanceEvents : ITrackDistanceEvents
    {
    }
}