using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Competitions.Migrations
{
    public partial class DistanceDrawGroupMode : DbMigration
    {
        public override void Up()
        {
            AddColumn("Competitions.DistanceDrawSettings", "GroupMode", c => c.Int(false));
            AddColumn("Competitions.DistanceDrawSettings", "CategoryLength", c => c.Int(false));
        }

        public override void Down()
        {
            DropColumn("Competitions.DistanceDrawSettings", "CategoryLength");
            DropColumn("Competitions.DistanceDrawSettings", "GroupMode");
        }
    }
}