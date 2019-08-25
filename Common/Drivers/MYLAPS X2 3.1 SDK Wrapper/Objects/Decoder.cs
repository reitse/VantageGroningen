using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MylapsSDK.Utilities;
using MylapsSDK.MylapsSDKLibrary;

namespace MylapsSDK.Objects
{
    public partial class Decoder
    {
        public String MAC
        {
            get { return SDKHelperFunctions.MACToString(_data.id, true); }
        }

        ///<summary>
        ///The ID of the decoder-preset-group (UINT32_MAX if not used).
        ///</summary>
        public uint DecoderPresetGroupID
        {
            get { return (uint)NativeMethods.mta_decoder_get_decoderpresetgroupid(_handleWrapper.NativeHandle, _data.id); }
        }
    }
}
