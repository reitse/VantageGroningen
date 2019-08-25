using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MylapsSDK.Objects;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.NotifyHandlers;
using MylapsSDK.Containers.Generics;

namespace MylapsSDK.Containers
{
    public class PassingContainer : AbstractSortedGenericContainer<Passing, UInt32, EventData>
    {
        private SortedDictionary<UInt32, Passing> _latestPassings = new SortedDictionary<UInt32, Passing>();

        internal PassingContainer(EventData handleWrapper, bool cacheObjects) :
            base(handleWrapper, handleWrapper.NativeHandle, Passing.FromNativePointerArray, cacheObjects)
        {
        }

        protected override UInt32 GetKeyOfObject(Passing obj)
        {
            return obj.ID;
        }

        protected override void RegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_passing(base.NativeHandle, base.DefaultNativeNotifyMultiObjectDelegate);
        }

        protected override void UnRegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_passing(base.NativeHandle, null);
        }

        public Passing LatestForTransponder(Transponder transponder)
        {
            Passing latest;
            if (_latestPassings.TryGetValue(transponder.ID, out latest))
                return latest;
            else
                return null;
        }

        protected override void HandleInsert(Passing passing)
        {
            _latestPassings[passing.TransponderID] = passing;
        }

        protected override void HandleSelect(Passing passing)
        {
            _latestPassings[passing.TransponderID] = passing;
        }

        protected override void HandleUpdate(Passing passing)
        {
            _latestPassings[passing.TransponderID] = passing;
        }

        protected override void HandleDelete(Passing passing)
        {
            _latestPassings.Remove(passing.TransponderID);
        }

        protected override void ClearData()
        {
            base.ClearData();
            _latestPassings.Clear();
        }
    }
}

