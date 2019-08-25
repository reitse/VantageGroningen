using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Competitions.Migrations
{
    public partial class RecordTimes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Competitions.RecordTimes",
                c => new
                {
                    LicenseIssuerId = c.String(false, 50),
                    Type = c.Int(false),
                    FromAge = c.Int(false),
                    ToAge = c.Int(false),
                    VenueCode = c.String(false, 50),
                    Discipline = c.String(false, 100),
                    DistanceDiscipline = c.String(false, 100),
                    Distance = c.Int(false),
                    Name = c.String(maxLength: 200),
                    Date = c.DateTime(false, storeType: "date"),
                    Time = c.Time(false, 7),
                    NationalityCode = c.String(maxLength: 3)
                })
                .PrimaryKey(t => new
                {
                    t.LicenseIssuerId,
                    t.Type,
                    t.FromAge,
                    t.ToAge,
                    t.VenueCode,
                    t.Discipline,
                    t.DistanceDiscipline,
                    t.Distance
                })
                .ForeignKey("dbo.Venues", t => new
                {
                    t.VenueCode,
                    t.Discipline
                }, true)
                .Index(t => new
                {
                    t.VenueCode,
                    t.Discipline
                });
        }

        public override void Down()
        {
            DropForeignKey("Competitions.RecordTimes", new[] { "VenueCode", "Discipline" }, "dbo.Venues");
            DropIndex("Competitions.RecordTimes", new[] { "VenueCode", "Discipline" });
            DropTable("Competitions.RecordTimes");
        }
    }
}