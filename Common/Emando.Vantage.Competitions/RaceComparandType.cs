﻿using System.Runtime.Serialization;

namespace Emando.Vantage.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Competitions")]
    public enum RaceComparandType
    {
        [EnumMember]
        Result,
        [EnumMember]
        Classification,
        [EnumMember]
        Historical
    }
}