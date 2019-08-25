using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MylapsSDK.Objects;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.Containers.generics;

namespace MylapsSDK.Containers
{
    public class AvailableApplianceContainer : AbstractSortedSingleObjectContainer<AvailableAppliance, Int64, SDK> 
    {
        internal AvailableApplianceContainer(SDK sdkHandleWrapper)
            : base(sdkHandleWrapper, sdkHandleWrapper.NativeHandle, AvailableAppliance.FromNativePointer, true)
        {
        }

        protected override Int64 GetKeyOfObject(AvailableAppliance obj)
        {
            return obj.MacAddress;
        }

        protected override void RegisterNotifyDelegate()
        {
            NativeMethods.mdp_sdk_notify_appliance(base.NativeHandle, base.NativeNotifySingleObjectDelegate);
        }

        protected override void UnRegisterNotifyDelegate()
        {
            NativeMethods.mdp_sdk_notify_appliance(base.NativeHandle, null);
        }
    }
}
