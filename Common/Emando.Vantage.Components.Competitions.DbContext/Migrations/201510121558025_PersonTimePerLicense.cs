using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Competitions.Migrations
{
    public partial class PersonTimePerLicense : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM Competitions.PersonTimes");

            DropForeignKey("Competitions.PersonTimes", "PersonId", "dbo.People");
            DropIndex("Competitions.PersonTimes", new[] { "PersonId" });
            DropPrimaryKey("Competitions.PersonTimes");

            AddColumn("Competitions.PersonTimes", "LicenseIssuerId", c => c.String(false, 50));
            AddColumn("Competitions.PersonTimes", "LicenseDiscipline", c => c.String(false, 100));
            AddColumn("Competitions.PersonTimes", "LicenseKey", c => c.String(false, 100));
            DropColumn("Competitions.PersonTimes", "PersonId");

            AddPrimaryKey("Competitions.PersonTimes",
                new[] { "LicenseIssuerId", "LicenseDiscipline", "LicenseKey", "VenueCode", "Discipline", "DistanceDiscipline", "Distance", "Date", "Time" });
            CreateIndex("Competitions.PersonTimes", new[] { "LicenseIssuerId", "LicenseDiscipline", "LicenseKey" });
            AddForeignKey("Competitions.PersonTimes", new[] { "LicenseIssuerId", "LicenseDiscipline", "LicenseKey" }, "dbo.PersonLicenses",
                new[] { "IssuerId", "Discipline", "Key" }, true);

            AddColumn("Competitions.Competitors", "LicenseDiscipline", c => c.String(maxLength: 100));
            Sql("UPDATE c " +
                "SET c.LicenseDiscipline = pl.Discipline " +
                "FROM Competitions.Competitors c " +
                "INNER JOIN Competitions.CompetitorLists cl ON c.ListId = cl.Id " +
                "INNER JOIN Competitions.Competitions cs ON cl.CompetitionId = cs.Id " +
                "INNER JOIN dbo.PersonLicenses pl ON cs.LicenseIssuerId = pl.IssuerId AND cs.Discipline = pl.Discipline AND c.LicenseKey = pl.[Key]");
        }

        public override void Down()
        {
            DropForeignKey("Competitions.PersonTimes", new[] { "LicenseIssuerId", "LicenseDiscipline", "LicenseKey" }, "dbo.PersonLicenses");
            DropIndex("Competitions.PersonTimes", new[] { "LicenseIssuerId", "LicenseDiscipline", "LicenseKey" });
            DropPrimaryKey("Competitions.PersonTimes");

            AddColumn("Competitions.PersonTimes", "PersonId", c => c.Guid(false));
            Sql("UPDATE pt " +
                "SET pt.PersonId = pl.PersonId " +
                "FROM Competitions.PersonTimes pt " +
                "INNER JOIN dbo.PersonLicenses pl ON pt.LicenseIssuerId = pl.IssuerId AND pt.LicenseDiscipline = pl.Discipline AND pt.LicenseKey = pl.[Key]");

            DropColumn("Competitions.PersonTimes", "LicenseKey");
            DropColumn("Competitions.PersonTimes", "LicenseDiscipline");
            DropColumn("Competitions.PersonTimes", "LicenseIssuerId");
            DropColumn("Competitions.Competitors", "LicenseDiscipline");
            AddPrimaryKey("Competitions.PersonTimes", new[] { "PersonId", "VenueCode", "Discipline", "DistanceDiscipline", "Distance", "Date", "Time" });
            CreateIndex("Competitions.PersonTimes", "PersonId");
            AddForeignKey("Competitions.PersonTimes", "PersonId", "dbo.People", "Id", true);
        }
    }
}