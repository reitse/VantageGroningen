namespace Emando.Vantage.Components.Competitions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DefaultLastRaceCommitted : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Competitions.Distances SET LastRaceCommitted = Starts");
        }

        public override void Down()
        {
        }
    }
}
