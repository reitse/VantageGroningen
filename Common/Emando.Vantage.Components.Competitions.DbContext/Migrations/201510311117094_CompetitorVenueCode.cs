namespace Emando.Vantage.Components.Competitions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompetitorVenueCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("Competitions.Competitors", "VenueCode", c => c.String(maxLength: 50));
            Sql("UPDATE c " +
                "SET c.VenueCode = pl.VenueCode " +
                "FROM Competitions.Competitors c " +
                "INNER JOIN Competitions.CompetitorLists cl ON c.ListId = cl.Id " +
                "INNER JOIN Competitions.Competitions co ON cl.CompetitionId = co.Id " +
                "INNER JOIN PersonLicenses pl ON co.LicenseIssuerId = pl.IssuerId AND c.LicenseDiscipline = pl.Discipline AND c.LicenseKey = pl.[Key]");
        }
        
        public override void Down()
        {
            DropColumn("Competitions.Competitors", "VenueCode");
        }
    }
}
