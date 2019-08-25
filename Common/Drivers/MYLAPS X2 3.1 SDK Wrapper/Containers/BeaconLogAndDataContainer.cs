using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.Objects;

namespace MylapsSDK.Containers
{
    public class BeaconLogAndDataContainer
    {
        private readonly EventData _eventData;
        private readonly pfNotifyBeaconLog _pfBeaconLogNotifier;
        private Dictionary<UInt32, List<BeaconData>> _beaconDataMap = new Dictionary<UInt32, List<BeaconData>>();
        private Dictionary<UInt32, List<BeaconLog>> _beaconTriggerLogMap = new Dictionary<UInt32, List<BeaconLog>>();
        private Dictionary<UInt32, BeaconLog> _beaconLogMap = new Dictionary<UInt32, BeaconLog>();

        public Action<MDP_NOTIFY_TYPE, BeaconLog, List<BeaconData>, EventData> NotifyHandlers;

        internal BeaconLogAndDataContainer(EventData eventData)
        {
            // Keep a reference to these objects or else it will be deleted by the GC
            _eventData = eventData;
            _pfBeaconLogNotifier = HandleNotify;

            NativeMethods.mta_notify_beaconlog(eventData.NativeHandle, _pfBeaconLogNotifier);
        }

        private void HandleNotify(IntPtr eventDataHandle, MDP_NOTIFY_TYPE nType, IntPtr beaconlogPtr, IntPtr beacondataArray, uint count, IntPtr context)
        {
            var data = BeaconData.FromNativePointerArray(beacondataArray, count, _eventData);
            BeaconLog beaconLog = null;
            switch(nType)
            {
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_SELECT:
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_INSERT:
                    beaconLog = new BeaconLog(beaconlogPtr, _eventData);
                    Insert(beaconLog, data);
                    break;
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_CLEAR:
                    Clear();
                    break;
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_DELETE:
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_UPDATE:
                    /* not handled */
                    break;
            }
            if (NotifyHandlers != null)
                NotifyHandlers(nType, beaconLog, data, _eventData);
        }

        internal void Clear()
        {
            _beaconDataMap.Clear();
            _beaconTriggerLogMap.Clear();
            _beaconLogMap.Clear();
        }

        private void InsertIntoTriggerLogMap(BeaconLog beaconLog)
        {
            List<BeaconLog> logs;
            if (!_beaconTriggerLogMap.TryGetValue(beaconLog.TriggerID, out logs))
            {
                logs = new List<BeaconLog>();
                _beaconTriggerLogMap[beaconLog.TriggerID] = logs;
            }

            logs.Add(beaconLog);
        }

        private void Insert(BeaconLog beaconLog, List<BeaconData> data)
        {
            _beaconLogMap[beaconLog.ID] = beaconLog;
            _beaconDataMap[beaconLog.ID] = data;
            InsertIntoTriggerLogMap(beaconLog);
        }

        public Boolean RequestBeaconLogsForTrigger(BeaconDownloadTrigger trigger)
        {
            return NativeMethods.mta_beaconlog_request(_eventData.NativeHandle, trigger.ID);
        }

        public List<BeaconLog> BeaconLogsForTrigger(BeaconDownloadTrigger trigger)
        {
            List<BeaconLog> logs;
            if (_beaconTriggerLogMap.TryGetValue(trigger.ID, out logs))
                return logs;
            else
                return new List<BeaconLog>();
        }

        public List<BeaconData> BeaconDataForLog(BeaconLog log)
        {
            List<BeaconData> items;
            if (_beaconDataMap.TryGetValue(log.ID, out items))
                return items;
            else
                return new List<BeaconData>();
        }

        public BeaconLog Find(uint id)
        {
            BeaconLog log;
            _beaconLogMap.TryGetValue(id, out log);
            return log;
        }
    }
}
