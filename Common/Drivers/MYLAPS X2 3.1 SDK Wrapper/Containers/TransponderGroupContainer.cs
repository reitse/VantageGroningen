using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using MylapsSDK.Exceptions;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.NotifyHandlers;
using MylapsSDK.Objects;
using MylapsSDK.Containers.Generics;

namespace MylapsSDK.Containers
{
    public class TransponderGroupContainer : AbstractSortedGenericWithModifierContainer<TransponderGroup, UInt32, TransponderGroupModifier, MTA> 
    {
        internal TransponderGroupContainer(MTA mtaData)
            : base(mtaData, mtaData.NativeHandle, TransponderGroup.FromNativePointerArray, true)
        {
        }

        protected override UInt32 GetKeyOfObject(TransponderGroup obj)
        {
            return obj.ID;
        }

        protected override void RegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_transpondergroup(base.NativeHandle, base.DefaultNativeNotifyMultiObjectDelegate);
        }

        protected override void UnRegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_transpondergroup(base.NativeHandle, null);
        }

        protected override TransponderGroupModifier NewModifier(IntPtr nativePointer)
        {
            return new TransponderGroupModifier(nativePointer, base.Handle);
        }

        protected override IntPtr NativeUpdate(uint id)
        {
            return NativeMethods.mta_transpondergroup_update(base.NativeHandle, id);
        }

        protected override IntPtr NativeInsert(uint id)
        {
            return NativeMethods.mta_transpondergroup_insert(base.NativeHandle, id);
        }

        public TransponderGroupModifier Insert(UInt32 NewID)
        {
            return base.InternalInsert(NewID);
        }

        protected override bool NativeDelete(uint id)
        {
            return NativeMethods.mta_transpondergroup_delete(base.NativeHandle, id);
        }
    }

}
