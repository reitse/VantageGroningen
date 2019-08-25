using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Emando.Vantage.Competitions
{
    [Flags]
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Competitions")]
    public enum TimeInfo
    {
        [EnumMember]
        None = 0x0,
        [EnumMember]
        Fall = 0x1,
        [EnumMember]
        Restart = 0x2,
        [EnumMember]
        PersonalBest = 0x4,
        [EnumMember]
        TrackRecord = 0x8,
        [EnumMember]
        NationalRecord = 0x10,
        [EnumMember]
        WorldRecord = 0x20,
        [EnumMember]
        OutOfCompetition = 0x40,
        [EnumMember]
        ManualTime = 0x80,
        [EnumMember]
        TrackRecordAge = 0x100
    }

    public static class TimeInfoExtensions
    {
        private static IList<TimeInfo> order = new List<TimeInfo>
        {
            TimeInfo.ManualTime,
            TimeInfo.OutOfCompetition,
            TimeInfo.PersonalBest,
            TimeInfo.TrackRecordAge,
            TimeInfo.TrackRecord,
            TimeInfo.NationalRecord,
            TimeInfo.WorldRecord,
            TimeInfo.Restart,
            TimeInfo.Fall,
        };

        public static IEnumerable<TimeInfo> Expand(this TimeInfo timeInfo)
        {
            var flags = (int)timeInfo;
            var i = 1;
            while (i <= flags)
            {
                if ((flags & i) == i)
                    yield return (TimeInfo)i;
                i <<= 1;
            }
        }

        public static IEnumerable<TimeInfo> PresentationOrder(this IEnumerable<TimeInfo> timeInfos)
        {
            return timeInfos.OrderBy(t => order.IndexOf(t));
        }
    }
}