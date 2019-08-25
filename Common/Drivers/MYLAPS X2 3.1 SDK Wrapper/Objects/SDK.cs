using System;
using System.Collections.Generic;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.Containers;
using MylapsSDK.NotifyHandlers;
using System.Collections.ObjectModel;
using MylapsSDK.Exceptions;

namespace MylapsSDK.Objects
{
    public class SDK : IDisposable
    {
        private bool _disposed;
        private IntPtr _nativeHandle = IntPtr.Zero;
        private readonly object _syncRoot = new object();

        /** keep a ref to this delegates or else it will be deleted by the GC */
        private readonly pfNotifyDefault _pfDefaultNotifier;
        private Action<SDK> _notifyMsgQueueHandlers;

        private readonly List<MTA> _mtaHandleWrappers = new List<MTA>();
        private AvailableApplianceContainer _availableApplianceContainer;

        /// <summary>
        /// Creates an instance of the SDK.
        /// </summary>
        /// <exception cref="MylapsException">
        /// Throws a MylapsException when it failes to allocate the SDK.
        /// A brief error description is available from MylapsMessage member.
        /// </exception>
        /// <param name="appName">
        /// A text string representing the name of you application
        /// </param>
        public static SDK CreateSDK(string appName)
        {
            var ptr = NativeMethods.mdp_sdk_alloc(appName, IntPtr.Zero);
            if (ptr == IntPtr.Zero)
                throw new MylapsException("failed to create a SDK instance");
            
            return new SDK(ptr);
        }

        private SDK(IntPtr nativeSDKHandle) 
        {
            _nativeHandle = nativeSDKHandle;
            _availableApplianceContainer = new AvailableApplianceContainer(this);

            _pfDefaultNotifier = NotifyProcessMsgQueue;
            NativeMethods.mdp_sdk_notify_messagequeue(_nativeHandle, _pfDefaultNotifier);
        }

        internal IntPtr NativeHandle
        {
            get { return _nativeHandle; }
        }

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            // Check to see if Dispose has already been called.
            if (!_disposed)
            {
                // Disposing equals true, dispose all managed and unmanaged resources.
                ClearContainers();
                ClearNotifyMsgQueueHandler();

                _mtaHandleWrappers.ForEach(mtahandleWrapper => {
                    mtahandleWrapper.Dispose();
                });

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.
                NativeMethods.mdp_sdk_dealloc(_nativeHandle);
                _nativeHandle = IntPtr.Zero;

                // Note disposing has been done.
                _disposed = true;
            }
        }

        ////////////////////////////////////////////////////////////////////
        //Notify methods
        ////////////////////////////////////////////////////////////////////
        private void NotifyProcessMsgQueue(IntPtr handle, IntPtr context)
        {
            lock (_syncRoot)
            {
                if (_notifyMsgQueueHandlers != null)
                {
                    _notifyMsgQueueHandlers(this);
                }
            }
        }

        public void SetNotifyMsgQueueHandler(Action<SDK> msgQueueHandler)
        {
            lock (_syncRoot)
            {
                _notifyMsgQueueHandlers = msgQueueHandler;
            }
        }

        public void ClearNotifyMsgQueueHandler()
        {
            lock (_syncRoot)
            {
                _notifyMsgQueueHandlers = null;
            }
        }

        [Obsolete("Deprecated member function. Please use AvailableApplianceContainer property instead", false)]
        public void AddNotifyApplianceHandler(Action<MDP_NOTIFY_TYPE, AvailableAppliance, SDK> applianceHandler)
        {
            _availableApplianceContainer.NotifyHandlers += applianceHandler;
        }

        [Obsolete("Deprecated member function. Please use AvailableApplianceContainer property instead", false)]
        public void RemoveNotifyApplianceHandler(Action<MDP_NOTIFY_TYPE, AvailableAppliance, SDK> applianceHandler)
        {
            _availableApplianceContainer.NotifyHandlers -= applianceHandler;
        }
        
        ////////////////////////////////////////////////////////////////////
        //Getters
        ////////////////////////////////////////////////////////////////////
        [Obsolete("Deprecated property. Please use AvailableApplianceContainer property instead", false)]
        public ReadOnlyCollection<AvailableAppliance> AvailableAppliances
        {
            get { return _availableApplianceContainer.All; }
        }

        ////////////////////////////////////////////////////////////////////
        //Creating And Destroying Appliances
        ////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Creates an appliance instance.
        /// </summary>
        /// <exception cref="MylapsException">
        /// Throws a MylapsException when it failes to create an instance.
        /// A brief error description is available from MylapsMessage member.
        /// </exception>
        public MTA CreateMTA() 
        {
            var result = new MTA(this);
            _mtaHandleWrappers.Add(result);
            return result;
        }

        /// <summary>
        /// Disconnects the Appliance, releases the delegates and clears all Data
        /// </summary>
        /// <exception cref="MylapsException">
        /// Throws a MylapsException when the appliance is not created with this SDK instance.
        /// A brief error description is available from MylapsMessage member.
        /// </exception>
        /// <param name="mta">
        /// the appliance should be created by this SDK instance
        /// </param>
        public void ClearMTA(MTA mta)
        {
            if (!_mtaHandleWrappers.Contains(mta))
                throw new MylapsException("Appliance " + mta.GetHostname() + " does not exist in this SDK");

            _mtaHandleWrappers.Remove(mta);
            mta.Dispose();
        }

        [Obsolete("Deprecated member method. Please use ClearMTA(MTA mta) property instead", false)]
        public void DisposeAppliance(MTA mta)
        {
            ClearMTA(mta);
        }

        ////////////////////////////////////////////////////////////////////
        //Message Queue
        ////////////////////////////////////////////////////////////////////

        /// <summary>
        /// This is the non-blocking message processing main function.
        /// </summary>
        /// <remarks>
        /// Must be called from within the current (thread) context.
        /// </remarks>
        public void ProcessMessageQueue() 
        {
            ProcessMessageQueue( false, TimeSpan.FromSeconds(0) );
        }

        /// <summary>
        /// This is the message processing main function. 
        /// Use a TimeSpan if you want to wait (block) for a specific time.
        /// </summary>
        /// <remarks>
        /// Must be called from within the current (thread) context.
        /// If param 'wait' is true, this call will block until a message is received or until
        /// max. timeout 'time' has expired
        /// </remarks>
        public void ProcessMessageQueue(bool wait, TimeSpan time)
        {
            if ( !_disposed )// Check if this object hasn't been disposed. Might be  deallocated in a concurrend program
            {
                var us = time.Ticks > 10 && wait ? time.Ticks / 10 : 0;
                NativeMethods.mdp_sdk_messagequeue_process(_nativeHandle, wait, us);
            }
        }

        public AvailableApplianceContainer AvailableApplianceContainer
        {
            get { return _availableApplianceContainer; }
        }

        private void ClearContainers()
        {
            _availableApplianceContainer.Clear();
        }
    }
}
