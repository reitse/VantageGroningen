using System;
using MylapsSDK.Containers;
using MylapsSDK.NotifyHandlers;
using System.Runtime.InteropServices;
using MylapsSDK.MylapsSDKLibrary;
using System.Collections.ObjectModel;
using MylapsSDK.Utilities;
using MylapsSDK.Exceptions;
using System.Collections.Generic;

namespace MylapsSDK.Objects
{
    public class MTA : IDisposable
    {
        bool _disposed;
        private SDK _sdkHandleWrapper;
        private IntPtr _nativeHandle = IntPtr.Zero;

        /** keep a ref to this delegates or else it will be deleted by the GC */
        private readonly pfNotifyConnect _connectionNotifier;
        private readonly pfNotifyConnectionState _connectionStateNotifier;

        public OnNotifyConnectHandler NotifyConnectHandlers;
        public OnNotifyConnectionStateHandler NotifyConnectionStateHandlers;

        private readonly List<EventData> _eventDataHandleWrappers = new List<EventData>();

        private SystemSetupContainer _systemSetupContainer;
        private SegmentContainer _segmentContainer = null;
        private LoopContainer _loopContainer = null;
        private DecoderContainer _decoderContainer = null;
        private DecoderPresetGroupContainer _decoderPresetGroupContainer = null;
        private TransponderContainer _transponderContainer = null;
        private TransponderGroupContainer _transponderGroupContainer = null;
        private IOTerminalContainer _ioterminalContainer = null;
        private BeaconDownloadConfigContainer _beaconDownloadConfigContainer = null;
        private TrackSolutionContainer _trackSolutionContainer = null;
        private CompetitorContainer _competitorContainer = null;

        internal MTA(SDK sdkHandleWrapper)
        {
            _disposed = false;
            _sdkHandleWrapper = sdkHandleWrapper;
            _nativeHandle = NativeMethods.mta_handle_alloc(sdkHandleWrapper.NativeHandle, IntPtr.Zero);

            if (_nativeHandle == IntPtr.Zero)
                throw new MylapsException("failed to create a MTA instance");

            _connectionNotifier = NotifyConnect;
            _connectionStateNotifier = NotifyConnectionState;

            NativeMethods.mta_notify_connect(_nativeHandle, _connectionNotifier);
            NativeMethods.mta_notify_connectionstate(_nativeHandle, _connectionStateNotifier);

            SetupContainers();
        }

        private void SetupContainers()
        {
            _systemSetupContainer = new SystemSetupContainer(this);
            _trackSolutionContainer = new TrackSolutionContainer(this);
            _segmentContainer = new SegmentContainer(this);
            _beaconDownloadConfigContainer = new BeaconDownloadConfigContainer(this);
            _loopContainer = new LoopContainer(this);
            _transponderContainer = new TransponderContainer(this);
            _transponderGroupContainer = new TransponderGroupContainer(this);
            _ioterminalContainer = new IOTerminalContainer(this);
            _decoderContainer = new DecoderContainer(this);
            _decoderPresetGroupContainer = new DecoderPresetGroupContainer(this);
            _competitorContainer = new CompetitorContainer(this);
        }

        public SDK SDK
        {
            get { return _sdkHandleWrapper; }
        }

        internal IntPtr NativeHandle
        {
            get { return _nativeHandle; }
        }

        //EventData Factory methods
        public EventData CreateEventDataLive()
		{
			var eventData = EventData.CreateLive(this);
			_eventDataHandleWrappers.Add(eventData);
			return eventData;
		}

        [Obsolete("Deprecated member function. Please use CreateEventDataLive instead", false)]
        public EventData GetEventDataLive()
        {
            return this.CreateEventDataLive();
        }

        public EventData CreateEventDataLiveWithResend(DateTime beginTime)
        {
            var eventData = EventData.CreateLiveWithResend(this, beginTime);
            _eventDataHandleWrappers.Add(eventData);
            return eventData;
        }

        [Obsolete("Deprecated member function. Please use CreateEventDataLiveWithResend instead", false)]
        public EventData GetEventDataLiveWithResend(DateTime beginTime)
        {
            return this.CreateEventDataLiveWithResend(beginTime);
        }

        public EventData CreateEventDataResend(DateTime beginTime, DateTime endTime)
        {
            var eventData = EventData.CreateResend(this, beginTime, endTime);
            _eventDataHandleWrappers.Add(eventData);
            return eventData;
        }

        [Obsolete("Deprecated member function. Please use CreateEventDataResend instead", false)]
        public EventData GetEventDataResend(DateTime beginTime, DateTime endTime)
        {
            return this.CreateEventDataResend(beginTime, endTime);
        }

        public void ClearEventData(EventData eventData)
        {
            if (_eventDataHandleWrappers.Contains(eventData))
            {
                eventData.Dispose();
                _eventDataHandleWrappers.Remove(eventData);
            }
        }

        [Obsolete("Deprecated member function. Please use ClearEventData instead", false)]
    	public void CloseEventData(EventData eventData)
		{
            ClearEventData(eventData);
		}

          private void NotifyConnect(IntPtr handle, [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)] bool isConnected, IntPtr context)
        {
            if (NotifyConnectHandlers != null)
            {
                NotifyConnectHandlers(isConnected, this);
            }
        }

        private void NotifyConnectionState(IntPtr handle, uint connectionState, IntPtr context)
        {
            if (NotifyConnectionStateHandlers != null)
            {
                NotifyConnectionStateHandlers((CONNECTIONSTATE)connectionState, this);
            }
        }

         // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            // Check to see if Dispose has already been called.
            if (!_disposed)
            {
                // Dispose all managed and unmanaged resources.
                _eventDataHandleWrappers.ForEach(eventDataHandler => eventDataHandler.Dispose());

                ClearContainers();

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.
                if (IsConnected())
                {
                    Disconnect();
                }

                NativeMethods.mta_handle_dealloc(_sdkHandleWrapper.NativeHandle, _nativeHandle);
                _nativeHandle = IntPtr.Zero;

                // Note disposing has been done.
                _disposed = true;
            }
        }

        ////////////////////////////////////////////////////////////////////
        //AddNotifying/Removing Notify Delegates
        ////////////////////////////////////////////////////////////////////
        [Obsolete("Deprecated member function. Please use the NotifyConnectHandlers property instead", false)]
        public void AddNotifyConnectHandler(OnNotifyConnectHandler handler)
        {
            this.NotifyConnectHandlers += handler;
        }

        [Obsolete("Deprecated member function. Please use the NotifyConnectHandlers property instead", false)]
        public void RemoveNotifyConnectionHandler(OnNotifyConnectHandler handler)
        {
            this.NotifyConnectHandlers -= handler;
        }

        [Obsolete("Deprecated member function. Please use the NotifyConnectionStateHandlers property instead", false)]
        public void AddNotifyConnectionStateHandler(OnNotifyConnectionStateHandler handler)
        {
            this.NotifyConnectionStateHandlers += handler;
        }

        [Obsolete("Deprecated member function. Please use the NotifyConnectionStateHandlers property instead", false)]
        public void RemoveNotifyConnectionStateHandler(OnNotifyConnectionStateHandler handler)
        {
            this.NotifyConnectionStateHandlers -= handler;
        }

        public bool SubscribeToObjectData(MTAOBJECTDATA objectData)
        {
            return NativeMethods.mta_objectdata_subscribe(_nativeHandle, objectData);
        }

        public bool UnsubscribeFromObjectData(MTAOBJECTDATA objectData)
        {
            return NativeMethods.mta_objectdata_unsubscribe(_nativeHandle, objectData);
        }

        [Obsolete("Deprecated member function. Please use the LoopContainer property instead", false)]
        public bool IsAllLoopDataAvailable()
        {
            return _loopContainer.AllDataAvailable;
        }

        [Obsolete("Deprecated member function. Please use the DecoderContainer property instead", false)]
        public bool IsAllDecoderDataAvailable()
        {
            return _decoderContainer.AllDataAvailable;
        }

        [Obsolete("Deprecated member function. Please use the SystemSetupContainer property instead", false)]
        public void AddNotifySystemSetupHandler(Action<MDP_NOTIFY_TYPE, List<SystemSetup>, MTA> handler)
        {
            _systemSetupContainer.NotifyHandlers += handler;
        }

        [Obsolete("Deprecated member function. Please use the SystemSetupContainer property instead", false)]
        public void RemoveNotifySystemSetupHandler(Action<MDP_NOTIFY_TYPE, List<SystemSetup>, MTA> handler)
        {
            _systemSetupContainer.NotifyHandlers -= handler;
        }

        [Obsolete("Deprecated member function. Please use the SegmentContainer property instead", false)]
        public void AddNotifySegmentHandler(Action<MDP_NOTIFY_TYPE, List<Segment>, MTA> handler)
        {
            _segmentContainer.NotifyHandlers += handler;
        }

        [Obsolete("Deprecated member function. Please use the SegmentContainer property instead", false)]
        public void RemoveNotifySegmentHandler(Action<MDP_NOTIFY_TYPE, List<Segment>, MTA> handler)
        {
            _segmentContainer.NotifyHandlers -= handler;
        }

        [Obsolete("Deprecated member function. Please use the LoopContainer property instead", false)]
        public void AddNotifyLoopHandler(Action<MDP_NOTIFY_TYPE, List<Loop>, MTA> handler)
        {
            _loopContainer.NotifyHandlers += handler;
        }

        [Obsolete("Deprecated member function. Please use the LoopContainer property instead", false)]
        public void RemoveNotifyLoopHandler(Action<MDP_NOTIFY_TYPE, List<Loop>, MTA> handler)
        {
            _loopContainer.NotifyHandlers -= handler;
        }

        [Obsolete("Deprecated member function. Please use the DecoderContainer property instead", false)]
        public void AddNotifyDecoderHandler(Action<MDP_NOTIFY_TYPE, List<Decoder>, MTA> handler)
        {
            _decoderContainer.NotifyHandlers += handler;
        }

        [Obsolete("Deprecated member function. Please use the DecoderContainer property instead", false)]
        public void RemoveNotifyDecoderHandler(Action<MDP_NOTIFY_TYPE, List<Decoder>, MTA> handler)
        {
            _decoderContainer.NotifyHandlers -= handler;
        }

        [Obsolete("Deprecated member function. Please use the TransponderContainer property instead", false)]
        public void AddNotifyTransponderHandler(Action<MDP_NOTIFY_TYPE, List<Transponder>, MTA> handler)
        {
            _transponderContainer.NotifyHandlers += handler;
        }

        [Obsolete("Deprecated member function. Please use the TransponderContainer property instead", false)]
        public void RemoveNotifyTransponderHandler(Action<MDP_NOTIFY_TYPE, List<Transponder>, MTA> handler)
        {
            _transponderContainer.NotifyHandlers -= handler;
        }

        [Obsolete("Deprecated member function. Please use the TransponderGroupContainer property instead", false)]
        public void AddNotifyTransponderGroupHandler(Action<MDP_NOTIFY_TYPE, List<TransponderGroup>, MTA> handler)
        {
            _transponderGroupContainer.NotifyHandlers += handler;
        }

        [Obsolete("Deprecated member function. Please use the TransponderGroupContainer property instead", false)]
        public void RemoveNotifyTransponderGroupHandler(Action<MDP_NOTIFY_TYPE, List<TransponderGroup>, MTA> handler)
        {
            _transponderGroupContainer.NotifyHandlers -= handler;
        }

        [Obsolete("Deprecated member function. Please use the IOTerminalContainer property instead", false)]
        public void AddNotifyIOTerminalHandler(Action<MDP_NOTIFY_TYPE, List<IOTerminal>, MTA> handler)
        {
            _ioterminalContainer.NotifyHandlers += handler;
        }

        [Obsolete("Deprecated member function. Please use the IOTerminalContainer property instead", false)]
        public void RemoveNotifyIOTerminalHandler(Action<MDP_NOTIFY_TYPE, List<IOTerminal>, MTA> handler)
        {
            _ioterminalContainer.NotifyHandlers -= handler;
        }

        [Obsolete("Deprecated member function. Please use the BeaconDownloadConfigContainer property instead", false)]
        public void AddNotifyBeaconDownloadConfigHandler(Action<MDP_NOTIFY_TYPE, List<BeaconDownloadConfig>, MTA> handler)
        {
            _beaconDownloadConfigContainer.NotifyHandlers += handler;
        }

        [Obsolete("Deprecated member function. Please use the BeaconDownloadConfigContainer property instead", false)]
        public void RemoveNotifyBeaconDownloadConfigHandler(Action<MDP_NOTIFY_TYPE, List<BeaconDownloadConfig>, MTA> handler)
        {
            _beaconDownloadConfigContainer.NotifyHandlers -= handler;
        }

        [Obsolete("Deprecated member function. Please use the TrackSolutionContainer property instead", false)]
        public void AddNotifyTrackSolutionHandler(OnNotifyTrackSolutionHandler handler)
        {
            _trackSolutionContainer.NotifyTrackSolutionHandlers += handler;
        }

        [Obsolete("Deprecated member function. Please use the TrackSolutionContainer property instead", false)]
        public void RemoveNotifyTrackSolutionHandler(OnNotifyTrackSolutionHandler handler)
        {
            _trackSolutionContainer.NotifyTrackSolutionHandlers -= handler;
        }

        [Obsolete("Deprecated member function. Please use the TrackSolutionContainer property instead", false)]
        public void AddNotifyTrackSolutionGroupHandler(OnNotifyTrackSolutionGroupHandler handler)
        {
            _trackSolutionContainer.NotifyTrackSolutionGroupHandlers += handler;
        }

        [Obsolete("Deprecated member function. Please use the TrackSolutionContainer property instead", false)]
        public void RemoveNotifyTrackSolutionHandler(OnNotifyTrackSolutionGroupHandler handler)
        {
            _trackSolutionContainer.NotifyTrackSolutionGroupHandlers -= handler;
        }

        [Obsolete("Deprecated member function. Please use the TrackSolutionContainer property instead", false)]
        public void AddNotifySectorHandler(OnNotifySectorHandler handler)
        {
            _trackSolutionContainer.NotifySectorHandlers += handler;
        }

        [Obsolete("Deprecated member function. Please use the TrackSolutionContainer property instead", false)]
        public void RemoveNotifySectorHandler(OnNotifySectorHandler handler)
        {
            _trackSolutionContainer.NotifySectorHandlers -= handler;
        }

        [Obsolete("Deprecated member function. Please use the TrackSolutionContainer property instead", false)]
        public void AddNotifySequenceHandler(OnNotifySequenceHandler handler)
        {
            _trackSolutionContainer.NotifySequenceHandlers += handler;
        }

        [Obsolete("Deprecated member function. Please use the TrackSolutionContainer property instead", false)]
        public void RemoveNotifySequenceHandler(OnNotifySequenceHandler handler)
        {
            _trackSolutionContainer.NotifySequenceHandlers -= handler;
        }

        [Obsolete("Deprecated member function. Please use the TrackSolutionContainer property instead", false)]
        public void AddNotifySequenceSegmentHandler(OnNotifySequenceSegmentHandler handler)
        {
            _trackSolutionContainer.NotifySequenceSegmentHandlers += handler;
        }

        [Obsolete("Deprecated member function. Please use the TrackSolutionContainer property instead", false)]
        public void RemoveNotifySequenceSegmentHandler(OnNotifySequenceSegmentHandler handler)
        {
            _trackSolutionContainer.NotifySequenceSegmentHandlers -= handler;
        }

        /**
         * Try to connect to an MTA.
         */
        public Boolean Connect(String hostname, String username, String password)
        {
			// Try to make a secure (i.e. encrypted) connection to an MTA.
            return NativeMethods.mta_connect(_nativeHandle, hostname, username, password, true /* follow current system setup */);
        }

        /**
         * Try to connect to an MTA.
         */
        public Boolean ConnectInsecure(String hostname, String username, String password)
        {
            // Try to make a secure (i.e. encrypted) connection to an MTA.
            return NativeMethods.mta_connect_insecure(_nativeHandle, hostname, username, password, true /* follow current system setup */);
        }

        /**
         * Disconnect from the MTA.
         */
        public void Disconnect()
        {
            NativeMethods.mta_disconnect(_nativeHandle);
        }

        /**
         * Is connected to the MTA?
         * @return True when connected with an MTA
         */
        public bool IsConnected()
        {
            return NativeMethods.mta_is_connected(_nativeHandle);
        }

        /**
         * Get the hostname of the MTA
         * @return The hostAddNotifyress
         */
        public string GetHostname()
        {
            IntPtr p = NativeMethods.mta_get_hostname(_nativeHandle);
            return Marshal.PtrToStringAnsi(p);
        }

        /**
         * Get state of the connection as integer see:
         * MylapsSDKLibrary.CONNECTIONSTATE
         * return Connectionstate
         */
        public CONNECTIONSTATE GetConnectionState()
        {
            return (CONNECTIONSTATE)NativeMethods.mta_get_connectionstate(_nativeHandle);
        }

        /**
         * Get the authenticated user. The return value will be null if the authentication
         * failed or no authentication preceded.
         * @return Authenticated user object
         */
        public User GetUser()
        {
            var userPtr = NativeMethods.mta_get_user(_nativeHandle);
            return userPtr == IntPtr.Zero ? null : new User(userPtr, this);
        }

        public bool TimeIsSynced()
        {
            return NativeMethods.mta_time_is_synced(_nativeHandle);
        }

        public long GetLocalTime(long utcTime)
        {
            return NativeMethods.mta_time_get_local(_nativeHandle, utcTime);
        }

        public long GetTimezoneCorrection(long utcTime)
        {
            return NativeMethods.mta_time_get_timezone_correction(_nativeHandle, utcTime);
        }

        public DateTime GetLocalTimeAsDateTime()
        {
            long localNow = GetLocalTime(GetUTCTime());
            return SDKHelperFunctions.TimestampToDateTime(localNow, DateTimeKind.Local);
        }

        public Int64 GetUTCTime()
        {
            return NativeMethods.mta_time_get_utc(_nativeHandle);
        }

        public DateTime GetUTCTimeAsDateTime()
        {
            return SDKHelperFunctions.TimestampToDateTime(GetUTCTime(), DateTimeKind.Utc);
        }

        public SYNCMETHOD GetSyncSource()
        {
            return (SYNCMETHOD)NativeMethods.mta_time_get_utcsyncsource(_nativeHandle);
        }

        /**
         * Commit all the pending changes (Modify objects).
         */
        public void CommitChanges()
        {
            _systemSetupContainer.MarkChangesAsApplied();
            _loopContainer.MarkChangesAsApplied();
            _transponderContainer.MarkChangesAsApplied();
            _ioterminalContainer.MarkChangesAsApplied();
            _beaconDownloadConfigContainer.MarkChangesAsApplied();
            _trackSolutionContainer.MarkChangesAsApplied();
            _segmentContainer.MarkChangesAsApplied();
            _transponderGroupContainer.MarkChangesAsApplied();
            _competitorContainer.MarkChangesAsApplied();
            
            NativeMethods.mta_changes_commit(_nativeHandle);
            ClearChanges();
        }

        /**
         * Rollback all the pending changes (Modify objects).
         */
        public void RollbackChanges()
        {
            NativeMethods.mta_changes_rollback(_nativeHandle);
            ClearChanges();
        }

        private void ClearChanges()
        {
            _systemSetupContainer.ClearChanges();
            _loopContainer.ClearChanges();
            _transponderContainer.ClearChanges();
            _ioterminalContainer.ClearChanges();
            _beaconDownloadConfigContainer.ClearChanges();
            _trackSolutionContainer.ClearChanges();
            _segmentContainer.ClearChanges();
            _competitorContainer.ClearChanges();
            _transponderGroupContainer.ClearChanges();
        }

        private void ClearContainers()
        {
            _decoderContainer.Clear();
            _decoderPresetGroupContainer.Clear();
            _systemSetupContainer.Clear();
            _loopContainer.Clear();
            _transponderContainer.Clear();
            _transponderGroupContainer.Clear();
            _ioterminalContainer.Clear();
            _beaconDownloadConfigContainer.Clear();
            _trackSolutionContainer.Clear();
            _segmentContainer.Clear();
            _competitorContainer.Clear();
        }

        ////////////////////////////////////////////////////////////////////
        //Loop container data accessors & helpers
        ////////////////////////////////////////////////////////////////////
        [Obsolete("Deprecated member property. Please use the loopContainer property instead", false)]
        public ReadOnlyCollection<Loop> Loops
        {
            get { return _loopContainer.All; }
        }

        [Obsolete("Deprecated member method. Please use the loopContainer property instead", false)]
        public Loop FindLoop(uint loopID)
        {
            return _loopContainer.Find(loopID);
        }

        [Obsolete("Deprecated member method. Please use the loopContainer property instead", false)]
        public LoopModifier UpdateLoop(Loop loop)
        {
            return _loopContainer.Update(loop);
        }

        [Obsolete("Deprecated member method. Please use the loopContainer property instead", false)]
        public LoopModifier InsertLoop(UInt32 id)
        {
            return _loopContainer.Insert(id);
        }

        [Obsolete("Deprecated member method. Please use the loopContainer property instead", false)]
        public LoopModifier InsertLoop()
        {
            return _loopContainer.Insert();
        }

        [Obsolete("Deprecated member method. Please use the loopContainer property instead", false)]
        public bool DeleteLoop(Loop loop)
        {
            return _loopContainer.Delete(loop);
        }

        ////////////////////////////////////////////////////////////////////
        //Transponder container data accessors & helpers
        ////////////////////////////////////////////////////////////////////
        [Obsolete("Deprecated member method. Please use the transponderContainer property instead", false)]
        public bool IsAllTransponderDataAvailable()
        {
            return _transponderContainer.AllDataAvailable;
        }

        [Obsolete("Deprecated member property. Please use the transponderContainer property instead", false)]
        public ReadOnlyCollection<Transponder> Transponders
        {
            get { return _transponderContainer.All; }
        }

        [Obsolete("Deprecated member method. Please use the transponderContainer property instead", false)]
        public Transponder FindTransponder(UInt32 transponderID)
        {
            return _transponderContainer.Find(transponderID);
        }

        [Obsolete("Deprecated member method. Please use the transponderContainer property instead", false)]
        public TransponderModifier UpdateTransponder(Transponder transponder)
        {
            return _transponderContainer.Update(transponder);
        }

        [Obsolete("Deprecated member method. Please use the transponderContainer property instead", false)]
        public TransponderModifier InsertTransponder(UInt32 id)
        {
            return _transponderContainer.Insert(id);
        }

        [Obsolete("Deprecated member method. Please use the transponderContainer property instead", false)]
        public bool DeleteTransponder(Transponder transponder)
        {
            return _transponderContainer.Delete(transponder);
        }

        [Obsolete("Deprecated property. Please use TransponderGroupContainer property instead", false)]
        public TransponderGroupContainer TranspondersGroups
        {
            get { return _transponderGroupContainer; }
        }

        ////////////////////////////////////////////////////////////////////
        //Decoder container data accessors & helpers
        ////////////////////////////////////////////////////////////////////

        [Obsolete("Deprecated property. Please use DecoderContainer property instead", false)]
        public ReadOnlyCollection<Decoder> Decoders
        {
            get { return _decoderContainer.All; }
        }

        [Obsolete("Deprecated method. Please use DecoderContainer property instead", false)]
        public void ConnectDecoder(String ipAddress, UInt32 port, Loop loop)
        {
            _decoderContainer.ConnectDecoder(ipAddress, port, loop);
        }

        [Obsolete("Deprecated method. Please use DecoderContainer property instead", false)]
        public void ConnectDecoder(Decoder decoder, Loop loop)
        {
            _decoderContainer.ConnectDecoder(decoder, loop);
        }

        [Obsolete("Deprecated method. Please use DecoderContainer property instead", false)]
        public void ConnectDecoder(String ipAddress, UInt32 port, IOTerminal ioTerminal)
        {
            _decoderContainer.ConnectDecoder(ipAddress, port, ioTerminal);
        }

        [Obsolete("Deprecated method. Please use DecoderContainer property instead", false)]
        public void ConnectDecoder(Decoder decoder, IOTerminal ioTerminal)
        {
            _decoderContainer.ConnectDecoder(decoder, ioTerminal);
        }

        [Obsolete("Deprecated method. Please use DecoderContainer property instead", false)]
        public void ConnectDecoder(Decoder decoder, Loop loop, IOTerminal ioTerminal)
        {
            _decoderContainer.ConnectDecoder(decoder, loop, ioTerminal);
        }

        [Obsolete("Deprecated method. Please use DecoderContainer property instead", false)]
        public void ConnectDecoder(String ipAddress, UInt32 port, Loop loop, IOTerminal ioTerminal)
        {
            _decoderContainer.ConnectDecoder(ipAddress, port, loop, ioTerminal);
        }

        [Obsolete("Deprecated method. Please use DecoderContainer property instead", false)]
        //Disconnect a decoder from the Appliance. Decoder becomes state: unused
        public void DisconnectDecoder(Decoder decoder)
        {
            _decoderContainer.DisconnectDecoder(decoder);
        }

        [Obsolete("Deprecated method. Please use DecoderContainer property instead", false)]
        //Flash/blink the display of the decoder to identify it in the field
        public void IdentifyDecoder(Decoder decoder)
        {
            _decoderContainer.IdentifyDecoder(decoder);
        }

        ////////////////////////////////////////////////////////////////////
        //Beacon container data accessors & helpers
        ////////////////////////////////////////////////////////////////////
        [Obsolete("Deprecated method. Please use BeaconDownloadConfigContainer property instead", false)]
        public Boolean IsAllBeaconDownloadConfigAvailable()
        {
            return _beaconDownloadConfigContainer.AllDataAvailable;
        }

        [Obsolete("Deprecated method. Please use BeaconDownloadConfigContainer property instead", false)]
        public Boolean CancelBeaconDownload()
        {
            return _beaconDownloadConfigContainer.CancelDownload();
        }

        [Obsolete("Deprecated property. Please use BeaconDownloadConfigContainer property instead", false)]
        public Boolean DeleteBeaconDownload(BeaconDownloadConfig beaconDownload)
        {
            return _beaconDownloadConfigContainer.Delete(beaconDownload);
        }

        [Obsolete("Deprecated method. Please use BeaconDownloadConfigContainer property instead", false)]
        public BeaconDownloadConfig FindBeaconDownloadConfig(UInt32 id)
        {
            return _beaconDownloadConfigContainer.Find(id);
        }

        [Obsolete("Deprecated property. Please use BeaconDownloadConfigContainer property instead", false)]
        public ReadOnlyCollection<BeaconDownloadConfig> BeaconDownloadConfigs
        {
            get { return _beaconDownloadConfigContainer.All; }
        }

        [Obsolete("Deprecated method. Please use beaconDownloadConfigContaine property instead", false)]
        public BeaconDownloadConfigModifier InsertBeaconDownloadConfig()
        {
            return _beaconDownloadConfigContainer.Insert();
        }

        [Obsolete("Deprecated method. Please use beaconDownloadConfigContaine property instead", false)]
        public BeaconDownloadConfigModifier UpdateBeaconDownloadConfig(BeaconDownloadConfig beaconDownload)
        {
            return _beaconDownloadConfigContainer.Update(beaconDownload);
        }

        [Obsolete("Deprecated method. Please use beaconDownloadConfigContaine property instead", false)]
        public Boolean ManualBeaconDownload(BeaconDownloadConfig downloadConfig, Int64 utcTime)
        {
            return _beaconDownloadConfigContainer.ManualDownload(downloadConfig, utcTime);
        }

        ////////////////////////////////////////////////////////////////////
        //IOTerminal container data accessors & helpers
        ////////////////////////////////////////////////////////////////////

        [Obsolete("Deprecated member function. Please use the IOTerminalContainer property instead", false)]
        public ReadOnlyCollection<IOTerminal> IOTerminals
        {
            get { return _ioterminalContainer.All; }
        }

        [Obsolete("Deprecated member function. Please use the IOTerminalContainer property instead", false)]
        public IOTerminal FindIOTerminal(UInt32 ioTerminalID)
        {
            return _ioterminalContainer.Find(ioTerminalID);
        }

        [Obsolete("Deprecated member function. Please use the IOTerminalContainer property instead", false)]
        public IOTerminalModifier UpdateIOTerminal(IOTerminal ioTerminal)
        {
            return _ioterminalContainer.Update(ioTerminal);
        }

        [Obsolete("Deprecated member function. Please use the IOTerminalContainer property instead", false)]
        public IOTerminalModifier InsertIOTerminal()
        {
            return _ioterminalContainer.Insert();
        }

        [Obsolete("Deprecated member function. Please use the IOTerminalContainer property instead", false)]
        public IOTerminalModifier InsertIOTerminal(UInt32 ioTerminalID)
        {
            return _ioterminalContainer.Insert(ioTerminalID);
        }

        [Obsolete("Deprecated member function. Please use the IOTerminalContainer property instead", false)]
        public bool DeleteIOTerminal(IOTerminal ioTerminal)
        {
            return _ioterminalContainer.Delete(ioTerminal);
        }

        [Obsolete("Deprecated member function. Please use the IOTerminalContainer property instead", false)]
        public void SetIOTerminalOutputState(IOTerminal ioTerminal, Byte bitMask, AUX_OUTPUT_STATE outputState)
        {
            _ioterminalContainer.SetIOTerminalOutputState(ioTerminal, bitMask, outputState);
        }

        [Obsolete("Deprecated member function. Please use the IOTerminalContainer property instead", false)]
        public void SetIOTerminalOutputDAC(IOTerminal ioTerminal, Byte bitMask, UInt32 dacValue)
        {
            _ioterminalContainer.SetIOTerminalOutputDAC(ioTerminal, bitMask, dacValue);
        }

        ////////////////////////////////////////////////////////////////////
        //SystemSetup container data accessors & helpers
        ////////////////////////////////////////////////////////////////////
        [Obsolete("Deprecated member property. Please use the SystemSetupContainer property instead", false)]
        public SystemSetup SystemSetup
        {
            get { return _systemSetupContainer.CurrentSystemSetup; }
        }

        [Obsolete("Deprecated member property. Please use the SystemSetupContainer property instead", false)]
        public SystemSetupPicture SystemSetupPicture
        {
            get { return _systemSetupContainer.CurrentSystemSetupPicture; }
        }

        [Obsolete("Deprecated member method. Please use the SystemSetupContainer property instead", false)]
        public SystemSetupModifier InsertSystemSetup()
        {
            return _systemSetupContainer.Insert();
        }

        [Obsolete("Deprecated member method. Please use the SystemSetupContainer property instead", false)]
        public SystemSetupModifier InsertSystemSetup(UInt32 id)
        {
            return _systemSetupContainer.Insert(id);
        }

        [Obsolete("Deprecated member method. Please use the SystemSetupContainer property instead", false)]
        public SystemSetupModifier UpdateSystemSetup(SystemSetup systemSetup)
        {
            return _systemSetupContainer.Update(systemSetup);
        }

        [Obsolete("Deprecated member method. Please use the SystemSetupContainer property instead", false)]
        public Boolean DeleteSystemSetup(SystemSetup systemSetup)
        {
            return _systemSetupContainer.Delete(systemSetup);
        }

        [Obsolete("Deprecated member property. Please use the SystemSetupContainer property instead", false)]
        public ReadOnlyCollection<Timezone> TimeZones
        {
            get { return _systemSetupContainer.Timezones; }
        }

        [Obsolete("Deprecated member method. Please use the SystemSetupContainer property instead", false)]
        public Timezone FindTimezone(String name)
        {
            return _systemSetupContainer.FindTimezone(name);
        }

        [Obsolete("Deprecated member function. Please use the SegmentContainer property instead", false)]
        public bool IsAllSegmentDataAvailable()
        {
            return _segmentContainer.AllDataAvailable;
        }

        [Obsolete("Deprecated member function. Please use the SegmentContainer property instead", false)]
        public ReadOnlyCollection<Segment> Segments
        {
            get { return _segmentContainer.All; }
        }

        [Obsolete("Deprecated member function. Please use the SegmentContainer property instead", false)]
        public Segment FindSegment(uint segmentID)
        {
            return _segmentContainer.Find(segmentID);
        }

        [Obsolete("Deprecated member function. Please use the SegmentContainer property instead", false)]
        public SegmentModifier UpdateSegment(Segment segment)
        {
            return _segmentContainer.Update(segment);
        }

        [Obsolete("Deprecated member function. Please use the SegmentContainer property instead", false)]
        public SegmentModifier InsertSegment(UInt32 id)
        {
            return _segmentContainer.Insert(id);
        }

        [Obsolete("Deprecated member function. Please use the SegmentContainer property instead", false)]
        public SegmentModifier InsertSegment()
        {
            return _segmentContainer.Insert();
        }

        [Obsolete("Deprecated member function. Please use the SegmentContainer property instead", false)]
        public bool DeleteSegment(Segment segment)
        {
            return _segmentContainer.Delete(segment);
        }

        ////////////////////////////////////////////////////////////////////
        //Track solution container data accessors & helpers
        ////////////////////////////////////////////////////////////////////
        [Obsolete("Deprecated property. Please use TrackSolutionContainer property instead", false)]
        public TrackSolutionContainer TrackSolutions
        {
            get { return _trackSolutionContainer; }
        }

        public DecoderContainer DecoderContainer
        {
            get { return _decoderContainer; }
        }

        public DecoderPresetGroupContainer DecoderPresetGroupContainer
        {
            get { return _decoderPresetGroupContainer; }
        }

        public SystemSetupContainer SystemSetupContainer
        {
            get { return _systemSetupContainer; }
        }

        public TrackSolutionContainer TrackSolutionContainer
        {
            get { return _trackSolutionContainer; }
        }

        public LoopContainer LoopContainer
        {
            get { return _loopContainer; }
        }

        public CompetitorContainer CompetitorContainer
        {
            get { return _competitorContainer; }
        }

        public TransponderContainer TransponderContainer
        {
            get { return _transponderContainer; }
        }

        public TransponderGroupContainer TransponderGroupContainer
        {
            get { return _transponderGroupContainer; }
        }

        public IOTerminalContainer IOTerminalContainer
        {
            get { return _ioterminalContainer; }
        }

        public BeaconDownloadConfigContainer BeaconDownloadConfigContainer
        {
            get { return _beaconDownloadConfigContainer; }
        }

        public SegmentContainer SegmentContainer
        {
            get { return _segmentContainer; }
        }
    }
}
