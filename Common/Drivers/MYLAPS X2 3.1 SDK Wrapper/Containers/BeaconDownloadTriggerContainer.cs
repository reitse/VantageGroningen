using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MylapsSDK.Objects;
using MylapsSDK.MylapsSDKLibrary;

namespace MylapsSDK.Containers
{
    public class BeaconDownloadTriggerContainer : AbstractSortedGenericContainer<BeaconDownloadTrigger, UInt32, EventData>
    {
        internal BeaconDownloadTriggerContainer(EventData handleWrapper, bool cacheObjects) :
            base(handleWrapper, handleWrapper.NativeHandle, BeaconDownloadTrigger.FromNativePointerArray, cacheObjects)
        {
        }

        protected override UInt32 GetKeyOfObject(BeaconDownloadTrigger obj)
        {
            return obj.ID;
        }

        protected override void RegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_beacondownloadtrigger(base.NativeHandle, base.DefaultNativeNotifyMultiObjectDelegate);
        }

        protected override void UnRegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_beacondownloadtrigger(base.NativeHandle, null);
        }
    }
}
