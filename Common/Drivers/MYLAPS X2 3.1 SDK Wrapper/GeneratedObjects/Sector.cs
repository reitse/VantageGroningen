namespace MylapsSDK.Objects
{
    public enum SECTORFLAGS {
        sfUseSpeed, // The sector is used to calculate speed.
        sfIsPublic, // Is the sector information public (e.g. published by the X2 practice-provider)?
        sfIsLapSector // Is the sector part of the full lap (e.g. published by the X2 practice-provider)?
    }

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// The sector (spanning one or more segments) in a sequence within a track-solution.
    internal struct SectorStruct
    {
        public uint id; //  The unique ID of the sector.
        public uint systemsetupid; //  The unique ID of the system-setup this sector belongs to.
        public uint tracksolutionid; //  The unique ID of the track-solution this sector belongs to.
        public uint flags; //  Misc. flags of the sector.
        public uint order; //  The order of the sector in the track-solution (used for sorting).
        public uint entryloopid; //  The entry loop ID of the sector.
        public uint exitloopid; //  The exit loop ID of the sector.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 16)]
        public byte[] name; //  The name of the sector (e.g. 'S/F-I1'). The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 48)]
        public byte[] description; //  A sector description (e.g. 'Start/Finish to Intermediate 1'). The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 4)]
        public byte[] padding; //  padding
    }
#pragma warning restore 0649

/// <summary>
/// Information about Sector
/// </summary>
/// <remarks>
/// The sector (spanning one or more segments) in a sequence within a track-solution.
/// </remarks>
public partial class Sector: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly SectorStruct _data;

    private readonly string _name;
    private readonly string _description;

    private MTA _handleWrapper;

    internal Sector(System.IntPtr nativePointer, MTA context)
    {
        _nativePointer = nativePointer;
        _data = (SectorStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(SectorStruct));

        _handleWrapper = context;
        _name = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.name);
        _description = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.description);
    }
	
	internal static Sector FromNativePointer(
        System.IntPtr pointerToNativeStruct, MTA context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new Sector(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<Sector> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, MTA context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<Sector>(
            System.Array.ConvertAll<System.IntPtr,Sector>(ptrArray,
                ptr => new Sector(ptr, context)));
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
    ///The unique ID of the sector.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The unique ID of the system-setup this sector belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The unique ID of the track-solution this sector belongs to.
    ///</summary>
	public uint TrackSolutionID
    {
        get { return (uint) _data.tracksolutionid; }
    }
    ///<summary>
    ///The order of the sector in the track-solution (used for sorting).
    ///</summary>
	public uint Order
    {
        get { return (uint) _data.order; }
    }
    ///<summary>
    ///The entry loop ID of the sector.
    ///</summary>
	public uint EntryLoopID
    {
        get { return (uint) _data.entryloopid; }
    }
    ///<summary>
    ///The exit loop ID of the sector.
    ///</summary>
	public uint ExitLoopID
    {
        get { return (uint) _data.exitloopid; }
    }
    ///<summary>
    ///The name of the sector (e.g. 'S/F-I1'). The character data is in UTF-8 encoding.
    ///</summary>
    public string Name
    {
        get { return _name; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///A sector description (e.g. 'Start/Finish to Intermediate 1'). The character data is in UTF-8 encoding.
    ///</summary>
    public string Description
    {
        get { return _description; } // return local datamember which is an utf8 encoded string
    }

    ///<summary>
    ///Is the sector used to calculate speed?
    ///</summary>
    public bool IsUseSpeed()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) SECTORFLAGS.sfUseSpeed);
    }
    ///<summary>
    ///Is the sector information public (e.g. published by the X2 practice-provider)?
    ///</summary>
    public bool IsPublic()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) SECTORFLAGS.sfIsPublic);
    }
    ///<summary>
    ///Is the sector part of the full lap (e.g. published by the X2 practice-provider)?
    ///</summary>
    public bool IsLapSector()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) SECTORFLAGS.sfIsLapSector);
    }


    
}

/// <summary>
/// Modifier for Sector
/// </summary>
/// <remarks>
/// The sector (spanning one or more segments) in a sequence within a track-solution.
/// </remarks>
public class SectorModifier : MylapsSDK.MylapsSDKLibrary.GenericModifier<Sector>
{
    private readonly System.IntPtr _nativePointer;
    private SectorStruct _data;
    private MTA _handleWrapper;

    private readonly string _name;
    private readonly string _description;

    internal SectorModifier(System.IntPtr nativePointer, MTA context):
        base(nativePointer)
	{
	    _nativePointer = nativePointer;
	    _handleWrapper = context;
		_data = (SectorStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(nativePointer, typeof(SectorStruct));
        _name = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.name);
        _description = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.description);
	}

    ///<summary>
    ///The unique ID of the sector.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The unique ID of the system-setup this sector belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The unique ID of the track-solution this sector belongs to.
    ///</summary>
	public uint TrackSolutionID
    {
        get { return (uint) _data.tracksolutionid; }
    }
    ///<summary>
    ///The order of the sector in the track-solution (used for sorting).
    ///</summary>
	public uint Order
    {
        get { return (uint) _data.order; }
    }
    ///<summary>
    ///The entry loop ID of the sector.
    ///</summary>
	public uint EntryLoopID
    {
        get { return (uint) _data.entryloopid; }
    }
    ///<summary>
    ///The exit loop ID of the sector.
    ///</summary>
	public uint ExitLoopID
    {
        get { return (uint) _data.exitloopid; }
    }
    ///<summary>
    ///The name of the sector (e.g. 'S/F-I1'). The character data is in UTF-8 encoding.
    ///</summary>
    public string Name
    {
        get { return _name; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///A sector description (e.g. 'Start/Finish to Intermediate 1'). The character data is in UTF-8 encoding.
    ///</summary>
    public string Description
    {
        get { return _description; } // return local datamember which is an utf8 encoded string
    }


    ///<summary>
    ///Is the sector used to calculate speed?
    ///</summary>
    public bool IsUseSpeed()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) SECTORFLAGS.sfUseSpeed);
    }
    ///<summary>
    ///Is the sector information public (e.g. published by the X2 practice-provider)?
    ///</summary>
    public bool IsPublic()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) SECTORFLAGS.sfIsPublic);
    }
    ///<summary>
    ///Is the sector part of the full lap (e.g. published by the X2 practice-provider)?
    ///</summary>
    public bool IsLapSector()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) SECTORFLAGS.sfIsLapSector);
    }

    ///<summary>
    ///Set the track-solution ID of the sector.
    ///</summary>
    public void SetTrackSolutionID (uint trackSolutionID) // setter function
    {
		_data.tracksolutionid = (uint) trackSolutionID;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the order of the sector in the track-solution (used for sorting).
    ///</summary>
    public void SetOrder (uint order) // setter function
    {
		_data.order = (uint) order;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the entry loop ID of the sector.
    ///</summary>
    public void SetEntryLoopID (uint entryLoopID) // setter function
    {
		_data.entryloopid = (uint) entryLoopID;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the exit loop ID of the sector.
    ///</summary>
    public void SetExitLoopID (uint exitLoopID) // setter function
    {
		_data.exitloopid = (uint) exitLoopID;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the name for this sector
    ///</summary>
    public void SetName (string name) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(name,_data.name);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the description for this sector
    ///</summary>
    public void SetDescription (string description) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(description,_data.description);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Is the sector used to calculate speed?
    ///</summary>
    public void SetUseSpeed (bool useSpeed) // setter function
    {
        _data.flags = MylapsSDK.Utilities.SDKHelperFunctions.SetOrClearBit(
            (uint) _data.flags, (int) SECTORFLAGS.sfUseSpeed, useSpeed);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Is the sector information public (e.g. published by the X2 practice-provider)?
    ///</summary>
    public void SetPublic (bool publicValue) // setter function
    {
        _data.flags = MylapsSDK.Utilities.SDKHelperFunctions.SetOrClearBit(
            (uint) _data.flags, (int) SECTORFLAGS.sfIsPublic, publicValue);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Is the sector part of the full lap?
    ///</summary>
    public void SetLapSector (bool lapSector) // setter function
    {
        _data.flags = MylapsSDK.Utilities.SDKHelperFunctions.SetOrClearBit(
            (uint) _data.flags, (int) SECTORFLAGS.sfIsLapSector, lapSector);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }


    

    
}

}