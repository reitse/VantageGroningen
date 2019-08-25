namespace MylapsSDK.Objects
{
    public enum PASSINGBITS {
        pbLowBattery = 9, // Did the transponder issue a low-battery warning?
        pbResend = 10, // Is this passing the result of a resend?
        pbInPit = 11, // Is this passing on a loop in the pit?
        pbLowHitsWarning = 13, // Is the number of hits too low?
        pbLowStrengthWarning = 14, // Is the signal-strength too low?
        pbDecoderResend = 15 // Is this passing the result of a decoder resend?
    }
    public enum PASSINGQUALITY {
        pqFirstContact = 0, // Passing time determined on first contact.
        pqTranXAvg = 1, // Passing time has TranX precision.
        pqTranX4ProAvg = 2, // Passing time has TranX precision.
        pqTranX3ProV = 3, // Passing time has TranX-3 Pro precision (vertical placement).
        pqTranX3ProH = 4, // Passing time has TranX-3 Pro precision (horizontal placement).
        pqTranX4ProV = 5, // Passing time has X2 precision (vertical placement).
        pqTranX4ProH = 6, // Passing time has X2 precision (horizontal placement).
        pqFutureUse = 7 // Reserved for future use.
    }

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// Passing event on the decoder.
    internal struct PassingStruct
    {
        public long utctime; //  The time the passing event was received (in UTC).
        public long timeofday; //  The UTC time corrected for the local timezone.
        public uint id; //  The unique identifier.
        public uint systemsetupid; //  The unique ID of the system-setup this passing belongs to.
        public uint loopid; //  The loop ID for the passing event (if the loop ID is not set the value will be UINT32_MAX).
        public uint transponderid; //  The transponder ID.
        public uint transpondershortid; //  The 'short' (user-definable) transponder number.
        public uint flags; //  Passing flags (e.g. resend, GPS-locked).
        public uint userflags; //  Passing user-flags (bits), to be set by the SDK client.
        public ushort strength; //  The signal strength.
        public ushort hits; //  The number of hits.
    }
#pragma warning restore 0649

/// <summary>
/// Information about Passing
/// </summary>
/// <remarks>
/// Passing event on the decoder.
/// </remarks>
public partial class Passing: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly PassingStruct _data;


    private EventData _handleWrapper;

    internal Passing(System.IntPtr nativePointer, EventData context)
    {
        _nativePointer = nativePointer;
        _data = (PassingStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(PassingStruct));

        _handleWrapper = context;
    }
	
	internal static Passing FromNativePointer(
        System.IntPtr pointerToNativeStruct, EventData context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new Passing(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<Passing> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, EventData context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<Passing>(
            System.Array.ConvertAll<System.IntPtr,Passing>(ptrArray,
                ptr => new Passing(ptr, context)));
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
    ///The time the passing event was received (in UTC).
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
    ///The unique ID of the system-setup this passing belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The loop ID for the passing event (if the loop ID is not set the value will be UINT32_MAX).
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
    ///The number of hits.
    ///</summary>
	public ushort Hits
    {
        get { return (ushort) _data.hits; }
    }

    ///<summary>
    ///Get the timezone offset (in seconds).
    ///</summary>
    public int GetTimezoneOffset()
    {
        return (System.SByte)(((int) _data.flags >> 16) & 0xFF) * 15 * 60;

    }
    ///<summary>
    ///The quality of the passing, see #PASSINGQUALITY
    ///</summary>
    public uint GetQuality()
    {
        return (_data.flags >> 6) & 0x07;

    }
    ///<summary>
    ///Get the signal strength (in dBm).
    ///</summary>
    public double GetStrength()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.ConvertRssi2Dbm(_data.strength);

    }
    ///<summary>
    ///Did the transponder issue a low-battery warning?
    ///</summary>
    public bool IsLowBattery()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) PASSINGBITS.pbLowBattery);
    }
    ///<summary>
    ///Is this passing the result of a resend?
    ///</summary>
    public bool IsResend()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) PASSINGBITS.pbResend);
    }
    ///<summary>
    ///Is this passing the result of a decoder resend?
    ///</summary>
    public bool IsDecoderResend()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) PASSINGBITS.pbDecoderResend);
    }
    ///<summary>
    ///Is this passing on a loop in the pit?
    ///</summary>
    public bool IsInPit()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) PASSINGBITS.pbInPit);
    }
    ///<summary>
    ///Is the number of hits too low?
    ///</summary>
    public bool HasLowHitsWarning()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) PASSINGBITS.pbLowHitsWarning);
    }
    ///<summary>
    ///Is the signal-strength too low?
    ///</summary>
    public bool HasLowStrengthWarning()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) PASSINGBITS.pbLowStrengthWarning);
    }
    ///<summary>
    ///Get the transponder type (see #TRANSPONDERTYPE).
    ///</summary>
    public uint GetTransponderType()
    {
        return (_data.flags >> 24) & 0xFF;

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
/// Modifier for Passing
/// </summary>
/// <remarks>
/// Passing event on the decoder.
/// </remarks>
public class PassingModifier : MylapsSDK.MylapsSDKLibrary.GenericModifier<Passing>
{
    private readonly System.IntPtr _nativePointer;
    private PassingStruct _data;
    private EventData _handleWrapper;


    internal PassingModifier(System.IntPtr nativePointer, EventData context):
        base(nativePointer)
	{
	    _nativePointer = nativePointer;
	    _handleWrapper = context;
		_data = (PassingStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(nativePointer, typeof(PassingStruct));
	}

    ///<summary>
    ///The time the passing event was received (in UTC).
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
    ///The unique ID of the system-setup this passing belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The loop ID for the passing event (if the loop ID is not set the value will be UINT32_MAX).
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
    ///The number of hits.
    ///</summary>
	public ushort Hits
    {
        get { return (ushort) _data.hits; }
    }


    ///<summary>
    ///Get the timezone offset (in seconds).
    ///</summary>
    public int GetTimezoneOffset()
    {
        return (System.SByte)(((int) _data.flags >> 16) & 0xFF) * 15 * 60;

    }
    ///<summary>
    ///The quality of the passing, see #PASSINGQUALITY
    ///</summary>
    public uint GetQuality()
    {
        return (_data.flags >> 6) & 0x07;

    }
    ///<summary>
    ///Get the signal strength (in dBm).
    ///</summary>
    public double GetStrength()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.ConvertRssi2Dbm(_data.strength);

    }
    ///<summary>
    ///Did the transponder issue a low-battery warning?
    ///</summary>
    public bool IsLowBattery()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) PASSINGBITS.pbLowBattery);
    }
    ///<summary>
    ///Is this passing the result of a resend?
    ///</summary>
    public bool IsResend()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) PASSINGBITS.pbResend);
    }
    ///<summary>
    ///Is this passing the result of a decoder resend?
    ///</summary>
    public bool IsDecoderResend()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) PASSINGBITS.pbDecoderResend);
    }
    ///<summary>
    ///Is this passing on a loop in the pit?
    ///</summary>
    public bool IsInPit()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) PASSINGBITS.pbInPit);
    }
    ///<summary>
    ///Is the number of hits too low?
    ///</summary>
    public bool HasLowHitsWarning()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) PASSINGBITS.pbLowHitsWarning);
    }
    ///<summary>
    ///Is the signal-strength too low?
    ///</summary>
    public bool HasLowStrengthWarning()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) PASSINGBITS.pbLowStrengthWarning);
    }
    ///<summary>
    ///Get the transponder type (see #TRANSPONDERTYPE).
    ///</summary>
    public uint GetTransponderType()
    {
        return (_data.flags >> 24) & 0xFF;

    }
    ///<summary>
    ///Get the user-flags for a specific key (0..3).
    ///</summary>
    public byte GetUserFlags(byte key)
    {
        return (byte)((_data.userflags >> ((key & 0x03) * 8)) & 0xFF);

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