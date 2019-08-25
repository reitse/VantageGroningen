using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Competitions.Migrations
{
    public partial class DistanceStarterReferee : DbMigration
    {
        public override void Up()
        {
            AddColumn("Competitions.Distances", "LastRaceCommitted", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("Competitions.Distances", "Starter", c => c.String(maxLength: 200));
            AddColumn("Competitions.Distances", "Referee1", c => c.String(maxLength: 200));
            AddColumn("Competitions.Distances", "Referee2", c => c.String(maxLength: 200));
        }

        public override void Down()
        {
            DropColumn("Competitions.Distances", "Referee2");
            DropColumn("Competitions.Distances", "Referee1");
            DropColumn("Competitions.Distances", "Starter");
            DropColumn("Competitions.Distances", "LastRaceCommitted");
        }
    }
}