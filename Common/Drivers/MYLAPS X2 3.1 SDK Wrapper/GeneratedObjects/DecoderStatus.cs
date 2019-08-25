namespace MylapsSDK.Objects
{
    public enum DECODERSTATUSBITS {
        dsbResend = 0, // Is this decoder-status the result of a resend?
        dsbDecoderResend = 1 // Is this decoder-status the result of a decoder resend?
    }
    public enum DECODERSTATUSRECEIVERTYPE {
        dsrtTwoWay = 0, // The 2-way receiver.
        dsrtTranX = 1, // The TranX receiver?
        dsrtChipX = 2, // The ChipX receiver?
        dsrtNA = 7 // Receiver is not available.
    }

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// The current status of the decoder (e.g. voltage, temperature).
    internal struct DecoderStatusStruct
    {
        public long decoderid; //  The MAC address of the decoder for this decoder-status.
        public long utctime; //  The time the decoder-status was received (in UTC).
        public long timeofday; //  The UTC time corrected for the local timezone.
        public uint id; //  The unique id.
        public uint systemsetupid; //  The unique ID of the system-setup this decoder-status belongs to.
        public uint loopid; //  The loop ID for the decoder-status (UINT32_MAX if this decoder is not connected to a loop, e.g. auxiliary decoder).
        public uint ioterminalid; //  The I/O terminal ID for the decoder-status (UINT32_MAX if this decoder is not connected to an I/O terminal).
        public ushort flags; //  The decoder-status flag bits.
        public byte temperature; //  The temperature (in Celsius).
        public byte gps; //  GPS information.
        public byte voltage; //  The voltage of the backup battery in 1/10 volt.
        public byte noise_receiver0; //  The average noise level of the receiver 0 (in dBm). If the value is UINT8_MAX, the value is invalid.
        public byte noise_receiver1; //  The average noise level of receiver 1 (in dBm). If the value is UINT8_MAX, the value is invalid.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 1)]
        public byte[] padding; //  padding
    }
#pragma warning restore 0649

/// <summary>
/// Information about Decoder status
/// </summary>
/// <remarks>
/// The current status of the decoder (e.g. voltage, temperature).
/// </remarks>
public partial class DecoderStatus: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly DecoderStatusStruct _data;


    private EventData _handleWrapper;

    internal DecoderStatus(System.IntPtr nativePointer, EventData context)
    {
        _nativePointer = nativePointer;
        _data = (DecoderStatusStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(DecoderStatusStruct));

        _handleWrapper = context;
    }
	
	internal static DecoderStatus FromNativePointer(
        System.IntPtr pointerToNativeStruct, EventData context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new DecoderStatus(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<DecoderStatus> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, EventData context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<DecoderStatus>(
            System.Array.ConvertAll<System.IntPtr,DecoderStatus>(ptrArray,
                ptr => new DecoderStatus(ptr, context)));
    }

    internal System.IntPtr NativePointer
    {
        get { return _nativePointer; }
    }

    internal EventData Context
    {
        get { return _handleWrapper; }
    }

    ///<summary>
    ///The MAC address of the decoder for this decoder-status.
    ///</summary>
	public long DecoderID
    {
        get { return (long) _data.decoderid; }
    }
    ///<summary>
    ///The time the decoder-status was received (in UTC).
    ///</summary>
	public long UTCTime
    {
        get { return (long) _data.utctime; }
    }
    ///<summary>
    ///The UTC time corrected for the local timezone.
    ///</summary>
	public long TimeOfDay
    {
        get { return (long) _data.timeofday; }
    }
    ///<summary>
    ///The unique id.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The unique ID of the system-setup this decoder-status belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The loop ID for the decoder-status (UINT32_MAX if this decoder is not connected to a loop, e.g. auxiliary decoder).
    ///</summary>
	public uint LoopID
    {
        get { return (uint) _data.loopid; }
    }
    ///<summary>
    ///The I/O terminal ID for the decoder-status (UINT32_MAX if this decoder is not connected to an I/O terminal).
    ///</summary>
	public uint IOTerminalID
    {
        get { return (uint) _data.ioterminalid; }
    }
    ///<summary>
    ///The temperature (in Celsius).
    ///</summary>
	public byte Temperature
    {
        get { return (byte) _data.temperature; }
    }
    ///<summary>
    ///GPS information.
    ///</summary>
	public byte Gps
    {
        get { return (byte) _data.gps; }
    }

    ///<summary>
    ///Is this decoder-status the result of a resend?
    ///</summary>
    public bool IsResend()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) DECODERSTATUSBITS.dsbResend);
    }
    ///<summary>
    ///Is this decoder-status the result of a decoder resend?
    ///</summary>
    public bool IsDecoderResend()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) DECODERSTATUSBITS.dsbDecoderResend);
    }
    ///<summary>
    ///Get the timezone offset (in seconds).
    ///</summary>
    public int GetTimezoneOffset()
    {
        return (System.SByte)(((int) _data.flags >> 8) & 0xFF) * 15 * 60;

    }
    ///<summary>
    ///Get the voltage of the backup battery in Volt.
    ///</summary>
    public double GetVoltage()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.ConvertVoltage2Volt(_data.voltage);

    }
    ///<summary>
    ///Get the decoder temperature in Celcius.
    ///</summary>
    public double GetTemperature()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.ConvertDecoderTemperature2Celcius(_data.temperature);

    }
    ///<summary>
    ///Is the decoder temperature valid?
    ///</summary>
    public bool IsTemperatureValid()
    {
        return (bool)(_data.temperature != byte.MaxValue);

    }
    ///<summary>
    ///Get the receiver 0 type (see #DECODERSTATUSRECEIVERTYPE).
    ///</summary>
    public byte GetReceiver0Type()
    {
        return (byte)((_data.flags >> 2) & 0x07);

    }
    ///<summary>
    ///Get the receiver 1 type (see #DECODERSTATUSRECEIVERTYPE).
    ///</summary>
    public byte GetReceiver1Type()
    {
        return (byte)((_data.flags >> 5) & 0x07);

    }
    ///<summary>
    ///Get the noise level for the give receiver (in dBm).
    ///</summary>
    public double GetNoise(byte receiver)
    {
        byte noise = byte.MaxValue;
		switch ((DECODERSTATUSRECEIVERTYPE)receiver) {
		case DECODERSTATUSRECEIVERTYPE.dsrtTwoWay:
			if (GetReceiver0Type() == (byte)DECODERSTATUSRECEIVERTYPE.dsrtTwoWay) noise = _data.noise_receiver0;
			else if (GetReceiver0Type() == (byte)DECODERSTATUSRECEIVERTYPE.dsrtTwoWay) noise = _data.noise_receiver1;
			break;
		case DECODERSTATUSRECEIVERTYPE.dsrtTranX:
			if (GetReceiver0Type() == (byte)DECODERSTATUSRECEIVERTYPE.dsrtTranX) noise = _data.noise_receiver0;
			else if (GetReceiver1Type() == (byte)DECODERSTATUSRECEIVERTYPE.dsrtTranX) noise = _data.noise_receiver1;
			break;
		case DECODERSTATUSRECEIVERTYPE.dsrtChipX:
			if (GetReceiver0Type() == (byte)DECODERSTATUSRECEIVERTYPE.dsrtChipX) noise = _data.noise_receiver0;
			else if (GetReceiver1Type() == (byte)DECODERSTATUSRECEIVERTYPE.dsrtChipX) noise = _data.noise_receiver1;
			break;
		}
		return MylapsSDK.Utilities.SDKHelperFunctions.ConvertNoise2Dbm(noise);

    }
    ///<summary>
    ///Is the decoder noise level of the given receiver valid.
    ///</summary>
    public bool IsNoiseValid(byte receiver)
    {
        byte noise = byte.MaxValue;
		switch ((DECODERSTATUSRECEIVERTYPE)receiver) {
		case DECODERSTATUSRECEIVERTYPE.dsrtTwoWay:
			if (GetReceiver0Type() == (byte)DECODERSTATUSRECEIVERTYPE.dsrtTwoWay) noise = _data.noise_receiver0;
			else if (GetReceiver0Type() == (byte)DECODERSTATUSRECEIVERTYPE.dsrtTwoWay) noise = _data.noise_receiver1;
			break;
		case DECODERSTATUSRECEIVERTYPE.dsrtTranX:
			if (GetReceiver0Type() == (byte)DECODERSTATUSRECEIVERTYPE.dsrtTranX) noise = _data.noise_receiver0;
			else if (GetReceiver1Type() == (byte)DECODERSTATUSRECEIVERTYPE.dsrtTranX) noise = _data.noise_receiver1;
			break;
		case DECODERSTATUSRECEIVERTYPE.dsrtChipX:
			if (GetReceiver0Type() == (byte)DECODERSTATUSRECEIVERTYPE.dsrtChipX) noise = _data.noise_receiver0;
			else if (GetReceiver1Type() == (byte)DECODERSTATUSRECEIVERTYPE.dsrtChipX) noise = _data.noise_receiver1;
			break;
		}
		return (bool)(noise != byte.MaxValue);

    }
    ///<summary>
    ///Get the average noise level of the 2-way receiver (in dBm).
    ///</summary>
    public double GetNoiseTwoway()
    {
        return GetNoise((byte)DECODERSTATUSRECEIVERTYPE.dsrtTwoWay);

    }
    ///<summary>
    ///Is the decoder noise level of the 2-way receiver valid?
    ///</summary>
    public bool IsTwowayNoiseValid()
    {
        return IsNoiseValid((byte)DECODERSTATUSRECEIVERTYPE.dsrtTwoWay);

    }
    ///<summary>
    ///Get the average noise level of the TranX receiver (in dBm).
    ///</summary>
    public double GetNoiseTranx()
    {
        return GetNoise((byte)DECODERSTATUSRECEIVERTYPE.dsrtTranX);

    }
    ///<summary>
    ///Is the decoder noise level of the TranX receiver valid?
    ///</summary>
    public bool IsTranxNoiseValid()
    {
        return IsNoiseValid((byte)DECODERSTATUSRECEIVERTYPE.dsrtTranX);

    }
    ///<summary>
    ///Get the average noise level of the ChipX receiver (in dBm).
    ///</summary>
    public double GetNoiseChipx()
    {
        return GetNoise((byte)DECODERSTATUSRECEIVERTYPE.dsrtChipX);

    }
    ///<summary>
    ///Is the decoder noise level of the ChipX receiver valid?
    ///</summary>
    public bool IsChipxNoiseValid()
    {
        return IsNoiseValid((byte)DECODERSTATUSRECEIVERTYPE.dsrtChipX);

    }
    ///<summary>
    ///Get the number of satellites used for GPS synchronization.
    ///</summary>
    public byte GetSatellitesInUse()
    {
        return (byte) (_data.gps >> 2);

    }
    ///<summary>
    ///Is the GPS status OK (i.e. locked)?
    ///</summary>
    public bool IsGpsOk()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.gps, (int) 1);
    }


    
}

}