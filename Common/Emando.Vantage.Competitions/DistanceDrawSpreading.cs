using System.Runtime.Serialization;

namespace Emando.Vantage.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2015/06/Competitions")]
    public enum DistanceDrawSpreading
    {
        [EnumMember]
        None,
        [EnumMember]
        Clubs,
        [EnumMember]
        Nationalities
    }
}