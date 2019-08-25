using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MylapsSDK.Containers.generics;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.Objects;

namespace MylapsSDK.Containers
{
    public class LoopTriggerContainer : AbstractSortedSingleObjectContainer<LoopTrigger, UInt32, EventData>
    {
        private readonly SortedDictionary<UInt32, LoopTrigger> _latestLoopTriggeres = new SortedDictionary<UInt32, LoopTrigger>();

        internal LoopTriggerContainer(EventData handleWrapper, bool cacheObjects)
            : base(handleWrapper, handleWrapper.NativeHandle, LoopTrigger.FromNativePointer, cacheObjects)
        {
        }

        protected override UInt32 GetKeyOfObject(LoopTrigger obj)
        {
            return obj.ID;
        }

        protected override void RegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_looptrigger(base.NativeHandle, base.NativeNotifySingleObjectDelegate);
        }

        protected override void UnRegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_looptrigger(base.NativeHandle, null);
        }

        protected override void HandleInsert(LoopTrigger loopTrigger)
        {
            _latestLoopTriggeres[loopTrigger.LoopID] = loopTrigger;
        }

        protected override void HandleSelect(LoopTrigger loopTrigger)
        {
            _latestLoopTriggeres[loopTrigger.LoopID] = loopTrigger;
        }

        protected override void HandleUpdate(LoopTrigger loopTrigger)
        {
            _latestLoopTriggeres[loopTrigger.LoopID] = loopTrigger;
        }

        protected override void HandleDelete(LoopTrigger loopTrigger)
        {
            _latestLoopTriggeres.Remove(loopTrigger.LoopID);
        }

        protected override void ClearData()
        {
            base.ClearData();
            _latestLoopTriggeres.Clear();
        }

        public LoopTrigger LatestForLoop(Loop loop)
        {
            LoopTrigger latest;
            _latestLoopTriggeres.TryGetValue(loop.ID, out latest);
            return latest;
        }
    }
}
