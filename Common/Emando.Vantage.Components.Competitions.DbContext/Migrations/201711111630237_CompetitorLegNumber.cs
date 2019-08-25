namespace Emando.Vantage.Components.Competitions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompetitorLegNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("Competitions.Competitors", "LegNumber", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("Competitions.Competitors", "LegNumber");
        }
    }
}
