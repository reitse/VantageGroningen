using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Competitions.Migrations
{
    public partial class CompetitorLicenseFlags : DbMigration
    {
        public override void Up()
        {
            AddColumn("Competitions.Competitors", "LicenseFlags", c => c.Int(false));

            Sql("UPDATE co " +
                "SET LicenseFlags = pl.Flags " +
                "FROM Competitions.Competitors co " +
                "INNER JOIN Competitions.CompetitorLists cl ON co.ListId = cl.Id " +
                "INNER JOIN Competitions.Competitions c ON cl.CompetitionId = c.Id " +
                "INNER JOIN dbo.PersonLicenses pl ON c.LicenseIssuerId = pl.IssuerId AND co.LicenseDiscipline = pl.Discipline AND co.LicenseKey = pl.[Key]");
        }

        public override void Down()
        {
            DropColumn("Competitions.Competitors", "LicenseFlags");
        }
    }
}