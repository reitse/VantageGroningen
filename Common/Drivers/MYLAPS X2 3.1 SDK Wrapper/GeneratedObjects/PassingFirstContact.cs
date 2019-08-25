namespace MylapsSDK.Objects
{

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// Passing first-contact event (during a passing) from the decoder. Deprecated.
    internal struct PassingFirstContactStruct
    {
        public long utctime; //  The time the passing first-contact event was received (in UTC).
        public long timeofday; //  The UTC time corrected for the local timezone.
        public uint systemsetupid; //  The unique ID of the system-setup this passing first-contact event belongs to.
        public uint loopid; //  The loop ID for the passing first-contact event (if the loop ID is not set the value will be UINT32_MAX).
        public uint transponderid; //  The transponder ID.
        public uint transpondershortid; //  The 'short' (user-definable) transponder number.
        public uint flags; //  Passing first-contact flags (e.g. transponder-type and timezone information).
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 4)]
        public byte[] padding; //  padding
    }
#pragma warning restore 0649

/// <summary>
/// Information about Passing first-contact
/// </summary>
/// <remarks>
/// Passing first-contact event (during a passing) from the decoder. Deprecated.
/// </remarks>
public partial class PassingFirstContact{
    private readonly System.IntPtr _nativePointer;
    private readonly PassingFirstContactStruct _data;


    private EventData _handleWrapper;

    internal PassingFirstContact(System.IntPtr nativePointer, EventData context)
    {
        _nativePointer = nativePointer;
        _data = (PassingFirstContactStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(PassingFirstContactStruct));

        _handleWrapper = context;
    }
	
	internal static PassingFirstContact FromNativePointer(
        System.IntPtr pointerToNativeStruct, EventData context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new PassingFirstContact(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<PassingFirstContact> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, EventData context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<PassingFirstContact>(
            System.Array.ConvertAll<System.IntPtr,PassingFirstContact>(ptrArray,
                ptr => new PassingFirstContact(ptr, context)));
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
    ///The time the passing first-contact event was received (in UTC).
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
    ///The unique ID of the system-setup this passing first-contact event belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The loop ID for the passing first-contact event (if the loop ID is not set the value will be UINT32_MAX).
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
    ///Get the timezone offset (in seconds).
    ///</summary>
    public int GetTimezoneOffset()
    {
        return (System.SByte)((int)(_data.flags >> 16) & 0xFF) * 15 * 60;

    }
    ///<summary>
    ///Get the transponder type (see #TRANSPONDERTYPE).
    ///</summary>
    public uint GetPassingFirstContactType()
    {
        return _data.flags & 0x07;

    }


    
}

}