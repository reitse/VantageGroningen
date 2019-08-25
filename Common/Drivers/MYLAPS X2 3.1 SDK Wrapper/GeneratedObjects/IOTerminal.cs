namespace MylapsSDK.Objects
{
    public enum TERMINALFLAGS {
        tfAutoReconnect = 1, // Automatically reconnect the decoder when it is available on the network.
        tfCheckSyncPulse // Check the sync pulse.
    }
    public enum TERMINALSTATUS {
        tsOnline = 0, // The I/O terminal is on-line.
        tsSyncOK, // The synchronization is OK.
        tsWarnings, // Does the connected decoder have any warnings?
        tsErrors, // Does the connected decoder have any errors?
        tsUpdating, // Is the firmware of the connected decoder being updated?
        tsReconnecting // Is trying to reconnect to the decoder?
    }

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// A terminal at the track where I/O is done (e.g. traffic light).
    internal struct IOTerminalStruct
    {
        public uint id; //  The unique identifier of the I/O terminal.
        public uint systemsetupid; //  The unique ID of the system-setup this I/O terminal belongs to.
        public uint order; //  The order of the I/O terminal in the system-setup.
        public uint flags; //  Misc. flags of the I/O terminal (e.g. auto-reconnect).
        public uint statusflags; //  Misc. statuses of the I/O terminal (e.g. on-line, active, sync-ok).
        public uint xpos; //  The x-position of the center of the I/O terminal on the system-setup picture.
        public uint ypos; //  The y-position of the center of the I/O terminal on the system-setup picture.
        public int latitude; //  The latitude position (in 1e-7 units, signed).
        public int longitude; //  The longitude position (in 1e-7 units, signed).
        public byte syncmethod; //  The used sync method. (See #SYNCMETHOD, can be GPS or NTP)
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 16)]
        public byte[] name; //  The name of the I/O terminal in the system-setup. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 56)]
        public byte[] description; //  The I/O terminal description. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 3)]
        public byte[] padding; //  padding
    }
#pragma warning restore 0649

/// <summary>
/// Information about I/O Terminal
/// </summary>
/// <remarks>
/// A terminal at the track where I/O is done (e.g. traffic light).
/// </remarks>
public partial class IOTerminal: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly IOTerminalStruct _data;

    private readonly string _name;
    private readonly string _description;

    private MTA _handleWrapper;

    internal IOTerminal(System.IntPtr nativePointer, MTA context)
    {
        _nativePointer = nativePointer;
        _data = (IOTerminalStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(IOTerminalStruct));

        _handleWrapper = context;
        _name = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.name);
        _description = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.description);
    }
	
	internal static IOTerminal FromNativePointer(
        System.IntPtr pointerToNativeStruct, MTA context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new IOTerminal(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<IOTerminal> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, MTA context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<IOTerminal>(
            System.Array.ConvertAll<System.IntPtr,IOTerminal>(ptrArray,
                ptr => new IOTerminal(ptr, context)));
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
    ///The unique identifier of the I/O terminal.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The unique ID of the system-setup this I/O terminal belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The order of the I/O terminal in the system-setup.
    ///</summary>
	public uint Order
    {
        get { return (uint) _data.order; }
    }
    ///<summary>
    ///The x-position of the center of the I/O terminal on the system-setup picture.
    ///</summary>
	public uint XPos
    {
        get { return (uint) _data.xpos; }
    }
    ///<summary>
    ///The y-position of the center of the I/O terminal on the system-setup picture.
    ///</summary>
	public uint YPos
    {
        get { return (uint) _data.ypos; }
    }
    ///<summary>
    ///The latitude position (in 1e-7 units, signed).
    ///</summary>
	public int Latitude
    {
        get { return (int) _data.latitude; }
    }
    ///<summary>
    ///The longitude position (in 1e-7 units, signed).
    ///</summary>
	public int Longitude
    {
        get { return (int) _data.longitude; }
    }
    ///<summary>
    ///The used sync method. (See #SYNCMETHOD, can be GPS or NTP)
    ///</summary>
	public byte SyncMethod
    {
        get { return (byte) _data.syncmethod; }
    }
    ///<summary>
    ///The name of the I/O terminal in the system-setup. The character data is in UTF-8 encoding.
    ///</summary>
    public string Name
    {
        get { return _name; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The I/O terminal description. The character data is in UTF-8 encoding.
    ///</summary>
    public string Description
    {
        get { return _description; } // return local datamember which is an utf8 encoded string
    }

    ///<summary>
    ///Automatically reconnect the decoder when it is available on the network?
    ///</summary>
    public bool UseAutoReconnect()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TERMINALFLAGS.tfAutoReconnect);
    }
    ///<summary>
    ///Check the sync pulse?
    ///</summary>
    public bool CheckSyncPulse()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TERMINALFLAGS.tfCheckSyncPulse);
    }
    ///<summary>
    ///Is the I/O terminal on-line?
    ///</summary>
    public bool IsOnline()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) TERMINALSTATUS.tsOnline);
    }
    ///<summary>
    ///Is the I/O terminal synchronization OK?
    ///</summary>
    public bool IsSyncOk()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) TERMINALSTATUS.tsSyncOK);
    }
    ///<summary>
    ///Does the connected decoder have any warnings?
    ///</summary>
    public bool HasWarnings()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) TERMINALSTATUS.tsWarnings);
    }
    ///<summary>
    ///Does the connected decoder have any errors?
    ///</summary>
    public bool HasErrors()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) TERMINALSTATUS.tsErrors);
    }
    ///<summary>
    ///Is the firmware of the connected decoder being updated?
    ///</summary>
    public bool IsUpdating()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) TERMINALSTATUS.tsUpdating);
    }
    ///<summary>
    ///Is trying to reconnect to the decoder?
    ///</summary>
    public bool IsReconnecting()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) TERMINALSTATUS.tsReconnecting);
    }

    public string GetUserData(string key, uint length)
    {
        var result = new System.Text.StringBuilder((int)length);
        if (MylapsSDKLibrary.NativeMethods.mta_ioterminal_get_userdata(_handleWrapper.NativeHandle, _data.id, key, result, length))
            return result.ToString();
        else
            return null;
    }

    
}

/// <summary>
/// Modifier for I/O Terminal
/// </summary>
/// <remarks>
/// A terminal at the track where I/O is done (e.g. traffic light).
/// </remarks>
public class IOTerminalModifier : MylapsSDK.MylapsSDKLibrary.GenericModifier<IOTerminal>
{
    private readonly System.IntPtr _nativePointer;
    private IOTerminalStruct _data;
    private MTA _handleWrapper;

    private readonly string _name;
    private readonly string _description;

    internal IOTerminalModifier(System.IntPtr nativePointer, MTA context):
        base(nativePointer)
	{
	    _nativePointer = nativePointer;
	    _handleWrapper = context;
		_data = (IOTerminalStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(nativePointer, typeof(IOTerminalStruct));
        _name = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.name);
        _description = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.description);
	}

    ///<summary>
    ///The unique identifier of the I/O terminal.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The unique ID of the system-setup this I/O terminal belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The order of the I/O terminal in the system-setup.
    ///</summary>
	public uint Order
    {
        get { return (uint) _data.order; }
    }
    ///<summary>
    ///The x-position of the center of the I/O terminal on the system-setup picture.
    ///</summary>
	public uint XPos
    {
        get { return (uint) _data.xpos; }
    }
    ///<summary>
    ///The y-position of the center of the I/O terminal on the system-setup picture.
    ///</summary>
	public uint YPos
    {
        get { return (uint) _data.ypos; }
    }
    ///<summary>
    ///The latitude position (in 1e-7 units, signed).
    ///</summary>
	public int Latitude
    {
        get { return (int) _data.latitude; }
    }
    ///<summary>
    ///The longitude position (in 1e-7 units, signed).
    ///</summary>
	public int Longitude
    {
        get { return (int) _data.longitude; }
    }
    ///<summary>
    ///The used sync method. (See #SYNCMETHOD, can be GPS or NTP)
    ///</summary>
	public byte SyncMethod
    {
        get { return (byte) _data.syncmethod; }
    }
    ///<summary>
    ///The name of the I/O terminal in the system-setup. The character data is in UTF-8 encoding.
    ///</summary>
    public string Name
    {
        get { return _name; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The I/O terminal description. The character data is in UTF-8 encoding.
    ///</summary>
    public string Description
    {
        get { return _description; } // return local datamember which is an utf8 encoded string
    }


    ///<summary>
    ///Automatically reconnect the decoder when it is available on the network?
    ///</summary>
    public bool UseAutoReconnect()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TERMINALFLAGS.tfAutoReconnect);
    }
    ///<summary>
    ///Check the sync pulse?
    ///</summary>
    public bool CheckSyncPulse()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TERMINALFLAGS.tfCheckSyncPulse);
    }
    ///<summary>
    ///Is the I/O terminal on-line?
    ///</summary>
    public bool IsOnline()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) TERMINALSTATUS.tsOnline);
    }
    ///<summary>
    ///Is the I/O terminal synchronization OK?
    ///</summary>
    public bool IsSyncOk()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) TERMINALSTATUS.tsSyncOK);
    }
    ///<summary>
    ///Does the connected decoder have any warnings?
    ///</summary>
    public bool HasWarnings()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) TERMINALSTATUS.tsWarnings);
    }
    ///<summary>
    ///Does the connected decoder have any errors?
    ///</summary>
    public bool HasErrors()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) TERMINALSTATUS.tsErrors);
    }
    ///<summary>
    ///Is the firmware of the connected decoder being updated?
    ///</summary>
    public bool IsUpdating()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) TERMINALSTATUS.tsUpdating);
    }
    ///<summary>
    ///Is trying to reconnect to the decoder?
    ///</summary>
    public bool IsReconnecting()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) TERMINALSTATUS.tsReconnecting);
    }

    ///<summary>
    ///Set the order for this I/O terminal.
    ///</summary>
    public void SetOrder (uint order) // setter function
    {
		_data.order = (uint) order;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the x-position of the center of the I/O terminal on the system-setup picture.
    ///</summary>
    public void SetXPos (uint xPos) // setter function
    {
		_data.xpos = (uint) xPos;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the y-position of the center of the I/O terminal on the system-setup picture.
    ///</summary>
    public void SetYPos (uint yPos) // setter function
    {
		_data.ypos = (uint) yPos;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the latitude of the position for this I/O terminal (in 1e-7 units, signed).
    ///</summary>
    public void SetLatitude (int latitude) // setter function
    {
        _data.latitude = MylapsSDK.Utilities.SDKHelperFunctions.ConvertLatLongDouble2Int(latitude);

        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the longitude of the position for this I/O terminal (in 1e-7 units, signed).
    ///</summary>
    public void SetLongitude (int longitude) // setter function
    {
        _data.longitude = MylapsSDK.Utilities.SDKHelperFunctions.ConvertLatLongDouble2Int(longitude);

        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the name for this I/O terminal.
    ///</summary>
    public void SetName (string name) // setter function
    {
        MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(name,_data.name);

        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the description for this I/O terminal.
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
    ///Set the auto-reconnect option for this I/O terminal.
    ///</summary>
    public void SetAutoReconnect (bool reconnect) // setter function
    {
        _data.flags = MylapsSDK.Utilities.SDKHelperFunctions.SetOrClearBit(
            (uint) _data.flags, (int) TERMINALFLAGS.tfAutoReconnect, reconnect);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the check-sync-pulse option for this I/O terminal.
    ///</summary>
    public void SetCheckSyncPulse (bool check) // setter function
    {
        _data.flags = MylapsSDK.Utilities.SDKHelperFunctions.SetOrClearBit(
            (uint) _data.flags, (int) TERMINALFLAGS.tfCheckSyncPulse, check);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }

    public string GetUserData(string key, uint length)
    {
        var result = new System.Text.StringBuilder((int)length);
        if (MylapsSDKLibrary.NativeMethods.mta_ioterminal_get_userdata(_handleWrapper.NativeHandle, _data.id, key, result, length))
            return result.ToString();
        else
            return null;
    }

    public bool AddUserData(string key, string value)
    {
        return MylapsSDKLibrary.NativeMethods.mta_ioterminal_add_userdata(_handleWrapper.NativeHandle, _nativePointer, key, value);
    }

    public bool RemoveUserData(string key)
    {
        return MylapsSDKLibrary.NativeMethods.mta_ioterminal_remove_userdata(_handleWrapper.NativeHandle, _nativePointer, key);
    }

    

    
}

}