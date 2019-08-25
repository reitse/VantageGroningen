namespace MylapsSDK.MylapsSDKLibrary
{

    public enum MDP_NOTIFY_TYPE
    {
        MDP_NOTIFY_SELECT,
        MDP_NOTIFY_INSERT,
        MDP_NOTIFY_UPDATE,
        MDP_NOTIFY_DELETE,
        MDP_NOTIFY_CLEAR,
    }

    public enum MDP_LOG_LEVEL
    {

        /// MDP_LOG_ERROR -> 0x00
        MDP_LOG_ERROR = 0,

        /// MDP_LOG_WARNING -> 0x01
        MDP_LOG_WARNING = 1,

        /// MDP_LOG_INFO -> 0x02
        MDP_LOG_INFO = 2,

        /// MDP_LOG_DEBUG1 -> 0x03
        MDP_LOG_DEBUG1 = 3,

        /// MDP_LOG_DEBUG2 -> 0x04
        MDP_LOG_DEBUG2 = 4,

        /// MDP_LOG_DEBUG3 -> 0x05
        MDP_LOG_DEBUG3 = 5,
    }

    public enum CONNECTIONSTATE
    {
        csInitial,
        csTryingConnect,
        csConnectFailed,
        csTryingAuthenticate,
        csAuthenticationFailed,
        csIsStreaming,
        csStoppedStreaming,
        csAutoReconnect,
        csForceDisconnect
    }

    public enum SYNCMETHOD
    {

        /// smRTC -> 0
        smRTC = 0,

        /// smGPS -> 1
        smGPS = 1,

        /// smNTP -> 2
        smNTP = 2,

        /// smDSMA -> 3
        smDSMA = 3,
    }

    public enum MTAOBJECTDATA
    {
        // Decoder-manager related objects.
        mtaDecoder = 0,				//!< The decoders.
        mtaIOTerminal,				//!< The I/O terminals.
        mtaLoop,					//!< The loops.
        mtaSegment,					//!< The segments.
        mtaLegacy01,				//!< Legacy define to make the enumeration compatible with older versions.
        mtaSystemSetupPicture,		//!< The system-setup picture (deprecated).
        mtaTransponder,				//!< The transponders.
        mtaTransponderGroup,		//!< The transponder-groups.
        // Beacon-manager related objects.
        mtaBeaconDownloadConfig,	//!< Beacon-download configuration information.
        // Track-manager related objects.
        mtaSector,					//!< Sector information.
        mtaSequence,				//!< Sequence information.
        mtaSequenceSegment,			//!< Sequence-segment information.
        mtaTrackSolution,			//!< Track-solution information.
        mtaTrackSolutionGroup,		//!< Track-solution-group information.
        // Legacy objects.
        mtaLegacy02,				//!< Legacy define to make the enumeration compatible with older versions.
        // System-setup manager related objects.
        mtaSystemSetup,				//!< System Setup. (Only use for checking the user permissions, setup are automatically subscribed and unsubscribed).
        // Competitor related objects.
        mtaCompetitor,				//!< Competitor information.
        mtaDecoderPresetGroup       //!< Decoder-preset-groups.
    }

    public enum MTAEVENTDATA
    {
        // Decoder-manager related 'events'.
        mtaAuxEvent,				//!< The auxiliary events.
        mtaAuxStatus,				//!< The auxiliary statuses.
        mtaDecoderStatus,			//!< The decoder statuses.
        mtaLoopTrigger,				//!< The loop trigger.
        mtaPassing,					//!< The passings.
        mtaPassingFirstContact,		//!< The passing first-contacts.
        mtaTransponderStatus,		//!< The transponder-statuses.
        mtaTwoWayMessage,			//!< The 2-way messages.
        // Beacon-manager related 'events'.
        mtaBeaconData,				//!< Beacon-data information.
        mtaBeaconDownloadStatus,	//!< Beacon-download status information.
        mtaBeaconDownloadTrigger,	//!< Beacon-download trigger information.
        mtaBeaconLog,				//!< Beacon-log information.
        // Decoder-manager related 'events'.
        mtaDriverInfo,				//!< The driver-information.
        mtaManualEvent,				//!< The manual-event information.
        mtaPassingTrigger           //!< The passing-trigger information.
    }

    /// generic delegate for multi object notifier
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void NativeNotifyMultiObjectDelegate(System.IntPtr handle, MDP_NOTIFY_TYPE nType, System.IntPtr structurearray, uint count, System.IntPtr context);

    /// generic delegate for single object notifier
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void NativeNotifySingleObjectDelegate(System.IntPtr handle, MDP_NOTIFY_TYPE nType, System.IntPtr ptrToStruct, System.IntPtr context);

    /// Return Type: void
    ///handle: mdp_sdk_handle_t->mdp_sdk_handle_dummystruct*
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyDefault(System.IntPtr sdkHandle, System.IntPtr context);

    /// Return Type: void
    ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
    ///is_connected: boolean
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyConnect(
        System.IntPtr mtaHandle,
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)] bool
            is_connected, System.IntPtr context);

    /// Return Type: void
    ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
    ///connection_state: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyConnectionState(
        System.IntPtr mtaHandle, uint connection_state, System.IntPtr context);

    /// Return Type: void
    ///handle: mdp_sdk_handle_t->mdp_sdk_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///appliance: availableappliance_t*
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyAppliance(
        System.IntPtr sdkHandle, MDP_NOTIFY_TYPE nType, System.IntPtr appliancePtr,
        System.IntPtr context);

    /// Return Type: void
    ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
    ///systemsetupid: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifySystemSetupSwitched(
        System.IntPtr mtaHandle, uint systemsetupid, System.IntPtr context);

    /// Return Type: void
    ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///decoderarray: decoder_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyDecoder(
        System.IntPtr mtaHandle, MDP_NOTIFY_TYPE nType, System.IntPtr decoderarray, uint count,
        System.IntPtr context);

    /// Return Type: void
    ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///decoderarray: decoderpresetgroup_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyDecoderPresetGroup(
        System.IntPtr mtaHandle, MDP_NOTIFY_TYPE nType, System.IntPtr decoderpresetgrouparray, uint count,
        System.IntPtr context);

    /// Return Type: void
    ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///ioterminalarray: ioterminal_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyIOTerminal(
        System.IntPtr mtaHandle, MDP_NOTIFY_TYPE nType, System.IntPtr ioterminalarray,
        uint count, System.IntPtr context);

    /// Return Type: void
    ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///looparray: loop_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyLoop(
        System.IntPtr mtaHandle, MDP_NOTIFY_TYPE nType, System.IntPtr looparray, uint count,
        System.IntPtr context);

    /// Return Type: void
    ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///segmentarray: segment_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifySegment(
        System.IntPtr mtaHandle, MDP_NOTIFY_TYPE nType, System.IntPtr segmentarray, uint count,
        System.IntPtr context);

    /// Return Type: void
    ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///segmentdiagsettingsarray: segmentdiagsettings_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifySegmentDiagSettings(
        System.IntPtr mtaHandle, MDP_NOTIFY_TYPE nType, System.IntPtr segmentdiagsettingsarray,
        uint count, System.IntPtr context);

    /// Return Type: void
    ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///systemsetuparray: systemsetup_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifySystemSetup(
        System.IntPtr mtaHandle, MDP_NOTIFY_TYPE nType, System.IntPtr systemsetuparray,
        uint count, System.IntPtr context);

    /// Return Type: void
    ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///systemsetuppicture: systemsetuppicture_t*
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifySystemSetupPicture(
        System.IntPtr mtaHandle, MDP_NOTIFY_TYPE nType, System.IntPtr systemsetuppicture,
        System.IntPtr context);

    /// Return Type: void
    ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///transponderarray: transponder_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyTransponder(
        System.IntPtr mtaHandle, MDP_NOTIFY_TYPE nType, System.IntPtr transponderarray,
        uint count, System.IntPtr context);

    /// Return Type: void
    ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///transpondergrouparray: transpondergroup_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyTransponderGroup(
        System.IntPtr mtaHandle, MDP_NOTIFY_TYPE nType, System.IntPtr transpondergrouparray,
        uint count, System.IntPtr context);

    /// Return Type: void
    ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///logarray: log_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyLog(
        System.IntPtr eventDataHandle, MDP_NOTIFY_TYPE nType, System.IntPtr logarray, uint count,
        System.IntPtr context);

    /// Return Type: void
    ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///markerarray: marker_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyMarker(
        System.IntPtr eventDataHandle, MDP_NOTIFY_TYPE nType, System.IntPtr markerarray, uint count,
        System.IntPtr context);

    /// Return Type: void
    ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///auxeventarray: auxevent_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyAuxEvent(
        System.IntPtr eventDataHandle, MDP_NOTIFY_TYPE nType, System.IntPtr auxeventarray, uint count,
        System.IntPtr context);

    /// Return Type: void
    ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///auxstatus: auxstatus_t*
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyAuxStatus(
        System.IntPtr eventDataHandle, MDP_NOTIFY_TYPE nType, System.IntPtr auxstatus,
        System.IntPtr context);

    /// Return Type: void
    ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///looptrigger: looptrigger_t*
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyLoopTrigger(
        System.IntPtr eventDataHandle, MDP_NOTIFY_TYPE nType, System.IntPtr looptrigger,
        System.IntPtr context);

    /// Return Type: void
    ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///status: decoderstatus_t*
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyDecoderStatus(
        System.IntPtr eventDataHandle, MDP_NOTIFY_TYPE nType, System.IntPtr status,
        System.IntPtr context);

    /// Return Type: void
    ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///driverinfoarray: driverinfo_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyDriverInfo(
        System.IntPtr eventDataHandle, MDP_NOTIFY_TYPE nType, System.IntPtr driverinfoarray, uint count,
        System.IntPtr context);

    /// Return Type: void
    ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///passingarray: passing_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyPassing(
        System.IntPtr eventDataHandle, MDP_NOTIFY_TYPE nType, System.IntPtr passingarray, uint count,
        System.IntPtr context);

    /// Return Type: void
    ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///passingarray: passingtrigger_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyPassingTrigger(
        System.IntPtr eventDataHandle, MDP_NOTIFY_TYPE nType, System.IntPtr passingtriggerarray, uint count,
        System.IntPtr context);

    /// Return Type: void
    ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///firstcontact: passingfirstcontact_t*
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyPassingFirstContact(
        System.IntPtr eventDataHandle, MDP_NOTIFY_TYPE nType, System.IntPtr firstcontactPtr,
        System.IntPtr context);

    /// Return Type: void
    ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///transponderstatus: transponderstatus_t*
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyTransponderStatus(
        System.IntPtr eventDataHandle, MDP_NOTIFY_TYPE nType, System.IntPtr transponderstatusPtr,
        System.IntPtr context);

    /// Return Type: void
    ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///messagearray: twowaymessage_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyTwoWayMessage(
        System.IntPtr eventDataHandle, MDP_NOTIFY_TYPE nType, System.IntPtr messagearray, uint count,
        System.IntPtr context);

    /// Return Type: void
    ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///beacondownloadconfigarray: beacondownloadconfig_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyBeaconDownloadConfig(
        System.IntPtr mtaHandle, MDP_NOTIFY_TYPE nType, System.IntPtr beacondownloadconfigarray,
        uint count, System.IntPtr context);

    /// Return Type: void
    ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///beacondownloadstatusarray: beacondownloadstatus_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyBeaconDownloadStatus(
        System.IntPtr eventDataHandle, MDP_NOTIFY_TYPE nType, System.IntPtr beacondownloadstatusarray,
        uint count, System.IntPtr context);

    /// Return Type: void
    ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///beacondownloadtriggerarray: beacondownloadtrigger_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyBeaconDownloadTrigger(
        System.IntPtr eventDataHandle, MDP_NOTIFY_TYPE nType, System.IntPtr beacondownloadtriggerarray,
        uint count, System.IntPtr context);

    /// Return Type: void
    ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///beaconlog: beaconlog_t*
    ///beacondataarray: beacondata_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyBeaconLog(System.IntPtr eventDataHandle, MDP_NOTIFY_TYPE nType, System.IntPtr beaconlogPtr,
        System.IntPtr beacondataarray, uint count, System.IntPtr context);

    /// Return Type: void
    ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///tracksolutiongrouparray: tracksolutiongroup_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyTrackSolutionGroup(
        System.IntPtr mtaHandle, MDP_NOTIFY_TYPE nType, System.IntPtr tracksolutiongrouparray,
        uint count, System.IntPtr context);

    /// Return Type: void
    ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///tracksolutionarray: tracksolution_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyTrackSolution(
        System.IntPtr mtaHandle, MDP_NOTIFY_TYPE nType, System.IntPtr tracksolutionarray,
        uint count, System.IntPtr context);

    /// Return Type: void
    ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///sequencesegmentarray: sequencesegment_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifySequenceSegment(
        System.IntPtr mtaHandle, MDP_NOTIFY_TYPE nType, System.IntPtr sequencesegmentarray,
        uint count, System.IntPtr context);

    /// Return Type: void
    ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///sequencearray: sequence_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifySequence(
        System.IntPtr mtaHandle, MDP_NOTIFY_TYPE nType, System.IntPtr sequencearray, uint count,
        System.IntPtr context);

    /// Return Type: void
    ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///sectorarray: sector_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifySector(
        System.IntPtr mtaHandle, MDP_NOTIFY_TYPE nType, System.IntPtr sectorarray, uint count,
        System.IntPtr context);

     /// Return Type: void
    ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
    ///nType: MDP_NOTIFY_TYPE->_MDP_NOTIFY_TYPE
    ///manualeventarray: manualevent_t**
    ///count: uint32_t->unsigned int
    ///context: void*
    [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(
        System.Runtime.InteropServices.CallingConvention.StdCall)]
    public delegate void pfNotifyManualEvent(
        System.IntPtr eventDataHandle, MDP_NOTIFY_TYPE nType, System.IntPtr manualeventarray, uint count,
        System.IntPtr context);
    
    public partial class NativeMethods
    {
        /// Return Type: mdp_sdk_handle_t->mdp_sdk_handle_dummystruct*
        ///appname: char*
        ///context: void*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mdp_sdk_alloc",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mdp_sdk_alloc(
            [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string appname, System.IntPtr context);


        /// Return Type: void
        ///handle: mdp_sdk_handle_t->mdp_sdk_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mdp_sdk_dealloc",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mdp_sdk_dealloc(System.IntPtr sdkHandle);


        /// Return Type: void
        ///handle: mdp_sdk_handle_t->mdp_sdk_handle_dummystruct*
        ///wait: boolean
        ///timeout: mdp_time_t->int64_t->__int64
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mdp_sdk_messagequeue_process",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mdp_sdk_messagequeue_process(System.IntPtr sdkHandle,
                                                               [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                   System.Runtime.InteropServices.UnmanagedType.I1)] bool wait, long timeout);


        /// Return Type: void
        ///handle: mdp_sdk_handle_t->mdp_sdk_handle_dummystruct*
        ///redirect: boolean
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mdp_sdk_redirect_to_stderr",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mdp_sdk_redirect_to_stderr(System.IntPtr sdkHandle,
                                                             [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                 System.Runtime.InteropServices.UnmanagedType.I1)] bool
                                                                 redirect);


        /// Return Type: void
        ///handle: mdp_sdk_handle_t->mdp_sdk_handle_dummystruct*
        ///NotifyMessageAvailable: pfNotifyDefault
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mdp_sdk_notify_messagequeue",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mdp_sdk_notify_messagequeue(System.IntPtr sdkHandle,
                                                              pfNotifyDefault NotifyMessageAvailable);


        /// Return Type: void
        ///handle: mdp_sdk_handle_t->mdp_sdk_handle_dummystruct*
        ///NotifyService: pfNotifyAppliance
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mdp_sdk_notify_appliance",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mdp_sdk_notify_appliance(System.IntPtr sdkHandle,
                                                           NativeNotifySingleObjectDelegate notifyDelegate);

        /// Return Type: char*
        ///buf: char*
        ///length: size_t->unsigned int
        ///time: mdp_time_t->int64_t->__int64
        ///adjust_format: boolean
        ///decimals: int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mdp_get_time_as_string",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mdp_get_time_as_string(System.IntPtr buf,
                                                                  [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                      System.Runtime.InteropServices.UnmanagedType.
                                                                      SysUInt)] uint length, long time,
                                                                  [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                      System.Runtime.InteropServices.UnmanagedType.I1)] bool adjust_format, int decimals);


        /// Return Type: char*
        ///buf: char*
        ///length: size_t->unsigned int
        ///time: mdp_time_t->int64_t->__int64
        ///format: char*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mdp_get_time_as_strfstring",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mdp_get_time_as_strfstring(System.IntPtr buf,
                                                                      [System.Runtime.InteropServices.MarshalAsAttribute
                                                                          (
                                                                          System.Runtime.InteropServices.UnmanagedType.
                                                                          SysUInt)] uint length, long time,
                                                                      [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute
                                                                          (
                                                                          System.Runtime.InteropServices.UnmanagedType.
                                                                          LPStr)] string format);


        /// Return Type: char*
        ///buf: char*
        ///length: size_t->unsigned int
        ///time: mdp_time_t->int64_t->__int64
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mdp_get_date_as_string",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mdp_get_date_as_string(System.IntPtr buf,
                                                                  [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                      System.Runtime.InteropServices.UnmanagedType.
                                                                      SysUInt)] uint length, long time);


        /// Return Type: char*
        ///buf: char*
        ///length: size_t->unsigned int
        ///number: double
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mdp_double_to_string",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mdp_double_to_string(System.IntPtr buf,
                                                                [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                    System.Runtime.InteropServices.UnmanagedType.SysUInt
                                                                    )] uint length, double number);


        /// Return Type: double
        ///buf: char*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mdp_string_to_double",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern double mdp_string_to_double(
            [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string buf);


        /// Return Type: char*
        ///buf: char*
        ///length: size_t->unsigned int
        ///ip: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mdp_ipaddress_to_string",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mdp_ipaddress_to_string(System.IntPtr buf,
                                                                   [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                       System.Runtime.InteropServices.UnmanagedType.
                                                                       SysUInt)] uint length, uint ip);


        /// Return Type: uint32_t->unsigned int
        ///buf: char*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mdp_string_to_ipaddress",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mdp_string_to_ipaddress(
            [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string buf);


        /// Return Type: char*
        ///buf: char*
        ///length: size_t->unsigned int
        ///mac: int64_t->__int64
        ///include_dash: boolean
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mdp_mac_to_string",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mdp_mac_to_string(System.IntPtr buf,
                                                             [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                 System.Runtime.InteropServices.UnmanagedType.SysUInt)] uint length, long mac,
                                                             [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                 System.Runtime.InteropServices.UnmanagedType.I1)] bool
                                                                 include_dash);


        /// Return Type: char*
        ///buf: char*
        ///length: size_t->unsigned int
        ///number: int64_t->__int64
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mdp_int64_to_string",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mdp_int64_to_string(System.IntPtr buf,
                                                               [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                   System.Runtime.InteropServices.UnmanagedType.SysUInt)
                                                               ] uint length, long number);


        /// Return Type: int64_t->__int64
        ///buf: char*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mdp_string_to_int64",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern long mdp_string_to_int64(
            [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string buf);


        /// Return Type: char*
        ///buf: char*
        ///length: size_t->unsigned int
        ///version: uint32_t->unsigned int
        ///include_build: boolean
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mdp_version_to_string",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mdp_version_to_string(System.IntPtr buf,
                                                                 [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                     System.Runtime.InteropServices.UnmanagedType.
                                                                     SysUInt)] uint length, uint version,
                                                                 [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                     System.Runtime.InteropServices.UnmanagedType.I1)] bool include_build);


        /// Return Type: uint32_t->unsigned int
        ///buf: char*
        ///type: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mdp_string_to_transponder",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mdp_string_to_transponder(
            [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string buf, uint type);


        /// Return Type: char*
        ///buf: char*
        ///length: size_t->unsigned int
        ///transponder: uint32_t->unsigned int
        ///type: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mdp_transponder_to_string",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mdp_transponder_to_string(System.IntPtr buf,
                                                                     [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                         System.Runtime.InteropServices.UnmanagedType.
                                                                         SysUInt)] uint length, uint transponder,
                                                                     uint type);


        /// Return Type: char*
        ///buf: char*
        ///length: size_t->unsigned int
        ///type: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mdp_transponder_type_to_string",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mdp_transponder_type_to_string(System.IntPtr buf,
                                                                          [System.Runtime.InteropServices.
                                                                              MarshalAsAttribute(
                                                                              System.Runtime.InteropServices.
                                                                              UnmanagedType.SysUInt)] uint length,
                                                                          uint type);

        /// Convert the time (in microseconds since 1/1/70) and length (in micrometer) to a speed string. The scale
        /// can be used to convert the speed from m/s to e.g. km/h (scale = 3.6) or mph (scale = 2.2369362920544).
        /// Return Type: char*
        ///buf: char*
        ///buflength: size_t->unsigned int
        ///time: mdp_time_t->int64_t->__int64
        ///length: int64_t->__int64
        ///scale: double
        ///decimals: int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mdp_get_speed_as_string",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mdp_get_speed_as_string(System.IntPtr buf,
                                                                  [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                      System.Runtime.InteropServices.UnmanagedType.
                                                                      SysUInt)] uint buflength, long time,
                                                                  long length, double scale, int decimals);


        /// Return Type: boolean
        ///handle: mdp_sdk_handle_t->mdp_sdk_handle_dummystruct*
        ///hostname: char*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mdp_sdk_appliance_verify",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mdp_sdk_appliance_verify(System.IntPtr sdkHandle,
                                                           [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(
                                                               System.Runtime.InteropServices.UnmanagedType.LPStr)] string hostname);


        /// Return Type: availableappliance_t*
        ///handle: mdp_sdk_handle_t->mdp_sdk_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mdp_sdk_appliance_get_head",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mdp_sdk_appliance_get_head(System.IntPtr sdkHandle);


        /// Return Type: availableappliance_t*
        ///handle: mdp_sdk_handle_t->mdp_sdk_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mdp_sdk_appliance_get_next",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mdp_sdk_appliance_get_next(System.IntPtr sdkHandle);


        /// Return Type: availableappliance_t*
        ///handle: mdp_sdk_handle_t->mdp_sdk_handle_dummystruct*
        ///mac: int64_t->__int64
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mdp_sdk_appliance_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mdp_sdk_appliance_find(System.IntPtr sdkHandle, long mac);

        /// Return Type: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///sdk_handle: mdp_sdk_handle_t->mdp_sdk_handle_dummystruct*
        ///context: void*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_handle_alloc",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_handle_alloc(System.IntPtr sdkHandle,
                                                            System.IntPtr context);


        /// Return Type: void
        ///sdk_handle: mdp_sdk_handle_t->mdp_sdk_handle_dummystruct*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_handle_dealloc",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_handle_dealloc(System.IntPtr sdkHandle,
                                                     System.IntPtr mtaHandle);

        /// Return Type: bool, true if synced else false
        ///app_handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_time_is_synced",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_time_is_synced(System.IntPtr mta_handle);

        /// Return Type: mdp_time_t->int64_t->__int64
        ///app_handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_time_get_utc",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern long mta_time_get_utc(System.IntPtr appHandle);


        /// Return Type: int
        ///app_handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_time_get_utcsyncsource",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int mta_time_get_utcsyncsource(System.IntPtr appHandle);


        /// Return Type: mdp_time_t->int64_t->__int64
        ///app_handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///utctime: mdp_time_t->int64_t->__int64
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_time_get_local",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern long mta_time_get_local(System.IntPtr mtaHandle, long utctime);


        /// Return Type: mdp_time_t->int64_t->__int64
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///utctime: mdp_time_t->int64_t->__int64
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_time_get_timezone_correction",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern long mta_time_get_timezone_correction(System.IntPtr mtaHandle,
                                                                   long utctime);


        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///NotifyConnect: pfNotifyConnect
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_connect",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_connect(System.IntPtr mtaHandle,
                                                     pfNotifyConnect NotifyConnect);


        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///NotifyConnectionState: pfNotifyConnectionState
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_connectionstate",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_connectionstate(System.IntPtr mtaHandle,
                                                             pfNotifyConnectionState NotifyConnectionState);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///hostname: char*
        ///username: char*
        ///password: char*
        ///follow_current_systemsetup: boolean
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_connect_insecure",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_connect_insecure(System.IntPtr mtaHandle,
                                              [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(
                                                  System.Runtime.InteropServices.UnmanagedType.LPStr)] string hostname,
                                              [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(
                                                  System.Runtime.InteropServices.UnmanagedType.LPStr)] string username,
                                              [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(
                                                  System.Runtime.InteropServices.UnmanagedType.LPStr)] string password,
                                              [System.Runtime.InteropServices.MarshalAsAttribute(
                                                  System.Runtime.InteropServices.UnmanagedType.I1)] bool follow_current_systemsetup);

        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///hostname: char*
        ///username: char*
        ///password: char*
        ///follow_current_systemsetup: boolean
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_connect",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_connect(System.IntPtr mtaHandle,
                                              [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(
                                                  System.Runtime.InteropServices.UnmanagedType.LPStr)] string hostname,
                                              [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(
                                                  System.Runtime.InteropServices.UnmanagedType.LPStr)] string username,
                                              [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(
                                                  System.Runtime.InteropServices.UnmanagedType.LPStr)] string password,
                                              [System.Runtime.InteropServices.MarshalAsAttribute(
                                                  System.Runtime.InteropServices.UnmanagedType.I1)] bool follow_current_systemsetup);

        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_disconnect",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_disconnect(System.IntPtr mtaHandle);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_is_connected",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_is_connected(System.IntPtr mtaHandle);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_is_authenticated",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_is_authenticated(System.IntPtr mtaHandle);


        /// Return Type: uint32_t->unsigned int
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_get_connectionstate",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_get_connectionstate(System.IntPtr mtaHandle);


        /// Return Type: char*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_get_hostname",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_get_hostname(System.IntPtr mtaHandle);


        /// Return Type: user_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_get_user",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_get_user(System.IntPtr mtaHandle);


        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///NotifySystemSetupSwitched: pfNotifySystemSetupSwitched
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_systemsetup_switched",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_systemsetup_switched(System.IntPtr mtaHandle,
                                                                  pfNotifySystemSetupSwitched NotifySystemSetupSwitched);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///systemsetupid: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_systemsetup_switch",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_systemsetup_switch(System.IntPtr mtaHandle, uint systemsetupid);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///object_type: MTAOBJECTDATA->MTAOBJECTDATA_
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_objectdata_subscribe",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_objectdata_subscribe(System.IntPtr mtaHandle,
                                                           MTAOBJECTDATA object_type);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///object_type: MTAOBJECTDATA->MTAOBJECTDATA_
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_objectdata_unsubscribe",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_objectdata_unsubscribe(System.IntPtr mtaHandle,
                                                             MTAOBJECTDATA object_type);


        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_changes_commit",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_changes_commit(System.IntPtr mtaHandle);


        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_changes_rollback",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_changes_rollback(System.IntPtr mtaHandle);


        /// Return Type: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///utcfromtime: mdp_time_t->int64_t->__int64
        ///utctotime: mdp_time_t->int64_t->__int64
        ///context: void*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_eventdata_handle_alloc_resend"
            , CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_eventdata_handle_alloc_resend(
            System.IntPtr mtaHandle, long utcfromtime, long utctotime, System.IntPtr context);


        /// Return Type: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///utcfromtime: mdp_time_t->int64_t->__int64
        ///context: void*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            EntryPoint = "mta_eventdata_handle_alloc_live_with_resend",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_eventdata_handle_alloc_live_with_resend(
            System.IntPtr mtaHandle, long utcfromtime, System.IntPtr context);


        /// Return Type: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///context: void*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_eventdata_handle_alloc_live",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_eventdata_handle_alloc_live(System.IntPtr mtaHandle,
                                                                           System.IntPtr context);


        /// Return Type: boolean
        ///app_handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_eventdata_handle_dealloc",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_eventdata_handle_dealloc(System.IntPtr mtaHandle,
                                                               System.IntPtr eventDataHandle);


        /// Return Type: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            EntryPoint = "mta_eventdata_get_appliance_handle",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_eventdata_get_appliance_handle(
            System.IntPtr eventDataHandle);


//mta subscribe and unsubscribe native method calls
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_eventdata_subscribe",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_eventdata_subscribe(System.IntPtr eventDataHandle,
                                                          MTAEVENTDATA eventdata_type, uint request_limit,
                                                          [System.Runtime.InteropServices.MarshalAsAttribute(
                                                              System.Runtime.InteropServices.UnmanagedType.I1)] bool
                                                              cache_objects);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_eventdata_unsubscribe",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_eventdata_unsubscribe(System.IntPtr eventDataHandle,
                                                            MTAEVENTDATA eventdata_type);

        /// Return Type: mdp_time_t->int64_t->__int64
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_eventdata_get_begintime",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern long mta_eventdata_get_begintime(System.IntPtr eventDataHandle);


        /// Return Type: void
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_eventdata_changes_commit",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_eventdata_changes_commit(System.IntPtr eventDataHandle);


        /// Return Type: void
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_eventdata_changes_rollback",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_eventdata_changes_rollback(System.IntPtr eventDataHandle);

        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///NotifyDecoder: pfNotifyDecoder
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_decoder",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_decoder(System.IntPtr mtaHandle,
                                                     NativeNotifyMultiObjectDelegate NotifyDecoder);

        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///NotifyDecoder: pfNotifyDecoderPresetGroup
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_decoderpresetgroup",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_decoderpresetgroup(System.IntPtr mtaHandle,
                                                     NativeNotifyMultiObjectDelegate NotifyDecoderPresetGroup);

        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///NotifyIOTerminal: pfNotifyIOTerminal
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_ioterminal",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_ioterminal(System.IntPtr mtaHandle,
                                                        NativeNotifyMultiObjectDelegate NotifyIOTerminal);


        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///NotifyLoop: pfNotifyLoop
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_loop",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_loop(System.IntPtr mtaHandle, NativeNotifyMultiObjectDelegate NotifyLoop);

        ///Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///NotifyLoop: pfNotifyCompetitor
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_competitor",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_competitor(System.IntPtr mtaHandle, NativeNotifyMultiObjectDelegate NotifyCompetitor);


        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///NotifySegment: pfNotifySegment
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_segment",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_segment(System.IntPtr mtaHandle,
                                                     NativeNotifyMultiObjectDelegate NotifySegment);


        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///NotifySegmentDiagSettings: pfNotifySegmentDiagSettings
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_segmentdiagsettings",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_segmentdiagsettings(System.IntPtr mtaHandle,
                                                                 NativeNotifyMultiObjectDelegate NotifySegmentDiagSettings);


        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///NotifySystemSetup: pfNotifySystemSetup
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_systemsetup",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_systemsetup(System.IntPtr mtaHandle,
                                                         NativeNotifyMultiObjectDelegate NotifySystemSetup);


        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///NotifySystemSetupPicture: pfNotifySystemSetupPicture
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_systemsetuppicture",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_systemsetuppicture(System.IntPtr mtaHandle,
                                                                NativeNotifySingleObjectDelegate NotifySystemSetupPicture);


        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///NotifyTransponder: pfNotifyTransponder
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_transponder",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_transponder(System.IntPtr mtaHandle,
                                                         NativeNotifyMultiObjectDelegate NotifyTransponder);


        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///NotifyTransponderGroup: pfNotifyTransponderGroup
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_transpondergroup",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_transpondergroup(System.IntPtr mtaHandle,
                                                              NativeNotifyMultiObjectDelegate NotifyTransponderGroup);


        /// Return Type: void
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///NotifyLog: pfNotifyLog
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_log",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_log(System.IntPtr eventDataHandle, NativeNotifyMultiObjectDelegate NotifyLog);


        /// Return Type: void
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///NotifyAuxEvent: pfNotifyAuxEvent
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_auxevent",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_auxevent(System.IntPtr eventDataHandle,
                                                      NativeNotifyMultiObjectDelegate NotifyAuxEvent);


        /// Return Type: void
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///NotifyAuxStatus: pfNotifyAuxStatus
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_auxstatus",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_auxstatus(System.IntPtr eventDataHandle,
                                                       NativeNotifySingleObjectDelegate NotifyAuxStatus);


        /// Return Type: void
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///NotifyLoopTrigger: pfNotifyLoopTrigger
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_looptrigger",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_looptrigger(System.IntPtr eventDataHandle,
                                                       NativeNotifySingleObjectDelegate NotifyLoopTrigger);


        /// Return Type: void
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///NotifyDecoderStatus: pfNotifyDecoderStatus
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_decoderstatus",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_decoderstatus(System.IntPtr eventDataHandle,
                                                           NativeNotifySingleObjectDelegate NotifyDecoderStatus);

        /// Return Type: void
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///NotifyDriverInfo: pfNotifyDriverInfo
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_driverinfo",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_driverinfo(System.IntPtr eventDataHandle,
                                                     NativeNotifyMultiObjectDelegate NotifyDriverInfo);

        /// Return Type: void
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///NotifyPassing: pfNotifyPassing
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_passing",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_passing(System.IntPtr eventDataHandle,
                                                     NativeNotifyMultiObjectDelegate NotifyPassing);


        /// Return Type: void
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///NotifyPassing: pfNotifyPassingTrigger
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_passingtrigger",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_passingtrigger(System.IntPtr eventDataHandle,
                                                     NativeNotifyMultiObjectDelegate NotifyPassingTrigger);
        
        
        /// Return Type: void
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///NotifyPassingFirstContact: pfNotifyPassingFirstContact
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_passingfirstcontact",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_passingfirstcontact(System.IntPtr eventDataHandle,
                                                                 NativeNotifySingleObjectDelegate NotifyPassingFirstContact);


        /// Return Type: void
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///NotifyTransponderStatus: pfNotifyTransponderStatus
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_transponderstatus",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_transponderstatus(System.IntPtr eventDataHandle,
                                                               NativeNotifySingleObjectDelegate NotifyTransponderStatus);


        /// Return Type: void
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///NotifyTwoWayMessage: pfNotifyTwoWayMessage
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_twowaymessage",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_twowaymessage(System.IntPtr eventDataHandle,
                                                           NativeNotifyMultiObjectDelegate NotifyTwoWayMessage);


        [System.Runtime.InteropServices.DllImport("MylapsSDK.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_marker_insert(System.IntPtr eventDataHandle);

        [System.Runtime.InteropServices.DllImport("MylapsSDK.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_marker_update(System.IntPtr eventDataHandle, uint markerID);

        [System.Runtime.InteropServices.DllImport("MylapsSDK.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_marker_delete(System.IntPtr eventDataHandle, uint markerID);
        

        /// Return Type: boolean
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///id: uint32_t->unsigned int
        ///key: char*
        ///dst: char*
        ///dstlen: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_marker_get_userdata",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_marker_get_userdata(System.IntPtr eventDataHandle, uint id,
                                                          [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(
                                                              System.Runtime.InteropServices.UnmanagedType.LPStr)] string key, System.IntPtr dst, uint dstlen);


        /// Return Type: marker_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_marker_get_head",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_marker_get_head(System.IntPtr eventDataHandle);


        /// Return Type: marker_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_marker_get_next",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_marker_get_next(System.IntPtr eventDataHandle);


        /// Return Type: marker_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_marker_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_marker_find(System.IntPtr eventDataHandle, uint id);


        /// Return Type: uint32_t->unsigned int
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_marker_get_count",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_marker_get_count(System.IntPtr eventDataHandle);


        /// Return Type: boolean
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_marker_is_data_available",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_marker_is_data_available(System.IntPtr eventDataHandle);


        /// Return Type: log_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_log_get_head",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_log_get_head(System.IntPtr eventDataHandle);


        /// Return Type: log_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_log_get_next",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_log_get_next(System.IntPtr eventDataHandle);


        /// Return Type: log_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_log_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_log_find(System.IntPtr eventDataHandle, uint id);


        /// Return Type: uint32_t->unsigned int
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_log_get_count",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_log_get_count(System.IntPtr eventDataHandle);


        /// Return Type: boolean
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_log_is_data_available",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_log_is_data_available(System.IntPtr eventDataHandle);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_systemsetup_update(System.IntPtr mtaHandle, uint id);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_systemsetup_insert(System.IntPtr mtaHandle, uint id);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
           CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_systemsetuppicture_update(System.IntPtr mtaHandle);

        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_systemsetup_delete",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_systemsetup_delete(System.IntPtr mtaHandle, uint id);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        ///key: char*
        ///dst: char*
        ///dstlen: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_systemsetup_get_userdata",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_systemsetup_get_userdata(System.IntPtr mtaHandle, uint id,
                                                               [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                   System.Runtime.InteropServices.UnmanagedType.LPStr)] string key,
            [System.Runtime.InteropServices.OutAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                   System.Runtime.InteropServices.UnmanagedType.LPStr)] System.Text.StringBuilder dst, uint dstlen);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_systemsetup_add_userdata(System.IntPtr mtaHandle, System.IntPtr modifyPtr,
                                                              [System.Runtime.InteropServices.In] [System.Runtime.InteropServices.MarshalAs(
                                                                  System.Runtime.InteropServices.UnmanagedType.LPStr)] string key,
                                                              [System.Runtime.InteropServices.In] [System.Runtime.InteropServices.MarshalAs(
                                                                  System.Runtime.InteropServices.UnmanagedType.LPStr)] string value);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_systemsetup_remove_userdata(System.IntPtr mtaHandle, System.IntPtr modifyPtr,
                                                                 [System.Runtime.InteropServices.In] [System.Runtime.InteropServices.MarshalAs(
                                                                     System.Runtime.InteropServices.UnmanagedType.LPStr)
                                                                                                     ] string key);
        /// Return Type: systemsetup_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_systemsetup_get_head",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_systemsetup_get_head(System.IntPtr mtaHandle);


        /// Return Type: systemsetup_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_systemsetup_get_next",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_systemsetup_get_next(System.IntPtr mtaHandle);


        /// Return Type: systemsetup_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_systemsetup_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_systemsetup_find(System.IntPtr mtaHandle, uint id);


        /// Return Type: systemsetup_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_systemsetup_in_use",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_systemsetup_in_use(System.IntPtr mtaHandle);


        /// Return Type: uint32_t->unsigned int
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_systemsetup_get_count",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_systemsetup_get_count(System.IntPtr mtaHandle);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_systemsetup_is_data_available"
            , CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_systemsetup_is_data_available(System.IntPtr mtaHandle);


        /// Return Type: uint8_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            EntryPoint = "mta_systemsetup_get_thumbnail_data",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_systemsetup_get_thumbnail_data(
            System.IntPtr mtaHandle, uint id);


        /// Return Type: uint32_t->unsigned int
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            EntryPoint = "mta_systemsetup_get_thumbnail_data_length",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_systemsetup_get_thumbnail_data_length(System.IntPtr mtaHandle,
                                                                            uint id);


        /// Return Type: systemsetuppicture_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_systemsetuppicture_get",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_systemsetuppicture_get(System.IntPtr mtaHandle);


        /// Return Type: uint8_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_systemsetuppicture_get_data",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_systemsetuppicture_get_data(System.IntPtr mtaHandle);


        /// Return Type: uint32_t->unsigned int
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            EntryPoint = "mta_systemsetuppicture_get_data_length",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_systemsetuppicture_get_data_length(System.IntPtr mtaHandle);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", 
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_systemsetuppicture_set_data(System.IntPtr mtaHandle, System.IntPtr systemSetupPicturePtr, System.IntPtr pictureData, uint pictureDataLength);

        /// Return Type: timezone_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_timezone_get_head",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_timezone_get_head(System.IntPtr mtaHandle);


        /// Return Type: timezone_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_timezone_get_next",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_timezone_get_next(System.IntPtr mtaHandle);


        /// Return Type: timezone_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: char*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_timezone_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_timezone_find(System.IntPtr mtaHandle,
                                                             [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                 System.Runtime.InteropServices.UnmanagedType.LPStr)] string id);


        /// Return Type: uint32_t->unsigned int
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_timezone_get_count",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_timezone_get_count(System.IntPtr mtaHandle);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_timezone_is_data_available",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_timezone_is_data_available(System.IntPtr mtaHandle);


        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: int64_t->__int64
        ///loopid: uint32_t->unsigned int
        ///ioterminalid: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_decoder_connect",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_decoder_connect(System.IntPtr mtaHandle, long id, uint loopid,
                                                      uint ioterminalid);


        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///hostname: char*
        ///port: uint32_t->unsigned int
        ///loopid: uint32_t->unsigned int
        ///ioterminalid: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_decoder_connect_manual",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_decoder_connect_manual(System.IntPtr mtaHandle,
                                                             [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                 System.Runtime.InteropServices.UnmanagedType.LPStr)] string hostname, uint port, uint loopid,
                                                             uint ioterminalid);


        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: int64_t->__int64
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_decoder_disconnect",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_decoder_disconnect(System.IntPtr mtaHandle, long id);


        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: int64_t->__int64
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_decoder_identify",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_decoder_identify(System.IntPtr mtaHandle, long id);


        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: int64_t->__int64
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_decoder_get_decoderpresetgroupid",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_decoder_get_decoderpresetgroupid(System.IntPtr mtaHandle, long id);


        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: int64_t->__int64
        ///decoderpresetgroupid: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_decoder_set_decoderpresetgroupid",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_decoder_set_decoderpresetgroupid(System.IntPtr mtaHandle, long id, uint decoderpresetgroupid);


        /// Return Type: decoder_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_decoder_get_head",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_decoder_get_head(System.IntPtr mtaHandle);


        /// Return Type: decoder_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_decoder_get_next",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_decoder_get_next(System.IntPtr mtaHandle);


        /// Return Type: decoder_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: int64_t->__int64
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_decoder_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_decoder_find(System.IntPtr mtaHandle, long id);


        /// Return Type: uint32_t->unsigned int
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_decoder_get_count",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_decoder_get_count(System.IntPtr mtaHandle);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_decoder_is_data_available",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_decoder_is_data_available(System.IntPtr mtaHandle);


        /// Return Type: decoderpresetgroup_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_decoderpresetgroup_get_head",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_decoderpresetgroup_get_head(System.IntPtr mtaHandle);


        /// Return Type: decoderpresetgroup_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_decoderpresetgroup_get_next",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_decoderpresetgroup_get_next(System.IntPtr mtaHandle);


        /// Return Type: decoderpresetgroup_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: int64_t->__int64
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_decoderpresetgroup_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_decoderpresetgroup_find(System.IntPtr mtaHandle, long id);


        /// Return Type: uint32_t->unsigned int
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_decoderpresetgroup_get_count",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_decoderpresetgroup_get_count(System.IntPtr mtaHandle);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_decoderpresetgroup_is_data_available",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_decoderpresetgroup_is_data_available(System.IntPtr mtaHandle);


        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_ioterminal_insert(System.IntPtr mtaHandle, uint id);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_ioterminal_update(System.IntPtr mtaHandle, uint id);

        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_ioterminal_delete",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_ioterminal_delete(System.IntPtr mtaHandle, uint id);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        ///key: char*
        ///dst: char*
        ///dstlen: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_ioterminal_get_userdata",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_ioterminal_get_userdata(System.IntPtr mtaHandle, uint id,
                                                              [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                  System.Runtime.InteropServices.UnmanagedType.LPStr)] string key, System.Text.StringBuilder dst, uint dstlen);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_ioterminal_add_userdata(System.IntPtr mtaHandle, System.IntPtr modifyPtr,
                                                              [System.Runtime.InteropServices.In] [System.Runtime.InteropServices.MarshalAs(
                                                                  System.Runtime.InteropServices.UnmanagedType.LPStr)] string key,
                                                              [System.Runtime.InteropServices.In] [System.Runtime.InteropServices.MarshalAs(
                                                                  System.Runtime.InteropServices.UnmanagedType.LPStr)] string value);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_ioterminal_remove_userdata(System.IntPtr mtaHandle, System.IntPtr modifyPtr,
                                                                 [System.Runtime.InteropServices.In] [System.Runtime.InteropServices.MarshalAs(
                                                                     System.Runtime.InteropServices.UnmanagedType.LPStr)
                                                                                                     ] string key);
                                                       


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        ///bitmask: uint8_t->unsigned int
        ///output_state: uint8_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_ioterminal_output_set_state",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_ioterminal_output_set_state(System.IntPtr mtaHandle, uint id,
                                                                  uint bitmask, uint output_state);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        ///bitmask: uint8_t->unsigned int
        ///dac_value: uint8_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            EntryPoint = "mta_ioterminal_analogoutput_set_dac",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_ioterminal_analogoutput_set_dac(System.IntPtr mtaHandle,
                                                                      uint id, uint bitmask, uint dac_value);


        /// Return Type: ioterminal_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_ioterminal_get_head",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_ioterminal_get_head(System.IntPtr mtaHandle);


        /// Return Type: ioterminal_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_ioterminal_get_next",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_ioterminal_get_next(System.IntPtr mtaHandle);


        /// Return Type: ioterminal_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_ioterminal_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_ioterminal_find(System.IntPtr mtaHandle, uint id);


        /// Return Type: uint32_t->unsigned int
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_ioterminal_get_count",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_ioterminal_get_count(System.IntPtr mtaHandle);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_ioterminal_is_data_available",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_ioterminal_is_data_available(System.IntPtr mtaHandle);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_loop_insert(System.IntPtr mtaHandle, uint id);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", 
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_loop_update(System.IntPtr mtaHandle, uint id);

        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_loop_delete",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_loop_delete(System.IntPtr mtaHandle, uint id);


        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_competitor_insert",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_competitor_insert(System.IntPtr mtaHandle, string guid);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_competitor_update",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_competitor_update(System.IntPtr mtaHandle, string guid);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_competitor_delete",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_competitor_delete(System.IntPtr mtaHandle, string guid);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_twowaymessage_insert(System.IntPtr mtaHandle);

        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        ///key: char*
        ///dst: char*
        ///dstlen: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_loop_get_userdata",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_loop_get_userdata(System.IntPtr mtaHandle, uint id,
                                                        [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(
                                                            System.Runtime.InteropServices.UnmanagedType.LPStr)] string
                                                            key,
                                                        [System.Runtime.InteropServices.MarshalAsAttribute(
                                                            System.Runtime.InteropServices.UnmanagedType.LPStr)]System.Text.StringBuilder dst, uint dstlen);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_loop_add_userdata(System.IntPtr mtaHandle, System.IntPtr modifyPtr,
                                                              [System.Runtime.InteropServices.In] [System.Runtime.InteropServices.MarshalAs(
                                                                  System.Runtime.InteropServices.UnmanagedType.LPStr)] string key,
                                                              [System.Runtime.InteropServices.In] [System.Runtime.InteropServices.MarshalAs(
                                                                  System.Runtime.InteropServices.UnmanagedType.LPStr)] string value);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_loop_remove_userdata(System.IntPtr mtaHandle, System.IntPtr modifyPtr,
                                                                 [System.Runtime.InteropServices.In] [System.Runtime.InteropServices.MarshalAs(
                                                                     System.Runtime.InteropServices.UnmanagedType.LPStr)
                                                                                                     ] string key);

        /// Return Type: loop_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_loop_get_head",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_loop_get_head(System.IntPtr mtaHandle);


        /// Return Type: loop_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_loop_get_next",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_loop_get_next(System.IntPtr mtaHandle);


        /// Return Type: loop_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_loop_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_loop_find(System.IntPtr mtaHandle, uint id);


        /// Return Type: uint32_t->unsigned int
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_loop_get_count",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_loop_get_count(System.IntPtr mtaHandle);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_loop_is_data_available",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_loop_is_data_available(System.IntPtr mtaHandle);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_segment_insert",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_segment_insert(System.IntPtr mtaHandle, uint id);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_segment_update(System.IntPtr mtaHandle, uint id);

        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_segment_delete",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_segment_delete(System.IntPtr mtaHandle, uint id);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        ///key: char*
        ///dst: char*
        ///dstlen: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_segment_get_userdata",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_segment_get_userdata(System.IntPtr mtaHandle, uint id,
                                                           [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(
                                                               System.Runtime.InteropServices.UnmanagedType.LPStr)] string key,
            [System.Runtime.InteropServices.MarshalAsAttribute(
                                                               System.Runtime.InteropServices.UnmanagedType.LPStr)]System.Text.StringBuilder dst, uint dstlen);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_segment_add_userdata(System.IntPtr mtaHandle, System.IntPtr modifyPtr,
                                                              [System.Runtime.InteropServices.In] [System.Runtime.InteropServices.MarshalAs(
                                                                  System.Runtime.InteropServices.UnmanagedType.LPStr)] string key,
                                                              [System.Runtime.InteropServices.In] [System.Runtime.InteropServices.MarshalAs(
                                                                  System.Runtime.InteropServices.UnmanagedType.LPStr)] string value);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_segment_remove_userdata(System.IntPtr mtaHandle, System.IntPtr modifyPtr,
                                                                 [System.Runtime.InteropServices.In] [System.Runtime.InteropServices.MarshalAs(
                                                                     System.Runtime.InteropServices.UnmanagedType.LPStr)
                                                                                                     ] string key);
        /// Return Type: segment_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_segment_get_head",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_segment_get_head(System.IntPtr mtaHandle);


        /// Return Type: segment_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_segment_get_next",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_segment_get_next(System.IntPtr mtaHandle);


        /// Return Type: segment_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_segment_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_segment_find(System.IntPtr mtaHandle, uint id);


        /// Return Type: uint32_t->unsigned int
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_segment_get_count",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_segment_get_count(System.IntPtr mtaHandle);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_segment_is_data_available",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_segment_is_data_available(System.IntPtr mtaHandle);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        ///x: uint16_t->unsigned short
        ///y: uint16_t->unsigned short
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_segment_bitmask_bit_is_set",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_segment_bitmask_bit_is_set(System.IntPtr mtaHandle, uint id,
                                                                 ushort x, ushort y);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_segmentdiagsettings_insert(System.IntPtr mtaHandle, uint id);
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_segmentdiagsettings_update(System.IntPtr mtaHandle, uint id);

        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_segmentdiagsettings_delete",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_segmentdiagsettings_delete(System.IntPtr mtaHandle, uint id);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        ///key: char*
        ///dst: char*
        ///dstlen: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            EntryPoint = "mta_segmentdiagsettings_get_userdata",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_segmentdiagsettings_get_userdata(System.IntPtr mtaHandle,
                                                                       uint id,
                                                                       [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.
                                                                           MarshalAsAttribute(
                                                                           System.Runtime.InteropServices.UnmanagedType.
                                                                           LPStr)] string key, [System.Runtime.InteropServices.Out][System.Runtime.InteropServices.
                                                                           MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)] System.Text.StringBuilder dst,
                                                                       uint dstlen);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_segmentdiagsettings_add_userdata(System.IntPtr mtaHandle, System.IntPtr modifyPtr,
                                                              [System.Runtime.InteropServices.In] [System.Runtime.InteropServices.MarshalAs(
                                                                  System.Runtime.InteropServices.UnmanagedType.LPStr)] string key,
                                                              [System.Runtime.InteropServices.In] [System.Runtime.InteropServices.MarshalAs(
                                                                  System.Runtime.InteropServices.UnmanagedType.LPStr)] string value);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_segmentdiagsettings_remove_userdata(System.IntPtr mtaHandle, System.IntPtr modifyPtr,
                                                                 [System.Runtime.InteropServices.In] [System.Runtime.InteropServices.MarshalAs(
                                                                     System.Runtime.InteropServices.UnmanagedType.LPStr)
                                                                                                     ] string key);
        /// Return Type: segmentdiagsettings_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_segmentdiagsettings_get_head",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_segmentdiagsettings_get_head(System.IntPtr mtaHandle);


        /// Return Type: segmentdiagsettings_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_segmentdiagsettings_get_next",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_segmentdiagsettings_get_next(System.IntPtr mtaHandle);


        /// Return Type: segmentdiagsettings_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_segmentdiagsettings_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_segmentdiagsettings_find(System.IntPtr mtaHandle,
                                                                        uint id);


        /// Return Type: uint32_t->unsigned int
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_segmentdiagsettings_get_count"
            , CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_segmentdiagsettings_get_count(System.IntPtr mtaHandle);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            EntryPoint = "mta_segmentdiagsettings_is_data_available",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_segmentdiagsettings_is_data_available(System.IntPtr mtaHandle);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_transponder_insert(System.IntPtr mtaHandle, uint id);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_transponder_update(System.IntPtr mtaHandle, uint id);

        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_transponder_delete",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_transponder_delete(System.IntPtr mtaHandle, uint id);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        ///key: char*
        ///dst: char*
        ///dstlen: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_transponder_get_userdata",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_transponder_get_userdata(System.IntPtr mtaHandle, uint id,
                                                               [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                   System.Runtime.InteropServices.UnmanagedType.LPStr)] string key,
            [System.Runtime.InteropServices.Out] [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                   System.Runtime.InteropServices.UnmanagedType.LPStr)]System.Text.StringBuilder dst, uint dstlen);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_transponder_add_userdata(System.IntPtr mtaHandle, System.IntPtr modifyPtr,
                                                              [System.Runtime.InteropServices.In] [System.Runtime.InteropServices.MarshalAs(
                                                                  System.Runtime.InteropServices.UnmanagedType.LPStr)] string key,
                                                              [System.Runtime.InteropServices.In] [System.Runtime.InteropServices.MarshalAs(
                                                                  System.Runtime.InteropServices.UnmanagedType.LPStr)] string value);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_transponder_remove_userdata(System.IntPtr mtaHandle, System.IntPtr modifyPtr,
                                                                 [System.Runtime.InteropServices.In] [System.Runtime.InteropServices.MarshalAs(
                                                                     System.Runtime.InteropServices.UnmanagedType.LPStr)
                                                                                                     ] string key);

        /// Return Type: transponder_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_transponder_get_head",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_transponder_get_head(System.IntPtr mtaHandle);


        /// Return Type: transponder_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_transponder_get_next",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_transponder_get_next(System.IntPtr mtaHandle);


        /// Return Type: transponder_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_transponder_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_transponder_find(System.IntPtr mtaHandle, uint id);


        /// Return Type: uint32_t->unsigned int
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_transponder_get_count",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_transponder_get_count(System.IntPtr mtaHandle);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_transponder_is_data_available"
            , CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_transponder_is_data_available(System.IntPtr mtaHandle);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_transpondergroup_insert(System.IntPtr mtaHandle, uint id);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_transpondergroup_update(System.IntPtr mtaHandle, uint id);

        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_transpondergroup_delete",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_transpondergroup_delete(System.IntPtr mtaHandle, uint id);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        ///key: char*
        ///dst: char*
        ///dstlen: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_transpondergroup_get_userdata"
            , CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_transpondergroup_get_userdata(System.IntPtr mtaHandle, uint id,
                                                                    [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                        System.Runtime.InteropServices.UnmanagedType.
                                                                        LPStr)] string key,
            [System.Runtime.InteropServices.Out] [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                   System.Runtime.InteropServices.UnmanagedType.LPStr)]System.Text.StringBuilder dst,
                                                                    uint dstlen);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_transpondergroup_add_userdata(System.IntPtr mtaHandle, System.IntPtr modifyPtr,
                                                              [System.Runtime.InteropServices.In] [System.Runtime.InteropServices.MarshalAs(
                                                                  System.Runtime.InteropServices.UnmanagedType.LPStr)] string key,
                                                              [System.Runtime.InteropServices.In] [System.Runtime.InteropServices.MarshalAs(
                                                                  System.Runtime.InteropServices.UnmanagedType.LPStr)] string value);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_transpondergroup_remove_userdata(System.IntPtr mtaHandle, System.IntPtr modifyPtr,
                                                                 [System.Runtime.InteropServices.In] [System.Runtime.InteropServices.MarshalAs(
                                                                     System.Runtime.InteropServices.UnmanagedType.LPStr)
                                                                                                     ] string key);
        /// Return Type: transpondergroup_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_transpondergroup_get_head",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_transpondergroup_get_head(System.IntPtr mtaHandle);


        /// Return Type: transpondergroup_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_transpondergroup_get_next",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_transpondergroup_get_next(System.IntPtr mtaHandle);


        /// Return Type: transpondergroup_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_transpondergroup_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_transpondergroup_find(System.IntPtr mtaHandle,
                                                                     uint id);


        /// Return Type: uint32_t->unsigned int
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_transpondergroup_get_count",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_transpondergroup_get_count(System.IntPtr mtaHandle);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            EntryPoint = "mta_transpondergroup_is_data_available",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_transpondergroup_is_data_available(System.IntPtr mtaHandle);


        /// Return Type: auxevent_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_auxevent_get_head",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_auxevent_get_head(System.IntPtr eventDataHandle);


        /// Return Type: auxevent_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_auxevent_get_next",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_auxevent_get_next(System.IntPtr eventDataHandle);


        /// Return Type: auxevent_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_auxevent_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_auxevent_find(System.IntPtr eventDataHandle, uint id);


        /// Return Type: uint32_t->unsigned int
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_auxevent_get_count",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_auxevent_get_count(System.IntPtr eventDataHandle);


        /// Return Type: auxstatus_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///ioterminalid: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_auxstatus_current_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_auxstatus_current_find(System.IntPtr eventDataHandle,
                                                                      uint ioterminalid);


        /// Return Type: decoderstatus_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///decoderid: int64_t->__int64
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_decoderstatus_current_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_decoderstatus_current_find(System.IntPtr eventDataHandle,
                                                                          long decoderid);


        /// Return Type: driverinfo_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_driverinfo_get_head",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_driverinfo_get_head(System.IntPtr eventDataHandle);


        /// Return Type: driverinfo_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_driverinfo_get_next",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_driverinfo_get_next(System.IntPtr eventDataHandle);


        /// Return Type: driverinfo_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_driverinfo_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_driverinfo_find(System.IntPtr eventDataHandle, uint id);


        /// Return Type: uint32_t->unsigned int
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_driverinfo_get_count",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_driverinfo_get_count(System.IntPtr eventDataHandle);


        /// Return Type: driverinfo_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_driverinfo_current_get_head",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_driverinfo_current_get_head(System.IntPtr eventDataHandle);


        /// Return Type: driverinfo_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_driverinfo_current_get_next",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_driverinfo_current_get_next(System.IntPtr eventDataHandle);


        /// Return Type: driverinfo_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_driverinfo_current_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_driverinfo_current_find(System.IntPtr eventDataHandle, uint id);


        /// Return Type: boolean
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_driverinfo_is_data_available",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_driverinfo_is_data_available(System.IntPtr eventDataHandle);


        /// Return Type: passingfirstcontact_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///transponderid: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            EntryPoint = "mta_passingfirstcontact_current_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_passingfirstcontact_current_find(
            System.IntPtr eventDataHandle, uint transponderid);


        /// Return Type: passing_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_passing_get_head",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_passing_get_head(System.IntPtr eventDataHandle);


        /// Return Type: passing_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_passing_get_tail",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_passing_get_tail(System.IntPtr eventDataHandle);


        /// Return Type: passing_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_passing_get_next",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_passing_get_next(System.IntPtr eventDataHandle);


        /// Return Type: passing_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_passing_get_previous",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_passing_get_previous(System.IntPtr eventDataHandle);


        /// Return Type: passing_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_passing_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_passing_find(System.IntPtr eventDataHandle, uint id);


        /// Return Type: uint32_t->unsigned int
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_passing_get_count",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_passing_get_count(System.IntPtr eventDataHandle);


        /// Return Type: passingtrigger_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_passingtrigger_get_head",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_passingtrigger_get_head(System.IntPtr eventDataHandle);


        /// Return Type: passingtrigger_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_passingtrigger_get_tail",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_passingtrigger_get_tail(System.IntPtr eventDataHandle);


        /// Return Type: passingtrigger_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_passingtrigger_get_next",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_passingtrigger_get_next(System.IntPtr eventDataHandle);


        /// Return Type: passingtrigger_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_passingtrigger_get_previous",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_passingtrigger_get_previous(System.IntPtr eventDataHandle);


        /// Return Type: passingtrigger_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_passingtrigger_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_passingtrigger_find(System.IntPtr eventDataHandle, uint id);


        /// Return Type: uint32_t->unsigned int
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_passingtrigger_get_count",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_passingtrigger_get_count(System.IntPtr eventDataHandle);


        /// Return Type: transponderstatus_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///transponderid: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            EntryPoint = "mta_transponderstatus_current_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_transponderstatus_current_find(
            System.IntPtr eventDataHandle, uint transponderid);


        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///NotifyBeaconDownloadConfig: pfNotifyBeaconDownloadConfig
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_beacondownloadconfig",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_beacondownloadconfig(System.IntPtr mtaHandle,
                                                                  NativeNotifyMultiObjectDelegate
                                                                      NotifyBeaconDownloadConfig);


        /// Return Type: void
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///NotifyBeaconDownloadStatus: pfNotifyBeaconDownloadStatus
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_beacondownloadstatus",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_beacondownloadstatus(System.IntPtr eventDataHandle,
                                                                  NativeNotifyMultiObjectDelegate NotifyBeaconDownloadStatus);


        /// Return Type: void
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///NotifyBeaconDownloadTrigger: pfNotifyBeaconDownloadTrigger
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_beacondownloadtrigger",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_beacondownloadtrigger(System.IntPtr eventDataHandle,
                                                                   NativeNotifyMultiObjectDelegate NotifyBeaconDownloadTrigger);


        /// Return Type: void
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///NotifyBeaconLog: pfNotifyBeaconLog
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_beaconlog",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_beaconlog(System.IntPtr eventDataHandle,
                                                       pfNotifyBeaconLog NotifyBeaconLog);


        /// Return Type: beacondownloadconfig_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_beacondownloadconfig_get_head"
            , CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_beacondownloadconfig_get_head(System.IntPtr mtaHandle);


        /// Return Type: beacondownloadconfig_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_beacondownloadconfig_get_next"
            , CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_beacondownloadconfig_get_next(System.IntPtr mtaHandle);


        /// Return Type: beacondownloadconfig_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_beacondownloadconfig_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_beacondownloadconfig_find(System.IntPtr mtaHandle,
                                                                         uint id);


        /// Return Type: uint32_t->unsigned int
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            EntryPoint = "mta_beacondownloadconfig_get_count",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_beacondownloadconfig_get_count(System.IntPtr mtaHandle);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            EntryPoint = "mta_beacondownloadconfig_is_data_available",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_beacondownloadconfig_is_data_available(System.IntPtr mtaHandle);


        /// Return Type: beacondownloadstatus_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_beacondownloadstatus_get_head"
            , CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_beacondownloadstatus_get_head(System.IntPtr eventDataHandle);


        /// Return Type: beacondownloadstatus_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_beacondownloadstatus_get_next"
            , CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_beacondownloadstatus_get_next(System.IntPtr eventDataHandle);


        /// Return Type: beacondownloadstatus_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_beacondownloadstatus_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_beacondownloadstatus_find(System.IntPtr eventDataHandle,
                                                                         uint id);


        /// Return Type: uint32_t->unsigned int
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            EntryPoint = "mta_beacondownloadstatus_get_count",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_beacondownloadstatus_get_count(System.IntPtr eventDataHandle);


        /// Return Type: beacondownloadtrigger_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            EntryPoint = "mta_beacondownloadtrigger_get_head",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_beacondownloadtrigger_get_head(
            System.IntPtr eventDataHandle);


        /// Return Type: beacondownloadtrigger_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            EntryPoint = "mta_beacondownloadtrigger_get_next",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_beacondownloadtrigger_get_next(
            System.IntPtr eventDataHandle);


        /// Return Type: beacondownloadtrigger_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_beacondownloadtrigger_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_beacondownloadtrigger_find(System.IntPtr eventDataHandle,
                                                                          uint id);


        /// Return Type: uint32_t->unsigned int
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            EntryPoint = "mta_beacondownloadtrigger_get_count",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_beacondownloadtrigger_get_count(System.IntPtr eventDataHandle);


        /// Return Type: boolean
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///triggerid: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_beaconlog_request",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_beaconlog_request(System.IntPtr eventDataHandle, uint triggerid);


        /// Return Type: beaconlog_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///triggerid: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_beaconlog_get_head",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_beaconlog_get_head(System.IntPtr eventDataHandle,
                                                                  uint triggerid);


        /// Return Type: beaconlog_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///triggerid: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_beaconlog_get_next",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_beaconlog_get_next(System.IntPtr eventDataHandle,
                                                                  uint triggerid);


        /// Return Type: uint32_t->unsigned int
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///triggerid: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_beaconlog_count",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_beaconlog_count(System.IntPtr eventDataHandle, uint triggerid);


        /// Return Type: beaconlog_t*
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///triggerid: uint32_t->unsigned int
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_beaconlog_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_beaconlog_find(System.IntPtr eventDataHandle,
                                                              uint triggerid, uint id);
        
        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_beacondownloadconfig_delete",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_beacondownloadconfig_delete(System.IntPtr mtaHandle, uint id);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", 
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_beacondownloadconfig_insert(System.IntPtr mtaHandle);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_beacondownloadconfig_update(System.IntPtr mtaHandle, uint id);

        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_beacondownload_cancel",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_beacondownload_cancel(System.IntPtr mtaHandle);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        ///utctime: mdp_time_t->int64_t->__int64
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_beacondownload_manual",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_beacondownload_manual(System.IntPtr mtaHandle, uint id,
                                                            long utctime);

        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///NotifySector: pfNotifySector
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_sector",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_sector(System.IntPtr mtaHandle,
                                                     NativeNotifyMultiObjectDelegate NotifySector);
        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///NotifySequence: pfNotifySequence
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_sequence",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_sequence(System.IntPtr mtaHandle,
                                                      NativeNotifyMultiObjectDelegate NotifySequence);


        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///NotifySequenceSegment: pfNotifySequenceSegment
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_sequencesegment",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_sequencesegment(System.IntPtr mtaHandle,
                                                             NativeNotifyMultiObjectDelegate NotifySequenceSegment);


        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///NotifyTrackSolution: pfNotifyTrackSolution
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_tracksolution",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_tracksolution(System.IntPtr mtaHandle,
                                                           NativeNotifyMultiObjectDelegate NotifyTrackSolution);

        /// Return Type: void
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///NotifyTrackSolutionGroup: pfNotifyTrackSolutionGroup
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_tracksolutiongroup",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_tracksolutiongroup(System.IntPtr mtaHandle,
                                                                NativeNotifyMultiObjectDelegate NotifyTrackSolutionGroup);

        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sector_is_data_available",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_sector_is_data_available(System.IntPtr mtaHandle);


        /// Return Type: sector_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sector_get_head",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_sector_get_head(System.IntPtr mtaHandle);


        /// Return Type: sector_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sector_get_next",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_sector_get_next(System.IntPtr mtaHandle);


        /// Return Type: sector_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sector_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_sector_find(System.IntPtr mtaHandle, uint id);


        /// Return Type: uint32_t->unsigned int
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sector_count",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_sector_count(System.IntPtr mtaHandle);

        /// Return Type: sector length if available or else INT64_MAX
        /// handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        /// id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sector_get_length",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern long mta_sector_get_length(System.IntPtr mtaHandle, uint id);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_sector_insert(System.IntPtr mtaHandle, System.IntPtr trackSolutionPtr, uint id);


        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_sector_update(System.IntPtr mtaHandle, uint id);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sector_delete",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_sector_delete(System.IntPtr mtaHandle, uint id);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        ///key: char*
        ///dst: char*
        ///dstlen: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sector_get_userdata",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_sector_get_userdata(System.IntPtr mtaHandle, uint id,
                                                           [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(
                                                               System.Runtime.InteropServices.UnmanagedType.LPStr)] string key, System.IntPtr dst, uint dstlen);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sequence_is_data_available",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_sequence_is_data_available(System.IntPtr mtaHandle);


        /// Return Type: sequence_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sequence_get_head",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_sequence_get_head(System.IntPtr mtaHandle);


        /// Return Type: sequence_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sequence_get_next",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_sequence_get_next(System.IntPtr mtaHandle);


        /// Return Type: sequence_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sequence_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_sequence_find(System.IntPtr mtaHandle, uint id);


        /// Return Type: uint32_t->unsigned int
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sequence_count",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_sequence_count(System.IntPtr mtaHandle);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_sequence_insert(System.IntPtr mtaHandle, System.IntPtr trackSolutionPtr, uint id);
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_sequence_update(System.IntPtr mtaHandle, uint id);
        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sequence_delete",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_sequence_delete(System.IntPtr mtaHandle, uint id);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        ///key: char*
        ///dst: char*
        ///dstlen: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sequence_get_userdata",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_sequence_get_userdata(System.IntPtr mtaHandle, uint id,
                                                            [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                System.Runtime.InteropServices.UnmanagedType.LPStr)] string key, System.IntPtr dst, uint dstlen);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            EntryPoint = "mta_sequencesegment_is_data_available",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_sequencesegment_is_data_available(System.IntPtr mtaHandle);


        /// Return Type: sequencesegment_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sequencesegment_get_head",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_sequencesegment_get_head(System.IntPtr mtaHandle);


        /// Return Type: sequencesegment_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sequencesegment_get_next",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_sequencesegment_get_next(System.IntPtr mtaHandle);


        /// Return Type: sequencesegment_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sequencesegment_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_sequencesegment_find(System.IntPtr mtaHandle, uint id);


        /// Return Type: uint32_t->unsigned int
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sequencesegment_count",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_sequencesegment_count(System.IntPtr mtaHandle);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_sequencesegment_insert(System.IntPtr mtaHandle, System.IntPtr trackSolutionPtr, uint id);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_sequencesegment_update(System.IntPtr mtaHandle, uint id);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sequencesegment_delete",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_sequencesegment_delete(System.IntPtr mtaHandle, uint id);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        ///key: char*
        ///dst: char*
        ///dstlen: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sequencesegment_get_userdata",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_sequencesegment_get_userdata(System.IntPtr mtaHandle, uint id,
                                                                   [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                       System.Runtime.InteropServices.UnmanagedType.
                                                                       LPStr)] string key, System.IntPtr dst,
                                                                   uint dstlen);


        /// Return Type: tracksolution_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_tracksolution_get_head",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_tracksolution_get_head(System.IntPtr mtaHandle);


        /// Return Type: tracksolution_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_tracksolution_get_next",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_tracksolution_get_next(System.IntPtr mtaHandle);


        /// Return Type: tracksolution_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_tracksolution_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_tracksolution_find(System.IntPtr mtaHandle, uint id);


        /// Return Type: uint32_t->unsigned int
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_tracksolution_count",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_tracksolution_count(System.IntPtr mtaHandle);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            EntryPoint = "mta_tracksolution_is_data_available",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_tracksolution_is_data_available(System.IntPtr mtaHandle);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_tracksolution_insert(System.IntPtr mtaHandle, uint id);
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_tracksolution_update(System.IntPtr mtaHandle, uint id);
        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_tracksolution_delete",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_tracksolution_delete(System.IntPtr mtaHandle, uint id);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        ///key: char*
        ///dst: char*
        ///dstlen: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_tracksolution_get_userdata",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_tracksolution_get_userdata(System.IntPtr mtaHandle, uint id,
                                                                 [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(
                                                                     System.Runtime.InteropServices.UnmanagedType.LPStr)
                                                                                                                ] string key, System.IntPtr dst, uint dstlen);


        /// Return Type: tracksolutiongroup_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_tracksolutiongroup_get_head",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_tracksolutiongroup_get_head(System.IntPtr mtaHandle);


        /// Return Type: tracksolutiongroup_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_tracksolutiongroup_get_next",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_tracksolutiongroup_get_next(System.IntPtr mtaHandle);


        /// Return Type: tracksolutiongroup_t*
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_tracksolutiongroup_find",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_tracksolutiongroup_find(System.IntPtr mtaHandle,
                                                                       uint id);


        /// Return Type: uint32_t->unsigned int
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_tracksolutiongroup_count",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_tracksolutiongroup_count(System.IntPtr mtaHandle);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            EntryPoint = "mta_tracksolutiongroup_is_data_available",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_tracksolutiongroup_is_data_available(System.IntPtr mtaHandle);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_tracksolutiongroup_insert(System.IntPtr mtaHandle, uint id);
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_tracksolutiongroup_update(System.IntPtr mtaHandle, uint id);
        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_tracksolutiongroup_delete",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_tracksolutiongroup_delete(System.IntPtr mtaHandle, uint id);


        /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        ///key: char*
        ///dst: char*
        ///dstlen: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            EntryPoint = "mta_tracksolutiongroup_get_userdata",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_tracksolutiongroup_get_userdata(System.IntPtr mtaHandle,
                                                                      uint id,
                                                                      [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute
                                                                          (
                                                                          System.Runtime.InteropServices.UnmanagedType.
                                                                          LPStr)] string key, System.IntPtr dst,
                                                                      uint dstlen);

        /// Return Type: void
        ///handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        ///NotifyManualEvent: pfNotifyManualEvent
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_notify_manualevent",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mta_notify_manualevent(System.IntPtr eventDataHandle, NativeNotifyMultiObjectDelegate NotifyManualEvent);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_manualevent_insert(System.IntPtr mtaHandle);

        /// Get the latest sequence-row for the given competitorsessionid.
        /// Return Type: sequencerow_t
        /// handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sequencerow_get_latest",
                                                                            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_sequencerow_get_latest(System.IntPtr eventDataHandle, uint competitorsessionid);

        /// Returns the number of sequence-segment cell(s) on the sequence-row.
        /// Return Type: UInt32
        /// handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sequencesegmentcell_get_count",
                                                                            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_sequencesegmentcell_get_count(System.IntPtr eventDataHandle, uint sequencerowid);

        /// Get a sequence-segment cell at a position on the sequence-row. Returns NULL at the end.
        /// Return Type: sequencesegmentcell_t*
        /// handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sequencesegmentcell_get_at",
                                                                            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_sequencesegmentcell_get_at(System.IntPtr eventDataHandle, uint sequencerowid, uint index);

        /// Get the length of a sequence-segment cell at a position on the sequence-row. If sequence-row ID is UINT32_MAX the default-row will be returned.
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sequencesegmentcell_get_length",
                                                                            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern long mta_sequencesegmentcell_get_length(System.IntPtr eventDataHandle, uint sequencerowid, uint index);

        /// Returns the number of sector cell(s) on the sequence-row.
        /// Return Type: UInt32
        /// handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sectorcell_get_count",
                                                                            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern uint mta_sectorcell_get_count(System.IntPtr eventDataHandle, uint sequencerowid);

        /// Get a sector cell at a position on the sequence-row. Returns NULL at the end.
        /// Return Type: sectorcell_t*
        /// handle: mta_eventdata_handle_t->mta_eventdata_handle_dummystruct*
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sectorcell_get_at",
                                                                            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern System.IntPtr mta_sectorcell_get_at(System.IntPtr eventDataHandle, uint sequencerowid, uint index);

        /// Get the length of a sector cell at a position on the sequence-row. If sequence-row ID is UINT32_MAX the default-row will be returned.
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_sectorcell_get_length",
                                                                            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern long mta_sectorcell_get_length(System.IntPtr eventDataHandle, uint sequencerowid, uint index);

          /// Return Type: boolean
        ///handle: mdp_appliance_handle_t->mdp_appliance_handle_dummystruct*
        ///id: uint32_t->unsigned int
        ///key: char*
        ///dst: char*
        ///dstlen: uint32_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll", EntryPoint = "mta_competitor_get_userdata",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_competitor_get_userdata(System.IntPtr mtaHandle, uint id,
                                                        [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(
                                                            System.Runtime.InteropServices.UnmanagedType.LPStr)] string
                                                            key,
                                                        [System.Runtime.InteropServices.MarshalAsAttribute(
                                                            System.Runtime.InteropServices.UnmanagedType.LPStr)]System.Text.StringBuilder dst, uint dstlen);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_competitor_add_userdata(System.IntPtr mtaHandle, System.IntPtr modifyPtr,
                                                              [System.Runtime.InteropServices.In] [System.Runtime.InteropServices.MarshalAs(
                                                                  System.Runtime.InteropServices.UnmanagedType.LPStr)] string key,
                                                              [System.Runtime.InteropServices.In] [System.Runtime.InteropServices.MarshalAs(
                                                                  System.Runtime.InteropServices.UnmanagedType.LPStr)] string value);

        [System.Runtime.InteropServices.DllImportAttribute("MylapsSDK.dll",
            CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool mta_competitor_remove_userdata(System.IntPtr mtaHandle, System.IntPtr modifyPtr,
                                                                 [System.Runtime.InteropServices.In] [System.Runtime.InteropServices.MarshalAs(
                                                                     System.Runtime.InteropServices.UnmanagedType.LPStr)] string key);
    }
}
