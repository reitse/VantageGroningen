namespace Emando.Vantage.Components.Competitions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DistanceVenue : DbMigration
    {
        public override void Up()
        {
            AddColumn("Competitions.Distances", "VenueCode", c => c.String(maxLength: 50));
            AddColumn("Competitions.Distances", "VenueDiscipline", c => c.String(maxLength: 100));
            CreateIndex("Competitions.Distances", new[] { "VenueCode", "VenueDiscipline" });
            AddForeignKey("Competitions.Distances", new[] { "VenueCode", "VenueDiscipline" }, "dbo.Venues", new[] { "Code", "Discipline" });

            Sql("UPDATE d " +
                "SET VenueCode = c.VenueCode, VenueDiscipline = c.Discipline " +
                "FROM Competitions.Distances d " +
                "INNER JOIN Competitions.Competitions c ON d.CompetitionId = c.Id");
        }
        
        public override void Down()
        {
            DropForeignKey("Competitions.Distances", new[] { "VenueCode", "VenueDiscipline" }, "dbo.Venues");
            DropIndex("Competitions.Distances", new[] { "VenueCode", "VenueDiscipline" });
            DropColumn("Competitions.Distances", "VenueDiscipline");
            DropColumn("Competitions.Distances", "VenueCode");
        }
    }
}
