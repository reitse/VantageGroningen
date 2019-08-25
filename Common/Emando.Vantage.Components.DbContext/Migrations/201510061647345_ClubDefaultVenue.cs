namespace Emando.Vantage.Components.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClubDefaultVenue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clubs", "DefaultVenueCode", c => c.String(maxLength: 50));
            AddColumn("dbo.Clubs", "DefaultVenueDiscipline", c => c.String(maxLength: 100));
            CreateIndex("dbo.Clubs", new[] { "DefaultVenueCode", "DefaultVenueDiscipline" });
            AddForeignKey("dbo.Clubs", new[] { "DefaultVenueCode", "DefaultVenueDiscipline" }, "dbo.Venues", new[] { "Code", "Discipline" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clubs", new[] { "DefaultVenueCode", "DefaultVenueDiscipline" }, "dbo.Venues");
            DropIndex("dbo.Clubs", new[] { "DefaultVenueCode", "DefaultVenueDiscipline" });
            DropColumn("dbo.Clubs", "DefaultVenueDiscipline");
            DropColumn("dbo.Clubs", "DefaultVenueCode");
        }
    }
}
