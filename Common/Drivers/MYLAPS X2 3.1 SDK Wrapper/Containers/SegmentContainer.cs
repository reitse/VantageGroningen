using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MylapsSDK.Objects;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.Containers.Generics;

namespace MylapsSDK.Containers
{
    public class SegmentContainer : AbstractSortedGenericWithModifierContainer<Segment, UInt32, SegmentModifier, MTA> 
    {
        internal SegmentContainer(MTA mtaData)
            : base(mtaData, mtaData.NativeHandle, Segment.FromNativePointerArray, true)
        {
        }

        protected override UInt32 GetKeyOfObject(Segment obj)
        {
            return obj.ID;
        }

        protected override void RegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_segment(base.NativeHandle, base.DefaultNativeNotifyMultiObjectDelegate);
        }

        protected override void UnRegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_segment(base.NativeHandle, null);
        }

        protected override SegmentModifier NewModifier(IntPtr nativePointer)
        {
            return new SegmentModifier(nativePointer, base.Handle);
        }

        protected override IntPtr NativeUpdate(uint id)
        {
            return NativeMethods.mta_segment_update(base.NativeHandle, id);
        }

        protected override IntPtr NativeInsert(uint id)
        {
            return NativeMethods.mta_segment_insert(base.NativeHandle, id);
        }

        public SegmentModifier Insert()
        {
            return base.InternalInsert(UInt32.MaxValue);
        }

        public SegmentModifier Insert(UInt32 NewID)
        {
            return base.InternalInsert(NewID);
        }

        protected override bool NativeDelete(uint id)
        {
            return NativeMethods.mta_segment_delete(base.NativeHandle, id);
        }
    }
}
