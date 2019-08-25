namespace Emando.Vantage.Components.Competitions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RaceTransponderSet : DbMigration
    {
        public override void Up()
        {
            AddColumn("Competitions.Races", "TransponderSet", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("Competitions.Races", "TransponderSet");
        }
    }
}
