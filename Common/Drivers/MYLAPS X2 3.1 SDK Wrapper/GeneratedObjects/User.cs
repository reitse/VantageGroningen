namespace MylapsSDK.Objects
{

#pragma warning disable 0649 // ignore CS0649 "is never assigned to, and will always have its default value null"
    // struct
    /// A user of the appliance.
    internal struct UserStruct
    {
        public uint id; //  The unique identifier
        public ushort level; //  The security level of the user (100 = lowest, 0 = highest)
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 255)]
        public byte[] username; //  The username of the user. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 255)]
        public byte[] password; //  The password of the user. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 255)]
        public byte[] firstname; //  The first name of the user. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 255)]
        public byte[] lastname; //  The last name of the user. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 255)]
        public byte[] companyname; //  The company name of the user. The character data is in UTF-8 encoding.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 15)]
        public byte[] role; //  The role of the user.
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 40)]
        public byte[] imagehash; //  The md5 hash for the user picture (only PNG format is supported).
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray,
            SizeConst = 24)]
        public byte[] nickname; //  The nickname of the user
    }
#pragma warning restore 0649

/// <summary>
/// Information about User information
/// </summary>
/// <remarks>
/// A user of the appliance.
/// </remarks>
public partial class User: IObjectWithID{
    private readonly System.IntPtr _nativePointer;
    private readonly UserStruct _data;

    private readonly string _username;
    private readonly string _password;
    private readonly string _firstname;
    private readonly string _lastname;
    private readonly string _companyname;
    private readonly string _role;
    private readonly string _imagehash;
    private readonly string _nickname;

    private MTA _handleWrapper;

    internal User(System.IntPtr nativePointer, MTA context)
    {
        _nativePointer = nativePointer;
        _data = (UserStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(
            nativePointer, typeof(UserStruct));

        _handleWrapper = context;
        _username = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.username);
        _password = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.password);
        _firstname = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.firstname);
        _lastname = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.lastname);
        _companyname = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.companyname);
        _role = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.role);
        _imagehash = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.imagehash);
        _nickname = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.nickname);
    }
	
	internal static User FromNativePointer(
        System.IntPtr pointerToNativeStruct, MTA context)
    {
		if (pointerToNativeStruct == System.IntPtr.Zero) {
			return null;
		}
		else {
			return new User(pointerToNativeStruct, context);
		}
    }

    internal static System.Collections.Generic.List<User> FromNativePointerArray(
        System.IntPtr pointerToNativeArray, uint count, MTA context)
    {
        var ptrArray = new System.IntPtr[count];
        System.Runtime.InteropServices.Marshal.Copy(pointerToNativeArray, ptrArray, 0, (int) count);
        return new System.Collections.Generic.List<User>(
            System.Array.ConvertAll<System.IntPtr,User>(ptrArray,
                ptr => new User(ptr, context)));
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
    ///The unique identifier
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The security level of the user (100 = lowest, 0 = highest)
    ///</summary>
	public ushort Level
    {
        get { return (ushort) _data.level; }
    }
    ///<summary>
    ///The username of the user. The character data is in UTF-8 encoding.
    ///</summary>
    public string Username
    {
        get { return _username; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The password of the user. The character data is in UTF-8 encoding.
    ///</summary>
    public string Password
    {
        get { return _password; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The first name of the user. The character data is in UTF-8 encoding.
    ///</summary>
    public string FirstName
    {
        get { return _firstname; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The last name of the user. The character data is in UTF-8 encoding.
    ///</summary>
    public string LastName
    {
        get { return _lastname; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The company name of the user. The character data is in UTF-8 encoding.
    ///</summary>
    public string CompanyName
    {
        get { return _companyname; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The role of the user.
    ///</summary>
    public string Role
    {
        get { return _role; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The md5 hash for the user picture (only PNG format is supported).
    ///</summary>
    public string ImageHash
    {
        get { return _imagehash; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The nickname of the user
    ///</summary>
    public string Nickname
    {
        get { return _nickname; } // return local datamember which is an utf8 encoded string
    }



    
}

/// <summary>
/// Modifier for User information
/// </summary>
/// <remarks>
/// A user of the appliance.
/// </remarks>
public class UserModifier : MylapsSDK.MylapsSDKLibrary.GenericModifier<User>
{
    private readonly System.IntPtr _nativePointer;
    private UserStruct _data;
    private MTA _handleWrapper;

    private readonly string _username;
    private readonly string _password;
    private readonly string _firstname;
    private readonly string _lastname;
    private readonly string _companyname;
    private readonly string _role;
    private readonly string _imagehash;
    private readonly string _nickname;

    internal UserModifier(System.IntPtr nativePointer, MTA context):
        base(nativePointer)
	{
	    _nativePointer = nativePointer;
	    _handleWrapper = context;
		_data = (UserStruct) System.Runtime.InteropServices.Marshal.PtrToStructure(nativePointer, typeof(UserStruct));
        _username = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.username);
        _password = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.password);
        _firstname = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.firstname);
        _lastname = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.lastname);
        _companyname = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.companyname);
        _role = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.role);
        _imagehash = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.imagehash);
        _nickname = MylapsSDK.Utilities.SDKHelperFunctions.UTF8ByteArrayToString(_data.nickname);
	}

    ///<summary>
    ///The unique identifier
    ///</summary>
	public uint ID
    {
        get { return (uint) _data.id; }
    }
    ///<summary>
    ///The security level of the user (100 = lowest, 0 = highest)
    ///</summary>
	public ushort Level
    {
        get { return (ushort) _data.level; }
    }
    ///<summary>
    ///The username of the user. The character data is in UTF-8 encoding.
    ///</summary>
    public string Username
    {
        get { return _username; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The password of the user. The character data is in UTF-8 encoding.
    ///</summary>
    public string Password
    {
        get { return _password; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The first name of the user. The character data is in UTF-8 encoding.
    ///</summary>
    public string FirstName
    {
        get { return _firstname; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The last name of the user. The character data is in UTF-8 encoding.
    ///</summary>
    public string LastName
    {
        get { return _lastname; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The company name of the user. The character data is in UTF-8 encoding.
    ///</summary>
    public string CompanyName
    {
        get { return _companyname; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The role of the user.
    ///</summary>
    public string Role
    {
        get { return _role; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The md5 hash for the user picture (only PNG format is supported).
    ///</summary>
    public string ImageHash
    {
        get { return _imagehash; } // return local datamember which is an utf8 encoded string
    }
    ///<summary>
    ///The nickname of the user
    ///</summary>
    public string Nickname
    {
        get { return _nickname; } // return local datamember which is an utf8 encoded string
    }



    ///<summary>
    ///Set the password for the user.
    ///</summary>
    public void SetPassword (string password) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(password,_data.password);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the first name for the user.
    ///</summary>
    public void SetFirstName (string firstName) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(firstName,_data.firstname);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the last name for the user.
    ///</summary>
    public void SetLastName (string lastName) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(lastName,_data.lastname);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the company name for the user.
    ///</summary>
    public void SetCompanyName (string companyName) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(companyName,_data.companyname);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the md5 hash for the user picture.
    ///</summary>
    public void SetImageHash (string imageHash) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(imageHash,_data.imagehash);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }
    ///<summary>
    ///Set the nickname for the user.
    ///</summary>
    public void SetNickname (string nickname) // setter function
    {
		MylapsSDK.Utilities.SDKHelperFunctions.StringToUTF8ByteArray(nickname,_data.nickname);
        System.Runtime.InteropServices.Marshal.StructureToPtr(_data, _nativePointer, false);
    }


    

    
}

}