namespace Emando.Vantage.Components.Competitions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonTimesIndex : DbMigration
    {
        public override void Up()
        {
            DropIndex("Competitions.PersonTimes", "IX_PersonTimes_License_Distance");
            CreateIndex("Competitions.PersonTimes", new [] { "LicenseKey", "Distance", "Date", "LicenseIssuerId", "DistanceDiscipline", "LicenseDiscipline", "VenueCode" });
        }
        
        public override void Down()
        {
        }
    }
}
