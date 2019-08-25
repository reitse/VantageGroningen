namespace Emando.Vantage.Components.Competitions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonTimeNationalityCode : DbMigration
    {
        public override void Up()
        {
            DropIndex("Competitions.PersonTimes", new[] { "LicenseIssuerId", "LicenseDiscipline", "LicenseKey" });
            AddColumn("Competitions.PersonTimes", "NationalityCode", c => c.String(maxLength: 3));
            CreateIndex("Competitions.PersonTimes", new[] { "LicenseIssuerId", "LicenseDiscipline", "LicenseKey", "DistanceDiscipline", "Distance" }, name: "IX_PersonTimes_License_Distance");
            CreateIndex("Competitions.PersonTimes", "NationalityCode");
        }
        
        public override void Down()
        {
            DropIndex("Competitions.PersonTimes", new[] { "NationalityCode" });
            DropIndex("Competitions.PersonTimes", "IX_PersonTimes_License_Distance");
            DropColumn("Competitions.PersonTimes", "NationalityCode");
            CreateIndex("Competitions.PersonTimes", new[] { "LicenseIssuerId", "LicenseDiscipline", "LicenseKey" });
        }
    }
}
