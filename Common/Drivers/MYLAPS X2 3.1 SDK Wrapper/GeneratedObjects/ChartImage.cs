namespace MylapsSDK.Objects
{
    public enum CHARTTYPES {
        ctEmptyChart = 0, // Empty chart with no image-data
        ctDecoderTempChart, // Decoder temperature chart
        ctDecoderNoiseTwoWayChart, // Decoder noise of the 2-way receiver chart
        ctDecoderNoiseTranXChart, // Decoder noise of the TranX receiver chart
        ctDecoderDriftChart, // Decoder drift chart
        ctDecoderSatelliteChart, // Decoder drift chart
        ctDecoderSquelchChart, // Decoder squelch chart
        ctDecoderGatetimeChart, // Decoder gate time chart
        ctDecoderVoltageChart, // Decoder voltage chart
        ctDecoderCPULoadChart, // Decoder cpu load chart
        ctLoopTriggerStrength, // Loop-trigger strength chart
        ctLoopTriggerTemperature, // Loop-trigger temperature chart
        ctTransponderTempChart, // Transponder temperature chart
        ctTransponderNoiseChart, // Transponder temperature chart
        ctEDSClientChart, // EDS-stream client status chart
        ctHitProfileChart // Hit-profile chart contains an image of hit-groups
    }

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// Definition for a chart image
    internal struct ChartImageStruct
    {
        public long timecreated_utc; //  time of creation of chart image, in utc
        public uint requestid; //  request id of chart, relates chart response with request
        public ushort width; //  width of image in pixels
        public ushort height; //  height of image in pixels
        public byte type; //  chart type of chart
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 7)]
        public byte[] padding; //  padding
    }
#pragma warning restore 0649

/// <summary>
/// Information about GFX Chart Image
/// </summary>
/// <remarks>
/// Definition for a chart image
/// </remarks>
public partial class ChartImage{
    private readonly System.IntPtr _nativePointer;
    private readonly ChartImageStruct _data;


    private MTA _handleWrapper;

    internal ChartImage(System.IntPtr nativePointer, MTA context)
    {
        _nativePointer = nativePointer;
        _data = (ChartImageStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(ChartImageStruct));

        _handleWrapper = context;
    }
	
	internal static ChartImage FromNativePointer(
        System.IntPtr pointerToNativeStruct, MTA context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new ChartImage(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<ChartImage> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, MTA context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<ChartImage>(
            System.Array.ConvertAll<System.IntPtr,ChartImage>(ptrArray,
                ptr => new ChartImage(ptr, context)));
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
    ///time of creation of chart image, in utc
    ///</summary>
	public long TimeCreatedUTC
    {
        get { return (long) _data.timecreated_utc; }
    }
    ///<summary>
    ///request id of chart, relates chart response with request
    ///</summary>
	public uint RequestID
    {
        get { return (uint) _data.requestid; }
    }
    ///<summary>
    ///width of image in pixels
    ///</summary>
	public ushort Width
    {
        get { return (ushort) _data.width; }
    }
    ///<summary>
    ///height of image in pixels
    ///</summary>
	public ushort Height
    {
        get { return (ushort) _data.height; }
    }
    ///<summary>
    ///chart type of chart
    ///</summary>
	public byte Type
    {
        get { return (byte) _data.type; }
    }



    
}

}