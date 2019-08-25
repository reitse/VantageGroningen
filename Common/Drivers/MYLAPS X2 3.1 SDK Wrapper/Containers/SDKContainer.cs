using System;
using System.Collections.Generic;
using System.Linq;
using MylapsSDK.Objects;
using MylapsSDK.NotifyHandlers;
using MylapsSDK.MylapsSDKLibrary;
using System.Collections.ObjectModel;

namespace MylapsSDK.Containers
{
    class SDKContainer
    {
        private readonly SDK _sdk;
        private readonly SortedDictionary<Int64, AvailableAppliance> _availableAppliances = new SortedDictionary<Int64, AvailableAppliance>();

        private readonly object _syncRoot = new object();

        /** keep a ref to this delegates or else it will be deleted by the GC */
        private readonly pfNotifyDefault _pfDefaultNotifier;
        private readonly pfNotifyAppliance _pfApplianceNotifier;

        private OnNotifyMsgQueueHandler _notifyMsgQueueHandlers;
        private OnNotifyApplianceHandler _notifyApplianceHandlers;
        
        public SDKContainer(SDK sdk, IntPtr sdkHandle)
        {
            _sdk = sdk;
            
            _pfDefaultNotifier = NotifyProcessMsgQueue;
            _pfApplianceNotifier = NotifyAppliance;

            NativeMethods.mdp_sdk_notify_messagequeue(sdkHandle, _pfDefaultNotifier);
            NativeMethods.mdp_sdk_notify_appliance(sdkHandle, _pfApplianceNotifier);
        }

        ////////////////////////////////////////////////////////////////////
        //Notify methods
        ////////////////////////////////////////////////////////////////////
        private void NotifyProcessMsgQueue(IntPtr handle, IntPtr context)
        {
            lock (_syncRoot)
            {
                if (_notifyMsgQueueHandlers != null)
                {
                    _notifyMsgQueueHandlers(_sdk);
                }
            }
        }

        private void NotifyAppliance(IntPtr handle, MDP_NOTIFY_TYPE nType, IntPtr appliancePtr, IntPtr context)
        {
            var availableAppliance = new AvailableAppliance(appliancePtr, null);
            
            switch (nType)
            {
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_SELECT:
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_INSERT:
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_UPDATE:
                    _availableAppliances[availableAppliance.MacAddress] = availableAppliance;
                    break;

                case MDP_NOTIFY_TYPE.MDP_NOTIFY_DELETE:
                    _availableAppliances.Remove(availableAppliance.MacAddress);
                    break;

                case MDP_NOTIFY_TYPE.MDP_NOTIFY_CLEAR:
                    _availableAppliances.Clear();
                    break;
            }

            if (_notifyApplianceHandlers != null)
            {
                _notifyApplianceHandlers(nType, availableAppliance, _sdk);
            }
        }

        public void SetNotifyMsgQueueHandler(OnNotifyMsgQueueHandler msgQueueHandler)
        {
            lock (_syncRoot)
            {
                _notifyMsgQueueHandlers = msgQueueHandler;
            }
        }

        public void ClearNotifyMsgQueueHandler()
        {
            lock (_syncRoot)
            {
                _notifyMsgQueueHandlers = null;
            }
        }

        public void AddNotifyApplianceHandler(OnNotifyApplianceHandler applianceHandler)
        {
            _notifyApplianceHandlers += applianceHandler;
        }

        public void RemoveNotifyApplianceHandler(OnNotifyApplianceHandler applianceHandler)
        {
            if (_notifyApplianceHandlers != null) _notifyApplianceHandlers -= applianceHandler;
        }

        ////////////////////////////////////////////////////////////////////
        //Getters
        ////////////////////////////////////////////////////////////////////
        public ReadOnlyCollection<AvailableAppliance> AvailableAppliances
        {
            get
            {
                return _availableAppliances.Values.ToList().AsReadOnly();
            }
        }

        /** Clear SDK Container, notifying all observers if needed. */
        public void ClearData()
        {
            _availableAppliances.Clear();
        }

        public void ClearNotifiers()
        {
            ClearNotifyMsgQueueHandler(); //synchronized method
            _notifyApplianceHandlers = null;
        }
    }
}
