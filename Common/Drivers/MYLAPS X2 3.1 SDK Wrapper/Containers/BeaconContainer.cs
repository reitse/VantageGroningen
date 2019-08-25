
/*
 * This file is a part of the Mylaps Development Platform (MDP).
 * Copyright (C) 1999-2010 Track Timing B.V.
 * All rights reserved.
 *
 * This software is confidential and proprietary information of
 * Track Timing B.V. ("Confidential Information"). You shall not
 * disclose such Confidential Information and shall use it only in
 * accordance with the terms of the license agreement you entered
 * into with Track Timing.
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MylapsSDK.Objects;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.NotifyHandlers;
using MylapsSDK.Exceptions;
using MylapsSDK.Containers.Generics;

namespace MylapsSDK.Containers
{
    public class BeaconDownloadConfigContainer : AbstractSortedGenericWithModifierContainer<BeaconDownloadConfig, UInt32, BeaconDownloadConfigModifier, MTA> 
    {
        internal BeaconDownloadConfigContainer(MTA mtaData)
            : base(mtaData, mtaData.NativeHandle, BeaconDownloadConfig.FromNativePointerArray, true)
        {
        }

        protected override UInt32 GetKeyOfObject(BeaconDownloadConfig obj)
        {
            return obj.ID;
        }

        protected override void RegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_beacondownloadconfig(base.NativeHandle, base.DefaultNativeNotifyMultiObjectDelegate);
        }

        protected override void UnRegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_beacondownloadconfig(base.NativeHandle, null);
        }

        protected override BeaconDownloadConfigModifier NewModifier(IntPtr nativePointer)
        {
            return new BeaconDownloadConfigModifier(nativePointer, base.Handle);
        }

        protected override IntPtr NativeUpdate(uint id)
        {
            return NativeMethods.mta_beacondownloadconfig_update(base.NativeHandle, id);
        }

        protected override IntPtr NativeInsert(uint id)
        {
            return NativeMethods.mta_beacondownloadconfig_insert(base.NativeHandle);
        }

        protected override bool NativeDelete(uint id)
        {
            return NativeMethods.mta_beacondownloadconfig_delete(base.NativeHandle, id);
        }

        public BeaconDownloadConfigModifier Insert()
        {
            return InternalInsert(UInt32.MaxValue);
        }

        public Boolean CancelDownload() 
        {
            return NativeMethods.mta_beacondownload_cancel(base.NativeHandle);
        }

        public Boolean ManualDownload(BeaconDownloadConfig downloadConfig, Int64 UTCTime) 
        {
            return NativeMethods.mta_beacondownload_manual(base.NativeHandle, downloadConfig.ID, UTCTime);
        }
    }
}
