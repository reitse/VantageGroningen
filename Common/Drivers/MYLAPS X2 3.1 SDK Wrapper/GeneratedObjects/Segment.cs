namespace MylapsSDK.Objects
{

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// A part of the track between two loops.
    internal struct SegmentStruct
    {
        public long length; //  The length of the segment in micro-meters.
        public uint id; //  The unique identifier of the segment.
        public uint systemsetupid; //  The unique ID of the system-setup this segment belongs to.
        public uint loopid1; //  The ID of the first loop that defines the segment.
        public uint loopid2; //  The ID of the second loop that defines the segment.
        public uint flags; //  Misc. flags of the segment.
        public ushort bitmaskx; //  The x-position of the bitmask (in pixels).
        public ushort bitmasky; //  The y-position of the bitmask (in pixels).
        public ushort bitmaskwidth; //  The width of the bitmask (in pixels, always a multiple of 8 pixels).
        public ushort bitmaskheight; //  The heighty of the bitmask (in pixels).
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 16)]
        public byte[] name; //  The name of the segment in the system-setup. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 50)]
        public byte[] description; //  A segment description. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 2)]
        public byte[] padding; //  padding
    }
#pragma warning restore 0649

/// <summary>
/// Information about Segment
/// </summary>
/// <remarks>
/// A part of the track between two loops.
/// </remarks>
public partial class Segment: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly SegmentStruct _data;

    private readonly string _name;
    private readonly string _description;

    private MTA _handleWrapper;

    internal Segment(System.IntPtr nativePointer, MTA context)
    {
        _nativePointer = nativePointer;
        _data = (SegmentStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(SegmentStruct));

        _handleWrapper = context;
        _name = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.name);
        _description = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.description);
    }
	
	internal static Segment FromNativePointer(
        System.IntPtr pointerToNativeStruct, MTA context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new Segment(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<Segment> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, MTA context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<Segment>(
            System.Array.ConvertAll<System.IntPtr,Segment>(ptrArray,
                ptr => new Segment(ptr, context)));
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
    ///The length of the segment in micro-meters.
    ///</summary>
	public long Length
    {
        get { return (long) _data.length; }
    }
    ///<summary>
    ///The unique identifier of the segment.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The unique ID of the system-setup this segment belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The ID of the first loop that defines the segment.
    ///</summary>
	public uint LoopID1
    {
        get { return (uint) _data.loopid1; }
    }
    ///<summary>
    ///The ID of the second loop that defines the segment.
    ///</summary>
	public uint LoopID2
    {
        get { return (uint) _data.loopid2; }
    }
    ///<summary>
    ///The x-position of the bitmask (in pixels).
    ///</summary>
	public ushort BitmaskX
    {
        get { return (ushort) _data.bitmaskx; }
    }
    ///<summary>
    ///The y-position of the bitmask (in pixels).
    ///</summary>
	public ushort BitmaskY
    {
        get { return (ushort) _data.bitmasky; }
    }
    ///<summary>
    ///The width of the bitmask (in pixels, always a multiple of 8 pixels).
    ///</summary>
	public ushort BitmaskWidth
    {
        get { return (ushort) _data.bitmaskwidth; }
    }
    ///<summary>
    ///The heighty of the bitmask (in pixels).
    ///</summary>
	public ushort BitmaskHeight
    {
        get { return (ushort) _data.bitmaskheight; }
    }
    ///<summary>
    ///The name of the segment in the system-setup. The character data is in UTF-8 encoding.
    ///</summary>
    public string Name
    {
        get { return _name; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///A segment description. The character data is in UTF-8 encoding.
    ///</summary>
    public string Description
    {
        get { return _description; } // return local datamember which is an utf8 encoded string
    }

    ///<summary>
    ///Is the short ID set?
    ///</summary>
    public bool IsBitmaskSet(ushort x, ushort y)
    {
        return MylapsSDKLibrary.NativeMethods.mta_segment_bitmask_bit_is_set(_handleWrapper.NativeHandle, _data.id, x, y);

    }

    public string GetUserData(string key, uint length)
    {
        var result = new System.Text.StringBuilder((int)length);
        if (MylapsSDKLibrary.NativeMethods.mta_segment_get_userdata(_handleWrapper.NativeHandle, _data.id, key, result, length))
            return result.ToString();
        else
            return null;
    }

    
}

/// <summary>
/// Modifier for Segment
/// </summary>
/// <remarks>
/// A part of the track between two loops.
/// </remarks>
public class SegmentModifier : MylapsSDK.MylapsSDKLibrary.GenericModifier<Segment>
{
    private readonly System.IntPtr _nativePointer;
    private SegmentStruct _data;
    private MTA _handleWrapper;

    private readonly string _name;
    private readonly string _description;

    internal SegmentModifier(System.IntPtr nativePointer, MTA context):
        base(nativePointer)
	{
	    _nativePointer = nativePointer;
	    _handleWrapper = context;
		_data = (SegmentStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(nativePointer, typeof(SegmentStruct));
        _name = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.name);
        _description = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.description);
	}

    ///<summary>
    ///The length of the segment in micro-meters.
    ///</summary>
	public long Length
    {
        get { return (long) _data.length; }
    }
    ///<summary>
    ///The unique identifier of the segment.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The unique ID of the system-setup this segment belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The ID of the first loop that defines the segment.
    ///</summary>
	public uint LoopID1
    {
        get { return (uint) _data.loopid1; }
    }
    ///<summary>
    ///The ID of the second loop that defines the segment.
    ///</summary>
	public uint LoopID2
    {
        get { return (uint) _data.loopid2; }
    }
    ///<summary>
    ///The x-position of the bitmask (in pixels).
    ///</summary>
	public ushort BitmaskX
    {
        get { return (ushort) _data.bitmaskx; }
    }
    ///<summary>
    ///The y-position of the bitmask (in pixels).
    ///</summary>
	public ushort BitmaskY
    {
        get { return (ushort) _data.bitmasky; }
    }
    ///<summary>
    ///The width of the bitmask (in pixels, always a multiple of 8 pixels).
    ///</summary>
	public ushort BitmaskWidth
    {
        get { return (ushort) _data.bitmaskwidth; }
    }
    ///<summary>
    ///The heighty of the bitmask (in pixels).
    ///</summary>
	public ushort BitmaskHeight
    {
        get { return (ushort) _data.bitmaskheight; }
    }
    ///<summary>
    ///The name of the segment in the system-setup. The character data is in UTF-8 encoding.
    ///</summary>
    public string Name
    {
        get { return _name; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///A segment description. The character data is in UTF-8 encoding.
    ///</summary>
    public string Description
    {
        get { return _description; } // return local datamember which is an utf8 encoded string
    }


    ///<summary>
    ///Is the short ID set?
    ///</summary>
    public bool IsBitmaskSet(ushort x, ushort y)
    {
        return MylapsSDKLibrary.NativeMethods.mta_segment_bitmask_bit_is_set(_handleWrapper.NativeHandle, _data.id, x, y);

    }

    ///<summary>
    ///Set the name for this segment.
    ///</summary>
    public void SetName (string name) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(name,_data.name);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the description for this segment.
    ///</summary>
    public void SetDescription (string description) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(description,_data.description);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the ID of the first loop that defines the segment.
    ///</summary>
    public void SetLoopID1 (uint id) // setter function
    {
		_data.loopid1 = (uint) id;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the ID of the second loop that defines the segment.
    ///</summary>
    public void SetLoopID2 (uint id) // setter function
    {
		_data.loopid2 = (uint) id;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the length of the segment in micro-meters.
    ///</summary>
    public void SetLength (long length) // setter function
    {
		_data.length = (long) length;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }

    public string GetUserData(string key, uint length)
    {
        var result = new System.Text.StringBuilder((int)length);
        if (MylapsSDKLibrary.NativeMethods.mta_segment_get_userdata(_handleWrapper.NativeHandle, _data.id, key, result, length))
            return result.ToString();
        else
            return null;
    }

    public bool AddUserData(string key, string value)
    {
        return MylapsSDKLibrary.NativeMethods.mta_segment_add_userdata(_handleWrapper.NativeHandle, _nativePointer, key, value);
    }

    public bool RemoveUserData(string key)
    {
        return MylapsSDKLibrary.NativeMethods.mta_segment_remove_userdata(_handleWrapper.NativeHandle, _nativePointer, key);
    }

    

    
}

}