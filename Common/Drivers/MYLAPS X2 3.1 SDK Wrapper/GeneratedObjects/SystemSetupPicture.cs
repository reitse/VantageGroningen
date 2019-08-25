namespace MylapsSDK.Objects
{
    public enum PICTUREFORMAT {
        pfJPEG = 0, // JPEG format.
        pfBMP, // BMP format.
        pfPNG // PNG format.
    }

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// The picture that is shown for the system-setup. Deprecated.
    internal struct SystemSetupPictureStruct
    {
        public uint systemsetupid; //  The unique ID of the system-setup this system-setup-picture belongs to.
        public uint format; //  The picture format (JPEG, PNG, BMP).
    }
#pragma warning restore 0649

/// <summary>
/// Information about System-setup picture
/// </summary>
/// <remarks>
/// The picture that is shown for the system-setup. Deprecated.
/// </remarks>
public partial class SystemSetupPicture{
    private readonly System.IntPtr _nativePointer;
    private readonly SystemSetupPictureStruct _data;


    private MTA _handleWrapper;

    internal SystemSetupPicture(System.IntPtr nativePointer, MTA context)
    {
        _nativePointer = nativePointer;
        _data = (SystemSetupPictureStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(SystemSetupPictureStruct));

        _handleWrapper = context;
    }
	
	internal static SystemSetupPicture FromNativePointer(
        System.IntPtr pointerToNativeStruct, MTA context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new SystemSetupPicture(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<SystemSetupPicture> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, MTA context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<SystemSetupPicture>(
            System.Array.ConvertAll<System.IntPtr,SystemSetupPicture>(ptrArray,
                ptr => new SystemSetupPicture(ptr, context)));
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
    ///The unique ID of the system-setup this system-setup-picture belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The picture format (JPEG, PNG, BMP).
    ///</summary>
	public uint Format
    {
        get { return (uint) _data.format; }
    }



    public byte[] GetPictureData()
{
    var dataPtr = MylapsSDKLibrary.NativeMethods.mta_systemsetuppicture_get_data(_handleWrapper.NativeHandle);
    var length = (int) GetDataLength();

    var pictureData = new byte[length];
    System.Runtime.InteropServices.Marshal.Copy(dataPtr, pictureData, 0, length);

    return pictureData;
}

public uint GetDataLength() {
    return MylapsSDKLibrary.NativeMethods.mta_systemsetuppicture_get_data_length(_handleWrapper.NativeHandle);
}

}

/// <summary>
/// Modifier for System-setup picture
/// </summary>
/// <remarks>
/// The picture that is shown for the system-setup. Deprecated.
/// </remarks>
public class SystemSetupPictureModifier : MylapsSDK.MylapsSDKLibrary.GenericModifier<SystemSetupPicture>
{
    private readonly System.IntPtr _nativePointer;
    private SystemSetupPictureStruct _data;
    private MTA _handleWrapper;


    internal SystemSetupPictureModifier(System.IntPtr nativePointer, MTA context):
        base(nativePointer)
	{
	    _nativePointer = nativePointer;
	    _handleWrapper = context;
		_data = (SystemSetupPictureStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(nativePointer, typeof(SystemSetupPictureStruct));
	}

    ///<summary>
    ///The unique ID of the system-setup this system-setup-picture belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The picture format (JPEG, PNG, BMP).
    ///</summary>
	public uint Format
    {
        get { return (uint) _data.format; }
    }



    ///<summary>
    ///Set the picture format.
    ///</summary>
    public void SetFormat (uint format) // setter function
    {
		_data.format = (uint) format;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }


    public byte[] GetPictureData()
{
    var dataPtr = MylapsSDKLibrary.NativeMethods.mta_systemsetuppicture_get_data(_handleWrapper.NativeHandle);
    var length = (int) GetDataLength();

    var pictureData = new byte[length];
    System.Runtime.InteropServices.Marshal.Copy(dataPtr, pictureData, 0, length);

    return pictureData;
}

public uint GetDataLength() {
    return MylapsSDKLibrary.NativeMethods.mta_systemsetuppicture_get_data_length(_handleWrapper.NativeHandle);
}


    public void SetPictureData(byte[] data)
{
    var size = System.Runtime.InteropServices.Marshal.SizeOf(data[0]) * data.Length;
    var dataPtr = System.Runtime.InteropServices.Marshal.AllocHGlobal(size);
    MylapsSDKLibrary.NativeMethods.mta_systemsetuppicture_set_data(_handleWrapper.NativeHandle, _nativePointer, dataPtr, (uint) size);
    System.Runtime.InteropServices.Marshal.FreeHGlobal(dataPtr);
}

}

}