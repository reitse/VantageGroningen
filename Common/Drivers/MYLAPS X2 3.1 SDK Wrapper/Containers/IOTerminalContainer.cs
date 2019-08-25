using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using MylapsSDK.NotifyHandlers;
using MylapsSDK.Objects;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.Exceptions;
using MylapsSDK.Containers.Generics;

namespace MylapsSDK.Containers
{
    public class IOTerminalContainer : AbstractSortedGenericWithModifierContainer<IOTerminal, UInt32, IOTerminalModifier, MTA> 
    {
        internal IOTerminalContainer(MTA mtaData)
            : base(mtaData, mtaData.NativeHandle, IOTerminal.FromNativePointerArray, true)
        {
        }

        protected override UInt32 GetKeyOfObject(IOTerminal obj)
        {
            return obj.ID;
        }

        protected override void RegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_ioterminal(base.NativeHandle, base.DefaultNativeNotifyMultiObjectDelegate);
        }

        protected override void UnRegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_ioterminal(base.NativeHandle, null);
        }

        protected override IOTerminalModifier NewModifier(IntPtr nativePointer)
        {
            return new IOTerminalModifier(nativePointer, base.Handle);
        }

        protected override IntPtr NativeUpdate(uint id)
        {
            return NativeMethods.mta_ioterminal_update(base.NativeHandle, id);
        }

        protected override IntPtr NativeInsert(uint id)
        {
            return NativeMethods.mta_ioterminal_insert(base.NativeHandle, id);
        }

        public IOTerminalModifier Insert()
        {
            return InternalInsert(UInt32.MaxValue);
        }

        public IOTerminalModifier Insert(UInt32 NewID)
        {
            return base.InternalInsert(NewID);
        }

        protected override bool NativeDelete(uint id)
        {
            return NativeMethods.mta_ioterminal_delete(base.NativeHandle, id);
        }

        public void SetIOTerminalOutputState(IOTerminal ioTerminal, Byte bitMask, AUX_OUTPUT_STATE outputState)
        {
            NativeMethods.mta_ioterminal_output_set_state(base.NativeHandle, ioTerminal.ID, bitMask, (UInt32)outputState);
        }

        public void SetIOTerminalOutputDAC(IOTerminal ioTerminal, Byte bitMask, UInt32 dacValue)
        {
            NativeMethods.mta_ioterminal_analogoutput_set_dac(base.NativeHandle, ioTerminal.ID, bitMask, dacValue);
        }
    }
}
