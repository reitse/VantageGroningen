using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Competitions.Migrations
{
    public partial class PersonTimeSource : DbMigration
    {
        public override void Up()
        {
            AddColumn("Competitions.PersonTimes", "Source", c => c.String(false, 50, defaultValue: "Unknown"));
            Sql("UPDATE Competitions.PersonTimes " +
                "SET Source = 'Vantage' " +
                "WHERE CompetitionId IS NOT NULL");
            Sql("UPDATE Competitions.PersonTimes " +
                "SET Source = 'Covas' " +
                "WHERE CompetitionId IS NULL");
            CreateIndex("Competitions.PersonTimes", "Source", name: "IX_PersonTime_Source");
        }

        public override void Down()
        {
            DropIndex("Competitions.PersonTimes", "IX_PersonTime_Source");
            DropColumn("Competitions.PersonTimes", "Source");
        }
    }
}