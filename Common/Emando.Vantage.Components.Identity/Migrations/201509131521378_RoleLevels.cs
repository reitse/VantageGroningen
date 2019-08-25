using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Identity.Migrations
{
    public partial class RoleLevels : DbMigration
    {
        public override void Up()
        {
            AddColumn("Users.AspNetRoles", "Level", c => c.Int(false));
        }

        public override void Down()
        {
            DropColumn("Users.AspNetRoles", "Level");
        }
    }
}