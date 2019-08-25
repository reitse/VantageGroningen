using System;
using System.ServiceModel;
using Emando.Vantage.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Server.Services.Competitions
{
    [ServiceContract(Namespace = "http://emandovantage.com/2014/02/Server/Competitions")]
    public interface ITrackDistanceEvents : IDistanceEvents
    {
        [OperationContract(IsOneWay = true)]
        void RaceTransponderSeen(Guid raceId, long code, TimeSpan timeAgo);

        [OperationContract(IsOneWay = true)]
        void RacePassingAdded(RacePassingState passing);

        [OperationContract(IsOneWay = true)]
        void RacePassingUpdated(PresentationSource presentationSource, TimeSpan oldTime, RacePassingState passing);

        [OperationContract(IsOneWay = true)]
        void RaceLapAdded(RaceLapState lap);

        [OperationContract(IsOneWay = true)]
        void RaceLapUpdated(PresentationSource presentationSource, TimeSpan oldTime, RaceLapState update);

        [OperationContract(IsOneWay = true)]
        void RaceNextLapIndexChanged(Guid raceId, int index);

        [OperationContract(IsOneWay = true)]
        void RaceEstimatedLapsChanged(Guid raceId, CalculatedLap[] estimatedLaps);

        [OperationContract(IsOneWay = true)]
        void TimeInfoUpdated(Guid raceId, TimeInfo timeInfo);

        [OperationContract(IsOneWay = true)]
        void StartAdded(HeatStart start);

        [OperationContract(IsOneWay = true)]
        void HeatActivated(Heat heat, RaceState[] races);

        [OperationContract(IsOneWay = true)]
        void HeatDeactivated(Heat heat);

        [OperationContract(IsOneWay = true)]
        void HeatCleared(Heat heat);

        [OperationContract(IsOneWay = true)]
        void HeatCommitted(Heat heat, RaceState[] races);

        [OperationContract(IsOneWay = true)]
        void HeatStarted(Heat heat, HeatStart start, TimeSpan clock);

        [OperationContract(IsOneWay = true)]
        void HeatNextLapIndexChanged(Heat heat, int index);

        [OperationContract(IsOneWay = true)]
        void TimeInvalidReasonUpdated(Guid raceId, TimeInvalidReason? timeInvalidReason);

        [OperationContract(IsOneWay = true)]
        void UnhandledPassing(UnhandledPassing passing);
    }
}