using System;

using MylapsSDK.Containers;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.NotifyHandlers;
using MylapsSDK.Objects;
using MylapsSDK.Utilities;
using System.Collections.Generic;
using MylapsSDK.Exceptions;

namespace MylapsSDK.Objects
{
    public class EventData : IDisposable
    {
        bool _disposed;
        private IntPtr _nativeHandle;
        private MTA _mtaHandleWrapper;
        
        /* Containers */
        private readonly BeaconLogAndDataContainer _beaconLogAndDataContainer;
        private readonly BeaconDownloadStatusContainer _beaconDownloadStatusContainer;
        private readonly BeaconDownloadTriggerContainer _beaconDownloadTriggerContainer;
        private readonly ManualEventContainer _manualEventContainer;
        private readonly PassingContainer _passingContainer;
        private readonly PassingTriggerContainer _passingTriggerContainer;
        private readonly PassingFirstContactContainer _passingFirstContactContainer;
        private readonly TwoWayMessageContainer _twoWayMessageContainer;
		private readonly DriverInfoContainer _driverInfoContainer;
        private readonly AuxEventContainer _auxEventContainer;
        private readonly AuxStatusContainer _auxStatusContainer;
        private readonly DecoderStatusContainer _decoderStatusContainer;
        private readonly TransponderStatusContainer _transponderStatusContainer;
		private readonly LoopTriggerContainer _loopTriggerContainer;

        internal static EventData CreateLive(MTA mta)
        {
            var nativeHandle = NativeMethods.mta_eventdata_handle_alloc_live(mta.NativeHandle, IntPtr.Zero);
            if (nativeHandle == IntPtr.Zero)
                throw new MylapsSDK.Exceptions.MylapsException("Unable to allocate new event data handle");
            return new EventData(mta, nativeHandle);
        }

        internal static EventData CreateLiveWithResend(MTA mta, DateTime beginTime)
        {
            if (beginTime.Kind != DateTimeKind.Utc)
                throw new MylapsException("Begin time for interval should always be given in UTC, not local or unspecified time.");
            var mtaNow = mta.GetUTCTime();
            var time = SDKHelperFunctions.DateTimeToTimestamp(beginTime);
            
            var nativeHandle = NativeMethods.mta_eventdata_handle_alloc_live_with_resend(mta.NativeHandle, time, IntPtr.Zero);
            if (nativeHandle == IntPtr.Zero)
                throw new MylapsSDK.Exceptions.MylapsException("Unable to allocate new event data handle");
            return new EventData(mta, nativeHandle);
        }

        internal static EventData CreateResend(MTA mta, DateTime beginTime, DateTime endTime)
        {
            if (beginTime.Kind != DateTimeKind.Utc)
                throw new MylapsException("Begin time for interval should always be given in UTC, not local or unspecified time.");
            if (endTime.Kind != DateTimeKind.Utc)
                throw new MylapsException("End time for interval should always be given in UTC, not local or unspecified time.");
            var begin = SDKHelperFunctions.DateTimeToTimestamp(beginTime);
            var end = SDKHelperFunctions.DateTimeToTimestamp(endTime);
            var nativeHandle = NativeMethods.mta_eventdata_handle_alloc_resend(mta.NativeHandle, begin, end, IntPtr.Zero);
            if (nativeHandle == IntPtr.Zero)
                throw new MylapsSDK.Exceptions.MylapsException("Unable to allocate new event data handle");
            return new EventData(mta, nativeHandle);
        }

        private EventData(MTA mta, IntPtr nativeHandle)
        {
            _nativeHandle = nativeHandle;
            _mtaHandleWrapper = mta;

            _beaconLogAndDataContainer = new BeaconLogAndDataContainer(this);

            _auxStatusContainer = new AuxStatusContainer(this, false);
            _decoderStatusContainer = new DecoderStatusContainer(this, false);
            _transponderStatusContainer = new TransponderStatusContainer(this, false);
            _loopTriggerContainer = new LoopTriggerContainer(this, false);
            _passingFirstContactContainer = new PassingFirstContactContainer(this, false);

            _beaconDownloadStatusContainer = new BeaconDownloadStatusContainer(this, false);
            _beaconDownloadTriggerContainer = new BeaconDownloadTriggerContainer(this, false);
            _manualEventContainer = new ManualEventContainer(this, false);
            _passingContainer = new PassingContainer(this, false);
            _passingTriggerContainer = new PassingTriggerContainer(this, false);
            _twoWayMessageContainer = new TwoWayMessageContainer(this, false);
            _driverInfoContainer = new DriverInfoContainer(this, false);
            _auxEventContainer = new AuxEventContainer(this, false);
        }

        public MTA MTA
        {
            get { return _mtaHandleWrapper; }
        }

        internal IntPtr NativeHandle
        {
            get { return _nativeHandle; }
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
                // Dispose managed resources.
                ClearContainers();

                RollbackChanges();
                NativeMethods.mta_eventdata_handle_dealloc(_mtaHandleWrapper.NativeHandle, _nativeHandle);
                _nativeHandle = IntPtr.Zero;
                _mtaHandleWrapper = null;

                // Note disposing has been done.
                _disposed = true;
            }
        }

        public bool SubscribeToEventData(MTAEVENTDATA eventData, uint requestLimit, bool cache)
        {
            switch (eventData)
            {
                case MTAEVENTDATA.mtaManualEvent:
                    _manualEventContainer.Cache = cache;
                    break;
                case MTAEVENTDATA.mtaPassing:
                    _passingContainer.Cache = cache;
                    break;
                case MTAEVENTDATA.mtaPassingTrigger:
                    _passingTriggerContainer.Cache = cache;
                    break;
                case MTAEVENTDATA.mtaTwoWayMessage:
                    _twoWayMessageContainer.Cache = cache;
                    break;
                case MTAEVENTDATA.mtaBeaconDownloadStatus:
                    _beaconDownloadStatusContainer.Cache = cache;
                    break;
                case MTAEVENTDATA.mtaBeaconDownloadTrigger:
                    _beaconDownloadTriggerContainer.Cache = cache;
                    break;
                case MTAEVENTDATA.mtaDriverInfo:
                    _driverInfoContainer.Cache = cache;
                    break;
                case MTAEVENTDATA.mtaBeaconLog:
                    /* stored in the same container and are always cached */
                    break;
                case MTAEVENTDATA.mtaAuxEvent:
                    /* allways cache */
                    _auxEventContainer.Cache = true;
                    break;
                case MTAEVENTDATA.mtaAuxStatus:
                    /* never stored. */
                    break;
                case MTAEVENTDATA.mtaDecoderStatus:
                    /* never stored. */
                    break;
                case MTAEVENTDATA.mtaPassingFirstContact:
                    /* never stored. */
                    break;
                case MTAEVENTDATA.mtaTransponderStatus:
                    /* never stored. */
                    break;
                default:
                    /* other events never cached */
                    break;
            }
            return NativeMethods.mta_eventdata_subscribe(_nativeHandle, eventData, requestLimit, false /* don't cache data in lib, let wrapper handle it */);
        }

        public void CommitChanges()
        {
            NativeMethods.mta_eventdata_changes_commit(_nativeHandle);
        }

        public void RollbackChanges()
        {
            NativeMethods.mta_eventdata_changes_rollback(_nativeHandle);
        }

        public bool UnsubscribeFromEventData(MTAEVENTDATA eventData)
        {
            return NativeMethods.mta_eventdata_unsubscribe(_nativeHandle, eventData);
        }

        [Obsolete("Deprecated member method. Please use ManualEventContainer property instead", false)]
        public ManualEventModifier InsertManualEvent()
        {
            return _manualEventContainer.Insert();
        }

        [Obsolete("Deprecated member method. Please use TwoWayMessageContainer property instead", false)]
		public TwoWayMessageModifier InsertTwoWayMessage()
        {
            return _twoWayMessageContainer.Insert();
        }

        [Obsolete("Deprecated member method. Please use BeaconLogAndDataContainer property instead", false)]
        public Boolean RequestBeaconLogAndData(BeaconDownloadTrigger trigger)
        {
            return _beaconLogAndDataContainer.RequestBeaconLogsForTrigger(trigger);
        }

        ////////////////////////////////////////////////////////////////////
        // Add and Remove handlers
        ////////////////////////////////////////////////////////////////////
        [Obsolete("Deprecated member method. Please use BeaconDownloadStatusContainer property instead", false)]
        public void AddNotifyBeaconDownloadStatusHandler(Action<MDP_NOTIFY_TYPE, List<BeaconDownloadStatus>, EventData> handler)
        {
            _beaconDownloadStatusContainer.NotifyHandlers += handler;
        }

        [Obsolete("Deprecated member method. Please use BeaconDownloadStatusContainer property instead", false)]
        public void RemoveNotifyBeaconDownloadStatusHandler(Action<MDP_NOTIFY_TYPE, List<BeaconDownloadStatus>, EventData> handler)
        {
            _beaconDownloadStatusContainer.NotifyHandlers -= handler;
        }

        [Obsolete("Deprecated member method. Please use ManualEventContainer property instead", false)]
        public void AddNotifyManualEventHandler(Action<MDP_NOTIFY_TYPE, List<ManualEvent>, EventData> handler)
        {
            _manualEventContainer.NotifyHandlers += handler;
        }

        [Obsolete("Deprecated member method. Please use ManualEventContainer property instead", false)]
        public void RemoveNotifyManualEventHandler(Action<MDP_NOTIFY_TYPE, List<ManualEvent>, EventData> handler)
        {
            _manualEventContainer.NotifyHandlers -= handler;
        }

        [Obsolete("Deprecated member method. Please use PassingContainer property instead", false)]
        public void AddNotifyPassingHandler(Action<MDP_NOTIFY_TYPE,List<Passing>,EventData> handler)
        {
            _passingContainer.NotifyHandlers += handler;
        }

        [Obsolete("Deprecated member method. Please use PassingContainer property instead", false)]
        public void RemoveNotifyPassingHandler(Action<MDP_NOTIFY_TYPE, List<Passing>, EventData> handler)
        {
            _passingContainer.NotifyHandlers -= handler;
        }

        [Obsolete("Deprecated member method. Please use PassingFirstContactContainer property instead", false)]
        public void AddNotifyPassingFirstContactHandler(Action<MDP_NOTIFY_TYPE, PassingFirstContact, EventData> handler)
        {
            _passingFirstContactContainer.NotifyHandlers += handler;
        }

        [Obsolete("Deprecated member method. Please use PassingFirstContactContainer property instead", false)]
        public void RemoveNotifyPassingFirstContactHandler(Action<MDP_NOTIFY_TYPE, PassingFirstContact, EventData> handler)
        {
            _passingFirstContactContainer.NotifyHandlers -= handler;
        }

        [Obsolete("Deprecated member method. Please use TwoWayMessageContainer property instead", false)]
        public void AddNotifyTwoWayMessageHandler(Action<MDP_NOTIFY_TYPE, List<TwoWayMessage>, EventData> handler)
        {
            _twoWayMessageContainer.NotifyHandlers += handler;
        }

        [Obsolete("Deprecated member method. Please use TwoWayMessageContainer property instead", false)]
        public void RemoveNotifyTwoWayMessageHandler(Action<MDP_NOTIFY_TYPE, List<TwoWayMessage>, EventData> handler)
        {
            _twoWayMessageContainer.NotifyHandlers -= handler;
        }

        [Obsolete("Deprecated member method. Please use DriverInfoContainer property instead", false)]
        public void AddNotifyDriverInfoHandler(Action<MDP_NOTIFY_TYPE, List<DriverInfo>, EventData> handler)
        {
            _driverInfoContainer.NotifyHandlers += handler;
        }

        [Obsolete("Deprecated member method. Please use DriverInfoContainer property instead", false)]
        public void RemoveNotifyDriverInfoHandler(Action<MDP_NOTIFY_TYPE, List<DriverInfo>, EventData> handler)
        {
            _driverInfoContainer.NotifyHandlers -= handler;
        }

        [Obsolete("Deprecated member method. Please use AuxEventContainer property instead", false)]
        public void AddNotifyAuxEventHandler(Action<MDP_NOTIFY_TYPE, List<AuxEvent>, EventData> handler)
        {
            _auxEventContainer.NotifyHandlers += handler;
        }

        [Obsolete("Deprecated member method. Please use AuxEventContainer property instead", false)]
        public void RemoveNotifyAuxEventHandler(Action<MDP_NOTIFY_TYPE, List<AuxEvent>, EventData> handler)
        {
            _auxEventContainer.NotifyHandlers -= handler;
        }

        [Obsolete("Deprecated member method. Please use AuxStatusContainer property instead", false)]
        public void AddNotifyAuxStatusHandler(Action<MDP_NOTIFY_TYPE, AuxStatus, EventData> handler)
        {
            _auxStatusContainer.NotifyHandlers += handler;
        }

        [Obsolete("Deprecated member method. Please use AuxStatusContainer property instead", false)]
        public void RemoveNotifyAuxStatusHandler(Action<MDP_NOTIFY_TYPE, AuxStatus, EventData> handler)
        {
            _auxStatusContainer.NotifyHandlers -= handler;
        }

        [Obsolete("Deprecated member method. Please use LoopTriggerContainer property instead", false)]
        public void AddNotifyLoopTriggerHandler(Action<MDP_NOTIFY_TYPE, LoopTrigger, EventData> handler)
        {
            _loopTriggerContainer.NotifyHandlers += handler;
        }

        [Obsolete("Deprecated member method. Please use LoopTriggerContainer property instead", false)]
        public void RemoveNotifyLoopTriggerHandler(Action<MDP_NOTIFY_TYPE, LoopTrigger, EventData> handler)
        {
            _loopTriggerContainer.NotifyHandlers -= handler;
        }

        [Obsolete("Deprecated member method. Please use DecoderStatusContainer property instead", false)]
        public void AddNotifyDecoderStatusHandler(Action<MDP_NOTIFY_TYPE, DecoderStatus, EventData> handler)
        {
            _decoderStatusContainer.NotifyHandlers += handler;
        }

        [Obsolete("Deprecated member method. Please use DecoderStatusContainer property instead", false)]
        public void RemoveNotifyDecoderStatusHandler(Action<MDP_NOTIFY_TYPE, DecoderStatus, EventData> handler)
        {
            _decoderStatusContainer.NotifyHandlers -= handler;
        }

        [Obsolete("Deprecated member method. Please use TransponderStatusContainer property instead", false)]
        public void AddNotifyTransponderStatusHandler(Action<MDP_NOTIFY_TYPE, TransponderStatus, EventData> handler)
        {
            _transponderStatusContainer.NotifyHandlers += handler;
        }

        [Obsolete("Deprecated member method. Please use TransponderStatusContainer property instead", false)]
        public void RemoveNotifyTransponderStatusHandler(Action<MDP_NOTIFY_TYPE, TransponderStatus, EventData> handler)
        {
            _transponderStatusContainer.NotifyHandlers -= handler;
        }

        [Obsolete("Deprecated member method. Please use BeaconDownloadTriggerContainer property instead", false)]
        public void AddNotifyBeaconDownloadTriggerHandler(Action<MDP_NOTIFY_TYPE, List<BeaconDownloadTrigger>, EventData> handler)
        {
            _beaconDownloadTriggerContainer.NotifyHandlers += handler;
        }

        [Obsolete("Deprecated member method. Please use BeaconDownloadTriggerContainer property instead", false)]
        public void RemoveNotifyBeaconDownloadTriggerHandler(Action<MDP_NOTIFY_TYPE, List<BeaconDownloadTrigger>, EventData> handler)
        {
            _beaconDownloadTriggerContainer.NotifyHandlers -= handler;
        }

        [Obsolete("Deprecated member method. Please use beaconLogAndDataContainer property instead", false)]
        public void AddNotifyBeaconLogHandler(Action<MDP_NOTIFY_TYPE, BeaconLog, List<BeaconData>, EventData> handler)
        {
            _beaconLogAndDataContainer.NotifyHandlers += handler;
        }

        [Obsolete("Deprecated member method. Please use beaconLogAndDataContainer property instead", false)]
        public void RemoveNotifyBeaconLogHandler(Action<MDP_NOTIFY_TYPE, BeaconLog, List<BeaconData>, EventData> handler)
        {
            _beaconLogAndDataContainer.NotifyHandlers -= handler;
        }

        ////////////////////////////////////////////////////////////////////
        //ManualEvent container data accessors & helpers
        ////////////////////////////////////////////////////////////////////
        public ManualEventContainer ManualEventContainer
        {
            get { return _manualEventContainer; }
        }

        ////////////////////////////////////////////////////////////////////
        //DriverInfo container data accessors & helpers
        ////////////////////////////////////////////////////////////////////
        public DriverInfoContainer DriverInfoContainer
        {
            get { return _driverInfoContainer; }
        }

        ////////////////////////////////////////////////////////////////////
        //AuxEvent container data accessors & helpers
        ////////////////////////////////////////////////////////////////////
        public AuxEventContainer AuxEventContainer
        {
            get { return _auxEventContainer; }
        }

        ////////////////////////////////////////////////////////////////////
        //AuxStatus container data accessors & helpers
        ////////////////////////////////////////////////////////////////////
        public AuxStatusContainer AuxStatusContainer
        {
            get { return _auxStatusContainer; }
        }

        ////////////////////////////////////////////////////////////////////
        //BeaconLogAndData container data accessors & helpers
        ////////////////////////////////////////////////////////////////////
        public BeaconLogAndDataContainer BeaconLogAndDataContainer
        {
            get { return _beaconLogAndDataContainer; }
        }

        ////////////////////////////////////////////////////////////////////
        //TwoWayMessage container data accessors & helpers
        ////////////////////////////////////////////////////////////////////
        public TwoWayMessageContainer TwoWayMessageContainer
        {
            get { return _twoWayMessageContainer; }
        }

        ////////////////////////////////////////////////////////////////////
        //BeaconDownloadStatus container data accessors & helpers
        ////////////////////////////////////////////////////////////////////
        public BeaconDownloadStatusContainer BeaconDownloadStatusContainer
        {
            get { return _beaconDownloadStatusContainer; }
        }

        ////////////////////////////////////////////////////////////////////
        //BeaconDownloadTrigger container data accessors & helpers
        ////////////////////////////////////////////////////////////////////
        public BeaconDownloadTriggerContainer BeaconDownloadTriggerContainer
        {
            get { return _beaconDownloadTriggerContainer; }
        }

        ////////////////////////////////////////////////////////////////////
        //TransponderStatus container data accessors & helpers
        ////////////////////////////////////////////////////////////////////
        public TransponderStatusContainer TransponderStatusContainer
        {
            get { return _transponderStatusContainer; }
        }

        ////////////////////////////////////////////////////////////////////
        //Passing container data accessors & helpers
        ////////////////////////////////////////////////////////////////////
        public PassingContainer PassingContainer
        {
            get { return _passingContainer; }
        }

        ////////////////////////////////////////////////////////////////////
        //Passing-trigger container data accessors & helpers
        ////////////////////////////////////////////////////////////////////
        public PassingTriggerContainer PassingTriggerContainer
        {
            get { return _passingTriggerContainer; }
        }

        ////////////////////////////////////////////////////////////////////
        //PassingFirstContact container data accessors & helpers
        ////////////////////////////////////////////////////////////////////
        public PassingFirstContactContainer PassingFirstContactContainer
        {
            get { return _passingFirstContactContainer; }
        }

        ////////////////////////////////////////////////////////////////////
        //DecoderStatus container data accessors & helpers
        ////////////////////////////////////////////////////////////////////
        public DecoderStatusContainer DecoderStatusContainer
        {
            get { return _decoderStatusContainer; }
        }

        ////////////////////////////////////////////////////////////////////
        //Loop trigger container data accessors & helpers
        ////////////////////////////////////////////////////////////////////
        public LoopTriggerContainer LoopTriggerContainer
        {
            get { return _loopTriggerContainer; }
        }

        private void ClearContainers()
        {
            _manualEventContainer.Clear();
            _passingContainer.Clear();
            _passingTriggerContainer.Clear();
            _passingFirstContactContainer.Clear();
            _twoWayMessageContainer.Clear();
            _driverInfoContainer.Clear();
            _decoderStatusContainer.Clear();
            _beaconLogAndDataContainer.Clear();
            _beaconDownloadStatusContainer.Clear();
            _beaconDownloadTriggerContainer.Clear();
            _auxEventContainer.Clear();
            _auxStatusContainer.Clear();
            _transponderStatusContainer.Clear();
            _loopTriggerContainer.Clear();
        }
    }
}

