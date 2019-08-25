namespace MylapsSDK.Objects
{
    public enum AVAILABLEAPPLIANCEFLAGS {
        aafIsCompatible = 0, // Is this appliance compatible?
        aafIsZeroConf = 1, // Is this appliance available on the local network (resolved by zero-configuration networking)?
        aafIsRelease = 2 // Is the firmware version a release version?
    }
    public enum AVAILABLEAPPLIANCEMODE {
        aamMain = 0, // Is this appliance in main mode (normal operation)?
        aamBackup = 1, // Is this appliance in backup mode (backup mode doesn't send anything -to- the decoders)?
        aamSlave = 2, // Is this appliance in slave mode (the slave only copies the events from the master)?
        aamMirror = 3, // Is this appliance in mirror mode (the mirror copies all settings and events of the master)?
        aamReplayer = 4 // Is this appliance in replayer mode (replay a part of the captured data)?
    }
    public enum AVAILABLEAPPLIANCETYPE {
        APPLIANCE_TYPE_X2_PRO_SERVER = 0, // Type X2 Pro Server
        APPLIANCE_TYPE_X2_SERVER = 1, // Type X2 Server
        APPLIANCE_TYPE_X2_CLOUD = 66 // Type X2 Cloud
    }

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// The appliance is available to connect to (e.g. in the local network, and/or resolved through zero-configuration networking).
    internal struct AvailableApplianceStruct
    {
        public long macaddress; //  The MAC address
        public uint ipaddress; //  The IP address
        public uint mode; //  The appliance mode (see #AVAILABLEAPPLIANCEMODE)
        public uint buildnumber; //  The build number
        public uint flags; //  Misc. flags
        public ushort port; //  The port number
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 64)]
        public byte[] hostname; //  The fully qualified domain name (FQDN). Use this as the hostname to connect to an appliance.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 32)]
        public byte[] name; //  The name of the appliance. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 32)]
        public byte[] releasename; //  The release name (e.g. 1.0 SP1). The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 64)]
        public byte[] systemsetup; //  The system-setup description (e.g. 'Paul Ricard'). The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 64)]
        public byte[] timezoneid; //  The timezone of the system-setup (e.g. 'Europe/Amsterdam'). The character data is in UTF-8 encoding.
        public byte type; //  The appliance type (see #AVAILABLEAPPLIANCETYPE)
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 5)]
        public byte[] padding; //  padding
    }
#pragma warning restore 0649

/// <summary>
/// Information about Available appliance
/// </summary>
/// <remarks>
/// The appliance is available to connect to (e.g. in the local network, and/or resolved through zero-configuration networking).
/// </remarks>
public partial class AvailableAppliance{
    private readonly System.IntPtr _nativePointer;
    private readonly AvailableApplianceStruct _data;

    private readonly string _hostname;
    private readonly string _name;
    private readonly string _releasename;
    private readonly string _systemsetup;
    private readonly string _timezoneid;

    private SDK _handleWrapper;

    internal AvailableAppliance(System.IntPtr nativePointer, SDK context)
    {
        _nativePointer = nativePointer;
        _data = (AvailableApplianceStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(AvailableApplianceStruct));

        _handleWrapper = context;
        _hostname = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.hostname);
        _name = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.name);
        _releasename = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.releasename);
        _systemsetup = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.systemsetup);
        _timezoneid = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.timezoneid);
    }
	
	internal static AvailableAppliance FromNativePointer(
        System.IntPtr pointerToNativeStruct, SDK context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new AvailableAppliance(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<AvailableAppliance> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, SDK context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<AvailableAppliance>(
            System.Array.ConvertAll<System.IntPtr,AvailableAppliance>(ptrArray,
                ptr => new AvailableAppliance(ptr, context)));
    }

    internal System.IntPtr NativePointer
    {
        get { return _nativePointer; }
    }

    internal SDK Context
    {
        get { return _handleWrapper; }
    }

    ///<summary>
    ///The MAC address
    ///</summary>
	public long MacAddress
    {
        get { return (long) _data.macaddress; }
    }
    ///<summary>
    ///The IP address
    ///</summary>
	public uint IPAddress
    {
        get { return (uint) _data.ipaddress; }
    }
    ///<summary>
    ///The appliance mode (see #AVAILABLEAPPLIANCEMODE)
    ///</summary>
	public uint Mode
    {
        get { return (uint) _data.mode; }
    }
    ///<summary>
    ///The build number
    ///</summary>
	public uint BuildNumber
    {
        get { return (uint) _data.buildnumber; }
    }
    ///<summary>
    ///The port number
    ///</summary>
	public ushort Port
    {
        get { return (ushort) _data.port; }
    }
    ///<summary>
    ///The fully qualified domain name (FQDN). Use this as the hostname to connect to an appliance.
    ///</summary>
    public string Hostname
    {
        get { return _hostname; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The name of the appliance. The character data is in UTF-8 encoding.
    ///</summary>
    public string Name
    {
        get { return _name; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The release name (e.g. 1.0 SP1). The character data is in UTF-8 encoding.
    ///</summary>
    public string ReleaseName
    {
        get { return _releasename; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The system-setup description (e.g. 'Paul Ricard'). The character data is in UTF-8 encoding.
    ///</summary>
    public string SystemSetup
    {
        get { return _systemsetup; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The timezone of the system-setup (e.g. 'Europe/Amsterdam'). The character data is in UTF-8 encoding.
    ///</summary>
    public string TimezoneID
    {
        get { return _timezoneid; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The appliance type (see #AVAILABLEAPPLIANCETYPE)
    ///</summary>
	public byte Type
    {
        get { return (byte) _data.type; }
    }

    ///<summary>
    ///Is the available-appliance compatible?
    ///</summary>
    public bool IsCompatible()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) AVAILABLEAPPLIANCEFLAGS.aafIsCompatible);
    }
    ///<summary>
    ///Is this appliance available on the local network (resolved by zero-configuration networking)?
    ///</summary>
    public bool IsZeroconf()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) AVAILABLEAPPLIANCEFLAGS.aafIsZeroConf);
    }
    ///<summary>
    ///Is the firmware version on this appliance a release version?
    ///</summary>
    public bool IsRelease()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) AVAILABLEAPPLIANCEFLAGS.aafIsRelease);
    }


    
}

}