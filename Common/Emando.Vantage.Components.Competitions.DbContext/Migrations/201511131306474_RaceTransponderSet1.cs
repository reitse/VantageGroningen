namespace Emando.Vantage.Components.Competitions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RaceTransponderSet1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Competitions.RaceTransponders", "Set", c => c.Int());
            DropColumn("Competitions.Races", "TransponderSet");
        }
        
        public override void Down()
        {
            AddColumn("Competitions.Races", "TransponderSet", c => c.Int());
            DropColumn("Competitions.RaceTransponders", "Set");
        }
    }
}
