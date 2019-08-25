using System;
using System.Runtime.Serialization;

namespace Emando.Vantage
{
    [Flags]
    [DataContract(Namespace = "http://emandovantage.com/2014/02")]
    public enum PersonLicenseFlags
    {
        [EnumMember]
        None = 0x0,
        [EnumMember]
        TemporaryLicense = 0x1,
        [EnumMember]
        CanRegister = 0x2,
        [EnumMember]
        FederationLicense = 0x4,
        [EnumMember]
        CompetitionLicense = 0x8,
        [EnumMember]
        DisposableLicense = 0x10,

        LicenseIssuerFlags = TemporaryLicense | FederationLicense | CompetitionLicense | CanRegister,
        PriceFlags = TemporaryLicense,
        PermanentFlags = TemporaryLicense | FederationLicense | CompetitionLicense | CanRegister
    }
}