namespace MylapsSDK.Objects
{
    public enum AUXEVENTBITS {
        aebInput = 8, // Is this event triggered by an input or an output?
        aebRisingEdge = 9, // Is the edge for this event rising?
        aebResend = 10, // Is this auxiliary event the result of a resend?
        aebDecoderResend = 11, // Is this auxiliary event the result of a decoder resend?
        aebWireless = 12 // Is this auxiliary event from a wireless input (e.g. wireless photocell)?
    }

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// An event on the auxiliary I/O terminal of the decoder.
    internal struct AuxEventStruct
    {
        public long decoderid; //  The decoder MAC address where this auxiliary event came from.
        public long utctime; //  The time the aux-event was received (in UTC).
        public long timeofday; //  The UTC time corrected for the local timezone.
        public uint id; //  The unique identifier.
        public uint systemsetupid; //  The unique ID of the system-setup this auxiliary event belongs to.
        public uint ioterminalid; //  The I/O terminal ID for the aux-event (if the I/O terminal ID is not set the value will be UINT32_MAX).
        public uint flags; //  Aux-event flags (e.g. for storing timezone information).
    }
#pragma warning restore 0649

/// <summary>
/// Information about Auxiliary I/O event
/// </summary>
/// <remarks>
/// An event on the auxiliary I/O terminal of the decoder.
/// </remarks>
public partial class AuxEvent: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly AuxEventStruct _data;


    private EventData _handleWrapper;

    internal AuxEvent(System.IntPtr nativePointer, EventData context)
    {
        _nativePointer = nativePointer;
        _data = (AuxEventStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(AuxEventStruct));

        _handleWrapper = context;
    }
	
	internal static AuxEvent FromNativePointer(
        System.IntPtr pointerToNativeStruct, EventData context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new AuxEvent(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<AuxEvent> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, EventData context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<AuxEvent>(
            System.Array.ConvertAll<System.IntPtr,AuxEvent>(ptrArray,
                ptr => new AuxEvent(ptr, context)));
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
    ///The decoder MAC address where this auxiliary event came from.
    ///</summary>
	public long DecoderID
    {
        get { return (long) _data.decoderid; }
    }
    ///<summary>
    ///The time the aux-event was received (in UTC).
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
    ///The unique ID of the system-setup this auxiliary event belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The I/O terminal ID for the aux-event (if the I/O terminal ID is not set the value will be UINT32_MAX).
    ///</summary>
	public uint IOTerminalID
    {
        get { return (uint) _data.ioterminalid; }
    }

    ///<summary>
    ///Get the channel (0..7) for this auxiliary event.
    ///</summary>
    public byte GetChannel()
    {
        return (byte)(_data.flags & 0xFF);

    }
    ///<summary>
    ///Is this event triggered by an input or an output change?
    ///</summary>
    public bool IsInputChange()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) AUXEVENTBITS.aebInput);
    }
    ///<summary>
    ///Get the edge information for this auxiliary event (rising or falling).
    ///</summary>
    public bool IsEdgeRising()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) AUXEVENTBITS.aebRisingEdge);
    }
    ///<summary>
    ///Is this auxiliary event the result of a resend?
    ///</summary>
    public bool IsResend()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) AUXEVENTBITS.aebResend);
    }
    ///<summary>
    ///Is this auxiliary event the result of a decoder resend?
    ///</summary>
    public bool IsDecoderResend()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) AUXEVENTBITS.aebDecoderResend);
    }
    ///<summary>
    ///Is this auxiliary event from a wireless input (e.g. wireless photocell)?
    ///</summary>
    public bool IsWireless()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) AUXEVENTBITS.aebWireless);
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