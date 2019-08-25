namespace MylapsSDK.Objects
{
    public enum LOOPTRIGGERBITS {
        ltbLowStrengthWarning = 0 // Is the signal-strength too low?
    }

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// The information recieved from a loop-trigger.
    internal struct LoopTriggerStruct
    {
        public long utctime; //  The time the loop-trigger was seen (in UTC).
        public long timeofday; //  The loop-trigger time (in local time, calculated).
        public uint id; //  The unique identifier of the loop-trigger.
        public uint systemsetupid; //  The system-setup ID.
        public uint triggerid; //  The trigger code (XXX-00000 in different representation).
        public uint loopid; //  The loop ID for the loop this loop-trigger is placed in.
        public ushort strength; //  The strength of the signal.
        public ushort temperature; //   The temperature in 1/10 Celsius.
        public ushort flags; //  The loop-trigger flag bits.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 2)]
        public byte[] padding; //  padding
    }
#pragma warning restore 0649

/// <summary>
/// Information about Loop-trigger information
/// </summary>
/// <remarks>
/// The information recieved from a loop-trigger.
/// </remarks>
public partial class LoopTrigger: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly LoopTriggerStruct _data;


    private EventData _handleWrapper;

    internal LoopTrigger(System.IntPtr nativePointer, EventData context)
    {
        _nativePointer = nativePointer;
        _data = (LoopTriggerStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(LoopTriggerStruct));

        _handleWrapper = context;
    }
	
	internal static LoopTrigger FromNativePointer(
        System.IntPtr pointerToNativeStruct, EventData context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new LoopTrigger(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<LoopTrigger> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, EventData context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<LoopTrigger>(
            System.Array.ConvertAll<System.IntPtr,LoopTrigger>(ptrArray,
                ptr => new LoopTrigger(ptr, context)));
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
    ///The time the loop-trigger was seen (in UTC).
    ///</summary>
	public long UTCTime
    {
        get { return (long) _data.utctime; }
    }
    ///<summary>
    ///The loop-trigger time (in local time, calculated).
    ///</summary>
	public long TimeOfDay
    {
        get { return (long) _data.timeofday; }
    }
    ///<summary>
    ///The unique identifier of the loop-trigger.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The system-setup ID.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The trigger code (XXX-00000 in different representation).
    ///</summary>
	public uint TriggerID
    {
        get { return (uint) _data.triggerid; }
    }
    ///<summary>
    ///The loop ID for the loop this loop-trigger is placed in.
    ///</summary>
	public uint LoopID
    {
        get { return (uint) _data.loopid; }
    }

    ///<summary>
    ///Get the timezone offset (in seconds).
    ///</summary>
    public int GetTimezoneOffset()
    {
        return (System.SByte)(((int) _data.flags >> 8) & 0xFF) * 15 * 60;

    }
    ///<summary>
    ///Get the signal strength (in dBm).
    ///</summary>
    public double GetStrength()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.ConvertRssi2Dbm(_data.strength);

    }
    ///<summary>
    ///Is the loop-trigger temperature valid?
    ///</summary>
    public bool IsTemperatureValid()
    {
        return (_data.temperature != System.UInt16.MaxValue);

    }
    ///<summary>
    ///The temperature in Celsius.
    ///</summary>
    public double GetTemperature()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.ConvertLoopTriggerTemperature2Celcius(_data.temperature);

    }
    ///<summary>
    ///Is the signal strength low (e.g. smaller than -70 dBm)?
    ///</summary>
    public bool HasLowStrengthWarning()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) LOOPTRIGGERBITS.ltbLowStrengthWarning);
    }


    
}

}