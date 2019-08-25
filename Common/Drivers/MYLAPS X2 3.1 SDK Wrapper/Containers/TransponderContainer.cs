using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.Objects;
using MylapsSDK.NotifyHandlers;
using MylapsSDK.Exceptions;
using MylapsSDK.Containers.Generics;

namespace MylapsSDK.Containers
{
    public class TransponderContainer : AbstractSortedGenericWithModifierContainer<Transponder, UInt32, TransponderModifier, MTA> 
    {
        internal TransponderContainer(MTA mtaData)
            : base(mtaData, mtaData.NativeHandle, Transponder.FromNativePointerArray, true)
        {
        }

        protected override UInt32 GetKeyOfObject(Transponder obj)
        {
            return obj.ID;
        }

        protected override void RegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_transponder(base.NativeHandle, base.DefaultNativeNotifyMultiObjectDelegate);
        }

        protected override void UnRegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_transponder(base.NativeHandle, null);
        }

        protected override TransponderModifier NewModifier(IntPtr nativePointer)
        {
            return new TransponderModifier(nativePointer, base.Handle);
        }

        protected override IntPtr NativeUpdate(uint id)
        {
            return NativeMethods.mta_transponder_update(base.NativeHandle, id);
        }

        protected override IntPtr NativeInsert(uint id)
        {
            return NativeMethods.mta_transponder_insert(base.NativeHandle, id);
        }

        public TransponderModifier Insert(UInt32 NewID)
        {
            return base.InternalInsert(NewID);
        }

        protected override bool NativeDelete(uint id)
        {
            return NativeMethods.mta_transponder_delete(base.NativeHandle, id);
        }
    }
}
