using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Migrations
{
    public partial class ReportTemplates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReportTemplates",
                c => new
                {
                    LicenseIssuerId = c.String(false, 50),
                    Name = c.String(false, 100),
                    Discipline = c.String(false, 100)
                })
                .PrimaryKey(t => new
                {
                    t.LicenseIssuerId,
                    t.Name
                });

            CreateTable(
                "dbo.ReportLogos",
                c => new
                {
                    LicenseIssuerId = c.String(false, 50),
                    TemplateName = c.String(false, 100),
                    Name = c.String(false, 100),
                    Image = c.Binary(false)
                })
                .PrimaryKey(t => new
                {
                    t.LicenseIssuerId,
                    t.TemplateName,
                    t.Name
                })
                .ForeignKey("dbo.ReportTemplates", t => new
                {
                    t.LicenseIssuerId,
                    t.TemplateName
                }, true)
                .Index(t => new
                {
                    t.LicenseIssuerId,
                    t.TemplateName
                });
        }

        public override void Down()
        {
            DropForeignKey("dbo.ReportLogos", new[] { "LicenseIssuerId", "TemplateName" }, "dbo.ReportTemplates");
            DropIndex("dbo.ReportLogos", new[] { "LicenseIssuerId", "TemplateName" });
            DropTable("dbo.ReportLogos");
            DropTable("dbo.ReportTemplates");
        }
    }
}