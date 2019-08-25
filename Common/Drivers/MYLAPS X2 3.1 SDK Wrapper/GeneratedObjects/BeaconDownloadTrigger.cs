namespace MylapsSDK.Objects
{
    public enum BEACONDOWNLOADTYPE {
        bdtNone = 0, // No type.
        bdtAuxEvent, // Auxiliary event for a specific transponder.
        bdtPassing, // Passing for a specific transponder.
        bdtAuxEventAllTx, // Auxiliary event for all transponders.
        bdtPassingAllTx, // Passing for all transponders.
        bdtPassingFixedTime // Passing for a specific transponder with a fixed time.
    }

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// The trigger of triggered the start of a beacon download.
    internal struct BeaconDownloadTriggerStruct
    {
        public long utctime; //  The UTC time of the trigger.
        public long timeofday; //  The time-of-day of the trigger.
        public uint id; //  The unique ID.
        public uint sourceid; //  The source ID that created the trigger (passing ID or auxiliary-event ID).
        public uint systemsetupid; //  The unique ID of the system-setup this beacon-download trigger belongs to.
        public uint loopid; //  The loop ID (only filled if this is a trigger on transponder input).
        public uint ioterminalid; //  The I/O terminal ID (only filled if this is a trigger on auxiliary event).
        public uint transponderid; //  The transponder number.
        public uint transpondershortid; //  The 'short' (user-definable) transponder number.
        public uint type; //  The type (see #BEACONDOWNLOADTYPE enumeration).
        public byte input; //  The input channel (0..7). Only filled if this is a trigger on an auxiliary event.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 7)]
        public byte[] padding; //  padding
    }
#pragma warning restore 0649

/// <summary>
/// Information about Beacon-download trigger
/// </summary>
/// <remarks>
/// The trigger of triggered the start of a beacon download.
/// </remarks>
public partial class BeaconDownloadTrigger: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly BeaconDownloadTriggerStruct _data;


    private EventData _handleWrapper;

    internal BeaconDownloadTrigger(System.IntPtr nativePointer, EventData context)
    {
        _nativePointer = nativePointer;
        _data = (BeaconDownloadTriggerStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(BeaconDownloadTriggerStruct));

        _handleWrapper = context;
    }
	
	internal static BeaconDownloadTrigger FromNativePointer(
        System.IntPtr pointerToNativeStruct, EventData context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new BeaconDownloadTrigger(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<BeaconDownloadTrigger> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, EventData context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<BeaconDownloadTrigger>(
            System.Array.ConvertAll<System.IntPtr,BeaconDownloadTrigger>(ptrArray,
                ptr => new BeaconDownloadTrigger(ptr, context)));
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
    ///The UTC time of the trigger.
    ///</summary>
	public long UTCTime
    {
        get { return (long) _data.utctime; }
    }
    ///<summary>
    ///The time-of-day of the trigger.
    ///</summary>
	public long TimeOfDay
    {
        get { return (long) _data.timeofday; }
    }
    ///<summary>
    ///The unique ID.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The source ID that created the trigger (passing ID or auxiliary-event ID).
    ///</summary>
	public uint SourceID
    {
        get { return (uint) _data.sourceid; }
    }
    ///<summary>
    ///The unique ID of the system-setup this beacon-download trigger belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The loop ID (only filled if this is a trigger on transponder input).
    ///</summary>
	public uint LoopID
    {
        get { return (uint) _data.loopid; }
    }
    ///<summary>
    ///The I/O terminal ID (only filled if this is a trigger on auxiliary event).
    ///</summary>
	public uint IOTerminalID
    {
        get { return (uint) _data.ioterminalid; }
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
    ///The type (see #BEACONDOWNLOADTYPE enumeration).
    ///</summary>
	public uint Type
    {
        get { return (uint) _data.type; }
    }
    ///<summary>
    ///The input channel (0..7). Only filled if this is a trigger on an auxiliary event.
    ///</summary>
	public byte Input
    {
        get { return (byte) _data.input; }
    }



    
}

}