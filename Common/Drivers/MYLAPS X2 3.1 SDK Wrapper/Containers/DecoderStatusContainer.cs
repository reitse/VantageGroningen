using System;
using System.Collections.Generic;
using MylapsSDK.Objects;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.Containers.generics;

namespace MylapsSDK.Containers
{
    public class DecoderStatusContainer : AbstractSortedSingleObjectContainer<DecoderStatus, UInt32, EventData>
    {
        private Dictionary<Int64, DecoderStatus> _decoderStatuses = new Dictionary<Int64, DecoderStatus>();

        internal DecoderStatusContainer(EventData handleWrapper, bool cacheObjects)
            : base(handleWrapper, handleWrapper.NativeHandle, DecoderStatus.FromNativePointer, cacheObjects)
        {
        }

        protected override UInt32 GetKeyOfObject(DecoderStatus obj)
        {
            return obj.ID;
        }

        protected override void RegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_decoderstatus(base.NativeHandle, base.NativeNotifySingleObjectDelegate);
        }

        protected override void UnRegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_decoderstatus(base.NativeHandle, null);
        }

        protected override void HandleInsert(DecoderStatus decoderStatus)
        {
            _decoderStatuses[decoderStatus.DecoderID] = decoderStatus;
        }

        protected override void HandleSelect(DecoderStatus decoderStatus)
        {
            _decoderStatuses[decoderStatus.DecoderID] = decoderStatus;
        }

        protected override void HandleUpdate(DecoderStatus decoderStatus)
        {
            _decoderStatuses[decoderStatus.DecoderID] = decoderStatus;
        }

        protected override void HandleDelete(DecoderStatus decoderStatus)
        {
            _decoderStatuses.Remove(decoderStatus.DecoderID);
        }

        protected override void ClearData()
        {
            base.ClearData();
            _decoderStatuses.Clear();
        }

        public DecoderStatus LatestForDecoder(Decoder decoder)
        {
            DecoderStatus latest;
            _decoderStatuses.TryGetValue(decoder.ID, out latest);
            return latest;
        }
    }
}
