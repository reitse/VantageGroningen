using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MylapsSDK.Objects;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.Containers.generics;

namespace MylapsSDK.Containers
{
    public class PassingFirstContactContainer : AbstractSortedSingleObjectContainer<PassingFirstContact, Int32, EventData>
    {
        internal PassingFirstContactContainer(EventData handleWrapper, bool cacheObjects)
            : base(handleWrapper, handleWrapper.NativeHandle, PassingFirstContact.FromNativePointer, cacheObjects)
        {
        }

        protected override Int32 GetKeyOfObject(PassingFirstContact obj)
        {
            return obj.NativePointer.ToInt32();
        }

        protected override void RegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_passingfirstcontact(base.NativeHandle, base.NativeNotifySingleObjectDelegate);
        }

        protected override void UnRegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_passingfirstcontact(base.NativeHandle, null);
        }
    }
}
