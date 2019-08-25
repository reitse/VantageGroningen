using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Competitions.Migrations
{
    public partial class RecordTimeGender : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("Competitions.RecordTimes");
            AddColumn("Competitions.RecordTimes", "Gender", c => c.Int(false));
            AddPrimaryKey("Competitions.RecordTimes",
                new[] { "LicenseIssuerId", "Type", "Gender", "FromAge", "ToAge", "VenueCode", "Discipline", "DistanceDiscipline", "Distance" });
        }

        public override void Down()
        {
            DropPrimaryKey("Competitions.RecordTimes");
            DropColumn("Competitions.RecordTimes", "Gender");
            AddPrimaryKey("Competitions.RecordTimes", new[] { "LicenseIssuerId", "Type", "FromAge", "ToAge", "VenueCode", "Discipline", "DistanceDiscipline", "Distance" });
        }
    }
}