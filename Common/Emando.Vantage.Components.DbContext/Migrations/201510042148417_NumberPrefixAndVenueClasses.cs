using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Migrations
{
    public partial class NumberPrefixAndVenueClasses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PersonLicenseVenueClasses",
                c => new
                {
                    LicenseIssuerId = c.String(false, 50),
                    LicenseDiscipline = c.String(false, 100),
                    LicenseKey = c.String(false, 100),
                    VenueCode = c.String(false, 50),
                    VenueDiscipline = c.String(false, 100),
                    Class = c.Int(false),
                    Issued = c.DateTime(false, 7, storeType: "datetime2"),
                    ValidFrom = c.DateTime(false, 7, storeType: "date"),
                    ValidTo = c.DateTime(false, 7, storeType: "date")
                })
                .PrimaryKey(t => new
                {
                    t.LicenseIssuerId,
                    t.LicenseDiscipline,
                    t.LicenseKey,
                    t.VenueCode,
                    t.VenueDiscipline
                })
                .ForeignKey("dbo.PersonLicenses", t => new
                {
                    t.LicenseIssuerId,
                    t.LicenseDiscipline,
                    t.LicenseKey
                }, true)
                .ForeignKey("dbo.Venues", t => new
                {
                    t.VenueCode,
                    t.VenueDiscipline
                }, true)
                .Index(t => new
                {
                    t.LicenseIssuerId,
                    t.LicenseDiscipline,
                    t.LicenseKey
                })
                .Index(t => new
                {
                    t.VenueCode,
                    t.VenueDiscipline
                });

            AddColumn("dbo.PersonLicenses", "NumberPrefix", c => c.String(maxLength: 20));
            DropColumn("dbo.PersonLicenses", "Class");
        }

        public override void Down()
        {
            AddColumn("dbo.PersonLicenses", "Class", c => c.Int());
            DropForeignKey("dbo.PersonLicenseVenueClasses", new[] { "VenueCode", "VenueDiscipline" }, "dbo.Venues");
            DropForeignKey("dbo.PersonLicenseVenueClasses", new[] { "LicenseIssuerId", "LicenseDiscipline", "LicenseKey" }, "dbo.PersonLicenses");
            DropIndex("dbo.PersonLicenseVenueClasses", new[] { "VenueCode", "VenueDiscipline" });
            DropIndex("dbo.PersonLicenseVenueClasses", new[] { "LicenseIssuerId", "LicenseDiscipline", "LicenseKey" });
            DropColumn("dbo.PersonLicenses", "NumberPrefix");
            DropTable("dbo.PersonLicenseVenueClasses");
        }
    }
}