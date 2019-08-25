using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Continents",
                c => new
                {
                    Code = c.String(false, 3)
                })
                .PrimaryKey(t => t.Code);

            CreateTable(
                "dbo.Countries",
                c => new
                {
                    Code = c.String(false, 3),
                    Name = c.String(false, 100)
                })
                .PrimaryKey(t => t.Code);

            CreateTable(
                "dbo.LicenseIssuers",
                c => new
                {
                    Id = c.String(false, 50),
                    Name = c.String(false, 50),
                    ForwardUri = c.String(maxLength: 200),
                    EventUri = c.String(maxLength: 200),
                    TemporaryKeyPrefix = c.String(maxLength: 20),
                    TemporaryKeyFrom = c.Int(false)
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.PersonCategories",
                c => new
                {
                    LicenseIssuerId = c.String(false, 50),
                    Discipline = c.String(false, 100),
                    Code = c.String(false, 20),
                    FromAge = c.Int(false),
                    ToAge = c.Int(false),
                    Gender = c.Int(false),
                    Name = c.String(false, 100),
                    Flags = c.Int(false)
                })
                .PrimaryKey(t => new
                {
                    t.LicenseIssuerId,
                    t.Discipline,
                    t.Code
                })
                .ForeignKey("dbo.LicenseIssuers", t => t.LicenseIssuerId, true)
                .Index(t => t.LicenseIssuerId);

            CreateTable(
                "dbo.PersonLicensePrices",
                c => new
                {
                    LicenseIssuerId = c.String(false, 50),
                    Discipline = c.String(false, 100),
                    FromAge = c.Int(false),
                    ToAge = c.Int(false),
                    Currency = c.String(maxLength: 3),
                    Price = c.Decimal(false, 18, 2),
                    Flags = c.Int(false)
                })
                .PrimaryKey(t => new
                {
                    t.LicenseIssuerId,
                    t.Discipline,
                    t.FromAge
                })
                .ForeignKey("dbo.LicenseIssuers", t => t.LicenseIssuerId, true)
                .Index(t => t.LicenseIssuerId);

            CreateTable(
                "dbo.Venues",
                c => new
                {
                    Code = c.String(false, 50),
                    Discipline = c.String(false, 100),
                    Name = c.String(false, 100),
                    Address_Line1 = c.String(maxLength: 100),
                    Address_Line2 = c.String(maxLength: 100),
                    Address_StateOrProvince = c.String(maxLength: 50),
                    Address_PostalCode = c.String(maxLength: 20),
                    Address_City = c.String(maxLength: 100),
                    Address_CountryCode = c.String(maxLength: 3),
                    ContinentCode = c.String(maxLength: 3)
                })
                .PrimaryKey(t => new
                {
                    t.Code,
                    t.Discipline
                })
                .ForeignKey("dbo.Continents", t => t.ContinentCode)
                .Index(t => t.ContinentCode)
                .Index(t => t.Address_CountryCode);

            CreateTable(
                "dbo.PersonLicenses",
                c => new
                {
                    IssuerId = c.String(false, 50),
                    Discipline = c.String(false, 100),
                    Key = c.String(false, 100),
                    PersonId = c.Guid(false),
                    VenueCode = c.String(maxLength: 50),
                    Sponsor = c.String(maxLength: 100),
                    Club = c.String(maxLength: 100),
                    Flags = c.Int(false),
                    Season = c.Int(false),
                    ValidFrom = c.DateTime(false, storeType: "date"),
                    ValidTo = c.DateTime(false, storeType: "date"),
                    Category = c.String(maxLength: 20),
                    Class = c.Int(),
                    Number = c.Int(),
                    Transponder1 = c.String(maxLength: 50),
                    Transponder2 = c.String(maxLength: 50)
                })
                .PrimaryKey(t => new
                {
                    t.IssuerId,
                    t.Discipline,
                    t.Key
                })
                .ForeignKey("dbo.LicenseIssuers", t => t.IssuerId, true)
                .ForeignKey("dbo.People", t => t.PersonId, true)
                .ForeignKey("dbo.Venues", t => new
                {
                    t.VenueCode,
                    t.Discipline
                })
                .Index(t => t.IssuerId)
                .Index(t => new
                {
                    t.IssuerId,
                    t.Discipline
                })
                .Index(t => t.PersonId)
                .Index(t => new
                {
                    t.VenueCode,
                    t.Discipline
                });

            CreateTable(
                "dbo.People",
                c => new
                {
                    Id = c.Guid(false),
                    Name_Initials = c.String(maxLength: 20),
                    Name_FirstName = c.String(maxLength: 100),
                    Name_SurnamePrefix = c.String(maxLength: 20),
                    Name_Surname = c.String(maxLength: 100),
                    Email = c.String(maxLength: 100),
                    Phone = c.String(maxLength: 20),
                    Address_Line1 = c.String(maxLength: 100),
                    Address_Line2 = c.String(maxLength: 100),
                    Address_StateOrProvince = c.String(maxLength: 50),
                    Address_PostalCode = c.String(maxLength: 20),
                    Address_City = c.String(maxLength: 100),
                    Address_CountryCode = c.String(maxLength: 3),
                    Gender = c.Int(false),
                    NationalityCode = c.String(false, 3),
                    BirthDate = c.DateTime(false, storeType: "date"),
                    Iban = c.String(maxLength: 34)
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.NationalityCode)
                .Index(t => t.NationalityCode);

            CreateTable(
                "dbo.TransponderBags",
                c => new
                {
                    Name = c.String(false, 128),
                    Number = c.Int(false)
                })
                .PrimaryKey(t => new
                {
                    t.Name,
                    t.Number
                });

            CreateTable(
                "dbo.TransponderSetTransponders",
                c => new
                {
                    BagName = c.String(false, 128),
                    BagNumber = c.Int(false),
                    Number = c.Int(false),
                    Location = c.String(false, 128),
                    TransponderType = c.String(maxLength: 100),
                    TransponderCode = c.Long(false)
                })
                .PrimaryKey(t => new
                {
                    t.BagName,
                    t.BagNumber,
                    t.Number,
                    t.Location
                })
                .ForeignKey("dbo.TransponderBags", t => new
                {
                    t.BagName,
                    t.BagNumber
                }, true)
                .ForeignKey("dbo.Transponders", t => new
                {
                    t.TransponderType,
                    t.TransponderCode
                }, true)
                .Index(t => new
                {
                    t.BagName,
                    t.BagNumber
                })
                .Index(t => new
                {
                    t.TransponderType,
                    t.TransponderCode
                });

            CreateTable(
                "dbo.Transponders",
                c => new
                {
                    Type = c.String(false, 100),
                    Code = c.Long(false),
                    Label = c.String(maxLength: 50)
                })
                .PrimaryKey(t => new
                {
                    t.Type,
                    t.Code
                });

            CreateTable(
                "dbo.VenueDistricts",
                c => new
                {
                    Level = c.Int(false),
                    Code = c.String(false, 50),
                    Label = c.String(false, 50)
                })
                .PrimaryKey(t => new
                {
                    t.Level,
                    t.Code
                });

            CreateTable(
                "dbo.VenueVenueDistricts",
                c => new
                {
                    VenueCode = c.String(false, 50),
                    VenueDiscipline = c.String(false, 100),
                    DistrictLevel = c.Int(false),
                    DistrictCode = c.String(false, 50)
                })
                .PrimaryKey(t => new
                {
                    t.VenueCode,
                    t.VenueDiscipline,
                    t.DistrictLevel,
                    t.DistrictCode
                })
                .ForeignKey("dbo.Venues", t => new
                {
                    t.VenueCode,
                    t.VenueDiscipline
                }, true)
                .ForeignKey("dbo.VenueDistricts", t => new
                {
                    t.DistrictLevel,
                    t.DistrictCode
                }, true)
                .Index(t => new
                {
                    t.VenueCode,
                    t.VenueDiscipline
                })
                .Index(t => new
                {
                    t.DistrictLevel,
                    t.DistrictCode
                });

            CreateTable(
                "dbo.VenueTracks",
                c => new
                {
                    VenueCode = c.String(false, 50),
                    VenueDiscipline = c.String(false, 100),
                    Length = c.Decimal(false, 18, 3)
                })
                .PrimaryKey(t => new
                {
                    t.VenueCode,
                    t.VenueDiscipline,
                    t.Length
                })
                .ForeignKey("dbo.Venues", t => new
                {
                    t.VenueCode,
                    t.VenueDiscipline
                }, true)
                .Index(t => new
                {
                    t.VenueCode,
                    t.VenueDiscipline
                });
        }

        public override void Down()
        {
            DropForeignKey("dbo.VenueTracks", new[] { "VenueCode", "VenueDiscipline" }, "dbo.Venues");
            DropForeignKey("dbo.VenueVenueDistricts", new[] { "VenueCode", "VenueDiscipline" }, "dbo.Venues");
            DropForeignKey("dbo.VenueVenueDistricts", new[] { "DistrictLevel", "DistrictCode" }, "dbo.VenueDistricts");
            DropForeignKey("dbo.Venues", "ContinentCode", "dbo.Continents");
            DropForeignKey("dbo.TransponderSetTransponders", new[] { "TransponderType", "TransponderCode" }, "dbo.Transponders");
            DropForeignKey("dbo.TransponderSetTransponders", new[] { "BagName", "BagNumber" }, "dbo.TransponderBags");
            DropForeignKey("dbo.People", "NationalityCode", "dbo.Countries");
            DropForeignKey("dbo.PersonLicenses", new[] { "VenueCode", "Discipline" }, "dbo.Venues");
            DropForeignKey("dbo.PersonLicenses", "PersonId", "dbo.People");
            DropForeignKey("dbo.PersonLicenses", "IssuerId", "dbo.LicenseIssuers");
            DropForeignKey("dbo.PersonLicensePrices", "LicenseIssuerId", "dbo.LicenseIssuers");
            DropForeignKey("dbo.PersonCategories", "LicenseIssuerId", "dbo.LicenseIssuers");
            DropIndex("dbo.VenueTracks", new[] { "VenueCode", "VenueDiscipline" });
            DropIndex("dbo.Venues", new[] { "Address_CountryCode" });
            DropIndex("dbo.Venues", new[] { "ContinentCode" });
            DropIndex("dbo.VenueVenueDistricts", new[] { "VenueCode", "VenueDiscipline" });
            DropIndex("dbo.VenueVenueDistricts", new[] { "DistrictLevel", "DistrictCode" });
            DropIndex("dbo.TransponderSetTransponders", new[] { "TransponderType", "TransponderCode" });
            DropIndex("dbo.TransponderSetTransponders", new[] { "BagName", "BagNumber" });
            DropIndex("dbo.People", new[] { "NationalityCode" });
            DropIndex("dbo.PersonLicenses", new[] { "VenueCode", "Discipline" });
            DropIndex("dbo.PersonLicenses", new[] { "PersonId" });
            DropIndex("dbo.PersonLicenses", new[] { "IssuerId" });
            DropIndex("dbo.PersonLicensePrices", new[] { "LicenseIssuerId" });
            DropIndex("dbo.PersonCategories", new[] { "LicenseIssuerId" });
            DropTable("dbo.VenueTracks");
            DropTable("dbo.VenueVenueDistricts");
            DropTable("dbo.Venues");
            DropTable("dbo.VenueDistricts");
            DropTable("dbo.Transponders");
            DropTable("dbo.TransponderSetTransponders");
            DropTable("dbo.TransponderBags");
            DropTable("dbo.People");
            DropTable("dbo.PersonLicenses");
            DropTable("dbo.PersonLicensePrices");
            DropTable("dbo.PersonCategories");
            DropTable("dbo.LicenseIssuers");
            DropTable("dbo.Countries");
            DropTable("dbo.Continents");
        }
    }
}