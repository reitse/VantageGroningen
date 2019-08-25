namespace MylapsSDK.Objects
{
    public enum SYSTEMSETUPMODE {
        ssmMain = 0, // Is this system-setup in main mode (normal operation)?
        ssmBackup = 1, // Is this system-setup in backup mode (backup mode doesn't send anything -to- the decoders)?
        ssmSlave = 2, // Is this system-setup in slave mode (the slave only copies the events from the master)? Only available for X2 Pro Servers.
        ssmMirror = 3, // Is this system-setup in mirror mode (the mirror copies all settings and events of the master)?
        ssmReplayer = 4, // Is this system-setup in replayer mode (replay a part of the captured data)? Only available for X2 Pro Servers.
        ssmOffline = 5 // Is this system-setup in offline mode (it's not possible to connect to decoders)?
    }
    public enum SYSTEMSETUPSPORT {
        sssCarRacing = 0, // For this sport the system is optimized for car racing.
        sssBikeRacing = 1, // For this sport the system is optimized for bike racing.
        sssCompetitionKarting = 2, // For this sport the system is optimized for competition karting.
        sssRentalKarting = 3, // For this sport the system is optimized for rental karting.
        sssMotocross = 4, // For this sport the system is optimized for motocross.
        sssStockCarRacing = 5, // For this sport the system is optimized for stock car racing.
        sssIceSkating = 6, // For this sport the system is optimized for ice skating.
        sssInlineSkating = 7, // For this sport the system is optimized for inline skating.
        sssCycling = 8, // For this sport the system is optimized for cycling.
        sssRunning = 9 // For this sport the system is optimized for running.
    }
    public enum SYSTEMSETUPFLAGS {
        ssfIsCurrent, // The system-setup is currently in use.
        ssfStandalone, // The system-setup used standalone type synchronization.
        ssfFirstnameFirst = 2 // Show the first-name of the competitors first (default: last-name, first-name)?
    }
    public enum SYSTEMSETUPSTATUS {
        sssReplayerActive = 0 // Is the system-setup currently replaying some data?
    }

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// A system-setup contains all equipment that an organization has.
    internal struct SystemSetupStruct
    {
        public long replayerutcstarttime; //  The time (in UTC, microseconds since 1/1/1970) the replay for this system setup was started.
        public uint replayerutcbegintime; //  The begin time (in UTC, seconds since 1/1/1970) of the timespan to replay.
        public uint replayerutcendtime; //  The end time (in UTC, seconds since 1/1/1970) of the timespan to replay.
        public uint id; //  The unique id of the system setup.
        public uint flags; //  The misc. flags.
        public uint statusflags; //  Misc. statuses of the system-setup (e.g. master replayer active).
        public byte syncpulseinput; //  The synchronization-check input (UINT8_MAX means don't check the sync).
        public byte mode; //  The mode of the system setup (see #SYSTEMSETUPMODE).
        public byte masterconnectionstate; //  The connection state to the master MTA. (see #CONNECTIONSTATE).
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 64)]
        public byte[] description; //  A description of the system setup. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 40)]
        public byte[] imagehash; //  The md5 hash for the system-setup picture (only PNG format is supported).
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 24)]
        public byte[] reserved; //  Reserved for future use
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 64)]
        public byte[] timezoneid; //  The id (e.g. Europe/Amsterdam) of the timezone used in the system setup. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 64)]
        public byte[] masterhostname; //  The hostname of the master MTA. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 32)]
        public byte[] masterusername; //  The username for the master MTA. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 32)]
        public byte[] masterpassword; //  The password for the master MTA. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 1)]
        public byte[] padding; //  padding
    }
#pragma warning restore 0649

/// <summary>
/// Information about System setup
/// </summary>
/// <remarks>
/// A system-setup contains all equipment that an organization has.
/// </remarks>
public partial class SystemSetup: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly SystemSetupStruct _data;

    private readonly string _description;
    private readonly string _imagehash;
    private readonly string _reserved;
    private readonly string _timezoneid;
    private readonly string _masterhostname;
    private readonly string _masterusername;
    private readonly string _masterpassword;

    private MTA _handleWrapper;

    internal SystemSetup(System.IntPtr nativePointer, MTA context)
    {
        _nativePointer = nativePointer;
        _data = (SystemSetupStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(SystemSetupStruct));

        _handleWrapper = context;
        _description = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.description);
        _imagehash = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.imagehash);
        _reserved = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.reserved);
        _timezoneid = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.timezoneid);
        _masterhostname = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.masterhostname);
        _masterusername = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.masterusername);
        _masterpassword = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.masterpassword);
    }
	
	internal static SystemSetup FromNativePointer(
        System.IntPtr pointerToNativeStruct, MTA context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new SystemSetup(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<SystemSetup> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, MTA context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<SystemSetup>(
            System.Array.ConvertAll<System.IntPtr,SystemSetup>(ptrArray,
                ptr => new SystemSetup(ptr, context)));
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
    ///The time (in UTC, microseconds since 1/1/1970) the replay for this system setup was started.
    ///</summary>
	public long ReplayerUTCStartTime
    {
        get { return (long) _data.replayerutcstarttime; }
    }
    ///<summary>
    ///The begin time (in UTC, seconds since 1/1/1970) of the timespan to replay.
    ///</summary>
	public uint ReplayerUTCBeginTime
    {
        get { return (uint) _data.replayerutcbegintime; }
    }
    ///<summary>
    ///The end time (in UTC, seconds since 1/1/1970) of the timespan to replay.
    ///</summary>
	public uint ReplayerUTCEndTime
    {
        get { return (uint) _data.replayerutcendtime; }
    }
    ///<summary>
    ///The unique id of the system setup.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The misc. flags.
    ///</summary>
	public uint Flags
    {
        get { return (uint) _data.flags; }
    }
    ///<summary>
    ///Misc. statuses of the system-setup (e.g. master replayer active).
    ///</summary>
	public uint StatusFlags
    {
        get { return (uint) _data.statusflags; }
    }
    ///<summary>
    ///The synchronization-check input (UINT8_MAX means don't check the sync).
    ///</summary>
	public byte SyncPulseInput
    {
        get { return (byte) _data.syncpulseinput; }
    }
    ///<summary>
    ///The mode of the system setup (see #SYSTEMSETUPMODE).
    ///</summary>
	public byte Mode
    {
        get { return (byte) _data.mode; }
    }
    ///<summary>
    ///The connection state to the master MTA. (see #CONNECTIONSTATE).
    ///</summary>
	public byte MasterConnectionState
    {
        get { return (byte) _data.masterconnectionstate; }
    }
    ///<summary>
    ///A description of the system setup. The character data is in UTF-8 encoding.
    ///</summary>
    public string Description
    {
        get { return _description; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The md5 hash for the system-setup picture (only PNG format is supported).
    ///</summary>
    public string ImageHash
    {
        get { return _imagehash; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///Reserved for future use
    ///</summary>
    public string Reserved
    {
        get { return _reserved; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The id (e.g. Europe/Amsterdam) of the timezone used in the system setup. The character data is in UTF-8 encoding.
    ///</summary>
    public string TimezoneID
    {
        get { return _timezoneid; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The hostname of the master MTA. The character data is in UTF-8 encoding.
    ///</summary>
    public string MasterHostname
    {
        get { return _masterhostname; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The username for the master MTA. The character data is in UTF-8 encoding.
    ///</summary>
    public string MasterUsername
    {
        get { return _masterusername; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The password for the master MTA. The character data is in UTF-8 encoding.
    ///</summary>
    public string MasterPassword
    {
        get { return _masterpassword; } // return local datamember which is an utf8 encoded string
    }

    ///<summary>
    ///Is the system-setup currently in use?
    ///</summary>
    public bool IsCurrent()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) SYSTEMSETUPFLAGS.ssfIsCurrent);
    }
    ///<summary>
    ///Is the system-setup synchronized standalone?
    ///</summary>
    public bool IsStandalone()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) SYSTEMSETUPFLAGS.ssfStandalone);
    }
    ///<summary>
    ///Show the first-name of the competitor first (default: last-name, first-name)?
    ///</summary>
    public bool IsFirstNameFirst()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) SYSTEMSETUPFLAGS.ssfFirstnameFirst);
    }
    ///<summary>
    ///Is the system-setup in slave/mirror mode connected to the master MTA?
    ///</summary>
    public bool IsMasterConnected()
    {
        return (_data.masterconnectionstate == (byte) MylapsSDKLibrary.CONNECTIONSTATE.csIsStreaming);

    }
    ///<summary>
    ///Is the system-setup currently replaying some data?
    ///</summary>
    public bool IsReplayerActive()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) SYSTEMSETUPSTATUS.sssReplayerActive);
    }
    ///<summary>
    ///Get the active sport for this system-setup.
    ///</summary>
    public byte GetSport()
    {
        return (System.Byte)((int)(_data.flags >> 16) & 0xFF);

    }

    public string GetUserData(string key, uint length)
    {
        var result = new System.Text.StringBuilder((int)length);
        if (MylapsSDKLibrary.NativeMethods.mta_systemsetup_get_userdata(_handleWrapper.NativeHandle, _data.id, key, result, length))
            return result.ToString();
        else
            return null;
    }

    
}

/// <summary>
/// Modifier for System setup
/// </summary>
/// <remarks>
/// A system-setup contains all equipment that an organization has.
/// </remarks>
public class SystemSetupModifier : MylapsSDK.MylapsSDKLibrary.GenericModifier<SystemSetup>
{
    private readonly System.IntPtr _nativePointer;
    private SystemSetupStruct _data;
    private MTA _handleWrapper;

    private readonly string _description;
    private readonly string _imagehash;
    private readonly string _reserved;
    private readonly string _timezoneid;
    private readonly string _masterhostname;
    private readonly string _masterusername;
    private readonly string _masterpassword;

    internal SystemSetupModifier(System.IntPtr nativePointer, MTA context):
        base(nativePointer)
	{
	    _nativePointer = nativePointer;
	    _handleWrapper = context;
		_data = (SystemSetupStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(nativePointer, typeof(SystemSetupStruct));
        _description = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.description);
        _imagehash = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.imagehash);
        _reserved = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.reserved);
        _timezoneid = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.timezoneid);
        _masterhostname = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.masterhostname);
        _masterusername = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.masterusername);
        _masterpassword = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.masterpassword);
	}

    ///<summary>
    ///The time (in UTC, microseconds since 1/1/1970) the replay for this system setup was started.
    ///</summary>
	public long ReplayerUTCStartTime
    {
        get { return (long) _data.replayerutcstarttime; }
    }
    ///<summary>
    ///The begin time (in UTC, seconds since 1/1/1970) of the timespan to replay.
    ///</summary>
	public uint ReplayerUTCBeginTime
    {
        get { return (uint) _data.replayerutcbegintime; }
    }
    ///<summary>
    ///The end time (in UTC, seconds since 1/1/1970) of the timespan to replay.
    ///</summary>
	public uint ReplayerUTCEndTime
    {
        get { return (uint) _data.replayerutcendtime; }
    }
    ///<summary>
    ///The unique id of the system setup.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The misc. flags.
    ///</summary>
	public uint Flags
    {
        get { return (uint) _data.flags; }
    }
    ///<summary>
    ///Misc. statuses of the system-setup (e.g. master replayer active).
    ///</summary>
	public uint StatusFlags
    {
        get { return (uint) _data.statusflags; }
    }
    ///<summary>
    ///The synchronization-check input (UINT8_MAX means don't check the sync).
    ///</summary>
	public byte SyncPulseInput
    {
        get { return (byte) _data.syncpulseinput; }
    }
    ///<summary>
    ///The mode of the system setup (see #SYSTEMSETUPMODE).
    ///</summary>
	public byte Mode
    {
        get { return (byte) _data.mode; }
    }
    ///<summary>
    ///The connection state to the master MTA. (see #CONNECTIONSTATE).
    ///</summary>
	public byte MasterConnectionState
    {
        get { return (byte) _data.masterconnectionstate; }
    }
    ///<summary>
    ///A description of the system setup. The character data is in UTF-8 encoding.
    ///</summary>
    public string Description
    {
        get { return _description; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The md5 hash for the system-setup picture (only PNG format is supported).
    ///</summary>
    public string ImageHash
    {
        get { return _imagehash; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///Reserved for future use
    ///</summary>
    public string Reserved
    {
        get { return _reserved; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The id (e.g. Europe/Amsterdam) of the timezone used in the system setup. The character data is in UTF-8 encoding.
    ///</summary>
    public string TimezoneID
    {
        get { return _timezoneid; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The hostname of the master MTA. The character data is in UTF-8 encoding.
    ///</summary>
    public string MasterHostname
    {
        get { return _masterhostname; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The username for the master MTA. The character data is in UTF-8 encoding.
    ///</summary>
    public string MasterUsername
    {
        get { return _masterusername; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The password for the master MTA. The character data is in UTF-8 encoding.
    ///</summary>
    public string MasterPassword
    {
        get { return _masterpassword; } // return local datamember which is an utf8 encoded string
    }


    ///<summary>
    ///Is the system-setup currently in use?
    ///</summary>
    public bool IsCurrent()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) SYSTEMSETUPFLAGS.ssfIsCurrent);
    }
    ///<summary>
    ///Is the system-setup synchronized standalone?
    ///</summary>
    public bool IsStandalone()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) SYSTEMSETUPFLAGS.ssfStandalone);
    }
    ///<summary>
    ///Show the first-name of the competitor first (default: last-name, first-name)?
    ///</summary>
    public bool IsFirstNameFirst()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) SYSTEMSETUPFLAGS.ssfFirstnameFirst);
    }
    ///<summary>
    ///Is the system-setup in slave/mirror mode connected to the master MTA?
    ///</summary>
    public bool IsMasterConnected()
    {
        return (_data.masterconnectionstate == (byte) MylapsSDKLibrary.CONNECTIONSTATE.csIsStreaming);

    }
    ///<summary>
    ///Is the system-setup currently replaying some data?
    ///</summary>
    public bool IsReplayerActive()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.statusflags, (int) SYSTEMSETUPSTATUS.sssReplayerActive);
    }
    ///<summary>
    ///Get the active sport for this system-setup.
    ///</summary>
    public byte GetSport()
    {
        return (System.Byte)((int)(_data.flags >> 16) & 0xFF);

    }

    ///<summary>
    ///Set the description for this system-setup.
    ///</summary>
    public void SetDescription (string description) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(description,_data.description);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the timezone identifier for this system-setup.
    ///</summary>
    public void SetTimezoneID (string timezoneID) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(timezoneID,_data.timezoneid);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the md5 hash for the system-setup picture.
    ///</summary>
    public void SetImageHash (string imageHash) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(imageHash,_data.imagehash);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Is the system-setup currently in use?
    ///</summary>
    public void SetIsCurrent (bool current) // setter function
    {
        _data.flags = MylapsSDK.Utilities.SDKHelperFunctions.SetOrClearBit(
            (uint) _data.flags, (int) SYSTEMSETUPFLAGS.ssfIsCurrent, current);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Is the system-setup synchronized standalone?
    ///</summary>
    public void SetIsStandalone (bool standalone) // setter function
    {
        _data.flags = MylapsSDK.Utilities.SDKHelperFunctions.SetOrClearBit(
            (uint) _data.flags, (int) SYSTEMSETUPFLAGS.ssfStandalone, standalone);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Show the first-name of the competitors first (default: 'last-name, first-name')?
    ///</summary>
    public void SetFirstNameFirst (bool firstNameFirst) // setter function
    {
        _data.flags = MylapsSDK.Utilities.SDKHelperFunctions.SetOrClearBit(
            (uint) _data.flags, (int) SYSTEMSETUPFLAGS.ssfFirstnameFirst, firstNameFirst);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the synchronization-check pulse input (UINT8_MAX means don't check).
    ///</summary>
    public void SetSyncPulseInput (byte syncPulseInput) // setter function
    {
		_data.syncpulseinput = (byte) syncPulseInput;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the mode for this system-setup (see #SYSTEMSETUPMODE).
    ///</summary>
    public void SetMode (byte mode) // setter function
    {
		_data.mode = (byte) mode;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the configuration of the master MTA for this system-setup.
    ///</summary>
    public void SetMasterConfig (string hostname, string username, string password) // setter function
    {
        MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(hostname,_data.masterhostname);
MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(username,_data.masterusername);
MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(password,_data.masterpassword);

        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set/reset the replayer configuration for this system-setup (specifying UINT32_MAX for the UTC begin time will end the replay).
    ///</summary>
    public void SetReplayerConfig (uint utcBeginTime, uint utcEndTime) // setter function
    {
        _data.replayerutcbegintime = utcBeginTime;
	_data.replayerutcendtime = utcEndTime;

        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the sport for this system-setup (see #SYSTEMSETUPSPORT).
    ///</summary>
    public void SetSport (byte sport) // setter function
    {
        _data.flags &= ~((uint)0x000F0000);
	_data.flags |= (uint)((sport & 0xFF) << 8);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }

    public string GetUserData(string key, uint length)
    {
        var result = new System.Text.StringBuilder((int)length);
        if (MylapsSDKLibrary.NativeMethods.mta_systemsetup_get_userdata(_handleWrapper.NativeHandle, _data.id, key, result, length))
            return result.ToString();
        else
            return null;
    }

    public bool AddUserData(string key, string value)
    {
        return MylapsSDKLibrary.NativeMethods.mta_systemsetup_add_userdata(_handleWrapper.NativeHandle, _nativePointer, key, value);
    }

    public bool RemoveUserData(string key)
    {
        return MylapsSDKLibrary.NativeMethods.mta_systemsetup_remove_userdata(_handleWrapper.NativeHandle, _nativePointer, key);
    }

    

    
}

}