namespace MylapsSDK.Objects
{

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// A measurement of the beacon strength.
    internal struct BeaconDataStruct
    {
        public uint utcoffset; //  The offset relative to the UTC begin time of the beaconlog.
        public byte rssi; //  The beacon strength in AMB units.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 3)]
        public byte[] padding; //  padding
    }
#pragma warning restore 0649

/// <summary>
/// Information about Beacon data
/// </summary>
/// <remarks>
/// A measurement of the beacon strength.
/// </remarks>
public partial class BeaconData{
    private readonly System.IntPtr _nativePointer;
    private readonly BeaconDataStruct _data;


    private EventData _handleWrapper;

    internal BeaconData(System.IntPtr nativePointer, EventData context)
    {
        _nativePointer = nativePointer;
        _data = (BeaconDataStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(BeaconDataStruct));

        _handleWrapper = context;
    }
	
	internal static BeaconData FromNativePointer(
        System.IntPtr pointerToNativeStruct, EventData context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new BeaconData(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<BeaconData> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, EventData context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<BeaconData>(
            System.Array.ConvertAll<System.IntPtr,BeaconData>(ptrArray,
                ptr => new BeaconData(ptr, context)));
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
    ///The offset relative to the UTC begin time of the beaconlog.
    ///</summary>
	public uint UTCOffset
    {
        get { return (uint) _data.utcoffset; }
    }

    ///<summary>
    ///Get the strength (in dBm).
    ///</summary>
    public double GetStrength()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.ConvertBeaconStrength2Dbm(_data.rssi);

    }


    
}

}