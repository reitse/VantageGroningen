namespace MylapsSDK.Objects
{
    public enum COMPETITORFLAGS {
        cfMainTransponderOwner = 0, // Is this competitor the current owner of the main transponder?
        cfBackupTransponderOwner = 1 // Is this competitor the current owner of the backup transponder?
    }
    public enum TRANSPONDERTYPE {
        ttUnavailable = 0, // Not available (or not applicable).
        ttTranX2 = 1, // TranX (160 or 260) transponder.
        ttCarBike = 2, // Car/bike transponder.
        ttX2 = 4, // X2 transponder.
        ttProChip = 6, // ProChip transponder.
        ttMX = 7, // MX transponder.
        ttCarBikeDP = 10, // Car/bike direct power transponder.
        ttRentalKart = 11, // Rental kart transponder.
        ttKart = 12, // Kart transponder.
        ttTranXPro = 13, // TranX pro transponder.
        ttProChipFlex = 14 // ProChip flex transponder.
    }

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// The competitor information.
    internal struct CompetitorStruct
    {
        public uint id; //  The unique identifier of the competitor
        public uint maintransponder; //  The unique identifier of the main transponder
        public uint backuptransponder; //  The unique identifier of the backup transponder
        public uint flags; //  Miscellaneous flags.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 8)]
        public byte[] number; //  The number of the competitor. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 32)]
        public byte[] firstname; //  The first name of the competitor. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 32)]
        public byte[] lastname; //  The last name of the competitor. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 32)]
        public byte[] competitorclass; //  The name of competitor class. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 40)]
        public byte[] imagehash; //  The md5 hash for the competitor picture.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 40)]
        public byte[] guid; //  The global unique identifier (GUID) for the competitor.
    }
#pragma warning restore 0649

/// <summary>
/// Information about Competitor information
/// </summary>
/// <remarks>
/// The competitor information.
/// </remarks>
public partial class Competitor: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly CompetitorStruct _data;

    private readonly string _number;
    private readonly string _firstname;
    private readonly string _lastname;
    private readonly string _competitorclass;
    private readonly string _imagehash;
    private readonly string _guid;

    private MTA _handleWrapper;

    internal Competitor(System.IntPtr nativePointer, MTA context)
    {
        _nativePointer = nativePointer;
        _data = (CompetitorStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(CompetitorStruct));

        _handleWrapper = context;
        _number = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.number);
        _firstname = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.firstname);
        _lastname = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.lastname);
        _competitorclass = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.competitorclass);
        _imagehash = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.imagehash);
        _guid = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.guid);
    }
	
	internal static Competitor FromNativePointer(
        System.IntPtr pointerToNativeStruct, MTA context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new Competitor(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<Competitor> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, MTA context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<Competitor>(
            System.Array.ConvertAll<System.IntPtr,Competitor>(ptrArray,
                ptr => new Competitor(ptr, context)));
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
    ///The unique identifier of the competitor
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The unique identifier of the main transponder
    ///</summary>
	public uint MainTransponder
    {
        get { return (uint) _data.maintransponder; }
    }
    ///<summary>
    ///The unique identifier of the backup transponder
    ///</summary>
	public uint BackupTransponder
    {
        get { return (uint) _data.backuptransponder; }
    }
    ///<summary>
    ///The number of the competitor. The character data is in UTF-8 encoding.
    ///</summary>
    public string Number
    {
        get { return _number; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The first name of the competitor. The character data is in UTF-8 encoding.
    ///</summary>
    public string FirstName
    {
        get { return _firstname; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The last name of the competitor. The character data is in UTF-8 encoding.
    ///</summary>
    public string LastName
    {
        get { return _lastname; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The name of competitor class. The character data is in UTF-8 encoding.
    ///</summary>
    public string CompetitorClass
    {
        get { return _competitorclass; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The md5 hash for the competitor picture.
    ///</summary>
    public string ImageHash
    {
        get { return _imagehash; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The global unique identifier (GUID) for the competitor.
    ///</summary>
    public string GUID
    {
        get { return _guid; } // return local datamember which is an utf8 encoded string
    }

    ///<summary>
    ///Is this competitor the current owner of the main transponder?
    ///</summary>
    public bool IsMainTransponderOwner()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) COMPETITORFLAGS.cfMainTransponderOwner);
    }
    ///<summary>
    ///Is this competitor the current owner of the backup transponder?
    ///</summary>
    public bool IsBackupTransponderOwner()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) COMPETITORFLAGS.cfBackupTransponderOwner);
    }
    ///<summary>
    ///Get the transponder type (for main -and- backup, see #TRANSPONDERTYPE).
    ///</summary>
    public byte GetTransponderType()
    {
        return (System.Byte)((int)(_data.flags >> 8) & 0xFF);

    }

    public string GetUserData(string key, uint length)
    {
        var result = new System.Text.StringBuilder((int)length);
        if (MylapsSDKLibrary.NativeMethods.mta_competitor_get_userdata(_handleWrapper.NativeHandle, _data.id, key, result, length))
            return result.ToString();
        else
            return null;
    }

    
}

/// <summary>
/// Modifier for Competitor information
/// </summary>
/// <remarks>
/// The competitor information.
/// </remarks>
public class CompetitorModifier : MylapsSDK.MylapsSDKLibrary.GenericModifier<Competitor>
{
    private readonly System.IntPtr _nativePointer;
    private CompetitorStruct _data;
    private MTA _handleWrapper;

    private readonly string _number;
    private readonly string _firstname;
    private readonly string _lastname;
    private readonly string _competitorclass;
    private readonly string _imagehash;
    private readonly string _guid;

    internal CompetitorModifier(System.IntPtr nativePointer, MTA context):
        base(nativePointer)
	{
	    _nativePointer = nativePointer;
	    _handleWrapper = context;
		_data = (CompetitorStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(nativePointer, typeof(CompetitorStruct));
        _number = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.number);
        _firstname = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.firstname);
        _lastname = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.lastname);
        _competitorclass = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.competitorclass);
        _imagehash = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.imagehash);
        _guid = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.guid);
	}

    ///<summary>
    ///The unique identifier of the competitor
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The unique identifier of the main transponder
    ///</summary>
	public uint MainTransponder
    {
        get { return (uint) _data.maintransponder; }
    }
    ///<summary>
    ///The unique identifier of the backup transponder
    ///</summary>
	public uint BackupTransponder
    {
        get { return (uint) _data.backuptransponder; }
    }
    ///<summary>
    ///The number of the competitor. The character data is in UTF-8 encoding.
    ///</summary>
    public string Number
    {
        get { return _number; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The first name of the competitor. The character data is in UTF-8 encoding.
    ///</summary>
    public string FirstName
    {
        get { return _firstname; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The last name of the competitor. The character data is in UTF-8 encoding.
    ///</summary>
    public string LastName
    {
        get { return _lastname; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The name of competitor class. The character data is in UTF-8 encoding.
    ///</summary>
    public string CompetitorClass
    {
        get { return _competitorclass; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The md5 hash for the competitor picture.
    ///</summary>
    public string ImageHash
    {
        get { return _imagehash; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The global unique identifier (GUID) for the competitor.
    ///</summary>
    public string GUID
    {
        get { return _guid; } // return local datamember which is an utf8 encoded string
    }


    ///<summary>
    ///Is this competitor the current owner of the main transponder?
    ///</summary>
    public bool IsMainTransponderOwner()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) COMPETITORFLAGS.cfMainTransponderOwner);
    }
    ///<summary>
    ///Is this competitor the current owner of the backup transponder?
    ///</summary>
    public bool IsBackupTransponderOwner()
    {
        return MylapsSDK.Utilities.SDKHelperFunctions.IsBitSet((uint) _data.flags, (int) COMPETITORFLAGS.cfBackupTransponderOwner);
    }
    ///<summary>
    ///Get the transponder type (for main -and- backup, see #TRANSPONDERTYPE).
    ///</summary>
    public byte GetTransponderType()
    {
        return (System.Byte)((int)(_data.flags >> 8) & 0xFF);

    }

    ///<summary>
    ///Set the number for the competitor.
    ///</summary>
    public void SetNumber (string number) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(number,_data.number);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the first name for the competitor.
    ///</summary>
    public void SetFirstName (string firstName) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(firstName,_data.firstname);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the last name for the competitor.
    ///</summary>
    public void SetLastName (string lastName) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(lastName,_data.lastname);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the class name for the competitor.
    ///</summary>
    public void SetCompetitorClass (string competitorClass) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(competitorClass,_data.competitorclass);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the md5 hash for the competitor picture.
    ///</summary>
    public void SetImageHash (string imageHash) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(imageHash,_data.imagehash);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the global unique identifier (GUID) for the competitor.
    ///</summary>
    public void SetGUID (string guid) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(guid,_data.guid);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the unique identifier of the main transponder.
    ///</summary>
    public void SetMainTransponder (uint mainTransponder, bool transponderOwner) // setter function
    {
        _data.maintransponder = mainTransponder;
		_data.flags = MylapsSDK.Utilities.SDKHelperFunctions.SetOrClearBit((uint) _data.flags, (int) COMPETITORFLAGS.cfMainTransponderOwner, transponderOwner);

        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the unique identifier of the backup transponder.
    ///</summary>
    public void SetBackupTransponder (uint backupTransponder, bool transponderOwner) // setter function
    {
        _data.backuptransponder = backupTransponder;
_data.flags = MylapsSDK.Utilities.SDKHelperFunctions.SetOrClearBit((uint) _data.flags, (int) COMPETITORFLAGS.cfBackupTransponderOwner, transponderOwner);

        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the transponder type (for main -and- backup, see #TRANSPONDERTYPE).
    ///</summary>
    public void SetTransponderType (uint transponderType) // setter function
    {
        _data.flags &= ~((uint)0x0000FF00);
_data.flags |= ((transponderType & 0xFF) << 8);

        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }

    public string GetUserData(string key, uint length)
    {
        var result = new System.Text.StringBuilder((int)length);
        if (MylapsSDKLibrary.NativeMethods.mta_competitor_get_userdata(_handleWrapper.NativeHandle, _data.id, key, result, length))
            return result.ToString();
        else
            return null;
    }

    public bool AddUserData(string key, string value)
    {
        return MylapsSDKLibrary.NativeMethods.mta_competitor_add_userdata(_handleWrapper.NativeHandle, _nativePointer, key, value);
    }

    public bool RemoveUserData(string key)
    {
        return MylapsSDKLibrary.NativeMethods.mta_competitor_remove_userdata(_handleWrapper.NativeHandle, _nativePointer, key);
    }

    

    
}

}