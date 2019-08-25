namespace MylapsSDK.Objects
{
    public enum LOOPFLAGS {
        lfInPit = 1, // This loop is in the pit.
        lfAutoReconnect, // Automatically reconnect the decoder when it is available on the network.
        lfCheckSyncPulse // Check the sync pulse.
    }
    public enum LOOPSTATUS {
        lsOnline = 0, // The loop is on-line.
        lsActivity, // There's activity on this loop.
        lsSyncOK, // The synchronization is OK.
        lsWarnings, // Does the connected decoder have any warnings?
        lsErrors, // Does the connected decoder have any errors?
        lsUpdating, // Is the firmware of the connected decoder being updated?
        lsReconnecting // Is trying to reconnect to the decoder?
    }

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// A line on the track where transponder passings are measured.
    internal struct LoopStruct
    {
        public uint id; //  The unique identifier of the loop.
        public uint systemsetupid; //  The unique ID of the system-setup this loop belongs to.
        public uint twowayid; //  The ID that is send by the loop to identify itself to the transponder.
        public uint order; //  The order of the loop in the system-setup.
        public uint flags; //  Misc. flags of the loop (e.g. in-pit, auto-reconnect).
        public uint statusflags; //  Misc. statuses of the loop (e.g. on-line, active, sync-ok).
        public uint xpos; //  The x-position of the center of the loop on the system-setup picture.
        public uint ypos; //  The y-position of the center of the loop on the system-setup picture.
        public int latitude0; //  The first latitude coordinate of the loop (in 1e-7 units, signed).
        public int latitude1; //  The second latitude coordinate of the loop (in 1e-7 units, signed).
        public int longitude0; //  The first longitude coordinate of the loop (in 1e-7 units, signed).
        public int longitude1; //  The second longitude coordinate of the loop (in 1e-7 units, signed).
        public byte syncmethod; //  The used sync method. (See #SYNCMETHOD, can be GPS or NTP)
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 16)]
        public byte[] name; //  The name of the loop in the system-setup. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 52)]
        public byte[] description; //  A loop description. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 3)]
        public byte[] padding; //  padding
    }
#pragma warning restore 0649

/// <summary>
/// Information about Loop
/// </summary>
/// <remarks>
/// A line on the track where transponder passings are measured.
/// </remarks>
public partial class Loop: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly LoopStruct _data;

    private readonly string _name;
    private readonly string _description;

    private MTA _handleWrapper;

    internal Loop(System.IntPtr nativePointer, MTA context)
    {
        _nativePointer = nativePointer;
        _data = (LoopStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(LoopStruct));

        _handleWrapper = context;
        _name = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.name);
        _description = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.description);
    }
	
	internal static Loop FromNativePointer(
        System.IntPtr pointerToNativeStruct, MTA context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new Loop(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<Loop> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, MTA context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<Loop>(
            System.Array.ConvertAll<System.IntPtr,Loop>(ptrArray,
                ptr => new Loop(ptr, context)));
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
    ///The unique identifier of the loop.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The unique ID of the system-setup this loop belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The ID that is send by the loop to identify itself to the transponder.
    ///</summary>
	public uint TwoWayID
    {
        get { return (uint) _data.twowayid; }
    }
    ///<summary>
    ///The order of the loop in the system-setup.
    ///</summary>
	public uint Order
    {
        get { return (uint) _data.order; }
    }
    ///<summary>
    ///The x-position of the center of the loop on the system-setup picture.
    ///</summary>
	public uint XPos
    {
        get { return (uint) _data.xpos; }
    }
    ///<summary>
    ///The y-position of the center of the loop on the system-setup picture.
    ///</summary>
	public uint YPos
    {
        get { return (uint) _data.ypos; }
    }
    ///<summary>
    ///The first latitude coordinate of the loop (in 1e-7 units, signed).
    ///</summary>
	public int Latitude0
    {
        get { return (int) _data.latitude0; }
    }
    ///<summary>
    ///The second latitude coordinate of the loop (in 1e-7 units, signed).
    ///</summary>
	public int Latitude1
    {
        get { return (int) _data.latitude1; }
    }
    ///<summary>
    ///The first longitude coordinate of the loop (in 1e-7 units, signed).
    ///</summary>
	public int Longitude0
    {
        get { return (int) _data.longitude0; }
    }
    ///<summary>
    ///The second longitude coordinate of the loop (in 1e-7 units, signed).
    ///</summary>
	public int Longitude1
    {
        get { return (int) _data.longitude1; }
    }
    ///<summary>
    ///The used sync method. (See #SYNCMETHOD, can be GPS or NTP)
    ///</summary>
	public byte SyncMethod
    {
        get { return (byte) _data.syncmethod; }
    }
    ///<summary>
    ///The name of the loop in the system-setup. The character data is in UTF-8 encoding.
    ///</summary>
    public string Name
    {
        get { return _name; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///A loop description. The character data is in UTF-8 encoding.
    ///</summary>
    public string Description
    {
        get { return _description; } // return local datamember which is an utf8 encoded string
    }

    ///<summary>
    ///Is the loop in the pit?
    ///</summary>
    public bool IsInPit()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) LOOPFLAGS.lfInPit);
    }
    ///<summary>
    ///Automatically reconnect the decoder when it is available on the network?
    ///</summary>
    public bool UseAutoReconnect()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) LOOPFLAGS.lfAutoReconnect);
    }
    ///<summary>
    ///Check the sync pulse?
    ///</summary>
    public bool CheckSyncPulse()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) LOOPFLAGS.lfCheckSyncPulse);
    }
    ///<summary>
    ///Get the number of loop-triggers for this loop.
    ///</summary>
    public byte GetLoopTriggers()
    {
        return (byte)((_data.flags >> 16) & 0x03);

    }
    ///<summary>
    ///Is the loop on-line?
    ///</summary>
    public bool IsOnline()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) LOOPSTATUS.lsOnline);
    }
    ///<summary>
    ///Is there activity on this loop?
    ///</summary>
    public bool HasActivity()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) LOOPSTATUS.lsActivity);
    }
    ///<summary>
    ///Is the loop synchronization OK?
    ///</summary>
    public bool IsSyncOk()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) LOOPSTATUS.lsSyncOK);
    }
    ///<summary>
    ///Does the connected decoder have any warnings?
    ///</summary>
    public bool HasWarnings()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) LOOPSTATUS.lsWarnings);
    }
    ///<summary>
    ///Does the connected decoder have any errors?
    ///</summary>
    public bool HasErrors()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) LOOPSTATUS.lsErrors);
    }
    ///<summary>
    ///Is the firmware of the connected decoder being updated?
    ///</summary>
    public bool IsUpdating()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) LOOPSTATUS.lsUpdating);
    }
    ///<summary>
    ///Is trying to reconnect to the decoder?
    ///</summary>
    public bool IsReconnecting()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) LOOPSTATUS.lsReconnecting);
    }

    public string GetUserData(string key, uint length)
    {
        var result = new System.Text.StringBuilder((int)length);
        if (MylapsSDKLibrary.NativeMethods.mta_loop_get_userdata(_handleWrapper.NativeHandle, _data.id, key, result, length))
            return result.ToString();
        else
            return null;
    }

    
}

/// <summary>
/// Modifier for Loop
/// </summary>
/// <remarks>
/// A line on the track where transponder passings are measured.
/// </remarks>
public class LoopModifier : MylapsSDK.MylapsSDKLibrary.GenericModifier<Loop>
{
    private readonly System.IntPtr _nativePointer;
    private LoopStruct _data;
    private MTA _handleWrapper;

    private readonly string _name;
    private readonly string _description;

    internal LoopModifier(System.IntPtr nativePointer, MTA context):
        base(nativePointer)
	{
	    _nativePointer = nativePointer;
	    _handleWrapper = context;
		_data = (LoopStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(nativePointer, typeof(LoopStruct));
        _name = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.name);
        _description = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.description);
	}

    ///<summary>
    ///The unique identifier of the loop.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The unique ID of the system-setup this loop belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The ID that is send by the loop to identify itself to the transponder.
    ///</summary>
	public uint TwoWayID
    {
        get { return (uint) _data.twowayid; }
    }
    ///<summary>
    ///The order of the loop in the system-setup.
    ///</summary>
	public uint Order
    {
        get { return (uint) _data.order; }
    }
    ///<summary>
    ///The x-position of the center of the loop on the system-setup picture.
    ///</summary>
	public uint XPos
    {
        get { return (uint) _data.xpos; }
    }
    ///<summary>
    ///The y-position of the center of the loop on the system-setup picture.
    ///</summary>
	public uint YPos
    {
        get { return (uint) _data.ypos; }
    }
    ///<summary>
    ///The first latitude coordinate of the loop (in 1e-7 units, signed).
    ///</summary>
	public int Latitude0
    {
        get { return (int) _data.latitude0; }
    }
    ///<summary>
    ///The second latitude coordinate of the loop (in 1e-7 units, signed).
    ///</summary>
	public int Latitude1
    {
        get { return (int) _data.latitude1; }
    }
    ///<summary>
    ///The first longitude coordinate of the loop (in 1e-7 units, signed).
    ///</summary>
	public int Longitude0
    {
        get { return (int) _data.longitude0; }
    }
    ///<summary>
    ///The second longitude coordinate of the loop (in 1e-7 units, signed).
    ///</summary>
	public int Longitude1
    {
        get { return (int) _data.longitude1; }
    }
    ///<summary>
    ///The used sync method. (See #SYNCMETHOD, can be GPS or NTP)
    ///</summary>
	public byte SyncMethod
    {
        get { return (byte) _data.syncmethod; }
    }
    ///<summary>
    ///The name of the loop in the system-setup. The character data is in UTF-8 encoding.
    ///</summary>
    public string Name
    {
        get { return _name; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///A loop description. The character data is in UTF-8 encoding.
    ///</summary>
    public string Description
    {
        get { return _description; } // return local datamember which is an utf8 encoded string
    }


    ///<summary>
    ///Is the loop in the pit?
    ///</summary>
    public bool IsInPit()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) LOOPFLAGS.lfInPit);
    }
    ///<summary>
    ///Automatically reconnect the decoder when it is available on the network?
    ///</summary>
    public bool UseAutoReconnect()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) LOOPFLAGS.lfAutoReconnect);
    }
    ///<summary>
    ///Check the sync pulse?
    ///</summary>
    public bool CheckSyncPulse()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) LOOPFLAGS.lfCheckSyncPulse);
    }
    ///<summary>
    ///Get the number of loop-triggers for this loop.
    ///</summary>
    public byte GetLoopTriggers()
    {
        return (byte)((_data.flags >> 16) & 0x03);

    }
    ///<summary>
    ///Is the loop on-line?
    ///</summary>
    public bool IsOnline()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) LOOPSTATUS.lsOnline);
    }
    ///<summary>
    ///Is there activity on this loop?
    ///</summary>
    public bool HasActivity()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) LOOPSTATUS.lsActivity);
    }
    ///<summary>
    ///Is the loop synchronization OK?
    ///</summary>
    public bool IsSyncOk()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) LOOPSTATUS.lsSyncOK);
    }
    ///<summary>
    ///Does the connected decoder have any warnings?
    ///</summary>
    public bool HasWarnings()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) LOOPSTATUS.lsWarnings);
    }
    ///<summary>
    ///Does the connected decoder have any errors?
    ///</summary>
    public bool HasErrors()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) LOOPSTATUS.lsErrors);
    }
    ///<summary>
    ///Is the firmware of the connected decoder being updated?
    ///</summary>
    public bool IsUpdating()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) LOOPSTATUS.lsUpdating);
    }
    ///<summary>
    ///Is trying to reconnect to the decoder?
    ///</summary>
    public bool IsReconnecting()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) LOOPSTATUS.lsReconnecting);
    }

    ///<summary>
    ///Set the 2-way ID for this loop.
    ///</summary>
    public void SetTwoWayID (uint id) // setter function
    {
		_data.twowayid = (uint) id;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the order for this loop.
    ///</summary>
    public void SetOrder (uint order) // setter function
    {
		_data.order = (uint) order;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the x-position of the center of the loop on the system-setup picture.
    ///</summary>
    public void SetXPos (uint xPos) // setter function
    {
		_data.xpos = (uint) xPos;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the y-position of the center of the loop on the system-setup picture.
    ///</summary>
    public void SetYPos (uint yPos) // setter function
    {
		_data.ypos = (uint) yPos;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the latitude of the left-end position for this loop (in 1e-7 units, signed).
    ///</summary>
    public void SetLatitude0 (int latitude) // setter function
    {
        _data.latitude0 = MylapsSDK.Utilities.SDKHelperFunctions.ConvertLatLongDouble2Int(latitude);

        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the latitude of the right-end position for this loop (in 1e-7 units, signed).
    ///</summary>
    public void SetLatitude1 (int latitude) // setter function
    {
        _data.latitude1 = MylapsSDK.Utilities.SDKHelperFunctions.ConvertLatLongDouble2Int(latitude);

        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the longitude of the left-end position for this loop (in 1e-7 units, signed).
    ///</summary>
    public void SetLongitude0 (int longitude) // setter function
    {
        _data.longitude0 = MylapsSDK.Utilities.SDKHelperFunctions.ConvertLatLongDouble2Int(longitude);

        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the longitude of the right-end position for this loop (in 1e-7 units, signed).
    ///</summary>
    public void SetLongitude1 (int longitude) // setter function
    {
        _data.longitude1 = MylapsSDK.Utilities.SDKHelperFunctions.ConvertLatLongDouble2Int(longitude);

        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the name for this loop.
    ///</summary>
    public void SetName (string name) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(name,_data.name);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the description for this loop.
    ///</summary>
    public void SetDescription (string description) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(description,_data.description);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the sync method (see #SYNCMETHOD) that is used to determine UTC.
    ///</summary>
    public void SetSyncMethod (byte syncMethod) // setter function
    {
		_data.syncmethod = (byte) syncMethod;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the in-pit option for this loop.
    ///</summary>
    public void SetInPit (bool inPit) // setter function
    {
        _data.flags = MylapsSDK.Utilities.SDKHelperFunctions.SetOrClearBit(
            (uint) _data.flags, (int) LOOPFLAGS.lfInPit, inPit);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the auto-reconnect option for this loop.
    ///</summary>
    public void SetAutoReconnect (bool autoReconnect) // setter function
    {
        _data.flags = MylapsSDK.Utilities.SDKHelperFunctions.SetOrClearBit(
            (uint) _data.flags, (int) LOOPFLAGS.lfAutoReconnect, autoReconnect);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the check-sync-pulse option for this loop.
    ///</summary>
    public void SetCheckSyncPulse (bool check) // setter function
    {
        _data.flags = MylapsSDK.Utilities.SDKHelperFunctions.SetOrClearBit(
            (uint) _data.flags, (int) LOOPFLAGS.lfCheckSyncPulse, check);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the number of loop-triggers for this loop.
    ///</summary>
    public void SetLoopTriggers (byte triggerCount) // setter function
    {
        unchecked {
    _data.flags &= (uint) ~(0x03 << 16);
    _data.flags |= (uint) (triggerCount & 0x03) << 16;
}

        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }

    public string GetUserData(string key, uint length)
    {
        var result = new System.Text.StringBuilder((int)length);
        if (MylapsSDKLibrary.NativeMethods.mta_loop_get_userdata(_handleWrapper.NativeHandle, _data.id, key, result, length))
            return result.ToString();
        else
            return null;
    }

    public bool AddUserData(string key, string value)
    {
        return MylapsSDKLibrary.NativeMethods.mta_loop_add_userdata(_handleWrapper.NativeHandle, _nativePointer, key, value);
    }

    public bool RemoveUserData(string key)
    {
        return MylapsSDKLibrary.NativeMethods.mta_loop_remove_userdata(_handleWrapper.NativeHandle, _nativePointer, key);
    }

    

    
}

}