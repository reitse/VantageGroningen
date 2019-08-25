namespace MylapsSDK.Objects
{
    public enum TRACKSOLUTIONFLAGS {
        tsfInUse // The track-solution is currently in use.
    }

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// A track-solution within the system-setup.
    internal struct TrackSolutionStruct
    {
        public long length; //  The length of the track-solution in micro-meters.
        public uint id; //  The unique ID of the track-solution.
        public uint systemsetupid; //  The unique ID of the system-setup this track-solution belongs to.
        public uint flags; //  Misc. flags of the track-solution.
        public uint groupid; //  The unique ID of the track-solution-group.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 32)]
        public byte[] name; //  The name of the track-solution in the system-setup. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 112)]
        public byte[] description; //  A track-solution description. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 16)]
        public byte[] lapcounters; //  The lap-counter loop ID's.
    }
#pragma warning restore 0649

/// <summary>
/// Information about Track-solution
/// </summary>
/// <remarks>
/// A track-solution within the system-setup.
/// </remarks>
public partial class TrackSolution: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly TrackSolutionStruct _data;

    private readonly string _name;
    private readonly string _description;
    private readonly string _lapcounters;

    private MTA _handleWrapper;

    internal TrackSolution(System.IntPtr nativePointer, MTA context)
    {
        _nativePointer = nativePointer;
        _data = (TrackSolutionStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(TrackSolutionStruct));

        _handleWrapper = context;
        _name = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.name);
        _description = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.description);
        _lapcounters = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.lapcounters);
    }
	
	internal static TrackSolution FromNativePointer(
        System.IntPtr pointerToNativeStruct, MTA context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new TrackSolution(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<TrackSolution> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, MTA context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<TrackSolution>(
            System.Array.ConvertAll<System.IntPtr,TrackSolution>(ptrArray,
                ptr => new TrackSolution(ptr, context)));
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
    ///The length of the track-solution in micro-meters.
    ///</summary>
	public long Length
    {
        get { return (long) _data.length; }
    }
    ///<summary>
    ///The unique ID of the track-solution.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The unique ID of the system-setup this track-solution belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The unique ID of the track-solution-group.
    ///</summary>
	public uint GroupID
    {
        get { return (uint) _data.groupid; }
    }
    ///<summary>
    ///The name of the track-solution in the system-setup. The character data is in UTF-8 encoding.
    ///</summary>
    public string Name
    {
        get { return _name; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///A track-solution description. The character data is in UTF-8 encoding.
    ///</summary>
    public string Description
    {
        get { return _description; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The lap-counter loop ID's.
    ///</summary>
    public string LapCounters
    {
        get { return _lapcounters; } // return local datamember which is an utf8 encoded string
    }

    ///<summary>
    ///Is the track-solution in use?
    ///</summary>
    public bool IsInUse()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TRACKSOLUTIONFLAGS.tsfInUse);
    }
    ///<summary>
    ///The number of decimals used in e.g. the calculation of the sequence-row data (0..6).
    ///</summary>
    public uint GetNumberOfDecimals()
    {
        return (uint)((_data.flags >> 16) & 0x00000007);

    }
    ///<summary>
    ///Get the ID for a lap-counter loop. You can specify up to four lap-counters (index = 0..3).
    ///</summary>
    public uint GetLapCounter(uint index)
    {
        // FIXME
return 0xFFFFFFFF;

    }


    
}

/// <summary>
/// Modifier for Track-solution
/// </summary>
/// <remarks>
/// A track-solution within the system-setup.
/// </remarks>
public class TrackSolutionModifier : MylapsSDK.MylapsSDKLibrary.GenericModifier<TrackSolution>
{
    private readonly System.IntPtr _nativePointer;
    private TrackSolutionStruct _data;
    private MTA _handleWrapper;

    private readonly string _name;
    private readonly string _description;
    private readonly string _lapcounters;

    internal TrackSolutionModifier(System.IntPtr nativePointer, MTA context):
        base(nativePointer)
	{
	    _nativePointer = nativePointer;
	    _handleWrapper = context;
		_data = (TrackSolutionStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(nativePointer, typeof(TrackSolutionStruct));
        _name = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.name);
        _description = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.description);
        _lapcounters = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.lapcounters);
	}

    ///<summary>
    ///The length of the track-solution in micro-meters.
    ///</summary>
	public long Length
    {
        get { return (long) _data.length; }
    }
    ///<summary>
    ///The unique ID of the track-solution.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The unique ID of the system-setup this track-solution belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The unique ID of the track-solution-group.
    ///</summary>
	public uint GroupID
    {
        get { return (uint) _data.groupid; }
    }
    ///<summary>
    ///The name of the track-solution in the system-setup. The character data is in UTF-8 encoding.
    ///</summary>
    public string Name
    {
        get { return _name; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///A track-solution description. The character data is in UTF-8 encoding.
    ///</summary>
    public string Description
    {
        get { return _description; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The lap-counter loop ID's.
    ///</summary>
    public string LapCounters
    {
        get { return _lapcounters; } // return local datamember which is an utf8 encoded string
    }


    ///<summary>
    ///Is the track-solution in use?
    ///</summary>
    public bool IsInUse()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TRACKSOLUTIONFLAGS.tsfInUse);
    }
    ///<summary>
    ///The number of decimals used in e.g. the calculation of the sequence-row data (0..6).
    ///</summary>
    public uint GetNumberOfDecimals()
    {
        return (uint)((_data.flags >> 16) & 0x00000007);

    }
    ///<summary>
    ///Get the ID for a lap-counter loop. You can specify up to four lap-counters (index = 0..3).
    ///</summary>
    public uint GetLapCounter(uint index)
    {
        // FIXME
return 0xFFFFFFFF;

    }

    ///<summary>
    ///Set the length (in micro-meters).
    ///</summary>
    public void SetLength (long length) // setter function
    {
		_data.length = (long) length;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Mark the track-solution as in-use/unused.
    ///</summary>
    public void SetInUse (bool inUse) // setter function
    {
        _data.flags = MylapsSDK.Utilities.SDKHelperFunctions.SetOrClearBit(
            (uint) _data.flags, (int) TRACKSOLUTIONFLAGS.tsfInUse, inUse);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the unique ID of the track-solution-group.
    ///</summary>
    public void SetGroupID (uint groupID) // setter function
    {
		_data.groupid = (uint) groupID;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the name for this track-solution
    ///</summary>
    public void SetName (string name) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(name,_data.name);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the description for this track-solution
    ///</summary>
    public void SetDescription (string description) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(description,_data.description);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the number of decimals used in e.g. the calculation of the sequence-row data (0..6).
    ///</summary>
    public void SetNumberOfDecimals (uint numberOfDecimals) // setter function
    {
        	unchecked {
    // clear current number-of-decimals
    _data.flags &= (uint) ~(0x000070000);
	}
	_data.flags |= (uint)((numberOfDecimals & 0x7) << 16);

        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the ID for a lap-counter loop. You can specify up to four lap-counters (index = 0..3).
    ///</summary>
    public void SetLapCounter (uint index, uint loopid) // setter function
    {
        // FIXME
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }


    

    
}

}