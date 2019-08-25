using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MylapsSDK.Containers.generics;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.Objects;

namespace MylapsSDK.Containers
{
    public class AuxStatusContainer : AbstractSortedSingleObjectContainer<AuxStatus, UInt32, EventData>
    {
        private readonly SortedDictionary<UInt32, AuxStatus> _latestAuxStatuses = new SortedDictionary<UInt32, AuxStatus>();

        internal AuxStatusContainer(EventData handleWrapper, bool cacheObjects)
            : base(handleWrapper, handleWrapper.NativeHandle, AuxStatus.FromNativePointer, cacheObjects)
        {
        }

        protected override UInt32 GetKeyOfObject(AuxStatus obj)
        {
            return obj.ID;
        }

        protected override void RegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_auxstatus(base.NativeHandle, base.NativeNotifySingleObjectDelegate);
        }

        protected override void UnRegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_auxstatus(base.NativeHandle, null);
        }

        protected override void HandleInsert(AuxStatus auxStatus)
        {
            _latestAuxStatuses[auxStatus.IOTerminalID] = auxStatus;
        }

        protected override void HandleSelect(AuxStatus auxStatus)
        {
            _latestAuxStatuses[auxStatus.IOTerminalID] = auxStatus;
        }

        protected override void HandleUpdate(AuxStatus auxStatus)
        {
            _latestAuxStatuses[auxStatus.IOTerminalID] = auxStatus;
        }

        protected override void HandleDelete(AuxStatus auxStatus)
        {
            _latestAuxStatuses.Remove(auxStatus.IOTerminalID);
        }

        protected override void ClearData()
        {
            base.ClearData();
            _latestAuxStatuses.Clear();
        }

        public AuxStatus LatestForIOTerminal(IOTerminal ioTerminal)
        {
            AuxStatus latest;
            _latestAuxStatuses.TryGetValue(ioTerminal.ID, out latest);
            return latest;
        }
    }
}
