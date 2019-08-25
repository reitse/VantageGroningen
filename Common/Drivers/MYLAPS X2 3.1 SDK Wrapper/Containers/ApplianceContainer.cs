using System;
using System.Collections.Generic;
using System.Linq;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.Objects;
using MylapsSDK.NotifyHandlers;
using System.Collections.ObjectModel;
using MylapsSDK.Exceptions;

namespace MylapsSDK.Containers
{
    public class ApplianceContainer
    {
        private readonly MTA _mta;
        private readonly IntPtr _mtaHandle;
        private readonly List<Modifier> _modifiers = new List<Modifier>();

        /** keep a ref to this delegates or else it will be deleted by the GC */
        private readonly pfNotifyConnect _connectionNotifier;
        private readonly pfNotifyConnectionState _connectionStateNotifier;
        
        public OnNotifyConnectHandler NotifyConnectHandlers;
        public OnNotifyConnectionStateHandler NotifyConnectionStateHandlers;
        
        internal ApplianceContainer(MTA mta, IntPtr mtaHandle)
        {
            _mta = mta;
            _mtaHandle = mtaHandle;

            _connectionNotifier = NotifyConnect;
            _connectionStateNotifier = NotifyConnectionState;
            
            NativeMethods.mta_notify_connect(mtaHandle, _connectionNotifier);
            NativeMethods.mta_notify_connectionstate(mtaHandle, _connectionStateNotifier);
        }

        private void NotifyConnect(IntPtr handle, [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)] bool isConnected, IntPtr context)
        {
            if (NotifyConnectHandlers != null)
            {
                NotifyConnectHandlers(isConnected, _mta);
            }
        }

        private void NotifyConnectionState(IntPtr handle, uint connectionState, IntPtr context)
        {
            if (NotifyConnectionStateHandlers != null)
            {
                NotifyConnectionStateHandlers((CONNECTIONSTATE)connectionState, _mta);
            }
        }

        internal void MarkChangesAsApplied()
        {
            _modifiers.ForEach(m => m.MarkApplied());
        }

        internal void ClearChanges()
        {
            MarkChangesAsApplied();
            _modifiers.Clear();
        }

        public void ClearNotifiers()
        {
            NotifyConnectHandlers = null;
            NotifyConnectionStateHandlers = null;
        }
    }
}
