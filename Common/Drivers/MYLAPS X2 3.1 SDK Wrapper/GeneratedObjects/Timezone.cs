namespace MylapsSDK.Objects
{

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// A name for a timezone.
    internal struct TimezoneStruct
    {
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 64)]
        public byte[] name; //  The unique timezone name (e.g. 'Europe/Amsterdam'). The character data is in UTF-8 encoding.
    }
#pragma warning restore 0649

/// <summary>
/// Information about Timezone
/// </summary>
/// <remarks>
/// A name for a timezone.
/// </remarks>
public partial class Timezone{
    private readonly System.IntPtr _nativePointer;
    private readonly TimezoneStruct _data;

    private readonly string _name;

    private MTA _handleWrapper;

    internal Timezone(System.IntPtr nativePointer, MTA context)
    {
        _nativePointer = nativePointer;
        _data = (TimezoneStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(TimezoneStruct));

        _handleWrapper = context;
        _name = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.name);
    }
	
	internal static Timezone FromNativePointer(
        System.IntPtr pointerToNativeStruct, MTA context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new Timezone(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<Timezone> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, MTA context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<Timezone>(
            System.Array.ConvertAll<System.IntPtr,Timezone>(ptrArray,
                ptr => new Timezone(ptr, context)));
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
    ///The unique timezone name (e.g. 'Europe/Amsterdam'). The character data is in UTF-8 encoding.
    ///</summary>
    public string Name
    {
        get { return _name; } // return local datamember which is an utf8 encoded string
    }



    
}

}