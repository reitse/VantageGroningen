using System;
using MylapsSDK.Exceptions;
using MylapsSDK.Objects;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.Containers.Generics;

namespace MylapsSDK.Containers
{
    public class ManualEventContainer : AbstractSortedGenericWithModifierContainer<ManualEvent, UInt32, ManualEventModifier, EventData>
    {
        internal ManualEventContainer(EventData handleWrapper, bool cacheObjects) :
            base(handleWrapper, handleWrapper.NativeHandle, ManualEvent.FromNativePointerArray, cacheObjects)
        {
        }

        protected override UInt32 GetKeyOfObject(ManualEvent obj)
        {
            return obj.ID;
        }

        protected override void RegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_manualevent(base.NativeHandle, base.DefaultNativeNotifyMultiObjectDelegate);
        }

        protected override void UnRegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_manualevent(base.NativeHandle, null);
        }

        protected override ManualEventModifier NewModifier(IntPtr nativePointer)
        {
            return new ManualEventModifier(nativePointer, base.Handle);
        }

        protected override IntPtr NativeUpdate(uint id)
        {
            return IntPtr.Zero;
        }

        protected override IntPtr NativeInsert(uint id)
        {
            return NativeMethods.mta_manualevent_insert(base.NativeHandle);
        }

        protected override bool NativeDelete(uint id)
        {
            return false;
        }

        public ManualEventModifier Insert()
        {
            return InternalInsert(UInt32.MaxValue);
        }
    }
}
