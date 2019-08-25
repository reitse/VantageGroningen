using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Identity.Migrations
{
    public partial class UserSettings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Users.UserSettings",
                c => new
                {
                    UserId = c.String(false, 128),
                    Type = c.String(false, 100),
                    Key = c.String(false, 200),
                    Value = c.String()
                })
                .PrimaryKey(t => new
                {
                    t.UserId,
                    t.Type,
                    t.Key
                })
                .ForeignKey("Users.AspNetUsers", t => t.UserId, true)
                .Index(t => t.UserId);
        }

        public override void Down()
        {
            DropForeignKey("Users.UserSettings", "UserId", "Users.AspNetUsers");
            DropIndex("Users.UserSettings", new[] { "UserId" });
            DropTable("Users.UserSettings");
        }
    }
}