using System.ServiceModel;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Server.Services.Competitions
{
    [ServiceContract(Namespace = "http://emandovantage.com/2014/02/Server/Competitions")]
    public interface IDistanceEvents
    {
        [OperationContract(IsOneWay = true)]
        void Deactivated();

        [OperationContract(IsOneWay = true)]
        void PresentationSourcesChanged(PresentationSource[] presentationSources);

        [OperationContract(IsOneWay = true)]
        void PresentationSourceChanged(PresentationSource? presentationSource);

        [OperationContract(IsOneWay = true)]
        void Activated(Competition competition, Distance distance);
    }
}