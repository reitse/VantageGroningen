namespace MylapsSDK.Objects
{
    public enum SEQUENCEFLAGS {
        sqfInPit = 0 // This sequence is in the pit.
    }

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// A sequence of unique segments within a track-solution.
    internal struct SequenceStruct
    {
        public uint id; //  The unique ID of the sequence.
        public uint systemsetupid; //  The unique ID of the system-setup this sequence belongs to.
        public uint tracksolutionid; //  The unique ID of the track-solution this sequence belongs to.
        public uint flags; //  Misc. flags of the sequence.
        public uint weight; //  The weight of this sequence (higher is less likely to occur).
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 16)]
        public byte[] name; //  The name of the sequence in the track-solution (e.g. 'Track'). The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 56)]
        public byte[] description; //  A sequence description. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 4)]
        public byte[] padding; //  padding
    }
#pragma warning restore 0649

/// <summary>
/// Information about Sequence
/// </summary>
/// <remarks>
/// A sequence of unique segments within a track-solution.
/// </remarks>
public partial class Sequence: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly SequenceStruct _data;

    private readonly string _name;
    private readonly string _description;

    private MTA _handleWrapper;

    internal Sequence(System.IntPtr nativePointer, MTA context)
    {
        _nativePointer = nativePointer;
        _data = (SequenceStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(SequenceStruct));

        _handleWrapper = context;
        _name = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.name);
        _description = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.description);
    }
	
	internal static Sequence FromNativePointer(
        System.IntPtr pointerToNativeStruct, MTA context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new Sequence(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<Sequence> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, MTA context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<Sequence>(
            System.Array.ConvertAll<System.IntPtr,Sequence>(ptrArray,
                ptr => new Sequence(ptr, context)));
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
    ///The unique ID of the sequence.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The unique ID of the system-setup this sequence belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The unique ID of the track-solution this sequence belongs to.
    ///</summary>
	public uint TrackSolutionID
    {
        get { return (uint) _data.tracksolutionid; }
    }
    ///<summary>
    ///The weight of this sequence (higher is less likely to occur).
    ///</summary>
	public uint Weight
    {
        get { return (uint) _data.weight; }
    }
    ///<summary>
    ///The name of the sequence in the track-solution (e.g. 'Track'). The character data is in UTF-8 encoding.
    ///</summary>
    public string Name
    {
        get { return _name; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///A sequence description. The character data is in UTF-8 encoding.
    ///</summary>
    public string Description
    {
        get { return _description; } // return local datamember which is an utf8 encoded string
    }

    ///<summary>
    ///Is the sequence in the pit?
    ///</summary>
    public bool IsInPit()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) SEQUENCEFLAGS.sqfInPit);
    }


    
}

/// <summary>
/// Modifier for Sequence
/// </summary>
/// <remarks>
/// A sequence of unique segments within a track-solution.
/// </remarks>
public class SequenceModifier : MylapsSDK.MylapsSDKLibrary.GenericModifier<Sequence>
{
    private readonly System.IntPtr _nativePointer;
    private SequenceStruct _data;
    private MTA _handleWrapper;

    private readonly string _name;
    private readonly string _description;

    internal SequenceModifier(System.IntPtr nativePointer, MTA context):
        base(nativePointer)
	{
	    _nativePointer = nativePointer;
	    _handleWrapper = context;
		_data = (SequenceStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(nativePointer, typeof(SequenceStruct));
        _name = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.name);
        _description = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.description);
	}

    ///<summary>
    ///The unique ID of the sequence.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The unique ID of the system-setup this sequence belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The unique ID of the track-solution this sequence belongs to.
    ///</summary>
	public uint TrackSolutionID
    {
        get { return (uint) _data.tracksolutionid; }
    }
    ///<summary>
    ///The weight of this sequence (higher is less likely to occur).
    ///</summary>
	public uint Weight
    {
        get { return (uint) _data.weight; }
    }
    ///<summary>
    ///The name of the sequence in the track-solution (e.g. 'Track'). The character data is in UTF-8 encoding.
    ///</summary>
    public string Name
    {
        get { return _name; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///A sequence description. The character data is in UTF-8 encoding.
    ///</summary>
    public string Description
    {
        get { return _description; } // return local datamember which is an utf8 encoded string
    }


    ///<summary>
    ///Is the sequence in the pit?
    ///</summary>
    public bool IsInPit()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) SEQUENCEFLAGS.sqfInPit);
    }

    ///<summary>
    ///Set the track-solution ID of the sequence.
    ///</summary>
    public void SetTrackSolutionID (uint trackSolutionID) // setter function
    {
		_data.tracksolutionid = (uint) trackSolutionID;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the weight of this sequence (higher is less likely to occur).
    ///</summary>
    public void SetWeight (uint order) // setter function
    {
		_data.weight = (uint) order;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the in-pit option for this sequence.
    ///</summary>
    public void SetInPit (bool inPit) // setter function
    {
        _data.flags = MylapsSDK.Utilities.SDKHelperFunctions.SetOrClearBit(
            (uint) _data.flags, (int) SEQUENCEFLAGS.sqfInPit, inPit);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the name for this sequence
    ///</summary>
    public void SetName (string name) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(name,_data.name);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the description for this sequence
    ///</summary>
    public void SetDescription (string description) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(description,_data.description);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }


    

    
}

}