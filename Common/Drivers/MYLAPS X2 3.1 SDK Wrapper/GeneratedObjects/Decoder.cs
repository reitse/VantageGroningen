namespace MylapsSDK.Objects
{
    public enum DEVICEIDENTIFIERS {
        DEVICE_TYPE4_TRANX = 0x30, // Type-4 TranX decoder.
        DEVICE_TYPE4_TRANX_PRO = 0x31, // Type-4 TranX-pro decoder.
        DEVICE_TYPE4_TRANX_PRO_AUX = 0x32, // Type-4 TranX-pro aux decoder.
        DEVICE_TYPE4_MOTORIZED = 0x33, // Type-4 motorized club loop decoder.
        DEVICE_TYPE4_ACTIVE = 0x34 // Type-4 motorized club active decoder.
    }
    public enum DECODERSTATUSFLAGS {
        dsfOffline = 0, // The decoder is offline.
        dsfUnused, // The decoder available on the LAN but unused.
        dsfConnected, // The decoder is on-line.
        dsfSyncOK, // The decoder synchronization is OK.
        dsfActivity // There's activity on this decoder (passings).
    }
    public enum DECODERFLAGS {
        dfNetworkStatic = 3, // The network settings are static (if not set it is DHCP/APIPA).
        dfVersionMismatch, // The version of the decoder doesn't match the version of the MTA (firmware upgrade required).
        dfSendPassingInfo, // The passing-algorithm information will be sent by the decoder.
        dfUpdatingFirmware, // The decoder is updating the firmware.
        dfResendingData, // The decoder is resending data (e.g. passings).
        dfReadOnly, // The decoder settings are read only.
        dfTemperatureHighError, // Decoder is too hot (e.g. more than 60C).
        dfNoiseHighError, // Noise is too high (e.g. larger than -65 dBm).
        dfGPSLostWarning, // GPS time is set, but the signal is lost.
        dfGPSLostError, // GPS time is set, but the signal is lost for more than 5 minutes.
        dfNTPLostWarning, // NTP time is set, but the NTP server is lost.
        dfNTPLostError, // NTP time is set, but the NTP server is lost for more than 5 minutes.
        dfLoopTriggerError, // One or more loop-trigger are lost.
        dfClockDriftError, // Decoder clock is drifting.
        dfSyncPulseWarning, // Decoder sync pulse is missing.
        dfClockDifferenceWarning, // There is a difference between the decoder clock and the clock of the MTA.
        dfReconnecting, // The system is trying to reconnect to the decoder.
        dfLoopTriggerWarning, // One or more loop-triggers have low signal strength (e.g. smaller than -70 dBm).
        dfFirmwareUpdateAvailable, // There is a firmware update available for this decoder.
        dfMultipleMainServersWarning // There are multiple X2 Servers connected decoder that are in main mode.
    }
    public enum AUXEDGESETTING {
        aeNone, // No edge will trigger (channel disabled).
        aeRising, // Positive (rising) edge will trigger.
        aeFalling, // Negative (falling) edge will trigger.
        aeAny // Any edge (positive or negative) will trigger.
    }
    public enum ANALOGINMODE {
        amVoltage = 0, // Measure in voltage mode.
        amCurrent // Measure in current mode.
    }
    public enum ANALOGINGRANULARITY {
        agOff = 0, // Off.
        agGridStep2, // Grid with a step size of 2.
        agGridStep4, // Grid with a step size of 4.
        agGridStep8, // Grid with a step size of 8.
        agGridStep16, // Grid with a step size of 16.
        agGridStep32, // Grid with a step size of 32.
        agGridStep64, // Grid with a step size of 64.
        agGridStep128 // Grid with a step of size 128.
    }
    public enum NTPSOURCE {
        nsGPS = 0, // Decoder is using the GPS as NTP source.
        nsRTC = 1 // Decoder is using the local RTC as NTP source.
    }

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// A decoder connected to a loop.
    internal struct DecoderStruct
    {
        public long id; //  The unique identifier of the decoder (MAC address).
        public uint systemsetupid; //  The unique ID of the system-setup this decoder belongs to.
        public uint loopid; //  The ID of the loop that is connected to the decoder (UINT32_MAX if not used).
        public uint ioterminalid; //  The ID of the I/O terminal that is connected to the decoder (UINT32_MAX if not used).
        public uint type; //  The device type (see defines above).
        public uint version; //  The firmware version.
        public uint connectionip; //  The IP address of the connection.
        public uint ipaddress; //  The IP address.
        public uint networkmask; //  The network subnet mask.
        public uint ntpserver; //  The NTP server address.
        public uint flags; //  Misc. flags of the decoder (on-line, sync-ok, active).
        public int latitude; //  The latitude position (in 1e-7 units, signed).
        public int longitude; //  The longitude position (in 1e-7 units, signed).
        public ushort squelch; //  The squelch setting of the decoder.
        public ushort gatetime; //  The gate-time in milliseconds.
        public ushort connectionport; //  The port number of the connection.
        public ushort toggle1period; //  The toggle period for toggle source 1 in milliseconds (only available for auxiliary decoders).
        public ushort toggle2period; //  The toggle period for toggle source 2 in milliseconds (only available for auxiliary decoders).
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.U2)]
        public ushort[] inputholdoff; //  The input hold-off in milliseconds (channel 0-7).
        public byte startofsecond; //  The start of second input channel (used for sync-pulse enhanced NTP synchronization).
        public byte syncmethod; //  The used sync method, specified by the loop or I/O terminal. (See #SYNCMETHOD, can be GPS or NTP)
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.U1)]
        public byte[] ioflags; //  The input/output flags (channel 0-7).
        public byte analoginmode; //  The analog in mode.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 32)]
        public byte[] name; //  Name of this device (e.g. TranX). The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 2, ArraySubType = System.Runtime.InteropServices.UnmanagedType.U1)]
        public byte[] analogingranularity; //  The granularity of the analog inputs.
        public byte ntpsource; //  The NTP server clock source.
    }
#pragma warning restore 0649

/// <summary>
/// Information about Decoder
/// </summary>
/// <remarks>
/// A decoder connected to a loop.
/// </remarks>
public partial class Decoder{
    private readonly System.IntPtr _nativePointer;
    private readonly DecoderStruct _data;

    private readonly string _name;

    private MTA _handleWrapper;

    internal Decoder(System.IntPtr nativePointer, MTA context)
    {
        _nativePointer = nativePointer;
        _data = (DecoderStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(DecoderStruct));

        _handleWrapper = context;
        _name = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.name);
    }
	
	internal static Decoder FromNativePointer(
        System.IntPtr pointerToNativeStruct, MTA context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new Decoder(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<Decoder> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, MTA context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<Decoder>(
            System.Array.ConvertAll<System.IntPtr,Decoder>(ptrArray,
                ptr => new Decoder(ptr, context)));
    }

    internal System.IntPtr NativePointer
    {
        get { return _nativePointer; }
    }

    internal MTA Context
    {
        get { return _handleWrapper; }
    }

    ///<summary>
    ///The unique identifier of the decoder (MAC address).
    ///</summary>
	public long ID
    {
        get { return (long) _data.id; }
    }
    ///<summary>
    ///The unique ID of the system-setup this decoder belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The ID of the loop that is connected to the decoder (UINT32_MAX if not used).
    ///</summary>
	public uint LoopID
    {
        get { return (uint) _data.loopid; }
    }
    ///<summary>
    ///The ID of the I/O terminal that is connected to the decoder (UINT32_MAX if not used).
    ///</summary>
	public uint IOTerminalID
    {
        get { return (uint) _data.ioterminalid; }
    }
    ///<summary>
    ///The device type (see defines above).
    ///</summary>
	public uint Type
    {
        get { return (uint) _data.type; }
    }
    ///<summary>
    ///The firmware version.
    ///</summary>
	public uint Version
    {
        get { return (uint) _data.version; }
    }
    ///<summary>
    ///The IP address of the connection.
    ///</summary>
	public uint ConnectionIP
    {
        get { return (uint) _data.connectionip; }
    }
    ///<summary>
    ///The IP address.
    ///</summary>
	public uint IPAddress
    {
        get { return (uint) _data.ipaddress; }
    }
    ///<summary>
    ///The network subnet mask.
    ///</summary>
	public uint NetworkMask
    {
        get { return (uint) _data.networkmask; }
    }
    ///<summary>
    ///The NTP server address.
    ///</summary>
	public uint NTPServer
    {
        get { return (uint) _data.ntpserver; }
    }
    ///<summary>
    ///The latitude position (in 1e-7 units, signed).
    ///</summary>
	public int Latitude
    {
        get { return (int) _data.latitude; }
    }
    ///<summary>
    ///The longitude position (in 1e-7 units, signed).
    ///</summary>
	public int Longitude
    {
        get { return (int) _data.longitude; }
    }
    ///<summary>
    ///The gate-time in milliseconds.
    ///</summary>
	public ushort GateTime
    {
        get { return (ushort) _data.gatetime; }
    }
    ///<summary>
    ///The port number of the connection.
    ///</summary>
	public ushort ConnectionPort
    {
        get { return (ushort) _data.connectionport; }
    }
    ///<summary>
    ///The toggle period for toggle source 1 in milliseconds (only available for auxiliary decoders).
    ///</summary>
	public ushort Toggle1Period
    {
        get { return (ushort) _data.toggle1period; }
    }
    ///<summary>
    ///The toggle period for toggle source 2 in milliseconds (only available for auxiliary decoders).
    ///</summary>
	public ushort Toggle2Period
    {
        get { return (ushort) _data.toggle2period; }
    }
    ///<summary>
    ///The start of second input channel (used for sync-pulse enhanced NTP synchronization).
    ///</summary>
	public byte StartOfSecond
    {
        get { return (byte) _data.startofsecond; }
    }
    ///<summary>
    ///The used sync method, specified by the loop or I/O terminal. (See #SYNCMETHOD, can be GPS or NTP)
    ///</summary>
	public byte SyncMethod
    {
        get { return (byte) _data.syncmethod; }
    }
    ///<summary>
    ///Name of this device (e.g. TranX). The character data is in UTF-8 encoding.
    ///</summary>
    public string Name
    {
        get { return _name; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The NTP server clock source.
    ///</summary>
	public byte NTPSource
    {
        get { return (byte) _data.ntpsource; }
    }

    ///<summary>
    ///Get the decoder status (see #DECODERSTATUSFLAGS).
    ///</summary>
    public byte GetStatus()
    {
        return (byte)(_data.flags & 0x07);

    }
    ///<summary>
    ///Is the decoder offline?
    ///</summary>
    public bool IsOffline()
    {
        return (_data.flags & 0x07) == (uint)DECODERSTATUSFLAGS.dsfOffline;

    }
    ///<summary>
    ///Is the decoder available on the LAN?
    ///</summary>
    public bool IsUnused()
    {
        return (_data.flags & 0x07) == (uint)DECODERSTATUSFLAGS.dsfUnused;

    }
    ///<summary>
    ///Is the decoder connected?
    ///</summary>
    public bool IsConnected()
    {
        return (_data.flags & 0x07) >= (uint)DECODERSTATUSFLAGS.dsfConnected;

    }
    ///<summary>
    ///Is the decoder connected?
    ///</summary>
    public bool IsSyncOk()
    {
        return (_data.flags & 0x07) >= (uint)DECODERSTATUSFLAGS.dsfSyncOK;

    }
    ///<summary>
    ///Is there activity on the decoder?
    ///</summary>
    public bool HasActivity()
    {
        return (_data.flags & 0x07) >= (uint)DECODERSTATUSFLAGS.dsfActivity;

    }
    ///<summary>
    ///Are the network settings static (if not set it is DHCP/APIPA)?
    ///</summary>
    public bool IsNetworkStatic()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) DECODERFLAGS.dfNetworkStatic);
    }
    ///<summary>
    ///Does the version of the decoder match the version of the MTA (firmware upgrade required)?
    ///</summary>
    public bool HasVersionMismatch()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) DECODERFLAGS.dfVersionMismatch);
    }
    ///<summary>
    ///Is the decoder firmware being updated?
    ///</summary>
    public bool IsUpdatingFirmware()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) DECODERFLAGS.dfUpdatingFirmware);
    }
    ///<summary>
    ///Is the decoder resending data?
    ///</summary>
    public bool IsResendingData()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) DECODERFLAGS.dfResendingData);
    }
    ///<summary>
    ///Are the decoder settings read only?
    ///</summary>
    public bool IsReadOnly()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) DECODERFLAGS.dfReadOnly);
    }
    ///<summary>
    ///Is the system trying to reconnect to the decoder.
    ///</summary>
    public bool IsReconnecting()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) DECODERFLAGS.dfReconnecting);
    }
    ///<summary>
    ///Get the squelch (in dBm).
    ///</summary>
    public double GetSquelch()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.ConvertRssi2Dbm(_data.squelch);

    }
    ///<summary>
    ///Is the decoder is too hot (e.g. more than 60C)?
    ///</summary>
    public bool IsTempHighError()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) DECODERFLAGS.dfTemperatureHighError);
    }
    ///<summary>
    ///Is noise is too high (e.g. larger than -65dBm)?
    ///</summary>
    public bool IsNoiseHighError()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) DECODERFLAGS.dfNoiseHighError);
    }
    ///<summary>
    ///Is GPS signal lost?
    ///</summary>
    public bool IsGpsLostWarning()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) DECODERFLAGS.dfGPSLostWarning);
    }
    ///<summary>
    ///Is GPS signal lost for more than 5 minutes?
    ///</summary>
    public bool IsGpsLostError()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) DECODERFLAGS.dfGPSLostError);
    }
    ///<summary>
    ///Is NTP lost warning?
    ///</summary>
    public bool IsNTPLostWarning()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) DECODERFLAGS.dfNTPLostWarning);
    }
    ///<summary>
    ///Is NTP server lost for more than 5 minutes?
    ///</summary>
    public bool IsNTPLostError()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) DECODERFLAGS.dfNTPLostError);
    }
    ///<summary>
    ///Is there a loop trigger error?
    ///</summary>
    public bool IsLoopTriggerError()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) DECODERFLAGS.dfLoopTriggerError);
    }
    ///<summary>
    ///One or more loop-triggers have low signal strength (e.g. smaller than -70 dBm)?
    ///</summary>
    public bool IsLoopTriggerWarning()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) DECODERFLAGS.dfLoopTriggerWarning);
    }
    ///<summary>
    ///Is there a clock drift error?
    ///</summary>
    public bool IsClockDriftError()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) DECODERFLAGS.dfClockDriftError);
    }
    ///<summary>
    ///Is there a sync pulse warning?
    ///</summary>
    public bool IsSyncPulseWarning()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) DECODERFLAGS.dfSyncPulseWarning);
    }
    ///<summary>
    ///Is there a difference between the decoder clock and the clock of the MTA?
    ///</summary>
    public bool IsClockDifferenceWarning()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) DECODERFLAGS.dfClockDifferenceWarning);
    }
    ///<summary>
    ///Is there a new firmware available for this decoder.
    ///</summary>
    public bool IsFirmwareUpdateAvailable()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) DECODERFLAGS.dfFirmwareUpdateAvailable);
    }
    ///<summary>
    ///Are there multiple X2 Servers connected decoder that are in main mode?
    ///</summary>
    public bool IsMultipleMainServersWarning()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) DECODERFLAGS.dfMultipleMainServersWarning);
    }
    ///<summary>
    ///The input hold-off in milliseconds (channel 0-7).
    ///</summary>
    public ushort GetInputHoldoff(byte channel)
    {
        return _data.inputholdoff[channel % 8];

    }
    ///<summary>
    ///The input edge (channel 0-7). See #AUXEDGESETTING.
    ///</summary>
    public byte GetInputEdge(byte channel)
    {
        return (byte)(_data.ioflags[channel % 8] & 0x0F);

    }
    ///<summary>
    ///Get the auxiliary outputs startup value (channel 0-7).
    ///</summary>
    public byte GetOutputStartupValue(byte channel)
    {
        return (byte)((_data.ioflags[channel % 8] >> 4) & 0x0F);

    }
    ///<summary>
    ///Get the analog in mode. See #ANALOGINMODE.
    ///</summary>
    public byte GetAnalogInMode(byte channel)
    {
        return (byte)((_data.analoginmode >> (channel & 0x03)) & 0x01);

    }
    ///<summary>
    ///Get the analog in granularity. See #ANALOGINGRANUALIRY.
    ///</summary>
    public byte GetAnalogInGranularity(byte channel)
    {
        return (byte)(_data.analogingranularity[((channel/2) % 2)] >> (((channel % 2) == 1) ? 4 : 0) & 0x0F);

    }


    
}

}