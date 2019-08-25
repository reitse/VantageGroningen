using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MylapsSDK.NotifyHandlers;
using MylapsSDK.MylapsSDKLibrary;
using MylapsSDK.Objects;

namespace MylapsSDK.Containers
{
    public class DecoderContainer : AbstractSortedGenericContainer<Decoder, Int64, MTA> 
    {
        internal DecoderContainer(MTA mtaData)
            : base(mtaData, mtaData.NativeHandle, Decoder.FromNativePointerArray, true)
        {
        }

        protected override Int64 GetKeyOfObject(Decoder obj)
        {
            return obj.ID;
        }

        protected override void RegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_decoder(base.NativeHandle, base.DefaultNativeNotifyMultiObjectDelegate);
        }

        protected override void UnRegisterNotifyDelegate()
        {
            NativeMethods.mta_notify_decoder(base.NativeHandle, null);
        }

        public void ConnectDecoder(string ip, UInt32 port, IOTerminal ioTerminal)
        {
            NativeMethods.mta_decoder_connect_manual(base.NativeHandle, ip, port, UInt32.MaxValue, ioTerminal.ID);
        }

        public void ConnectDecoder(Decoder decoder, IOTerminal ioTerminal)
        {
            NativeMethods.mta_decoder_connect(base.NativeHandle, decoder.ID, UInt32.MaxValue, ioTerminal.ID);
        }

        public void ConnectDecoder(string ip, UInt32 port, Loop loop)
        {
            NativeMethods.mta_decoder_connect_manual(base.NativeHandle, ip, port, loop.ID, UInt32.MaxValue);
        }

        public void ConnectDecoder(Decoder decoder, Loop loop)
        {
            NativeMethods.mta_decoder_connect(base.NativeHandle, decoder.ID, loop.ID, UInt32.MaxValue);
        }

        public void ConnectDecoder(Decoder decoder, Loop loop, IOTerminal ioTerminal)
        {
            NativeMethods.mta_decoder_connect(base.NativeHandle, decoder.ID, loop.ID, ioTerminal.ID);
        }

        public void ConnectDecoder(string ip, UInt32 port, Loop loop, IOTerminal ioTerminal)
        {
            NativeMethods.mta_decoder_connect_manual(base.NativeHandle, ip, port, loop.ID, ioTerminal.ID);
        }

        public void DisconnectDecoder(Decoder decoder)
        {
            NativeMethods.mta_decoder_disconnect(base.NativeHandle, decoder.ID);
        }

        public void IdentifyDecoder(Decoder decoder)
        {
            NativeMethods.mta_decoder_identify(base.NativeHandle, decoder.ID);
        }

        public void SetPresetGroupID(Decoder decoder, DecoderPresetGroup decoderPresetGroup)
        {
            NativeMethods.mta_decoder_set_decoderpresetgroupid(base.NativeHandle, decoder.ID, decoderPresetGroup.ID);
        }
    }
}
