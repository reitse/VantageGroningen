using System;
using System.Collections.Generic;
using MylapsSDK.Objects;
using MylapsSDK.MylapsSDKLibrary;

namespace MylapsSDK.Containers
{
    public class LoopStatusContainer : AbstractSingleEventDataContainer<LoopStatus>
    {
        private Dictionary<UInt32, LoopStatus> _loopStatuses = new Dictionary<UInt32, LoopStatus>();
        private pfNotifyLoopStatus _pfLoopStatusNotifier;

        internal LoopStatusContainer(EventData eventData)
            : base(eventData)
        { }

        protected override LoopStatus FromNativePointer(IntPtr nativePointer, EventData eventData)
        {
            return new LoopStatus(nativePointer, eventData);
        }

        protected override void RegisterNotifyFunction(EventData eventData)
        {
            // Keep a reference to this object or else it will be deleted by the GC
            _pfLoopStatusNotifier = base.HandleNotify;

            NativeMethods.mta_notify_loopstatus(eventData.Handle, _pfLoopStatusNotifier);
        }

        public LoopStatus LatestForLoop(Loop loop)
        {
            LoopStatus latest;
            _loopStatuses.TryGetValue(loop.ID, out latest);
            return latest;
        }

        protected override void Insert(LoopStatus loopStatus)
        {
            _loopStatuses[loopStatus.LoopID] = loopStatus;
        }

        protected override void Clear()
        {
            _loopStatuses.Clear();
        }
    }
}
