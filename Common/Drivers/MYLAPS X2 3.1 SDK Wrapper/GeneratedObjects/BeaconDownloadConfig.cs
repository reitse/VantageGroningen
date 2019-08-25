namespace MylapsSDK.Objects
{

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// The configuration for the beacon download (e.g. download beacon-log from all transponders after the start-pulse).
    internal struct BeaconDownloadConfigStruct
    {
        public uint id; //  The unique id.
        public uint systemsetupid; //  The unique ID of the system-setup this beacon-data belongs to.
        public uint loopid; //  The unique loop identifier used to create a trigger on a passing on a specific loop.
        public uint ioterminalid; //  The unique I/O terminal identifier used to create a trigger on an I/O event on a specific I/O terminal.
        public uint type; //  The type. See #BEACONDOWNLOADTYPE
        public uint transponderid; //  The transponder number used to create a trigger-on-passing.
        public uint transpondershortid; //  The 'short' (user-definable) transponder number used to create a trigger-on-passing.
        public uint resolution; //  The resolution in micro-seconds of the beacon download (can be 10ms or 100ms).
        public uint timeout; //  The timeout in micro-seconds before the beacon-download will be marked as failed (maximum of 720 seconds).
        public uint offset; //  The time to download the beacon-log in micro-seconds -before- the trigger.
        public uint duration; //  The total time to download the beacon-log in micro-seconds.
        public byte input; //  The input on the I/O terminal used to create the trigger.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 3)]
        public byte[] padding; //  padding
    }
#pragma warning restore 0649

/// <summary>
/// Information about Beacon-download configuration
/// </summary>
/// <remarks>
/// The configuration for the beacon download (e.g. download beacon-log from all transponders after the start-pulse).
/// </remarks>
public partial class BeaconDownloadConfig: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly BeaconDownloadConfigStruct _data;


    private MTA _handleWrapper;

    internal BeaconDownloadConfig(System.IntPtr nativePointer, MTA context)
    {
        _nativePointer = nativePointer;
        _data = (BeaconDownloadConfigStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(BeaconDownloadConfigStruct));

        _handleWrapper = context;
    }
	
	internal static BeaconDownloadConfig FromNativePointer(
        System.IntPtr pointerToNativeStruct, MTA context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new BeaconDownloadConfig(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<BeaconDownloadConfig> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, MTA context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<BeaconDownloadConfig>(
            System.Array.ConvertAll<System.IntPtr,BeaconDownloadConfig>(ptrArray,
                ptr => new BeaconDownloadConfig(ptr, context)));
    }

    internal System.IntPtr NativePointer
    {
        get { return _nativePointer; }
    }

    internal MTA Context
    {
        get { return _handleWrapper; }
    }

    ///<summary>
    ///The unique id.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The unique ID of the system-setup this beacon-data belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The unique loop identifier used to create a trigger on a passing on a specific loop.
    ///</summary>
	public uint LoopID
    {
        get { return (uint) _data.loopid; }
    }
    ///<summary>
    ///The unique I/O terminal identifier used to create a trigger on an I/O event on a specific I/O terminal.
    ///</summary>
	public uint IOTerminalID
    {
        get { return (uint) _data.ioterminalid; }
    }
    ///<summary>
    ///The type. See #BEACONDOWNLOADTYPE
    ///</summary>
	public uint Type
    {
        get { return (uint) _data.type; }
    }
    ///<summary>
    ///The transponder number used to create a trigger-on-passing.
    ///</summary>
	public uint TransponderID
    {
        get { return (uint) _data.transponderid; }
    }
    ///<summary>
    ///The 'short' (user-definable) transponder number used to create a trigger-on-passing.
    ///</summary>
	public uint TransponderShortID
    {
        get { return (uint) _data.transpondershortid; }
    }
    ///<summary>
    ///The resolution in micro-seconds of the beacon download (can be 10ms or 100ms).
    ///</summary>
	public uint Resolution
    {
        get { return (uint) _data.resolution; }
    }
    ///<summary>
    ///The timeout in micro-seconds before the beacon-download will be marked as failed (maximum of 720 seconds).
    ///</summary>
	public uint Timeout
    {
        get { return (uint) _data.timeout; }
    }
    ///<summary>
    ///The time to download the beacon-log in micro-seconds -before- the trigger.
    ///</summary>
	public uint Offset
    {
        get { return (uint) _data.offset; }
    }
    ///<summary>
    ///The total time to download the beacon-log in micro-seconds.
    ///</summary>
	public uint Duration
    {
        get { return (uint) _data.duration; }
    }
    ///<summary>
    ///The input on the I/O terminal used to create the trigger.
    ///</summary>
	public byte Input
    {
        get { return (byte) _data.input; }
    }



    
}

/// <summary>
/// Modifier for Beacon-download configuration
/// </summary>
/// <remarks>
/// The configuration for the beacon download (e.g. download beacon-log from all transponders after the start-pulse).
/// </remarks>
public class BeaconDownloadConfigModifier : MylapsSDK.MylapsSDKLibrary.GenericModifier<BeaconDownloadConfig>
{
    private readonly System.IntPtr _nativePointer;
    private BeaconDownloadConfigStruct _data;
    private MTA _handleWrapper;


    internal BeaconDownloadConfigModifier(System.IntPtr nativePointer, MTA context):
        base(nativePointer)
	{
	    _nativePointer = nativePointer;
	    _handleWrapper = context;
		_data = (BeaconDownloadConfigStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(nativePointer, typeof(BeaconDownloadConfigStruct));
	}

    ///<summary>
    ///The unique id.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The unique ID of the system-setup this beacon-data belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The unique loop identifier used to create a trigger on a passing on a specific loop.
    ///</summary>
	public uint LoopID
    {
        get { return (uint) _data.loopid; }
    }
    ///<summary>
    ///The unique I/O terminal identifier used to create a trigger on an I/O event on a specific I/O terminal.
    ///</summary>
	public uint IOTerminalID
    {
        get { return (uint) _data.ioterminalid; }
    }
    ///<summary>
    ///The type. See #BEACONDOWNLOADTYPE
    ///</summary>
	public uint Type
    {
        get { return (uint) _data.type; }
    }
    ///<summary>
    ///The transponder number used to create a trigger-on-passing.
    ///</summary>
	public uint TransponderID
    {
        get { return (uint) _data.transponderid; }
    }
    ///<summary>
    ///The 'short' (user-definable) transponder number used to create a trigger-on-passing.
    ///</summary>
	public uint TransponderShortID
    {
        get { return (uint) _data.transpondershortid; }
    }
    ///<summary>
    ///The resolution in micro-seconds of the beacon download (can be 10ms or 100ms).
    ///</summary>
	public uint Resolution
    {
        get { return (uint) _data.resolution; }
    }
    ///<summary>
    ///The timeout in micro-seconds before the beacon-download will be marked as failed (maximum of 720 seconds).
    ///</summary>
	public uint Timeout
    {
        get { return (uint) _data.timeout; }
    }
    ///<summary>
    ///The time to download the beacon-log in micro-seconds -before- the trigger.
    ///</summary>
	public uint Offset
    {
        get { return (uint) _data.offset; }
    }
    ///<summary>
    ///The total time to download the beacon-log in micro-seconds.
    ///</summary>
	public uint Duration
    {
        get { return (uint) _data.duration; }
    }
    ///<summary>
    ///The input on the I/O terminal used to create the trigger.
    ///</summary>
	public byte Input
    {
        get { return (byte) _data.input; }
    }



    ///<summary>
    ///Set the type, see #BEACONDOWNLOADTYPE.
    ///</summary>
    public void SetBeaconDownloadConfigType (uint type) // setter function
    {
		_data.type = (uint) type;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the unique loop identifier.
    ///</summary>
    public void SetLoopID (uint loopID) // setter function
    {
		_data.loopid = (uint) loopID;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the transponder number.
    ///</summary>
    public void SetTransponderID (uint transponder) // setter function
    {
		_data.transponderid = (uint) transponder;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the transponder short ID.
    ///</summary>
    public void SetShortID (uint id) // setter function
    {
		_data.transpondershortid = (uint) id;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the unique I/O terminal identifier.
    ///</summary>
    public void SetIOTerminalID (uint ioTerminalID) // setter function
    {
		_data.ioterminalid = (uint) ioTerminalID;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the I/O terminal input.
    ///</summary>
    public void SetInput (byte input) // setter function
    {
		_data.input = (byte) input;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the resolution.
    ///</summary>
    public void SetResolution (uint resolution) // setter function
    {
		_data.resolution = (uint) resolution;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the time-out.
    ///</summary>
    public void SetTimeout (uint timeout) // setter function
    {
		_data.timeout = (uint) timeout;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the offset.
    ///</summary>
    public void SetOffset (uint offset) // setter function
    {
		_data.offset = (uint) offset;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the duration.
    ///</summary>
    public void SetDuration (uint duration) // setter function
    {
		_data.duration = (uint) duration;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }


    

    
}

}