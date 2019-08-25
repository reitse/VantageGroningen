using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MylapsSDK.Objects;
using MylapsSDK.MylapsSDKLibrary;

namespace MylapsSDK.Containers
{
    public class BeaconDownloadStatusContainer : AbstractSortedGenericContainer<BeaconDownloadStatus, UInt32, EventData>
    {
        internal BeaconDownloadStatusContainer(EventData handleWrapper, bool cacheObjects) :
            base(handleWrapper, handleWrapper.NativeHandle, BeaconDownloadStatus.FromNativePointerArray, cacheObjects)
        {
        }

        protected override UInt32 GetKeyOfObject(BeaconDownloadStatus obj)
        {
            return obj.ID;
        }

        protected override void RegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_beacondownloadstatus(base.NativeHandle, base.DefaultNativeNotifyMultiObjectDelegate);
        }

        protected override void UnRegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_beacondownloadstatus(base.NativeHandle, null);
        }
    }
}
