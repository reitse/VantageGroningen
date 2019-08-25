namespace Emando.Vantage.Components.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonLicenseLegNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PersonLicenses", "LegNumber", c => c.String(maxLength: 20));
            DropColumn("dbo.PersonLicenses", "NumberPrefix");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PersonLicenses", "NumberPrefix", c => c.String(maxLength: 20));
            DropColumn("dbo.PersonLicenses", "LegNumber");
        }
    }
}
