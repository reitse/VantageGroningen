using System.Collections.Generic;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.Objects;

namespace MylapsSDK.NotifyHandlers
{
    
    ///////////////////////////////////////////////////////////////////
    //SDK delegates
    ///////////////////////////////////////////////////////////////////
    
    //!< A delegate that gets called when the message queue has data that can be processed.
    public delegate void OnNotifyMsgQueueHandler(SDK subject);

    //!< A delgate that get called when available appliances are inserted, updated or removed.
    public delegate void OnNotifyApplianceHandler(MDP_NOTIFY_TYPE nType, AvailableAppliance appliance, SDK subject);

    ///////////////////////////////////////////////////////////////////
    // Generic Delegates
    ///////////////////////////////////////////////////////////////////

    public delegate void OnNotifyObjects<T>(MDP_NOTIFY_TYPE nType, List<T> objectList, MTA subject);

    public delegate void OnNotifyObjects<ObjectClass, ObjectHandleClass>(MDP_NOTIFY_TYPE nType, List<ObjectClass> eventList, ObjectHandleClass objHandle);

    public delegate void OnNotifyEvent<T>(MDP_NOTIFY_TYPE nType, List<T> eventList, EventData eventData);

    public delegate void Action<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

    ///////////////////////////////////////////////////////////////////
    //General MTA delegates
    ///////////////////////////////////////////////////////////////////

    //!< A delegate that gets called when a MTA is connected.
    public delegate void OnNotifyConnectHandler(bool isConnected, MTA subject);

    //!< A delegate that gets called when the connection state changes.
    public delegate void OnNotifyConnectionStateHandler(CONNECTIONSTATE connectionState, MTA subject);

    //!< A delegate that gets called when SystemSetup gets inserted.
    public delegate void OnNotifySystemSetupHandler(MDP_NOTIFY_TYPE nType, SystemSetup systemSetup, MTA subject);

    //!< A delegate that gets called when segments get inserted, updated or removed.
    public delegate void OnNotifySegmentHandler(MDP_NOTIFY_TYPE nType, List<Segment> segmentList, MTA subject);

    ///////////////////////////////////////////////////////////////////
    //MTA delegates
    ///////////////////////////////////////////////////////////////////

    //!< A delegate that gets called when loops get inserted, updated or removed.
    public delegate void OnNotifyLoopHandler(MDP_NOTIFY_TYPE nType, List<Loop> loopList, MTA subject);

    //!< A delegate that gets called when passings get inserted.
    public delegate void OnNotifyPassingHandler(MDP_NOTIFY_TYPE nType, List<Passing> passingList, EventData subject);

    //!< A delegate that gets called when passing-triggerss get inserted.
    public delegate void OnNotifyPassingTriggerHandler(MDP_NOTIFY_TYPE nType, List<PassingTrigger> passingTriggerList, EventData subject);

    //!< A delegate that gets called when passings-firstcontact get inserted.
    public delegate void OnNotifyPassingFirstContactHandler(MDP_NOTIFY_TYPE nType, PassingFirstContact firstContact, MTA subject);

    //!< A delegate that gets called when two-way-messages get inserted.
    public delegate void OnNotifyTwoWayMessageHandler(MDP_NOTIFY_TYPE nType, List<TwoWayMessage> twoWayMessageList, MTA subject);
    
    //!< A delegate that gets called when a transponder get inserted.
    public delegate void OnNotifyTransponderHandler(MDP_NOTIFY_TYPE nType, List<Transponder> transponderList, MTA subject);

    //!< A delegate that gets called when a transponder-group get inserted.
    public delegate void OnNotifyTransponderGroupHandler(MDP_NOTIFY_TYPE nType, List<TransponderGroup> transponderGroupList, MTA subject);

    //!< A delegate that gets called when transponder-status get inserted.
    public delegate void OnNotifyTransponderStatusHandler(MDP_NOTIFY_TYPE nType, TransponderStatus transponderStatus, MTA subject);

    //!< A delegate that gets called when decoders get inserted, updated or removed.
    public delegate void OnNotifyDecoderHandler(MDP_NOTIFY_TYPE nType, List<Decoder> decoderList, MTA mta);

    //!< A delegate that gets called when decoder-preset-groups get inserted, updated or removed.
    public delegate void OnNotifyDecoderPresetGroupHandler(MDP_NOTIFY_TYPE nType, List<DecoderPresetGroup> decoderPresetGroupList, MTA mta);

    //!< A delegate that gets called when decoder statuses get inserted or updated.
    public delegate void OnNotifyDecoderStatusHandler(MDP_NOTIFY_TYPE nType, DecoderStatus decoderStatus, MTA mta);

    //!< A delegate that gets called when IO terminals get inserted, updated or removed.
    public delegate void OnNotifyIOTerminalHandler(MDP_NOTIFY_TYPE nType, List<IOTerminal> ioterminalList, MTA mta);

    //!< A delegate that gets called when aux events get inserted or updated.
    public delegate void OnNotifyAuxEventHandler(MDP_NOTIFY_TYPE nType, List<AuxEvent> auxEventList, MTA mta);

    //!< A delegate that gets called when aux statuses get inserted or updated.
    public delegate void OnNotifyAuxStatusHandler(MDP_NOTIFY_TYPE nType, AuxStatus auxStatus, MTA mta);

    //!< A delegate that gets called when beacon download configs get inserted or updated or removed.
    public delegate void OnNotifyBeaconDownloadConfigHandler(MDP_NOTIFY_TYPE nType, List<BeaconDownloadConfig> beaconDownloadConfigList, MTA mta);
   
    //!< A delegate that gets called when beacon download statuses get inserted.
    public delegate void OnNotifyBeaconDownloadStatusHandler(MDP_NOTIFY_TYPE nType, List<BeaconDownloadStatus> beaconDownloadStatusList, MTA mta);
    
    //!< A delegate that gets called when beacon download triggers get inserted.
    public delegate void OnNotifyBeaconDownloadTriggerHandler(MDP_NOTIFY_TYPE nType, List<BeaconDownloadTrigger> beaconDownloadTriggerList, MTA mta);

    //!< A delegate that gets called when beacon logs get inserted.
    public delegate void OnNotifyBeaconLogHandler(MDP_NOTIFY_TYPE nType, BeaconLog log, List<BeaconData> beaconDataList, MTA mta);

    //!< A delegate that gets called when tracksolutiongroups get inserted, updated or removed.
    public delegate void OnNotifyTrackSolutionGroupHandler(MDP_NOTIFY_TYPE nType, List<TrackSolutionGroup> trackSolutionGroupList, MTA subject);

    //!< A delegate that gets called when tracksolutions get inserted, updated or removed.
    public delegate void OnNotifyTrackSolutionHandler(MDP_NOTIFY_TYPE nType, List<TrackSolution> trackSolutionList, MTA subject);

    //!< A delegate that gets called when a sector get inserted, updated or removed.
    public delegate void OnNotifySectorHandler(MDP_NOTIFY_TYPE nType, List<Sector> sectorList, MTA subject);

    //!< A delegate that gets called when a sequence get inserted, updated or removed.
    public delegate void OnNotifySequenceHandler(MDP_NOTIFY_TYPE nType, List<Sequence> sequenceList, MTA subject);

    //!< A delegate that gets called when a sequence-segment get inserted, updated or removed.
    public delegate void OnNotifySequenceSegmentHandler(MDP_NOTIFY_TYPE nType, List<SequenceSegment> sequenceSegmentList, MTA subject);
}
