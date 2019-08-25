using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.Objects;

namespace MylapsSDK.Containers
{
    public class AuxEventContainer : AbstractSortedGenericContainer<AuxEvent, UInt32, EventData>
    {
        private readonly Dictionary<UInt32, AuxEvent> _latestAuxEvents = new Dictionary<UInt32, AuxEvent>();

        internal AuxEventContainer(EventData eventData, bool cacheObjects)
            : base(eventData, eventData.NativeHandle, AuxEvent.FromNativePointerArray, cacheObjects)
        {
        }

        protected override UInt32 GetKeyOfObject(AuxEvent obj)
        {
            return obj.ID;
        }

        protected override void RegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_auxevent(base.NativeHandle, base.DefaultNativeNotifyMultiObjectDelegate);
        }

        protected override void UnRegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_auxevent(base.NativeHandle, null);
        }

        public AuxEvent LatestForIOTerminal(IOTerminal ioTerminal)
        {
            AuxEvent latest;
            if (_latestAuxEvents.TryGetValue(ioTerminal.ID, out latest))
                return latest;
            else
                return null;
        }

        protected override void HandleInsert(AuxEvent auxEvent)
        {
            _latestAuxEvents[auxEvent.IOTerminalID] = auxEvent;
        }

        protected override void HandleSelect(AuxEvent auxEvent)
        {
            _latestAuxEvents[auxEvent.IOTerminalID] = auxEvent;
        }

        protected override void HandleUpdate(AuxEvent auxEvent)
        {
            _latestAuxEvents[auxEvent.IOTerminalID] = auxEvent;
        }

        protected override void HandleDelete(AuxEvent auxEvent)
        {
            _latestAuxEvents.Remove(auxEvent.IOTerminalID);
        }

        protected override void ClearData()
        {
            base.ClearData();
            _latestAuxEvents.Clear();
        }
    }
}
