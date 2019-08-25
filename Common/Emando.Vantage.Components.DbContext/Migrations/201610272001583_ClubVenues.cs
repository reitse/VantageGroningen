using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Migrations
{
    public partial class ClubVenues : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClubVenues",
                c => new
                {
                    ClubCountryCode = c.String(false, 3),
                    ClubCode = c.Int(false),
                    VenueCode = c.String(maxLength: 50),
                    VenueDiscipline = c.String(false, 100)
                })
                .PrimaryKey(t => new
                {
                    t.ClubCountryCode,
                    t.ClubCode,
                    t.VenueDiscipline
                })
                .ForeignKey("dbo.Clubs", t => new
                {
                    t.ClubCountryCode,
                    t.ClubCode
                }, true)
                .ForeignKey("dbo.Venues", t => new
                {
                    t.VenueCode,
                    t.VenueDiscipline
                })
                .Index(t => t.ClubCountryCode)
                .Index(t => new
                {
                    t.ClubCountryCode,
                    t.ClubCode
                })
                .Index(t => new
                {
                    t.VenueCode,
                    t.VenueDiscipline
                });

            DropColumn("dbo.Clubs", "DefaultVenueCode");
            DropColumn("dbo.Clubs", "DefaultVenueDiscipline");
        }

        public override void Down()
        {
            AddColumn("dbo.Clubs", "DefaultVenueDiscipline", c => c.String(maxLength: 100));
            AddColumn("dbo.Clubs", "DefaultVenueCode", c => c.String(maxLength: 50));
            DropForeignKey("dbo.ClubVenues", new[] { "VenueDiscipline", "VenueCode" }, "dbo.Venues");
            DropForeignKey("dbo.ClubVenues", new[] { "ClubCountryCode", "ClubCode" }, "dbo.Clubs");
            DropIndex("dbo.ClubVenues", new[] { "VenueDiscipline", "VenueCode" });
            DropIndex("dbo.ClubVenues", new[] { "ClubCountryCode", "ClubCode" });
            DropIndex("dbo.ClubVenues", new[] { "ClubCountryCode" });
            DropTable("dbo.ClubVenues");
        }
    }
}