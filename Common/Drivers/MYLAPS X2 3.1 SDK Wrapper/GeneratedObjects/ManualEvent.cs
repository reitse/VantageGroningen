namespace MylapsSDK.Objects
{
    public enum MANUALEVENTTYPE {
        metFlag = 0, // 'Flag'-type marker (e.g. 'green flag').
        metManualPassing = 1, // The manual passing
        metSinglePhotocell = 2 // The single photocell passing
    }
    public enum MANUALEVENTFLAGS {
        mefResend = 0 // Is this manual-event the result of a resend?
    }
    public enum MANUALEVENTFLAGTYPES {
        mftGreen = 0, // The 'green' flag
        mftYellow = 1, // The 'yellow' flag
        mftYellowSpecial = 2, // The 'yellow' flag (special condition, e.g. don't count)
        mftRed = 3, // The 'red' flag
        mftWhite = 4, // The 'white' flag
        mftCheckered = 5, // The 'checkered' flag
        mftPurple = 6, // The 'purple' flag
        mftBlue = 7 // The 'blue' flag
    }

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// A manually created event (e.g. flag).
    internal struct ManualEventStruct
    {
        public long utctime; //  The time the manual-event was received (in UTC).
        public long timeofday; //  The UTC time corrected for the local timezone.
        public long userdata; //  User definable data field. This is a signed 64-bit number (signed for compatibility with C# and java)
        public uint id; //  The unique identifier.
        public uint systemsetupid; //  The unique ID of the system-setup this auxiliary event belongs to.
        public uint flags; //  Manual-event flags (e.g. for storing timezone information).
        public uint userflags; //  Manual-event user-flags (bits), to be set by the SDK client.
    }
#pragma warning restore 0649

/// <summary>
/// Information about Manual event
/// </summary>
/// <remarks>
/// A manually created event (e.g. flag).
/// </remarks>
public partial class ManualEvent: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly ManualEventStruct _data;


    private EventData _handleWrapper;

    internal ManualEvent(System.IntPtr nativePointer, EventData context)
    {
        _nativePointer = nativePointer;
        _data = (ManualEventStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(ManualEventStruct));

        _handleWrapper = context;
    }
	
	internal static ManualEvent FromNativePointer(
        System.IntPtr pointerToNativeStruct, EventData context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new ManualEvent(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<ManualEvent> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, EventData context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<ManualEvent>(
            System.Array.ConvertAll<System.IntPtr,ManualEvent>(ptrArray,
                ptr => new ManualEvent(ptr, context)));
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
    ///The time the manual-event was received (in UTC).
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
    ///User definable data field. This is a signed 64-bit number (signed for compatibility with C# and java)
    ///</summary>
	public long UserData
    {
        get { return (long) _data.userdata; }
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
    ///Get the manual-event type (see #MANUALEVENTTYPE).
    ///</summary>
    public byte GetManualEventType()
    {
        return (byte)((_data.flags >> 8) & 0xFF);

    }
    ///<summary>
    ///Is this manual-event the result of a resend?
    ///</summary>
    public bool IsResend()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) MANUALEVENTFLAGS.mefResend);
    }
    ///<summary>
    ///Get the timezone offset (in seconds).
    ///</summary>
    public int GetTimezoneOffset()
    {
        return (System.SByte)(((int) _data.flags >> 16) & 0xFF) * 15 * 60;

    }
    ///<summary>
    ///Get the user-flags for a specific key (0..3).
    ///</summary>
    public byte GetUserFlags(byte key)
    {
        return (byte)((_data.userflags >> ((key & 0x03) * 8)) & 0xFF);

    }


    
}

/// <summary>
/// Modifier for Manual event
/// </summary>
/// <remarks>
/// A manually created event (e.g. flag).
/// </remarks>
public class ManualEventModifier : MylapsSDK.MylapsSDKLibrary.GenericModifier<ManualEvent>
{
    private readonly System.IntPtr _nativePointer;
    private ManualEventStruct _data;
    private EventData _handleWrapper;


    internal ManualEventModifier(System.IntPtr nativePointer, EventData context):
        base(nativePointer)
	{
	    _nativePointer = nativePointer;
	    _handleWrapper = context;
		_data = (ManualEventStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(nativePointer, typeof(ManualEventStruct));
	}

    ///<summary>
    ///The time the manual-event was received (in UTC).
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
    ///User definable data field. This is a signed 64-bit number (signed for compatibility with C# and java)
    ///</summary>
	public long UserData
    {
        get { return (long) _data.userdata; }
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
    ///Get the manual-event type (see #MANUALEVENTTYPE).
    ///</summary>
    public byte GetManualEventType()
    {
        return (byte)((_data.flags >> 8) & 0xFF);

    }
    ///<summary>
    ///Is this manual-event the result of a resend?
    ///</summary>
    public bool IsResend()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) MANUALEVENTFLAGS.mefResend);
    }
    ///<summary>
    ///Get the timezone offset (in seconds).
    ///</summary>
    public int GetTimezoneOffset()
    {
        return (System.SByte)(((int) _data.flags >> 16) & 0xFF) * 15 * 60;

    }
    ///<summary>
    ///Get the user-flags for a specific key (0..3).
    ///</summary>
    public byte GetUserFlags(byte key)
    {
        return (byte)((_data.userflags >> ((key & 0x03) * 8)) & 0xFF);

    }

    ///<summary>
    ///Set the manual-event type (see #MANUALEVENTTYPE).
    ///</summary>
    public void SetManualEventType (byte type) // setter function
    {
        	unchecked {
    // clear current type
    _data.flags &= (uint) ~(0x000000F00);
	}
	_data.flags |= (uint)((type & 0xF) << 8);

        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the time in UTC (timezone information will be calculated automatically).
    ///</summary>
    public void SetUTCTime (long utc) // setter function
    {
		_data.utctime = (long) utc;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the user-data.
    ///</summary>
    public void SetUserData (long userData) // setter function
    {
		_data.userdata = (long) userData;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the user-flags for a specific key (0..3).
    ///</summary>
    public void SetUserFlags (byte key, byte value) // setter function
    {
        	// Determine the position of the user-flags.
	int position = (key & 0x03) * 8;
	// Clear the position of the user-flags.
	_data.userflags &= (uint) ~(0x000000FF << position);
	// Store the user-flags.
	_data.userflags |= ((uint)value) << position;

        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }


    

    
}

}