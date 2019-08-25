using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MylapsSDK.Objects;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.NotifyHandlers;
using MylapsSDK.Containers.Generics;

namespace MylapsSDK.Containers
{
    public class PassingTriggerContainer : AbstractSortedGenericContainer<PassingTrigger, UInt32, EventData>
    {
        private SortedDictionary<UInt32, PassingTrigger> _latestPassingTriggers = new SortedDictionary<UInt32, PassingTrigger>();

        internal PassingTriggerContainer(EventData handleWrapper, bool cacheObjects) :
            base(handleWrapper, handleWrapper.NativeHandle, PassingTrigger.FromNativePointerArray, cacheObjects)
        {
        }

        protected override UInt32 GetKeyOfObject(PassingTrigger obj)
        {
            return obj.ID;
        }

        protected override void RegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_passingtrigger(base.NativeHandle, base.DefaultNativeNotifyMultiObjectDelegate);
        }

        protected override void UnRegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_passingtrigger(base.NativeHandle, null);
        }

        public PassingTrigger LatestForTransponder(Transponder transponder)
        {
            PassingTrigger latest;
            if (_latestPassingTriggers.TryGetValue(transponder.ID, out latest))
                return latest;
            else
                return null;
        }

        protected override void HandleInsert(PassingTrigger passing)
        {
            _latestPassingTriggers[passing.TransponderID] = passing;
        }

        protected override void HandleSelect(PassingTrigger passing)
        {
            _latestPassingTriggers[passing.TransponderID] = passing;
        }

        protected override void HandleUpdate(PassingTrigger passing)
        {
            _latestPassingTriggers[passing.TransponderID] = passing;
        }

        protected override void HandleDelete(PassingTrigger passing)
        {
            _latestPassingTriggers.Remove(passing.TransponderID);
        }

        protected override void ClearData()
        {
            base.ClearData();
            _latestPassingTriggers.Clear();
        }
    }
}

