using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Competitions.Migrations
{
    public partial class DistanceDrawDeleteExistingSetting : DbMigration
    {
        public override void Up()
        {
            AddColumn("Competitions.DistanceDrawSettings", "DeleteExisting", c => c.Boolean(false));
        }

        public override void Down()
        {
            DropColumn("Competitions.DistanceDrawSettings", "DeleteExisting");
        }
    }
}