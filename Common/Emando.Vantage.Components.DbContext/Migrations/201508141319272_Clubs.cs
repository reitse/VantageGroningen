using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Migrations
{
    public partial class Clubs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clubs",
                c => new
                {
                    CountryCode = c.String(false, 3),
                    Code = c.Int(false),
                    ShortName = c.String(maxLength: 20),
                    FullName = c.String(false, 100)
                })
                .PrimaryKey(t => new
                {
                    t.CountryCode,
                    t.Code
                })
                .ForeignKey("dbo.Countries", t => t.CountryCode)
                .Index(t => t.CountryCode);

            AddColumn("dbo.PersonLicenses", "ClubCountryCode", c => c.String(maxLength: 3));
            AddColumn("dbo.PersonLicenses", "ClubCode", c => c.Int());
            CreateIndex("dbo.PersonLicenses", new[] { "ClubCountryCode", "ClubCode" });
            AddForeignKey("dbo.PersonLicenses", new[] { "ClubCountryCode", "ClubCode" }, "dbo.Clubs", new[] { "CountryCode", "Code" });
            DropColumn("dbo.PersonLicenses", "Club");
        }

        public override void Down()
        {
            AddColumn("dbo.PersonLicenses", "Club", c => c.String(maxLength: 100));
            DropForeignKey("dbo.PersonLicenses", new[] { "ClubCountryCode", "ClubCode" }, "dbo.Clubs");
            DropForeignKey("dbo.Clubs", "CountryCode", "dbo.Countries");
            DropIndex("dbo.PersonLicenses", new[] { "ClubCountryCode", "ClubCode" });
            DropIndex("dbo.Clubs", new[] { "CountryCode" });
            DropColumn("dbo.PersonLicenses", "ClubCode");
            DropColumn("dbo.PersonLicenses", "ClubCountryCode");
            DropTable("dbo.Clubs");
        }
    }
}