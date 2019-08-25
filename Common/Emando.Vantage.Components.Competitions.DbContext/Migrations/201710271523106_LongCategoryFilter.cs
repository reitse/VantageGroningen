namespace Emando.Vantage.Components.Competitions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LongCategoryFilter : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Competitions.DistanceCombinations", "CategoryFilter", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("Competitions.DistanceCombinations", "CategoryFilter", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
