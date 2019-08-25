namespace Emando.Vantage.Competitions
{
    public enum RecordType
    {
        TrackAge,
        Track,
        National,
        World
    }

    public static class RecordTypeExtensions
    {
        public static TimeInfo ToTimeInfo(this RecordType type)
        {
            switch (type)
            {
                case RecordType.TrackAge:
                    return TimeInfo.TrackRecordAge;
                case RecordType.Track:
                    return TimeInfo.TrackRecord;
                case RecordType.National:
                    return TimeInfo.NationalRecord;
                case RecordType.World:
                    return TimeInfo.WorldRecord;
                default:
                    return TimeInfo.None;
            }
        }
    }
}