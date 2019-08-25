using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Competitions.Migrations
{
    public partial class ReportTemplates : DbMigration
    {
        public override void Up()
        {
            AddColumn("Competitions.Competitions", "ReportTemplateName", c => c.String(maxLength: 100));
            CreateIndex("Competitions.Competitions", new[] { "LicenseIssuerId", "ReportTemplateName" });
            AddForeignKey("Competitions.Competitions", new[] { "LicenseIssuerId", "ReportTemplateName" }, "dbo.ReportTemplates", new[] { "LicenseIssuerId", "Name" });
        }

        public override void Down()
        {
            DropForeignKey("Competitions.Competitions", new[] { "LicenseIssuerId", "ReportTemplateName" }, "dbo.ReportTemplates");
            DropIndex("Competitions.Competitions", new[] { "LicenseIssuerId", "ReportTemplateName" });
            DropColumn("Competitions.Competitions", "ReportTemplateName");
        }
    }
}