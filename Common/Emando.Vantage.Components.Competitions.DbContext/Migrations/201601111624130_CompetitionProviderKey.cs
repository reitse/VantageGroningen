namespace Emando.Vantage.Components.Competitions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompetitionProviderKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("Competitions.Competitions", "ProviderKey", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("Competitions.Competitions", "ProviderKey");
        }
    }
}
