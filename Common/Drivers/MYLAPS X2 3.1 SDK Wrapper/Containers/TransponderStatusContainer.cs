using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MylapsSDK.Objects;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.Containers.generics;

namespace MylapsSDK.Containers
{
    public class TransponderStatusContainer : AbstractSortedSingleObjectContainer<TransponderStatus, UInt32, EventData>
    {
        private Dictionary<UInt32, TransponderStatus> _statuses = new Dictionary<uint, TransponderStatus>();

        internal TransponderStatusContainer(EventData handleWrapper, bool cacheObjects)
            : base(handleWrapper, handleWrapper.NativeHandle, TransponderStatus.FromNativePointer, cacheObjects)
        {
        }

        protected override UInt32 GetKeyOfObject(TransponderStatus obj)
        {
            return obj.ID;
        }

        protected override void RegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_transponderstatus(base.NativeHandle, base.NativeNotifySingleObjectDelegate);
        }

        protected override void UnRegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_transponderstatus(base.NativeHandle, null);
        }

        protected override void HandleInsert(TransponderStatus status)
        {
            _statuses[status.TransponderID] = status;
        }

        protected override void HandleSelect(TransponderStatus status)
        {
            _statuses[status.TransponderID] = status;
        }

        protected override void HandleUpdate(TransponderStatus status)
        {
            _statuses[status.TransponderID] = status;
        }

        protected override void HandleDelete(TransponderStatus status)
        {
            _statuses.Remove(status.TransponderID);
        }

        protected override void ClearData()
        {
            base.ClearData();
            _statuses.Clear();
        }

        public TransponderStatus LatestForTransponder(Transponder transponder)
        {
            TransponderStatus latest;
            _statuses.TryGetValue(transponder.ID, out latest);
            return latest;
        }
    }
}
