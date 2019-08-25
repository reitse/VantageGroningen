using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Migrations
{
    public partial class PersonLicenseVenueSubscriptions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PersonLicenseVenueSubscriptions",
                c => new
                {
                    LicenseIssuerId = c.String(false, 50),
                    LicenseDiscipline = c.String(false, 100),
                    LicenseKey = c.String(false, 100),
                    VenueCode = c.String(false, 50),
                    VenueDiscipline = c.String(false, 100),
                    Issued = c.DateTime(false, 7, storeType: "datetime2"),
                    ValidFrom = c.DateTime(false, storeType: "date"),
                    ValidTo = c.DateTime(false, storeType: "date")
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
        }

        public override void Down()
        {
            DropForeignKey("dbo.PersonLicenseVenueSubscriptions", new[] { "VenueCode", "VenueDiscipline" }, "dbo.Venues");
            DropForeignKey("dbo.PersonLicenseVenueSubscriptions", new[] { "LicenseIssuerId", "LicenseDiscipline", "LicenseKey" }, "dbo.PersonLicenses");
            DropIndex("dbo.PersonLicenseVenueSubscriptions", new[] { "VenueCode", "VenueDiscipline" });
            DropIndex("dbo.PersonLicenseVenueSubscriptions", new[] { "LicenseIssuerId", "LicenseDiscipline", "LicenseKey" });
            DropTable("dbo.PersonLicenseVenueSubscriptions");
        }
    }
}