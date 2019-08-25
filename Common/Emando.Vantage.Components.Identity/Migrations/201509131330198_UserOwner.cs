using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Identity.Migrations
{
    public partial class UserOwner : DbMigration
    {
        public override void Up()
        {
            AddColumn("Users.AspNetUsers", "OwnerId", c => c.String(maxLength: 128));
        }

        public override void Down()
        {
            DropColumn("Users.AspNetUsers", "OwnerId");
        }
    }
}