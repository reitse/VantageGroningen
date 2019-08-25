using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MylapsSDK.Utilities;

namespace MylapsSDK.Objects
{
    public partial class DriverInfo
    {
        public DateTime BeginUTCTimeAsDateTime
        {
            get { return SDKHelperFunctions.TimestampToDateTime(_data.utcbegintime, DateTimeKind.Utc); }
        }

        public DateTime EndUTCTimeAsDateTime
        {
            get {
                if (_data.utcendtime != MdpTime.InvalidTime)
                {
                    return SDKHelperFunctions.TimestampToDateTime(_data.utcendtime, DateTimeKind.Utc);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public DateTime BeginTimeOfDayAsDateTime
        {
            get { return SDKHelperFunctions.TimestampToDateTime(GetBeginTimeOfDay(), DateTimeKind.Local); }
        }

        public DateTime EndTimeOfDayAsDateTime
        {
            get {
                if (GetEndTimeOfDay() != MdpTime.InvalidTime)
                {
                    return SDKHelperFunctions.TimestampToDateTime(GetEndTimeOfDay(), DateTimeKind.Local);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }
    }
}
