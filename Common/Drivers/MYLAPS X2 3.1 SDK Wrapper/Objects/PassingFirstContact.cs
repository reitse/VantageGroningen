﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MylapsSDK.Utilities;

namespace MylapsSDK.Objects
{
    partial class PassingFirstContact
    {
        public DateTime UTCTimeAsDateTime
        {
            get { return SDKHelperFunctions.TimestampToDateTime(_data.utctime, DateTimeKind.Utc); }
        }

        public DateTime TimeOfDayAsDateTime
        {
            get { return SDKHelperFunctions.TimestampToDateTime(_data.timeofday, DateTimeKind.Local); }
        }
    }
}
