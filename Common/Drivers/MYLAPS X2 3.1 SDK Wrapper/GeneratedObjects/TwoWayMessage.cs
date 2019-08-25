namespace MylapsSDK.Objects
{
    public enum TWOWAYMESSAGEBITS {
        twmbResend = 2, // Is this 2-way message the result of a resend?
        twmbDecoderResend = 3 // Is this 2-way message the result of a decoder resend?
    }
    public enum TWOWAYMESSAGESTATE {
        twmsQueued = 0, // The message is queued for delivery.
        twmsSent, // The message is send to the transponder (and successfully received by it).
        twmsFailed, // Failed to sent the message to the transponder.
        twmsReceived // The message is received from the 2-way transponder.
    }
    public enum TWOWAYMESSAGEPRIORITY {
        twmPriorityHighest = 0, // The 2-way message has the highest priority.
        twmPriorityHigh = 3, // The 2-way message has high priority.
        twmPriorityNormal = 7, // The 2-way message has normal priority.
        twmPriorityLow = 12, // The 2-way message has low priority.
        twmPriorityLowest = 15 // The 2-way message has the lowest priority.
    }

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// A CAN message send to a car with the given transponder.
    internal struct TwoWayMessageStruct
    {
        public long utctime; //  The time the 2-way message was received (in UTC).
        public long timeofday; //  The UTC time corrected for the local timezone.
        public uint id; //  The unique identifier.
        public uint systemsetupid; //  The unique ID of the system-setup this 2-way message belongs to.
        public uint requestid; //  The unique request ID (will be echoed by the system).
        public uint transponderid; //  The transponder ID.
        public uint transpondershortid; //  The 'short' (user-definable) transponder number.
        public uint flags; //  Misc. flags.
        public uint loopid; //  The loop ID for the 2-way message (where it was delivered).
        public ushort strength; //  The signal strength (in dBm).
        public ushort reserved; //  Reserved for future use.
        public ushort timetoexpire; //  The time for this 2-way message to expire (in seconds), if it is not sent.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 12, ArraySubType = System.Runtime.InteropServices.UnmanagedType.U1)]
        public byte[] payload; //  The 2-way message data.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 2)]
        public byte[] padding; //  padding
    }
#pragma warning restore 0649

/// <summary>
/// Information about 2-way message
/// </summary>
/// <remarks>
/// A CAN message send to a car with the given transponder.
/// </remarks>
public partial class TwoWayMessage: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly TwoWayMessageStruct _data;


    private EventData _handleWrapper;

    internal TwoWayMessage(System.IntPtr nativePointer, EventData context)
    {
        _nativePointer = nativePointer;
        _data = (TwoWayMessageStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(TwoWayMessageStruct));

        _handleWrapper = context;
    }
	
	internal static TwoWayMessage FromNativePointer(
        System.IntPtr pointerToNativeStruct, EventData context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new TwoWayMessage(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<TwoWayMessage> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, EventData context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<TwoWayMessage>(
            System.Array.ConvertAll<System.IntPtr,TwoWayMessage>(ptrArray,
                ptr => new TwoWayMessage(ptr, context)));
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
    ///The time the 2-way message was received (in UTC).
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
    ///The unique ID of the system-setup this 2-way message belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The unique request ID (will be echoed by the system).
    ///</summary>
	public uint RequestID
    {
        get { return (uint) _data.requestid; }
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
    ///The loop ID for the 2-way message (where it was delivered).
    ///</summary>
	public uint LoopID
    {
        get { return (uint) _data.loopid; }
    }
    ///<summary>
    ///Reserved for future use.
    ///</summary>
	public ushort Reserved
    {
        get { return (ushort) _data.reserved; }
    }
    ///<summary>
    ///The time for this 2-way message to expire (in seconds), if it is not sent.
    ///</summary>
	public ushort TimeToExpire
    {
        get { return (ushort) _data.timetoexpire; }
    }

    ///<summary>
    ///Is this 2-way message the result of a resend?
    ///</summary>
    public bool IsResend()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TWOWAYMESSAGEBITS.twmbResend);
    }
    ///<summary>
    ///Is this 2-way message the result of a decoder resend?
    ///</summary>
    public bool IsDecoderResend()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TWOWAYMESSAGEBITS.twmbDecoderResend);
    }
    ///<summary>
    ///Get the signal strength (in dBm).
    ///</summary>
    public double GetStrength()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.ConvertRssi2Dbm(_data.strength);

    }
    ///<summary>
    ///Return the state for this 2-way message (see #TWOWAYMESSAGESTATE).
    ///</summary>
    public uint GetState()
    {
        return _data.flags & 0x03;

    }
    ///<summary>
    ///Get the priority of this 2-way message in the send queue for this transponder (see #TWOWAYMESSAGEPRIORITY).
    ///</summary>
    public byte GetPriority()
    {
        return (byte)((_data.flags >> 8) & 0x0F);

    }
    ///<summary>
    ///Get the timezone offset (in seconds).
    ///</summary>
    public int GetTimezoneOffset()
    {
        return ((int)(_data.flags >> 24) & 0xFF) * 15 * 60;

    }
    ///<summary>
    ///Get the CAN header
    ///</summary>
    public ushort GetCANHeader()
    {
        return (ushort)((_data.payload[0] << 8) | _data.payload[1]);

    }
    ///<summary>
    ///Get the CAN data length
    ///</summary>
    public ushort GetCANDataLength()
    {
        return (ushort)(GetCANHeader() & 0x0F);

    }
    ///<summary>
    ///Get the CAN ID
    ///</summary>
    public ushort GetCANID()
    {
        return (ushort)((GetCANHeader() >> 5) & 0x7FF);

    }
    ///<summary>
    ///Get the CAN RTR
    ///</summary>
    public bool IsCANRTR()
    {
        return ((GetCANHeader() >> 4) & 0x1) == 0x1;

    }
    ///<summary>
    ///Get the CAN payload (max. 8 bytes).
    ///</summary>
    public byte[] GetCANData()
    {
        byte[] canData = new byte[GetCANDataLength()];
System.Array.Copy(_data.payload, 2, canData, 0, GetCANDataLength());
return canData;

    }


    
}

/// <summary>
/// Modifier for 2-way message
/// </summary>
/// <remarks>
/// A CAN message send to a car with the given transponder.
/// </remarks>
public class TwoWayMessageModifier : MylapsSDK.MylapsSDKLibrary.GenericModifier<TwoWayMessage>
{
    private readonly System.IntPtr _nativePointer;
    private TwoWayMessageStruct _data;
    private EventData _handleWrapper;


    internal TwoWayMessageModifier(System.IntPtr nativePointer, EventData context):
        base(nativePointer)
	{
	    _nativePointer = nativePointer;
	    _handleWrapper = context;
		_data = (TwoWayMessageStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(nativePointer, typeof(TwoWayMessageStruct));
	}

    ///<summary>
    ///The time the 2-way message was received (in UTC).
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
    ///The unique ID of the system-setup this 2-way message belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The unique request ID (will be echoed by the system).
    ///</summary>
	public uint RequestID
    {
        get { return (uint) _data.requestid; }
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
    ///The loop ID for the 2-way message (where it was delivered).
    ///</summary>
	public uint LoopID
    {
        get { return (uint) _data.loopid; }
    }
    ///<summary>
    ///Reserved for future use.
    ///</summary>
	public ushort Reserved
    {
        get { return (ushort) _data.reserved; }
    }
    ///<summary>
    ///The time for this 2-way message to expire (in seconds), if it is not sent.
    ///</summary>
	public ushort TimeToExpire
    {
        get { return (ushort) _data.timetoexpire; }
    }


    ///<summary>
    ///Is this 2-way message the result of a resend?
    ///</summary>
    public bool IsResend()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TWOWAYMESSAGEBITS.twmbResend);
    }
    ///<summary>
    ///Is this 2-way message the result of a decoder resend?
    ///</summary>
    public bool IsDecoderResend()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TWOWAYMESSAGEBITS.twmbDecoderResend);
    }
    ///<summary>
    ///Get the signal strength (in dBm).
    ///</summary>
    public double GetStrength()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.ConvertRssi2Dbm(_data.strength);

    }
    ///<summary>
    ///Return the state for this 2-way message (see #TWOWAYMESSAGESTATE).
    ///</summary>
    public uint GetState()
    {
        return _data.flags & 0x03;

    }
    ///<summary>
    ///Get the priority of this 2-way message in the send queue for this transponder (see #TWOWAYMESSAGEPRIORITY).
    ///</summary>
    public byte GetPriority()
    {
        return (byte)((_data.flags >> 8) & 0x0F);

    }
    ///<summary>
    ///Get the timezone offset (in seconds).
    ///</summary>
    public int GetTimezoneOffset()
    {
        return ((int)(_data.flags >> 24) & 0xFF) * 15 * 60;

    }
    ///<summary>
    ///Get the CAN header
    ///</summary>
    public ushort GetCANHeader()
    {
        return (ushort)((_data.payload[0] << 8) | _data.payload[1]);

    }
    ///<summary>
    ///Get the CAN data length
    ///</summary>
    public ushort GetCANDataLength()
    {
        return (ushort)(GetCANHeader() & 0x0F);

    }
    ///<summary>
    ///Get the CAN ID
    ///</summary>
    public ushort GetCANID()
    {
        return (ushort)((GetCANHeader() >> 5) & 0x7FF);

    }
    ///<summary>
    ///Get the CAN RTR
    ///</summary>
    public bool IsCANRTR()
    {
        return ((GetCANHeader() >> 4) & 0x1) == 0x1;

    }
    ///<summary>
    ///Get the CAN payload (max. 8 bytes).
    ///</summary>
    public byte[] GetCANData()
    {
        byte[] canData = new byte[GetCANDataLength()];
System.Array.Copy(_data.payload, 2, canData, 0, GetCANDataLength());
return canData;

    }

    ///<summary>
    ///Set the CAN data in the 2-way message payload.
    ///</summary>
    public void SetCANData (ushort canID, ushort canRTR, byte[] canData) // setter function
    {
        canID = (ushort) (canID & 0x7FF);
// Limit length of CAN data to 8
int dataLength = System.Math.Min(8, canData.Length);
// Convert CAN values to 2-way message payload
byte[] payload = new byte[12];
ushort header = (ushort)(((canID & 0x7FF) << 5) | ((canRTR & 0x1) << 4) | dataLength);
payload[0] = (byte)((header >> 8) & 0xFF);
payload[1] = (byte)(header & 0xFF);
System.Array.Copy(canData, 0, payload, 2, dataLength);
_data.payload = payload;

        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Copy the raw payload-data to the 2-way message data.
    ///</summary>
    public void SetPayload (byte[] data) // setter function
    {
        int dataLength = System.Math.Min(12, data.Length);
System.Array.Copy(data, _data.payload, dataLength);

        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the priority of this 2-way message in the send queue for this transponder (see #TWOWAYMESSAGEPRIORITY).
    ///</summary>
    public void SetPriority (byte priority) // setter function
    {
        unchecked {
    // clear current priority
    _data.flags &= (uint) ~(0x000000F00);
}
_data.flags |= (uint)((priority & 0xF) << 8);


        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the time-to-expire value for this 2-way message.
    ///</summary>
    public void SetTimeToExpire (ushort time) // setter function
    {
		_data.timetoexpire = (ushort) time;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the transponder number for this 2-way message.
    ///</summary>
    public void SetTransponderID (uint transponder) // setter function
    {
		_data.transponderid = (uint) transponder;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the transponder short ID for this 2-way message.
    ///</summary>
    public void SetTransponderShortID (uint id) // setter function
    {
		_data.transpondershortid = (uint) id;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }


    

    
}

}