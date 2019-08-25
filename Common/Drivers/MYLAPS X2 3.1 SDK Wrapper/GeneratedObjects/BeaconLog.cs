namespace MylapsSDK.Objects
{

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// A collection of beacondata (measurements).
    internal struct BeaconLogStruct
    {
        public long utcbegintime; //  The UTC begin time of the log.
        public long utcendtime; //  The UTC end time of the log.
        public long begintimeofday; //  The begin time-of-day of the log.
        public long endtimeofday; //  The end time-of-day of the log.
        public uint id; //  The unique beaconlog ID.
        public uint beaconid; //  The beacon ID.
        public uint systemsetupid; //  The unique ID of the system-setup this beacon-log belongs to.
        public uint triggerid; //  The trigger ID for this log.
        public uint transponderid; //  The transponder number.
        public uint transpondershortid; //  The 'short' (user-definable) transponder number.
        public uint resolution; //  The resolution in micro-seconds.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 4)]
        public byte[] padding; //  padding
    }
#pragma warning restore 0649

/// <summary>
/// Information about Beacon log
/// </summary>
/// <remarks>
/// A collection of beacondata (measurements).
/// </remarks>
public partial class BeaconLog: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly BeaconLogStruct _data;


    private EventData _handleWrapper;

    internal BeaconLog(System.IntPtr nativePointer, EventData context)
    {
        _nativePointer = nativePointer;
        _data = (BeaconLogStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(BeaconLogStruct));

        _handleWrapper = context;
    }
	
	internal static BeaconLog FromNativePointer(
        System.IntPtr pointerToNativeStruct, EventData context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new BeaconLog(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<BeaconLog> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, EventData context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<BeaconLog>(
            System.Array.ConvertAll<System.IntPtr,BeaconLog>(ptrArray,
                ptr => new BeaconLog(ptr, context)));
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
    ///The UTC begin time of the log.
    ///</summary>
	public long UTCBeginTime
    {
        get { return (long) _data.utcbegintime; }
    }
    ///<summary>
    ///The UTC end time of the log.
    ///</summary>
	public long UTCEndTime
    {
        get { return (long) _data.utcendtime; }
    }
    ///<summary>
    ///The begin time-of-day of the log.
    ///</summary>
	public long BeginTimeOfDay
    {
        get { return (long) _data.begintimeofday; }
    }
    ///<summary>
    ///The end time-of-day of the log.
    ///</summary>
	public long EndTimeOfDay
    {
        get { return (long) _data.endtimeofday; }
    }
    ///<summary>
    ///The unique beaconlog ID.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The beacon ID.
    ///</summary>
	public uint BeaconID
    {
        get { return (uint) _data.beaconid; }
    }
    ///<summary>
    ///The unique ID of the system-setup this beacon-log belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The trigger ID for this log.
    ///</summary>
	public uint TriggerID
    {
        get { return (uint) _data.triggerid; }
    }
    ///<summary>
    ///The transponder number.
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
    ///The resolution in micro-seconds.
    ///</summary>
	public uint Resolution
    {
        get { return (uint) _data.resolution; }
    }



    
}

}