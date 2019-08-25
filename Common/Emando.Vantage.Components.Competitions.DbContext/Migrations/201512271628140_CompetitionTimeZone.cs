using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Competitions.Migrations
{
    public partial class CompetitionTimeZone : DbMigration
    {
        public override void Up()
        {
            AddColumn("Competitions.Competitions", "TimeZone", c => c.String(maxLength: 50));
            Sql("UPDATE Competitions.Competitions SET TimeZone = 'W. Europe Standard Time'");
        }

        public override void Down()
        {
            DropColumn("Competitions.Competitions", "TimeZone");
        }
    }
}