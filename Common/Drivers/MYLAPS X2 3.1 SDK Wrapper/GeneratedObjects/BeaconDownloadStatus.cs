namespace MylapsSDK.Objects
{
    public enum DOWNLOADSTATUS {
        dsNone = 0, // No download status available
        dsFailed, // Beacon-download has failed
        dsCreated, // 2-way message is created to download the beacon-log
        dsQueued, // 2-way message is queued to download the beacon-log
        dsSent, // 2-way message is delivered to the transponder to download the beacon-log
        dsAcknowledged, // 2-way message is acknowledged by the transponder to download the beacon-log
        dsDownloading, // Downloading the beacon-log measurements
        dsDownloaded // Beacon-log download is completed
    }

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// The status of a specific beacon download.
    internal struct BeaconDownloadStatusStruct
    {
        public long createdtime; //  The time-of-day the beacon-download request was created.
        public long queuedtime; //  The time-of-day the beacon-download request was queued.
        public long senttime; //  The time-of-day the beacon-download request was sent.
        public long acknowledgedtime; //  The time-of-day the beacon-download was acknowledged.
        public uint id; //  The unique ID.
        public uint transponderid; //  The transponder number.
        public uint transpondershortid; //  The 'short' (user-definable) transponder number.
        public uint systemsetupid; //  The unique ID of the system-setup this download-status belongs to.
        public uint loopid; //  The last loop ID.
        public uint ioterminalid; //  The last I/O terminal ID.
        public uint triggerid; //  The trigger ID.
        public byte status; //  The status of the download.
        public byte requestnr; //  The request number.
        public byte beaconcount; //  The number of beacons that will be in the download.
        public byte beaconindex; //  The current beacon index.
        public byte packetcount; //  The number of packets that will be in the download.
        public byte packetindex; //  The current packet index.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 6)]
        public byte[] padding; //  padding
    }
#pragma warning restore 0649

/// <summary>
/// Information about Beacon-download status
/// </summary>
/// <remarks>
/// The status of a specific beacon download.
/// </remarks>
public partial class BeaconDownloadStatus: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly BeaconDownloadStatusStruct _data;


    private EventData _handleWrapper;

    internal BeaconDownloadStatus(System.IntPtr nativePointer, EventData context)
    {
        _nativePointer = nativePointer;
        _data = (BeaconDownloadStatusStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(BeaconDownloadStatusStruct));

        _handleWrapper = context;
    }
	
	internal static BeaconDownloadStatus FromNativePointer(
        System.IntPtr pointerToNativeStruct, EventData context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new BeaconDownloadStatus(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<BeaconDownloadStatus> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, EventData context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<BeaconDownloadStatus>(
            System.Array.ConvertAll<System.IntPtr,BeaconDownloadStatus>(ptrArray,
                ptr => new BeaconDownloadStatus(ptr, context)));
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
    ///The time-of-day the beacon-download request was created.
    ///</summary>
	public long CreatedTime
    {
        get { return (long) _data.createdtime; }
    }
    ///<summary>
    ///The time-of-day the beacon-download request was queued.
    ///</summary>
	public long QueuedTime
    {
        get { return (long) _data.queuedtime; }
    }
    ///<summary>
    ///The time-of-day the beacon-download request was sent.
    ///</summary>
	public long SentTime
    {
        get { return (long) _data.senttime; }
    }
    ///<summary>
    ///The time-of-day the beacon-download was acknowledged.
    ///</summary>
	public long AcknowledgedTime
    {
        get { return (long) _data.acknowledgedtime; }
    }
    ///<summary>
    ///The unique ID.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The transponder number.
    ///</summary>
	public uint TransponderID
    {
        get { return (uint) _data.transponderid; }
    }
    ///<summary>
    ///The 'short' (user-definable) transponder number.
    ///</summary>
	public uint TransponderShortID
    {
        get { return (uint) _data.transpondershortid; }
    }
    ///<summary>
    ///The unique ID of the system-setup this download-status belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The last loop ID.
    ///</summary>
	public uint LoopID
    {
        get { return (uint) _data.loopid; }
    }
    ///<summary>
    ///The last I/O terminal ID.
    ///</summary>
	public uint IOTerminalID
    {
        get { return (uint) _data.ioterminalid; }
    }
    ///<summary>
    ///The trigger ID.
    ///</summary>
	public uint TriggerID
    {
        get { return (uint) _data.triggerid; }
    }
    ///<summary>
    ///The status of the download.
    ///</summary>
	public byte Status
    {
        get { return (byte) _data.status; }
    }
    ///<summary>
    ///The request number.
    ///</summary>
	public byte RequestNr
    {
        get { return (byte) _data.requestnr; }
    }
    ///<summary>
    ///The number of beacons that will be in the download.
    ///</summary>
	public byte BeaconCount
    {
        get { return (byte) _data.beaconcount; }
    }
    ///<summary>
    ///The current beacon index.
    ///</summary>
	public byte BeaconIndex
    {
        get { return (byte) _data.beaconindex; }
    }
    ///<summary>
    ///The number of packets that will be in the download.
    ///</summary>
	public byte PacketCount
    {
        get { return (byte) _data.packetcount; }
    }
    ///<summary>
    ///The current packet index.
    ///</summary>
	public byte PacketIndex
    {
        get { return (byte) _data.packetindex; }
    }



    
}

}