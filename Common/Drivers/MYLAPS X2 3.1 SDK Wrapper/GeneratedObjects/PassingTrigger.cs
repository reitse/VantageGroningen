namespace MylapsSDK.Objects
{
    public enum PASSINGTRIGGERBITS {
        ptbResend = 10 // Is this passing the result of a resend?
    }
    public enum PASSINGTRIGGERTYPE {
        pttFirstContact = 0, // Passing first-contact event.
        pttRealTime = 1 // Passing real-time event.
    }

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// Passing-trigger event (e.g. fist contact, realtime) from the decoder.
    internal struct PassingTriggerStruct
    {
        public long utctime; //  The time the passing trigger event was received (in UTC).
        public long timeofday; //  The UTC time corrected for the local timezone.
        public uint id; //  The unique identifier.
        public uint systemsetupid; //  The unique ID of the system-setup this passing trigger event belongs to.
        public uint loopid; //  The loop ID for the passing trigger event (if the loop ID is not set the value will be UINT32_MAX).
        public uint transponderid; //  The transponder ID.
        public uint transpondershortid; //  The 'short' (user-definable) transponder number.
        public uint flags; //  Passing-trigger flags (e.g. transponder-type and timezone information).
        public byte type; //  Passing-trigger type (see #PASSINGTRIGGERTYPE).
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 7)]
        public byte[] padding; //  padding
    }
#pragma warning restore 0649

/// <summary>
/// Information about Passing-trigger
/// </summary>
/// <remarks>
/// Passing-trigger event (e.g. fist contact, realtime) from the decoder.
/// </remarks>
public partial class PassingTrigger: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly PassingTriggerStruct _data;


    private EventData _handleWrapper;

    internal PassingTrigger(System.IntPtr nativePointer, EventData context)
    {
        _nativePointer = nativePointer;
        _data = (PassingTriggerStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(PassingTriggerStruct));

        _handleWrapper = context;
    }
	
	internal static PassingTrigger FromNativePointer(
        System.IntPtr pointerToNativeStruct, EventData context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new PassingTrigger(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<PassingTrigger> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, EventData context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<PassingTrigger>(
            System.Array.ConvertAll<System.IntPtr,PassingTrigger>(ptrArray,
                ptr => new PassingTrigger(ptr, context)));
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
    ///The time the passing trigger event was received (in UTC).
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
    ///The unique ID of the system-setup this passing trigger event belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The loop ID for the passing trigger event (if the loop ID is not set the value will be UINT32_MAX).
    ///</summary>
	public uint LoopID
    {
        get { return (uint) _data.loopid; }
    }
    ///<summary>
    ///The transponder ID.
    ///</summary>
	public uint TransponderID
    {
        get { return (uint) _data.transponderid; }
    }
    ///<summary>
    ///The 'short' (user-definable) transponder number.
    ///</summary>
	public uint TransponderShortID
    {
        get { return (uint) _data.transpondershortid; }
    }
    ///<summary>
    ///Passing-trigger type (see #PASSINGTRIGGERTYPE).
    ///</summary>
	public byte Type
    {
        get { return (byte) _data.type; }
    }

    ///<summary>
    ///Get the timezone offset (in seconds).
    ///</summary>
    public int GetTimezoneOffset()
    {
        return (System.SByte)((int)(_data.flags >> 16) & 0xFF) * 15 * 60;

    }
    ///<summary>
    ///Get the transponder type (see #TRANSPONDERTYPE).
    ///</summary>
    public uint GetTransponderType()
    {
        return _data.flags & 0xFF;

    }
    ///<summary>
    ///Is this passing-trigger the result of a resend?
    ///</summary>
    public bool IsResend()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) PASSINGTRIGGERBITS.ptbResend);
    }


    
}

}