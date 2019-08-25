namespace MylapsSDK.Objects
{
    public enum TRANSPONDERFLAGS {
        tlfActive = 0, // This transponder is active.
        tlfClockSynced = 1, // The transponder clock is synced with the decoder clock. (only used for X2 transponders)
        tlfLowBattery = 2, // The transponder battery is low.
        tlfBlocked = 3, // The transponder is blocked.
        tlfRunningFromBackup = 4, // Running from the backup transponder (only used for X2 transponders).
        tlfTwoWayOnBattery = 5, // The transponder is running 2-way functions from battery (only used for X2 transponders).
        tlfStatusAvailable = 6, // The transponder status is available (only used for X2 transponders).
        tlfLabelSet = 7, // The transponder label is set.
        tlfHighTemperature = 8, // The transponder temperature is high.
        tlfIncompatible = 9 // The transponder is incompatible with the selected sport.
    }
    public enum TRANSPONDERCOLORS {
        tlcWhite = 0, // Transponder has a white label.
        tlcBlack = 1, // Transponder has a black label.
        tlcYellow = 2, // Transponder has a yellow label.
        tlcOrange = 3, // Transponder has a orange label.
        tlcRed = 4, // Transponder has a red label.
        tlcGreen = 5, // Transponder has a green label.
        tlcBlue = 6, // Transponder has a blue label.
        tlcPurple = 7 // Transponder has a purple label.
    }

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// The transponder is used to put a label on e.g. a passing that makes more sense to the end user (e.g. 'TLA') than a long transponder number.
    internal struct TransponderStruct
    {
        public uint id; //  The transponder number.
        public uint systemsetupid; //  The unique ID of the system-setup this transponder belongs to.
        public uint shortid; //  The 'short' (user-definable) transponder number.
        public uint groupid; //  The unique group ID.
        public uint flags; //  Misc. flags.
        public uint lastactivitydate; //  The last activity date.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 16)]
        public byte[] label; //  The label. The character data is in UTF-8 encoding.
    }
#pragma warning restore 0649

/// <summary>
/// Information about Transponder
/// </summary>
/// <remarks>
/// The transponder is used to put a label on e.g. a passing that makes more sense to the end user (e.g. 'TLA') than a long transponder number.
/// </remarks>
public partial class Transponder: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly TransponderStruct _data;

    private readonly string _label;

    private MTA _handleWrapper;

    internal Transponder(System.IntPtr nativePointer, MTA context)
    {
        _nativePointer = nativePointer;
        _data = (TransponderStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(TransponderStruct));

        _handleWrapper = context;
        _label = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.label);
    }
	
	internal static Transponder FromNativePointer(
        System.IntPtr pointerToNativeStruct, MTA context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new Transponder(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<Transponder> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, MTA context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<Transponder>(
            System.Array.ConvertAll<System.IntPtr,Transponder>(ptrArray,
                ptr => new Transponder(ptr, context)));
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
    ///The transponder number.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The unique ID of the system-setup this transponder belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The 'short' (user-definable) transponder number.
    ///</summary>
	public uint ShortID
    {
        get { return (uint) _data.shortid; }
    }
    ///<summary>
    ///The unique group ID.
    ///</summary>
	public uint GroupID
    {
        get { return (uint) _data.groupid; }
    }
    ///<summary>
    ///The label. The character data is in UTF-8 encoding.
    ///</summary>
    public string Label
    {
        get { return _label; } // return local datamember which is an utf8 encoded string
    }

    ///<summary>
    ///Is the short ID set?
    ///</summary>
    public bool IsShortIDSet()
    {
        return _data.shortid != System.UInt32.MaxValue;

    }
    ///<summary>
    ///Is the transponder active?
    ///</summary>
    public bool IsActive()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TRANSPONDERFLAGS.tlfActive);
    }
    ///<summary>
    ///Is the transponder clock synced with the decoder clock (only used for X2 transponders)?
    ///</summary>
    public bool IsClockSynced()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TRANSPONDERFLAGS.tlfClockSynced);
    }
    ///<summary>
    ///Is the transponders battery low?
    ///</summary>
    public bool IsLowBattery()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TRANSPONDERFLAGS.tlfLowBattery);
    }
    ///<summary>
    ///Is the transponder blocked?
    ///</summary>
    public bool IsBlocked()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TRANSPONDERFLAGS.tlfBlocked);
    }
    ///<summary>
    ///Is running from the backup transponder (only used for X2 transponders)?
    ///</summary>
    public bool IsRunningFromBackup()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TRANSPONDERFLAGS.tlfRunningFromBackup);
    }
    ///<summary>
    ///Is the transponder running 2-way functions from battery (only used for X2 transponders)?
    ///</summary>
    public bool IsTwowayOnBattery()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TRANSPONDERFLAGS.tlfTwoWayOnBattery);
    }
    ///<summary>
    ///Is the transponder status available (only used for X2 transponders)?
    ///</summary>
    public bool IsStatusAvailable()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TRANSPONDERFLAGS.tlfStatusAvailable);
    }
    ///<summary>
    ///Is the transponder label set?
    ///</summary>
    public bool IsLabelSet()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TRANSPONDERFLAGS.tlfLabelSet);
    }
    ///<summary>
    ///Is the transponders temperature high?
    ///</summary>
    public bool IsHighTemperature()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TRANSPONDERFLAGS.tlfHighTemperature);
    }
    ///<summary>
    ///Is the transponders incompatible with the selected sport?
    ///</summary>
    public bool IsIncompatible()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TRANSPONDERFLAGS.tlfIncompatible);
    }
    ///<summary>
    ///Get the transponder type (see #PASSINGTRANSPONDERTYPE).
    ///</summary>
    public uint GetTransponderType()
    {
        return (_data.flags >> 16) & 0x0F;

    }
    ///<summary>
    ///Get the transponder color (see #TRANSPONDERCOLORS).
    ///</summary>
    public uint GetColor()
    {
        return (_data.flags >> 20) & 0x07;

    }
    ///<summary>
    ///Get the date the transponder was last active.
    ///</summary>
    public long GetLastActivityDate()
    {
        if (_data.lastactivitydate == System.UInt32.MaxValue)
    return MylapsSDK.Utilities.MdpTime.InvalidTime;
else
    return _data.lastactivitydate * MylapsSDK.Utilities.MdpTime.Day;




    }

    public string GetUserData(string key, uint length)
    {
        var result = new System.Text.StringBuilder((int)length);
        if (MylapsSDKLibrary.NativeMethods.mta_transponder_get_userdata(_handleWrapper.NativeHandle, _data.id, key, result, length))
            return result.ToString();
        else
            return null;
    }

    
}

/// <summary>
/// Modifier for Transponder
/// </summary>
/// <remarks>
/// The transponder is used to put a label on e.g. a passing that makes more sense to the end user (e.g. 'TLA') than a long transponder number.
/// </remarks>
public class TransponderModifier : MylapsSDK.MylapsSDKLibrary.GenericModifier<Transponder>
{
    private readonly System.IntPtr _nativePointer;
    private TransponderStruct _data;
    private MTA _handleWrapper;

    private readonly string _label;

    internal TransponderModifier(System.IntPtr nativePointer, MTA context):
        base(nativePointer)
	{
	    _nativePointer = nativePointer;
	    _handleWrapper = context;
		_data = (TransponderStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(nativePointer, typeof(TransponderStruct));
        _label = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.label);
	}

    ///<summary>
    ///The transponder number.
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The unique ID of the system-setup this transponder belongs to.
    ///</summary>
	public uint SystemSetupID
    {
        get { return (uint) _data.systemsetupid; }
    }
    ///<summary>
    ///The 'short' (user-definable) transponder number.
    ///</summary>
	public uint ShortID
    {
        get { return (uint) _data.shortid; }
    }
    ///<summary>
    ///The unique group ID.
    ///</summary>
	public uint GroupID
    {
        get { return (uint) _data.groupid; }
    }
    ///<summary>
    ///The label. The character data is in UTF-8 encoding.
    ///</summary>
    public string Label
    {
        get { return _label; } // return local datamember which is an utf8 encoded string
    }


    ///<summary>
    ///Is the short ID set?
    ///</summary>
    public bool IsShortIDSet()
    {
        return _data.shortid != System.UInt32.MaxValue;

    }
    ///<summary>
    ///Is the transponder active?
    ///</summary>
    public bool IsActive()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TRANSPONDERFLAGS.tlfActive);
    }
    ///<summary>
    ///Is the transponder clock synced with the decoder clock (only used for X2 transponders)?
    ///</summary>
    public bool IsClockSynced()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TRANSPONDERFLAGS.tlfClockSynced);
    }
    ///<summary>
    ///Is the transponders battery low?
    ///</summary>
    public bool IsLowBattery()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TRANSPONDERFLAGS.tlfLowBattery);
    }
    ///<summary>
    ///Is the transponder blocked?
    ///</summary>
    public bool IsBlocked()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TRANSPONDERFLAGS.tlfBlocked);
    }
    ///<summary>
    ///Is running from the backup transponder (only used for X2 transponders)?
    ///</summary>
    public bool IsRunningFromBackup()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TRANSPONDERFLAGS.tlfRunningFromBackup);
    }
    ///<summary>
    ///Is the transponder running 2-way functions from battery (only used for X2 transponders)?
    ///</summary>
    public bool IsTwowayOnBattery()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TRANSPONDERFLAGS.tlfTwoWayOnBattery);
    }
    ///<summary>
    ///Is the transponder status available (only used for X2 transponders)?
    ///</summary>
    public bool IsStatusAvailable()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TRANSPONDERFLAGS.tlfStatusAvailable);
    }
    ///<summary>
    ///Is the transponder label set?
    ///</summary>
    public bool IsLabelSet()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TRANSPONDERFLAGS.tlfLabelSet);
    }
    ///<summary>
    ///Is the transponders temperature high?
    ///</summary>
    public bool IsHighTemperature()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TRANSPONDERFLAGS.tlfHighTemperature);
    }
    ///<summary>
    ///Is the transponders incompatible with the selected sport?
    ///</summary>
    public bool IsIncompatible()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) TRANSPONDERFLAGS.tlfIncompatible);
    }
    ///<summary>
    ///Get the transponder type (see #PASSINGTRANSPONDERTYPE).
    ///</summary>
    public uint GetTransponderType()
    {
        return (_data.flags >> 16) & 0x0F;

    }
    ///<summary>
    ///Get the transponder color (see #TRANSPONDERCOLORS).
    ///</summary>
    public uint GetColor()
    {
        return (_data.flags >> 20) & 0x07;

    }
    ///<summary>
    ///Get the date the transponder was last active.
    ///</summary>
    public long GetLastActivityDate()
    {
        if (_data.lastactivitydate == System.UInt32.MaxValue)
    return MylapsSDK.Utilities.MdpTime.InvalidTime;
else
    return _data.lastactivitydate * MylapsSDK.Utilities.MdpTime.Day;




    }

    ///<summary>
    ///Set the label for this transponder.
    ///</summary>
    public void SetLabel (string label) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(label,_data.label);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the group ID for this transponder.
    ///</summary>
    public void SetGroupid (uint id) // setter function
    {
		_data.groupid = (uint) id;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the short ID for this transponder.
    ///</summary>
    public void SetShortid (uint id) // setter function
    {
		_data.shortid = (uint) id;
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the transponder type (see #TRANSPONDERTYPE) for this transponder-label.
    ///</summary>
    public void SetTransponderType (uint transponderType) // setter function
    {
        unchecked {
    _data.flags &= (uint) ~(0x0F0F0000);
    _data.flags |=  (((transponderType >> 4) & 0x0F) << 24) | ((transponderType & 0x0F) << 16);
}

        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the transponder color (see #TRANSPONDERCOLORS) for this transponder-label.
    ///</summary>
    public void SetColor (uint color) // setter function
    {
        unchecked {
    _data.flags &= (uint) ~(0x00F00000);
    _data.flags |= ((color & 0x0F) << 20);
}

        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }

    public string GetUserData(string key, uint length)
    {
        var result = new System.Text.StringBuilder((int)length);
        if (MylapsSDKLibrary.NativeMethods.mta_transponder_get_userdata(_handleWrapper.NativeHandle, _data.id, key, result, length))
            return result.ToString();
        else
            return null;
    }

    public bool AddUserData(string key, string value)
    {
        return MylapsSDKLibrary.NativeMethods.mta_transponder_add_userdata(_handleWrapper.NativeHandle, _nativePointer, key, value);
    }

    public bool RemoveUserData(string key)
    {
        return MylapsSDKLibrary.NativeMethods.mta_transponder_remove_userdata(_handleWrapper.NativeHandle, _nativePointer, key);
    }

    

    
}

}