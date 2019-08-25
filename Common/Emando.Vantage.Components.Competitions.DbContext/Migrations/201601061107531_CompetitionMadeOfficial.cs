namespace Emando.Vantage.Components.Competitions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompetitionMadeOfficial : DbMigration
    {
        public override void Up()
        {
            AddColumn("Competitions.Competitions", "MadeOfficial", c => c.DateTime(precision: 7, storeType: "datetime2"));
            Sql("UPDATE Competitions.Competitions " +
                "SET MadeOfficial = GETUTCDATE() " +
                "WHERE ResultsStatus = 2");
        }
        
        public override void Down()
        {
            DropColumn("Competitions.Competitions", "MadeOfficial");
        }
    }
}
