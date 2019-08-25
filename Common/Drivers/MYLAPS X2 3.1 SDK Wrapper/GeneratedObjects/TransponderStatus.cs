namespace MylapsSDK.Objects
{
    public enum TRANSPONDERSTATUSBITS {
        tsbClockSynced = 0, // Is the internal clock synchronized to UTC?
        tsbResend = 1 // Is this transponder-status the result of a resend?
    }
    public enum TRANSPONDERORIENTATION {
        toHorizontal = 0, // The transponder orientation is horizontal.
        toVertical = 1, // The transponder orientation is vertical.
        toUnknown = 2 // The transponder orientation is unknown.
    }

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// Detailed status of a type-4 transponder (voltage, temperature, noise, etc).
    internal struct TransponderStatusStruct
    {
        public long utctime; //  The time the transponder-status was received (in UTC).
        public long timeofday; //  The UTC time corrected for the local timezone.
        public uint id; //  The unique identifier.
        public uint systemsetupid; //  The unique ID of the system-setup this transponder-status belongs to.
        public uint loopid; //  The loop ID for the transponder status (where it was received).
        public uint transponderid; //  The transponder ID for the transponder status.
        public uint transpondershortid; //  The 'short' (user-definable) transponder number.
        public uint flags; //  Misc. flags.
        public ushort batteryinfo; //  The battery information.
        public byte batteryvoltage; //  The backup-battery voltage information.
        public byte noise; //  Recorded noise level at the transponder (in AMB units).
        public byte strength; //  The strength of the signal while receiving the transponder status.
        public byte temperature; //  The temperature of the transponder in Celsius.
        public byte voltage; //  The input voltage at the transponder in 1/10 Volt.
        public byte version; //  The version, high-nibble is major number low-nibble is minor number.
    }
#pragma warning restore 0649

/// <summary>
/// Information about Transponder status
/// </summary>
/// <remarks>
/// Detailed status of a type-4 transponder (voltage, temperature, noise, etc).
/// </remarks>
public partial class TransponderStatus: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly TransponderStatusStruct _data;


    private EventData _handleWrapper;

    internal TransponderStatus(System.IntPtr nativePointer, EventData context)
    {
        _nativePointer = nativePointer;
        _data = (TransponderStatusStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(TransponderStatusStruct));

        _handleWrapper = context;
    }
	
	internal static TransponderStatus FromNativePointer(
        System.IntPtr pointerToNativeStruct, EventData context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new TransponderStatus(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<TransponderStatus> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, EventData context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<TransponderStatus>(
            System.Array.ConvertAll<System.IntPtr,TransponderStatus>(ptrArray,
                ptr => new TransponderStatus(ptr, context)));
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
    ///The time the transponder-status was received (in UTC).
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
    ///The unique ID of the system-setup this transponder-status belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The loop ID for the transponder status (where it was received).
    ///</summary>
	public uint LoopID
    {
        get { return (uint) _data.loopid; }
    }
    ///<summary>
    ///The transponder ID for the transponder status.
    ///</summary>
	public uint TransponderID
    {
        get { return (uint) _data.transponderid; }
    }
    ///<summary>
    ///The 'short' (user-definable) transponder number.
    ///</summary>
	public uint TranspondershortID
    {
        get { return (uint) _data.transpondershortid; }
    }
    ///<summary>
    ///The battery information.
    ///</summary>
	public ushort BatteryInfo
    {
        get { return (ushort) _data.batteryinfo; }
    }
    ///<summary>
    ///The version, high-nibble is major number low-nibble is minor number.
    ///</summary>
	public byte Version
    {
        get { return (byte) _data.version; }
    }

    ///<summary>
    ///Get the noise level (in dBm).
    ///</summary>
    public double GetNoise()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.ConvertTransponderNoise2Dbm(_data.noise);

    }
    ///<summary>
    ///Get the strength of the signal (in dBm).
    ///</summary>
    public double GetStrength()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.ConvertTransponderNoise2Dbm(_data.strength);

    }
    ///<summary>
    ///Get the input voltage of in Volt.
    ///</summary>
    public double GetVoltage()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.ConvertTransponderVoltage2Volt(_data.voltage);

    }
    ///<summary>
    ///Get the battery voltage of the battery in Volt.
    ///</summary>
    public double GetBatteryVoltage()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.ConvertTransponderVoltage2Volt(_data.batteryvoltage);

    }
    ///<summary>
    ///The temperature in Celsius.
    ///</summary>
    public double GetTemperature()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.ConvertTransponderTemperature2Celcius(_data.temperature);

    }
    ///<summary>
    ///Is the transponder clock synced?
    ///</summary>
    public bool IsClockSynced()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TRANSPONDERSTATUSBITS.tsbClockSynced);
    }
    ///<summary>
    ///Is this transponder status the result of a resend?
    ///</summary>
    public bool IsResend()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TRANSPONDERSTATUSBITS.tsbResend);
    }
    ///<summary>
    ///Is the battery info supported?
    ///</summary>
    public bool IsBatteryInfoSupported()
    {
        return _data.version >= ((1 << 4) | 8);

    }
    ///<summary>
    ///Get the battery level in percentage.
    ///</summary>
    public byte GetBatteryLevel()
    {
        if (IsBatteryInfoSupported())
    return (byte)(((_data.batteryinfo >> 2) & 0x07) * 20);
else
    return System.Byte.MaxValue;

    }
    ///<summary>
    ///Is the 2-way transponder running on battery?
    ///</summary>
    public bool IsRunningOnBattery()
    {
        if (IsBatteryInfoSupported())
    return _data.voltage < 50;
else
    return false;

    }
    ///<summary>
    ///Get the time left (in minutes) that the transponder can run with 2-way functionality from battery.
    ///</summary>
    public ushort GetBatteryTimeLeft()
    {
        if (IsBatteryInfoSupported())
    return (byte)((_data.batteryinfo >> 5) * 10);
else
    return (System.UInt16.MaxValue);

    }
    ///<summary>
    ///Get the timezone offset (in seconds).
    ///</summary>
    public int GetTimezoneOffset()
    {
        return (System.SByte)((int)(_data.flags >> 24) & 0xFF) * 15 * 60;

    }
    ///<summary>
    ///Get the orientation of the transponder.
    ///</summary>
    public byte GetOrientation()
    {
        return (byte)((_data.flags >> 8) & 0x03);

    }


    
}

}