using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MylapsSDK.Utilities
{
    public class MdpTime
    {
        public const long InvalidTime = 0x7FFFFFFFFFFFFFFF; // MAX_INT64
        public const long Millisecond = 1000L;
        public const long Second = Millisecond * 1000;
        public const long Minute = Second*60;
        public const long Hour = Minute*60;
        public const long Day = Hour*24;
    }
}
