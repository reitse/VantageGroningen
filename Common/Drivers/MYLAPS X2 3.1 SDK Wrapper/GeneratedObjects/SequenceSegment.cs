namespace MylapsSDK.Objects
{

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// The link of an unique segment to a sequence within a track-solution.
    internal struct SequenceSegmentStruct
    {
        public uint id; //  The unique ID of the sequence-segment.
        public uint systemsetupid; //  The unique ID of the system-setup this sequence-segment belongs to.
        public uint tracksolutionid; //  The unique ID of the track-solution this sequence-segment belongs to.
        public uint sequenceid; //  The unique ID of the sequence this sequence-segment belongs to.
        public uint flags; //  Misc. flags of the sequence-segment.
        public uint segmentid; //  The unique ID of the segment that is in the sequence.
        public uint order; //  The order of the segment in the sequence.
        public uint entryloopid; //  The entry loop ID of the segment that is in the sequence.
        public uint exitloopid; //  The exit loop ID of the segment that is in the sequence.
        public byte status; //  The status of the sequence-segment (e.g. 'yellow').
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 3)]
        public byte[] padding; //  padding
    }
#pragma warning restore 0649

/// <summary>
/// Information about Sequence segment
/// </summary>
/// <remarks>
/// The link of an unique segment to a sequence within a track-solution.
/// </remarks>
public partial class SequenceSegment: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly SequenceSegmentStruct _data;


    private MTA _handleWrapper;

    internal SequenceSegment(System.IntPtr nativePointer, MTA context)
    {
        _nativePointer = nativePointer;
        _data = (SequenceSegmentStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(SequenceSegmentStruct));

        _handleWrapper = context;
    }
	
	internal static SequenceSegment FromNativePointer(
        System.IntPtr pointerToNativeStruct, MTA context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new SequenceSegment(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<SequenceSegment> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, MTA context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<SequenceSegment>(
            System.Array.ConvertAll<System.IntPtr,SequenceSegment>(ptrArray,
                ptr => new SequenceSegment(ptr, context)));
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
    ///The unique ID of the sequence-segment.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The unique ID of the system-setup this sequence-segment belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The unique ID of the track-solution this sequence-segment belongs to.
    ///</summary>
	public uint TrackSolutionID
    {
        get { return (uint) _data.tracksolutionid; }
    }
    ///<summary>
    ///The unique ID of the sequence this sequence-segment belongs to.
    ///</summary>
	public uint SequenceID
    {
        get { return (uint) _data.sequenceid; }
    }
    ///<summary>
    ///The unique ID of the segment that is in the sequence.
    ///</summary>
	public uint SegmentID
    {
        get { return (uint) _data.segmentid; }
    }
    ///<summary>
    ///The order of the segment in the sequence.
    ///</summary>
	public uint Order
    {
        get { return (uint) _data.order; }
    }
    ///<summary>
    ///The entry loop ID of the segment that is in the sequence.
    ///</summary>
	public uint EntryLoopID
    {
        get { return (uint) _data.entryloopid; }
    }
    ///<summary>
    ///The exit loop ID of the segment that is in the sequence.
    ///</summary>
	public uint ExitLoopID
    {
        get { return (uint) _data.exitloopid; }
    }
    ///<summary>
    ///The status of the sequence-segment (e.g. 'yellow').
    ///</summary>
	public byte Status
    {
        get { return (byte) _data.status; }
    }



    
}

/// <summary>
/// Modifier for Sequence segment
/// </summary>
/// <remarks>
/// The link of an unique segment to a sequence within a track-solution.
/// </remarks>
public class SequenceSegmentModifier : MylapsSDK.MylapsSDKLibrary.GenericModifier<SequenceSegment>
{
    private readonly System.IntPtr _nativePointer;
    private SequenceSegmentStruct _data;
    private MTA _handleWrapper;


    internal SequenceSegmentModifier(System.IntPtr nativePointer, MTA context):
        base(nativePointer)
	{
	    _nativePointer = nativePointer;
	    _handleWrapper = context;
		_data = (SequenceSegmentStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(nativePointer, typeof(SequenceSegmentStruct));
	}

    ///<summary>
    ///The unique ID of the sequence-segment.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The unique ID of the system-setup this sequence-segment belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The unique ID of the track-solution this sequence-segment belongs to.
    ///</summary>
	public uint TrackSolutionID
    {
        get { return (uint) _data.tracksolutionid; }
    }
    ///<summary>
    ///The unique ID of the sequence this sequence-segment belongs to.
    ///</summary>
	public uint SequenceID
    {
        get { return (uint) _data.sequenceid; }
    }
    ///<summary>
    ///The unique ID of the segment that is in the sequence.
    ///</summary>
	public uint SegmentID
    {
        get { return (uint) _data.segmentid; }
    }
    ///<summary>
    ///The order of the segment in the sequence.
    ///</summary>
	public uint Order
    {
        get { return (uint) _data.order; }
    }
    ///<summary>
    ///The entry loop ID of the segment that is in the sequence.
    ///</summary>
	public uint EntryLoopID
    {
        get { return (uint) _data.entryloopid; }
    }
    ///<summary>
    ///The exit loop ID of the segment that is in the sequence.
    ///</summary>
	public uint ExitLoopID
    {
        get { return (uint) _data.exitloopid; }
    }
    ///<summary>
    ///The status of the sequence-segment (e.g. 'yellow').
    ///</summary>
	public byte Status
    {
        get { return (byte) _data.status; }
    }



    ///<summary>
    ///Set the track-solution ID of the sequence-segment.
    ///</summary>
    public void SetTrackSolutionID (uint trackSolutionID) // setter function
    {
		_data.tracksolutionid = (uint) trackSolutionID;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the unique ID of the segment that is in the sequence.
    ///</summary>
    public void SetSegmentID (uint segmentID) // setter function
    {
		_data.segmentid = (uint) segmentID;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the order of the segment in the sequence.
    ///</summary>
    public void SetOrder (uint order) // setter function
    {
		_data.order = (uint) order;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the unique ID of the sequence for this sequence-segment.
    ///</summary>
    public void SetSequenceID (uint sequenceID) // setter function
    {
		_data.sequenceid = (uint) sequenceID;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the entry loop ID of the segment that is in the sequence.
    ///</summary>
    public void SetEntryLoopID (uint entryLoopID) // setter function
    {
		_data.entryloopid = (uint) entryLoopID;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the exit loop ID of the segment that is in the sequence.
    ///</summary>
    public void SetExitLoopID (uint exitLoopID) // setter function
    {
		_data.exitloopid = (uint) exitLoopID;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the status of the segment in the sequence.
    ///</summary>
    public void SetStatus (byte status) // setter function
    {
		_data.status = (byte) status;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }


    

    
}

}