namespace MylapsSDK.Objects
{
    public enum DECODERPRESETGROUPFLAGS {
        dpgfDefault = 0 // Is this the default decoder-preset-group for this sport.
    }

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// Some common information about a group of decoder-presets.
    internal struct DecoderPresetGroupStruct
    {
        public uint id; //  The decoder-preset-group ID.
        public uint flags; //  Misc. flags.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 32)]
        public byte[] name; //  The decoder-preset-group name. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 64)]
        public byte[] description; //  A decoder-preset-group description. The character data is in UTF-8 encoding.
    }
#pragma warning restore 0649

/// <summary>
/// Information about Decoder-preset-group
/// </summary>
/// <remarks>
/// Some common information about a group of decoder-presets.
/// </remarks>
public partial class DecoderPresetGroup: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly DecoderPresetGroupStruct _data;

    private readonly string _name;
    private readonly string _description;

    private MTA _handleWrapper;

    internal DecoderPresetGroup(System.IntPtr nativePointer, MTA context)
    {
        _nativePointer = nativePointer;
        _data = (DecoderPresetGroupStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(DecoderPresetGroupStruct));

        _handleWrapper = context;
        _name = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.name);
        _description = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.description);
    }
	
	internal static DecoderPresetGroup FromNativePointer(
        System.IntPtr pointerToNativeStruct, MTA context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new DecoderPresetGroup(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<DecoderPresetGroup> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, MTA context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<DecoderPresetGroup>(
            System.Array.ConvertAll<System.IntPtr,DecoderPresetGroup>(ptrArray,
                ptr => new DecoderPresetGroup(ptr, context)));
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
    ///The decoder-preset-group ID.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The decoder-preset-group name. The character data is in UTF-8 encoding.
    ///</summary>
    public string Name
    {
        get { return _name; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///A decoder-preset-group description. The character data is in UTF-8 encoding.
    ///</summary>
    public string Description
    {
        get { return _description; } // return local datamember which is an utf8 encoded string
    }

    ///<summary>
    ///Is this the default decoder-preset-group for the given sport?
    ///</summary>
    public bool IsCurrent()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) DECODERPRESETGROUPFLAGS.dpgfDefault);
    }
    ///<summary>
    ///Get the sport for this decoder-preset-group.
    ///</summary>
    public byte GetSport()
    {
        return (System.Byte)((int)(_data.flags >> 16) & 0xFF);

    }


    
}

}