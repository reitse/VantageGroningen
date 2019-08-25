using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Migrations
{
    public partial class LicenseIssuerDisposableLicense : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LicenseIssuers", "DisposableKeyPrefix", c => c.String(maxLength: 20));
            AddColumn("dbo.LicenseIssuers", "DisposableKeyFrom", c => c.Int(false));
        }

        public override void Down()
        {
            DropColumn("dbo.LicenseIssuers", "DisposableKeyFrom");
            DropColumn("dbo.LicenseIssuers", "DisposableKeyPrefix");
        }
    }
}