using System;
using System.Runtime.Serialization;

namespace Emando.Vantage.Competitions.Registrations
{
    [Flags]
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Competitions/Registrations")]
    public enum AllowedRegistrations
    {
        [EnumMember]
        None = 0x0,
        [EnumMember]
        CompetitionLicensees = 0x1,
        [EnumMember]
        TemporaryLicensees = 0x2,
        [EnumMember]
        Invitation = 0x4,
        [EnumMember]
        FederationLicensees = 0x8
    }
}