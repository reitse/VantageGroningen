namespace MylapsSDK.Objects
{
    public enum AUXSTATUSBITS {
        asbResend = 0 // Is this auxiliary status the result of a resend?
    }
    public enum AUX_OUTPUT_STATE {
        aosOff = 0, // The output is off.
        aosOn = 1, // The output is on.
        aosToggleSource1 = 2, // The output toggles with the toggle1period. See toggle1period from decoder.
        aosToggleSource2 = 3, // The output toggles with the toggle2period. See toggle2period from decoder.
        aosGPSMinutePulse = 4 // The output gives a 10 ms pulse every minute (only when GPS is locked).
    }

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// The auxiliary I/O status of the auxiliary input of the decoder.
    internal struct AuxStatusStruct
    {
        public long decoderid; //  The decoder MAC address where this status came from.
        public long utctime; //  The time the auxiliary status was received (in UTC).
        public long timeofday; //  The UTC time corrected for the local timezone.
        public uint id; //  The unique identifier.
        public uint systemsetupid; //  The unique ID of the system-setup this auxiliary status belongs to.
        public uint ioterminalid; //  The I/O-terminal ID for the auxiliary status (if the I/O terminal ID is not set the value will be UINT32_MAX).
        public uint flags; //  Misc. flags.
        public uint analog_in; //  Analog input values (1 byte per channel).
        public uint analog_out; //  Analog output values (1 byte per channel).
        public uint output; //  Flags indicating the output state for each pin of the digital aux-out. 4 bits per output.
        public byte input; //  Bitmask indicating for each pin of the digital aux-in if it is high(1) or low(0).
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 3)]
        public byte[] padding; //  padding
    }
#pragma warning restore 0649

/// <summary>
/// Information about Auxiliary I/O status
/// </summary>
/// <remarks>
/// The auxiliary I/O status of the auxiliary input of the decoder.
/// </remarks>
public partial class AuxStatus: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly AuxStatusStruct _data;


    private EventData _handleWrapper;

    internal AuxStatus(System.IntPtr nativePointer, EventData context)
    {
        _nativePointer = nativePointer;
        _data = (AuxStatusStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(AuxStatusStruct));

        _handleWrapper = context;
    }
	
	internal static AuxStatus FromNativePointer(
        System.IntPtr pointerToNativeStruct, EventData context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new AuxStatus(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<AuxStatus> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, EventData context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<AuxStatus>(
            System.Array.ConvertAll<System.IntPtr,AuxStatus>(ptrArray,
                ptr => new AuxStatus(ptr, context)));
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
    ///The decoder MAC address where this status came from.
    ///</summary>
	public long DecoderID
    {
        get { return (long) _data.decoderid; }
    }
    ///<summary>
    ///The time the auxiliary status was received (in UTC).
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
    ///The unique identifier.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The unique ID of the system-setup this auxiliary status belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The I/O-terminal ID for the auxiliary status (if the I/O terminal ID is not set the value will be UINT32_MAX).
    ///</summary>
	public uint IOTerminalID
    {
        get { return (uint) _data.ioterminalid; }
    }
    ///<summary>
    ///Analog input values (1 byte per channel).
    ///</summary>
	public uint AnalogIn
    {
        get { return (uint) _data.analog_in; }
    }
    ///<summary>
    ///Analog output values (1 byte per channel).
    ///</summary>
	public uint AnalogOut
    {
        get { return (uint) _data.analog_out; }
    }
    ///<summary>
    ///Flags indicating the output state for each pin of the digital aux-out. 4 bits per output.
    ///</summary>
	public uint Output
    {
        get { return (uint) _data.output; }
    }
    ///<summary>
    ///Bitmask indicating for each pin of the digital aux-in if it is high(1) or low(0).
    ///</summary>
	public byte Input
    {
        get { return (byte) _data.input; }
    }

    ///<summary>
    ///Is this auxiliary status the result of a resend?
    ///</summary>
    public bool IsResend()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) AUXSTATUSBITS.asbResend);
    }
    ///<summary>
    ///Is the input channel (0..7) for this auxiliary set?
    ///</summary>
    public bool IsInputSet(int channel)
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet(_data.input, channel & 0x07);

    }
    ///<summary>
    ///Get the output state for channel (0..7). See #AUX_OUTPUT_STATE.
    ///</summary>
    public byte GetOutputState(int channel)
    {
        return (byte)(_data.output >> ((channel & 0x07) * 4) &  0x0F);

    }
    ///<summary>
    ///Get the analog input value for channel (0..3) in volt.
    ///</summary>
    public double GetAnalogInput(int channel)
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.ConvertAuxAd2Volt((int)(_data.analog_in >> ((channel & 0x03) * 8) & 0xFF));

    }
    ///<summary>
    ///Get the raw ADC analog input value for channel (0..3).
    ///</summary>
    public byte GetAnalogInputADC(int channel)
    {
        return (byte)(_data.analog_in >> ((channel & 0x03) * 8) & 0xFF);

    }
    ///<summary>
    ///Get the analog output value for channel (0..3) in volt.
    ///</summary>
    public double GetAnalogOutput(int channel)
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.ConvertAuxAd2Volt((int)(_data.analog_out >> ((channel & 0x03) * 8) & 0xFF));

    }
    ///<summary>
    ///Get the raw DAC analog output value for channel (0..3).
    ///</summary>
    public byte GetAnalogOutputDAC(int channel)
    {
        return (byte)(_data.analog_out >> ((channel & 0x03) * 8) & 0xFF);

    }
    ///<summary>
    ///Get the timezone offset (in seconds).
    ///</summary>
    public int GetTimezoneOffset()
    {
        return (System.SByte)((int)(_data.flags >> 16) & 0xFF) * 15 * 60;

    }


    
}

}