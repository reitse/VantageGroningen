using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MylapsSDK.Containers.Generics;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.Objects;

namespace MylapsSDK.Containers
{
    public class CompetitorContainer : AbstractSortedGenericWithModifierContainer<Competitor, string, CompetitorModifier, MTA> 
    {
        internal CompetitorContainer(MTA mtaData)
            : base(mtaData, mtaData.NativeHandle, Competitor.FromNativePointerArray, true)
        {
        }

        protected override string GetKeyOfObject(Competitor obj)
        {
            return obj.GUID;
        }

        protected override void RegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_competitor(base.NativeHandle, base.DefaultNativeNotifyMultiObjectDelegate);
        }

        protected override void UnRegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_competitor(base.NativeHandle, null);
        }

        protected override CompetitorModifier NewModifier(IntPtr nativePointer)
        {
            return new CompetitorModifier(nativePointer, base.Handle);
        }

        protected override IntPtr NativeUpdate(string GUID)
        {
            return NativeMethods.mta_competitor_update(base.NativeHandle, GUID);
        }

        protected override IntPtr NativeInsert(string GUID)
        {
            return NativeMethods.mta_competitor_insert(base.NativeHandle, GUID);
        }

        public CompetitorModifier Insert(string GUID)
        {
            return base.InternalInsert(GUID);
        }

        protected override bool NativeDelete(string GUID)
        {
            return NativeMethods.mta_competitor_delete(base.NativeHandle, GUID);
        }
    }
}
