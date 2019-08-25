using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.Objects;

namespace MylapsSDK.Containers
{
    public class DriverInfoContainer : AbstractSortedGenericContainer<DriverInfo, UInt32, EventData>
    {
        private readonly Dictionary<UInt32, DriverInfo> _latestDriverInfos = new Dictionary<UInt32, DriverInfo>();

        internal DriverInfoContainer(EventData handleWrapper, bool cacheObjects)
            : base(handleWrapper, handleWrapper.NativeHandle, DriverInfo.FromNativePointerArray, cacheObjects)
        {
        }

        protected override UInt32 GetKeyOfObject(DriverInfo obj)
        {
            return obj.ID;
        }

        protected override void RegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_driverinfo(base.NativeHandle, base.DefaultNativeNotifyMultiObjectDelegate);
        }

        protected override void UnRegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_driverinfo(base.NativeHandle, null);
        }

        public DriverInfo LatestForTransponder(Transponder transponder)
        {
            DriverInfo latest;
            if (_latestDriverInfos.TryGetValue(transponder.ID, out latest))
                return latest;
            else
                return null;
        }

        protected override void HandleInsert(DriverInfo driverInfo)
        {
            _latestDriverInfos[driverInfo.TransponderID] = driverInfo;
        }

        protected override void HandleSelect(DriverInfo driverInfo)
        {
            _latestDriverInfos[driverInfo.TransponderID] = driverInfo;
        }

        protected override void HandleUpdate(DriverInfo driverInfo)
        {
            _latestDriverInfos[driverInfo.TransponderID] = driverInfo;
        }

        protected override void HandleDelete(DriverInfo driverInfo)
        {
            _latestDriverInfos.Remove(driverInfo.TransponderID);
        }

        protected override void ClearData()
        {
            base.ClearData();
            _latestDriverInfos.Clear();
        }
    }
}
