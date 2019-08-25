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
    public class DecoderPresetGroupContainer : AbstractSortedGenericContainer<DecoderPresetGroup, UInt32, MTA> 
    {
        internal DecoderPresetGroupContainer(MTA mtaData)
            : base(mtaData, mtaData.NativeHandle, DecoderPresetGroup.FromNativePointerArray, true)
        {
        }

        protected override UInt32 GetKeyOfObject(DecoderPresetGroup obj)
        {
            return obj.ID;
        }

        protected override void RegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_decoderpresetgroup(base.NativeHandle, base.DefaultNativeNotifyMultiObjectDelegate);
        }

        protected override void UnRegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_decoderpresetgroup(base.NativeHandle, null);
        }
    }
}
