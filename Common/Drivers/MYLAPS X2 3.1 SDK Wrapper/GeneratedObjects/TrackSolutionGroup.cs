namespace MylapsSDK.Objects
{

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// Some common information about a group of track-solutions.
    internal struct TrackSolutionGroupStruct
    {
        public uint id; //  The track-solution-group ID.
        public uint systemsetupid; //  The unique ID of the system-setup this track-solution group belongs to.
        public uint flags; //  Misc. flags.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 16)]
        public byte[] name; //  The track-solution-group name. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 48)]
        public byte[] description; //  A track-solution-group description. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 4)]
        public byte[] padding; //  padding
    }
#pragma warning restore 0649

/// <summary>
/// Information about Track-solution group
/// </summary>
/// <remarks>
/// Some common information about a group of track-solutions.
/// </remarks>
public partial class TrackSolutionGroup: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly TrackSolutionGroupStruct _data;

    private readonly string _name;
    private readonly string _description;

    private MTA _handleWrapper;

    internal TrackSolutionGroup(System.IntPtr nativePointer, MTA context)
    {
        _nativePointer = nativePointer;
        _data = (TrackSolutionGroupStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(TrackSolutionGroupStruct));

        _handleWrapper = context;
        _name = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.name);
        _description = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.description);
    }
	
	internal static TrackSolutionGroup FromNativePointer(
        System.IntPtr pointerToNativeStruct, MTA context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new TrackSolutionGroup(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<TrackSolutionGroup> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, MTA context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<TrackSolutionGroup>(
            System.Array.ConvertAll<System.IntPtr,TrackSolutionGroup>(ptrArray,
                ptr => new TrackSolutionGroup(ptr, context)));
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
    ///The track-solution-group ID.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The unique ID of the system-setup this track-solution group belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The track-solution-group name. The character data is in UTF-8 encoding.
    ///</summary>
    public string Name
    {
        get { return _name; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///A track-solution-group description. The character data is in UTF-8 encoding.
    ///</summary>
    public string Description
    {
        get { return _description; } // return local datamember which is an utf8 encoded string
    }



    
}

/// <summary>
/// Modifier for Track-solution group
/// </summary>
/// <remarks>
/// Some common information about a group of track-solutions.
/// </remarks>
public class TrackSolutionGroupModifier : MylapsSDK.MylapsSDKLibrary.GenericModifier<TrackSolutionGroup>
{
    private readonly System.IntPtr _nativePointer;
    private TrackSolutionGroupStruct _data;
    private MTA _handleWrapper;

    private readonly string _name;
    private readonly string _description;

    internal TrackSolutionGroupModifier(System.IntPtr nativePointer, MTA context):
        base(nativePointer)
	{
	    _nativePointer = nativePointer;
	    _handleWrapper = context;
		_data = (TrackSolutionGroupStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(nativePointer, typeof(TrackSolutionGroupStruct));
        _name = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.name);
        _description = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.description);
	}

    ///<summary>
    ///The track-solution-group ID.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The unique ID of the system-setup this track-solution group belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The track-solution-group name. The character data is in UTF-8 encoding.
    ///</summary>
    public string Name
    {
        get { return _name; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///A track-solution-group description. The character data is in UTF-8 encoding.
    ///</summary>
    public string Description
    {
        get { return _description; } // return local datamember which is an utf8 encoded string
    }



    ///<summary>
    ///Set the name for this track-solution-group
    ///</summary>
    public void SetName (string name) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(name,_data.name);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the description for this track-solution-group
    ///</summary>
    public void SetDescription (string description) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(description,_data.description);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }


    

    
}

}