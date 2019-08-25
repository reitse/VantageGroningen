using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.NotifyHandlers;
using MylapsSDK.Objects;
using MylapsSDK.Exceptions;
using MylapsSDK.Containers.Generics;

namespace MylapsSDK.Containers
{
    public class SystemSetupContainer : AbstractSortedGenericWithModifierContainer<SystemSetup, UInt32, SystemSetupModifier, MTA>
    {
        private SystemSetup _currentSystemSetup;
        private SystemSetupPicture _currentSystemSetupPicture;
        private readonly SortedDictionary<string, Timezone> _timezoneMap = new SortedDictionary<string, Timezone>();
        private readonly List<Timezone> _timezones = new List<Timezone>();

        /** keep a ref to this delegates or else it will be deleted by the GC */
        private NativeNotifySingleObjectDelegate _pfSystemSetupPictureNotifier;
        public Action<MDP_NOTIFY_TYPE, SystemSetupPicture, MTA> NotifySystemSetupPictureHandlers;

        internal SystemSetupContainer(MTA mtaData)
            : base(mtaData, mtaData.NativeHandle, SystemSetup.FromNativePointerArray, true)
        {
            _currentSystemSetup = null;
            _currentSystemSetupPicture = null;
        }

        protected override UInt32 GetKeyOfObject(SystemSetup obj)
        {
            return obj.ID;
        }

        protected override void RegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_systemsetup(base.NativeHandle, base.DefaultNativeNotifyMultiObjectDelegate);
            _pfSystemSetupPictureNotifier = NotifySystemSetupPicture;
            NativeMethods.mta_notify_systemsetuppicture(base.NativeHandle, _pfSystemSetupPictureNotifier);
        }

        protected override void UnRegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_systemsetup(base.NativeHandle, null);
            NativeMethods.mta_notify_systemsetuppicture(base.NativeHandle, null);
        }

        protected override void HandleSelect(SystemSetup systemSetup)
        {
            if (systemSetup.IsCurrent())
            {
                _currentSystemSetup = systemSetup;
                InitializeTimezones();
            }
        }

        protected override void HandleInsert(SystemSetup systemSetup)
        {
            if (systemSetup.IsCurrent())
            {
                _currentSystemSetup = systemSetup;
                InitializeTimezones();
            }
        }

        protected override void HandleUpdate(SystemSetup systemSetup)
        {
            if (systemSetup.IsCurrent())
            {
                _currentSystemSetup = systemSetup;
            }
        }

        protected override void HandleDelete(SystemSetup systemSetup)
        {
            if (systemSetup.IsCurrent())
            {
                _currentSystemSetup = null;
            }
        }

        protected override SystemSetupModifier NewModifier(IntPtr nativePointer)
        {
            return new SystemSetupModifier(nativePointer, base.Handle);
        }

        protected override IntPtr NativeUpdate(uint id)
        {
            return NativeMethods.mta_systemsetup_update(base.NativeHandle, id);
        }

        protected override IntPtr NativeInsert(uint id)
        {
            return NativeMethods.mta_systemsetup_insert(base.NativeHandle, id);
        }

        public SystemSetupModifier Insert()
        {
            return InternalInsert(UInt32.MaxValue);
        }

        public SystemSetupModifier Insert(UInt32 NewID)
        {
            return base.InternalInsert(NewID);
        }

        protected override bool NativeDelete(uint id)
        {
            return NativeMethods.mta_systemsetup_delete(base.NativeHandle, id);
        }

        public void NotifySystemSetupPicture(IntPtr handle, MDP_NOTIFY_TYPE nType, IntPtr systemsetuppicturePtr, IntPtr context)
        {
            switch (nType)
            {
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_SELECT:
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_UPDATE:
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_INSERT:
                    _currentSystemSetupPicture = new SystemSetupPicture(systemsetuppicturePtr, base.Handle);
                    break;

                case MDP_NOTIFY_TYPE.MDP_NOTIFY_DELETE:
                case MDP_NOTIFY_TYPE.MDP_NOTIFY_CLEAR:
                    _currentSystemSetupPicture = null;
                    break;
            }

            if (NotifySystemSetupPictureHandlers != null)
            {
                NotifySystemSetupPictureHandlers(nType, _currentSystemSetupPicture, base.Handle);
            }
        }

        [Obsolete("Deprecated member property. This property returns the current systemsetup. Please use the CurrentSystemSetup property instead", false)]
        public SystemSetup SystemSetup
        {
            get { return _currentSystemSetup; }
        }

        public SystemSetup CurrentSystemSetup
        {
            get { return _currentSystemSetup; }
        }

        public SystemSetupPicture CurrentSystemSetupPicture
        {
            get { return _currentSystemSetupPicture; }
        }

        private void InitializeTimezones()
        {
            NativeMethods.mta_timezone_get_head(base.NativeHandle);
            var p = NativeMethods.mta_timezone_get_head(base.NativeHandle);
            while (p != IntPtr.Zero) 
            {
                var timezone = new Timezone(p, base.Handle);
                _timezones.Add(timezone);
                _timezoneMap[timezone.Name] = timezone;
                p = NativeMethods.mta_timezone_get_next(base.NativeHandle);
	        }
        }

        public ReadOnlyCollection<Timezone> Timezones
        {
            get { return _timezones.AsReadOnly(); }
        }

        public Timezone FindTimezone(String name)
        {
            return _timezoneMap.ContainsKey(name) ? _timezoneMap[name] : null;
        }

        /** Clear SystemSetupContainer completely, notifying all oberservers if needed. */
        protected override void ClearData()
        {
            base.ClearData();
            _currentSystemSetup = null;
            _currentSystemSetupPicture = null;
            _timezoneMap.Clear();
            _timezones.Clear();
        }

        protected override void ClearNotifiers()
        {
            base.ClearNotifiers();
            NotifySystemSetupPictureHandlers = null;
        }
    }
}
