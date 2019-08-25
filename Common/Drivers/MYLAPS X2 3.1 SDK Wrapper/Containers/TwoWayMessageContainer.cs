using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MylapsSDK.Exceptions;
using MylapsSDK.Objects;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.Containers.Generics;

namespace MylapsSDK.Containers
{
    public class TwoWayMessageContainer : AbstractSortedGenericWithModifierContainer<TwoWayMessage, UInt32, TwoWayMessageModifier, EventData>
    {
        private SortedDictionary<UInt32, TwoWayMessage> _latestTwoWayMessages = new SortedDictionary<uint, TwoWayMessage>();

        internal TwoWayMessageContainer(EventData handleWrapper, bool cacheObjects) :
            base(handleWrapper, handleWrapper.NativeHandle, TwoWayMessage.FromNativePointerArray, cacheObjects)
        {
        }

        protected override UInt32 GetKeyOfObject(TwoWayMessage obj)
        {
            return obj.ID;
        }

        protected override void RegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_twowaymessage(base.NativeHandle, base.DefaultNativeNotifyMultiObjectDelegate);
        }

        protected override void UnRegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_twowaymessage(base.NativeHandle, null);
        }

        protected override TwoWayMessageModifier NewModifier(IntPtr nativePointer)
        {
            return new TwoWayMessageModifier(nativePointer, base.Handle);
        }

        protected override IntPtr NativeUpdate(uint id)
        {
            return IntPtr.Zero;
        }

        protected override IntPtr NativeInsert(uint id)
        {
            return NativeMethods.mta_twowaymessage_insert(base.NativeHandle);
        }

        protected override bool NativeDelete(uint id)
        {
            return false;
        }

        public TwoWayMessageModifier Insert()
        {
            return InternalInsert(UInt32.MaxValue);
        }

        public TwoWayMessage LatestForTransponder(Transponder transponder)
        {
            TwoWayMessage latest;
            if (_latestTwoWayMessages.TryGetValue(transponder.ID, out latest))
                return latest;
            else
                return null;
        }

        protected override void HandleInsert(TwoWayMessage passing)
        {
            _latestTwoWayMessages.Add(passing.TransponderID, passing);
        }

        protected override void HandleSelect(TwoWayMessage passing)
        {
            _latestTwoWayMessages.Add(passing.TransponderID, passing);
        }

        protected override void HandleUpdate(TwoWayMessage passing)
        {
            _latestTwoWayMessages[passing.TransponderID] = passing;
        }

        protected override void HandleDelete(TwoWayMessage passing)
        {
            _latestTwoWayMessages.Remove(passing.TransponderID);
        }

        protected override void ClearData()
        {
            base.ClearData();
            _latestTwoWayMessages.Clear();
        }
    }
}
