namespace MylapsSDK.Objects
{
    public enum DRIVERINFOBITS {
        dibResend = 0 // Is this driver-information the result of a resend?
    }

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// Information about the current driver.
    internal struct DriverInfoStruct
    {
        public long utcbegintime; //  The begin time the driver information is valid (in UTC).
        public long utcendtime; //  The end time the driver information is still valid (in UTC). The end time is _MDP_INVALID_TIME if this is the current driver.
        public uint id; //  The unique identifier.
        public uint systemsetupid; //  The unique ID of the system-setup this driver information belongs to.
        public uint flags; //  Misc. flags.
        public uint transponderid; //  The transponder ID for this driver information.
        public uint driverid; //  The driver unique identification number (e.g. the transponder number and 'driver ID' combined in the case of a DP-I).
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 4)]
        public byte[] padding; //  padding
    }
#pragma warning restore 0649

/// <summary>
/// Information about Driver Information
/// </summary>
/// <remarks>
/// Information about the current driver.
/// </remarks>
public partial class DriverInfo: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly DriverInfoStruct _data;


    private EventData _handleWrapper;

    internal DriverInfo(System.IntPtr nativePointer, EventData context)
    {
        _nativePointer = nativePointer;
        _data = (DriverInfoStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(DriverInfoStruct));

        _handleWrapper = context;
    }
	
	internal static DriverInfo FromNativePointer(
        System.IntPtr pointerToNativeStruct, EventData context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new DriverInfo(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<DriverInfo> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, EventData context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<DriverInfo>(
            System.Array.ConvertAll<System.IntPtr,DriverInfo>(ptrArray,
                ptr => new DriverInfo(ptr, context)));
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
    ///The begin time the driver information is valid (in UTC).
    ///</summary>
	public long UTCBeginTime
    {
        get { return (long) _data.utcbegintime; }
    }
    ///<summary>
    ///The end time the driver information is still valid (in UTC). The end time is _MDP_INVALID_TIME if this is the current driver.
    ///</summary>
	public long UTCEndTime
    {
        get { return (long) _data.utcendtime; }
    }
    ///<summary>
    ///The unique identifier.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The unique ID of the system-setup this driver information belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The transponder ID for this driver information.
    ///</summary>
	public uint TransponderID
    {
        get { return (uint) _data.transponderid; }
    }
    ///<summary>
    ///The driver unique identification number (e.g. the transponder number and 'driver ID' combined in the case of a DP-I).
    ///</summary>
	public uint DriverID
    {
        get { return (uint) _data.driverid; }
    }

    ///<summary>
    ///Is this driver information the result of a resend?
    ///</summary>
    public bool IsResend()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) DRIVERINFOBITS.dibResend);
    }
    ///<summary>
    ///Get the timezone offset (in seconds).
    ///</summary>
    public int GetTimezoneOffset()
    {
        return (System.SByte)((int)(_data.flags >> 8) & 0xFF) * 15 * 60;

    }
    ///<summary>
    ///Get the begin time-of-day.
    ///</summary>
    public long GetBeginTimeOfDay()
    {
        if (_data.utcbegintime == MylapsSDK.Utilities.MdpTime.InvalidTime)
	return _data.utcbegintime;
else
	return _data.utcbegintime + GetTimezoneOffset() * MylapsSDK.Utilities.MdpTime.Second;

    }
    ///<summary>
    ///Get the end time-of-day. The end time is _MDP_INVALID_TIME if this is the current driver.
    ///</summary>
    public long GetEndTimeOfDay()
    {
        if (_data.utcendtime == MylapsSDK.Utilities.MdpTime.InvalidTime)
	return _data.utcendtime;
else
	return _data.utcendtime + GetTimezoneOffset() * MylapsSDK.Utilities.MdpTime.Second;
    }


    
}

}