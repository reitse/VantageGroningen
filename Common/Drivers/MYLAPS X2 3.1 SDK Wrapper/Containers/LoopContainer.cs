using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MylapsSDK.Objects;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.NotifyHandlers;
using MylapsSDK.Containers.Generics;

namespace MylapsSDK.Containers
{
    public class LoopContainer : AbstractSortedGenericWithModifierContainer<Loop, UInt32, LoopModifier, MTA> 
    {
        internal LoopContainer(MTA mtaData)
            : base(mtaData, mtaData.NativeHandle, Loop.FromNativePointerArray, true)
        {
        }

        protected override UInt32 GetKeyOfObject(Loop obj)
        {
            return obj.ID;
        }

        protected override void RegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_loop(base.NativeHandle, base.DefaultNativeNotifyMultiObjectDelegate);
        }

        protected override void UnRegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_loop(base.NativeHandle, null);
        }

        protected override LoopModifier NewModifier(IntPtr nativePointer)
        {
            return new LoopModifier(nativePointer, base.Handle);
        }

        protected override IntPtr NativeUpdate(uint id)
        {
            return NativeMethods.mta_loop_update(base.NativeHandle, id);
        }

        protected override IntPtr NativeInsert(uint id)
        {
            return NativeMethods.mta_loop_insert(base.NativeHandle, id);
        }

        public LoopModifier Insert(UInt32 NewID)
        {
            return base.InternalInsert(NewID);
        }

        public LoopModifier Insert()
        {
            return InternalInsert(UInt32.MaxValue);
        }

        protected override bool NativeDelete(uint id)
        {
            return NativeMethods.mta_loop_delete(base.NativeHandle, id);
        }
    }
}
