using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Identity.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Users.ClientApplications",
                c => new
                {
                    Id = c.String(false, 128),
                    Secret = c.String(false),
                    Name = c.String(false, 100),
                    KnowsSecret = c.Boolean(false),
                    IsActive = c.Boolean(false),
                    CompetitionClass = c.Int(false),
                    RefreshTokenLifeTime = c.Int(false),
                    AllowedOrigin = c.String(maxLength: 100)
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "Users.ClientApplicationCompetitionRights",
                c => new
                {
                    ClientApplicationId = c.String(false, 128),
                    LicenseIssuerId = c.String(false, 128),
                    Discipline = c.String(false, 100),
                    CompetitionClass = c.Int(false),
                    Value = c.String(false, 100),
                    RoleId = c.String(false, 128)
                })
                .PrimaryKey(t => new
                {
                    t.ClientApplicationId,
                    t.LicenseIssuerId,
                    t.Discipline,
                    t.CompetitionClass,
                    t.Value,
                    t.RoleId
                })
                .ForeignKey("Users.ClientApplications", t => t.ClientApplicationId, true)
                .ForeignKey("Users.AspNetRoles", t => t.RoleId, true)
                .Index(t => t.ClientApplicationId)
                .Index(t => t.RoleId);

            CreateTable(
                "Users.AspNetRoles",
                c => new
                {
                    Id = c.String(false, 128),
                    Name = c.String(false, 256)
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "Users.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(false, 128),
                    RoleId = c.String(false, 128)
                })
                .PrimaryKey(t => new
                {
                    t.UserId,
                    t.RoleId
                })
                .ForeignKey("Users.AspNetRoles", t => t.RoleId, true)
                .ForeignKey("Users.AspNetUsers", t => t.UserId, true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "Users.RefreshTokens",
                c => new
                {
                    TokenHash = c.String(false, 50),
                    GrantType = c.String(maxLength: 100),
                    Subject = c.String(maxLength: 256),
                    ClientId = c.String(maxLength: 128),
                    Issued = c.DateTime(false),
                    Expires = c.DateTime(false),
                    ProtectedTicket = c.String(false)
                })
                .PrimaryKey(t => t.TokenHash)
                .ForeignKey("Users.ClientApplications", t => t.ClientId, true)
                .Index(t => t.ClientId)
                .Index(t => t.Expires);

            CreateTable(
                "Users.AspNetUsers",
                c => new
                {
                    Id = c.String(false, 128),
                    Name_Initials = c.String(maxLength: 20),
                    Name_FirstName = c.String(maxLength: 100),
                    Name_SurnamePrefix = c.String(maxLength: 20),
                    Name_Surname = c.String(maxLength: 100),
                    LastLogin = c.DateTime(),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(false),
                    TwoFactorEnabled = c.Boolean(false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(false),
                    AccessFailedCount = c.Int(false),
                    UserName = c.String(false, 256)
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "Users.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(false, true),
                    UserId = c.String(false, 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String()
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Users.AspNetUsers", t => t.UserId, true)
                .Index(t => t.UserId);

            CreateTable(
                "Users.AspNetUserCompetitionRights",
                c => new
                {
                    UserId = c.String(false, 128),
                    LicenseIssuerId = c.String(false, 50),
                    Discipline = c.String(false, 100),
                    CompetitionClass = c.Int(false),
                    Value = c.String(false, 100),
                    RoleId = c.String(false, 128)
                })
                .PrimaryKey(t => new
                {
                    t.UserId,
                    t.LicenseIssuerId,
                    t.Discipline,
                    t.CompetitionClass,
                    t.Value,
                    t.RoleId
                })
                .ForeignKey("Users.AspNetRoles", t => t.RoleId, true)
                .ForeignKey("Users.AspNetUsers", t => t.UserId, true)
                .Index(t => t.UserId)
                .Index(t => t.LicenseIssuerId)
                .Index(t => t.RoleId);

            CreateTable(
                "Users.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(false, 128),
                    ProviderKey = c.String(false, 128),
                    UserId = c.String(false, 128)
                })
                .PrimaryKey(t => new
                {
                    t.LoginProvider,
                    t.ProviderKey,
                    t.UserId
                })
                .ForeignKey("Users.AspNetUsers", t => t.UserId, true)
                .Index(t => t.UserId);

            CreateTable(
                "Users.ClientApplicationRoles",
                c => new
                {
                    ClientApplicationId = c.String(false, 128),
                    RoleId = c.String(false, 128)
                })
                .PrimaryKey(t => new
                {
                    t.ClientApplicationId,
                    t.RoleId
                })
                .ForeignKey("Users.ClientApplications", t => t.ClientApplicationId, true)
                .ForeignKey("Users.AspNetRoles", t => t.RoleId, true)
                .Index(t => t.ClientApplicationId)
                .Index(t => t.RoleId);
        }

        public override void Down()
        {
            DropForeignKey("Users.AspNetUserRoles", "UserId", "Users.AspNetUsers");
            DropForeignKey("Users.AspNetUserLogins", "UserId", "Users.AspNetUsers");
            DropForeignKey("Users.AspNetUserCompetitionRights", "UserId", "Users.AspNetUsers");
            DropForeignKey("Users.AspNetUserCompetitionRights", "RoleId", "Users.AspNetRoles");
            DropForeignKey("Users.AspNetUserClaims", "UserId", "Users.AspNetUsers");
            DropForeignKey("Users.RefreshTokens", "ClientId", "Users.ClientApplications");
            DropForeignKey("Users.ClientApplicationRoles", "RoleId", "Users.AspNetRoles");
            DropForeignKey("Users.ClientApplicationRoles", "ClientApplicationId", "Users.ClientApplications");
            DropForeignKey("Users.ClientApplicationCompetitionRights", "RoleId", "Users.AspNetRoles");
            DropForeignKey("Users.AspNetUserRoles", "RoleId", "Users.AspNetRoles");
            DropForeignKey("Users.ClientApplicationCompetitionRights", "ClientApplicationId", "Users.ClientApplications");
            DropIndex("Users.ClientApplicationRoles", new[] { "RoleId" });
            DropIndex("Users.ClientApplicationRoles", new[] { "ClientApplicationId" });
            DropIndex("Users.AspNetUserLogins", new[] { "UserId" });
            DropIndex("Users.AspNetUserCompetitionRights", new[] { "RoleId" });
            DropIndex("Users.AspNetUserCompetitionRights", new[] { "LicenseIssuerId" });
            DropIndex("Users.AspNetUserCompetitionRights", new[] { "UserId" });
            DropIndex("Users.AspNetUserClaims", new[] { "UserId" });
            DropIndex("Users.AspNetUsers", "UserNameIndex");
            DropIndex("Users.RefreshTokens", new[] { "Expires" });
            DropIndex("Users.RefreshTokens", new[] { "ClientId" });
            DropIndex("Users.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("Users.AspNetUserRoles", new[] { "UserId" });
            DropIndex("Users.AspNetRoles", "RoleNameIndex");
            DropIndex("Users.ClientApplicationCompetitionRights", new[] { "RoleId" });
            DropIndex("Users.ClientApplicationCompetitionRights", new[] { "ClientApplicationId" });
            DropTable("Users.ClientApplicationRoles");
            DropTable("Users.AspNetUserLogins");
            DropTable("Users.LicenseIssuers");
            DropTable("Users.AspNetUserCompetitionRights");
            DropTable("Users.AspNetUserClaims");
            DropTable("Users.AspNetUsers");
            DropTable("Users.RefreshTokens");
            DropTable("Users.AspNetUserRoles");
            DropTable("Users.AspNetRoles");
            DropTable("Users.ClientApplicationCompetitionRights");
            DropTable("Users.ClientApplications");
        }
    }
}