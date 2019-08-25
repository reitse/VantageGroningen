using System;
using System.ServiceModel;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Server.Services.Appliances.Test
{
    [ServiceContract(Namespace = "http://emandovantage.com/2014/10/Appliances/Test", SessionMode = SessionMode.Required)]
    public interface ITestApplianceEventsService
    {
        [OperationContract]
        void Register(string instanceName);

        [OperationContract(IsInitiating = false)]
        void Start(long eventId, DateTime when, bool isResend);

        [OperationContract(IsInitiating = false)]
        void OpticalPassing(long eventId, long where, DateTime when);

        [OperationContract(IsInitiating = false)]
        void ManualPassing(long eventId, DateTime when, long what);

        [OperationContract(IsInitiating = false)]
        void PhotofinishPassing(long eventId, DateTime when, RacePath what);

        [OperationContract(IsInitiating = false)]
        void TransponderPassing(long eventId, long where, DateTime when, long what);
    }
}