using System;

namespace Emando.Vantage
{
    [Flags]
    public enum PersonLicenseExpertise
    {
        Sponsor = 0x1,
        Category = 0x2,
        //Reserved = 0x4,
        Number = 0x8,
        Transponders = 0x10,
        Person = 0x20,
        Validity = 0x40,
        Club = 0x80,
        Venue = 0x100,
        Iban = 0x200
    }
}