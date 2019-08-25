using System.ServiceModel;

namespace Emando.Vantage.Server.Services.Competitions
{
    [ServiceContract(Namespace = "http://emandovantage.com/2014/02/Server/Competitions")]
    public interface ITimingInstanceEvents
    {
        [OperationContract(IsOneWay = true)]
        void PrioritiesUpdated(string[] instanceNames);

        [OperationContract(IsOneWay = true)]
        void PausedChanged(bool isPaused);
    }
}