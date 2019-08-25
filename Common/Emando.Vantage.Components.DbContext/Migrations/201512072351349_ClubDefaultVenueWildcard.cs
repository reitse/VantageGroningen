namespace Emando.Vantage.Components.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClubDefaultVenueWildcard : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clubs", new[] { "DefaultVenueCode", "DefaultVenueDiscipline" }, "dbo.Venues");
            DropIndex("dbo.Clubs", new[] { "DefaultVenueCode", "DefaultVenueDiscipline" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Clubs", new[] { "DefaultVenueCode", "DefaultVenueDiscipline" });
            AddForeignKey("dbo.Clubs", new[] { "DefaultVenueCode", "DefaultVenueDiscipline" }, "dbo.Venues", new[] { "Code", "Discipline" });
        }
    }
}
