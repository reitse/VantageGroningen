using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Migrations
{
    public partial class TransponderBags : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TransponderSetTransponders", new[] { "BagName", "BagNumber" }, "dbo.TransponderBags");
            DropIndex("dbo.TransponderSetTransponders", new[] { "BagName", "BagNumber" });
            DropPrimaryKey("dbo.TransponderBags");
            DropPrimaryKey("dbo.TransponderSetTransponders");
            CreateTable(
                "dbo.TransponderBagSets",
                c => new
                {
                    LicenseIssuerId = c.String(false, 50),
                    Discipline = c.String(false, 100),
                    BagName = c.String(false, 20),
                    SetNumber = c.Int(false),
                    Added = c.DateTime(false, 7, storeType: "datetime2")
                })
                .PrimaryKey(t => new
                {
                    t.LicenseIssuerId,
                    t.Discipline,
                    t.BagName,
                    t.SetNumber
                })
                .ForeignKey("dbo.TransponderSets", t => new
                {
                    t.LicenseIssuerId,
                    t.Discipline,
                    t.SetNumber
                }, true)
                .ForeignKey("dbo.TransponderBags", t => new
                {
                    t.LicenseIssuerId,
                    t.Discipline,
                    t.BagName
                }, true)
                .Index(t => new
                {
                    t.LicenseIssuerId,
                    t.Discipline,
                    t.BagName
                })
                .Index(t => new
                {
                    t.LicenseIssuerId,
                    t.Discipline,
                    t.SetNumber
                });

            CreateTable(
                "dbo.TransponderSets",
                c => new
                {
                    LicenseIssuerId = c.String(false, 50),
                    Discipline = c.String(false, 100),
                    Number = c.Int(false)
                })
                .PrimaryKey(t => new
                {
                    t.LicenseIssuerId,
                    t.Discipline,
                    t.Number
                });

            AddColumn("dbo.TransponderBags", "LicenseIssuerId", c => c.String(false, 50));
            AddColumn("dbo.TransponderBags", "Discipline", c => c.String(false, 100));
            AddColumn("dbo.TransponderSetTransponders", "SetLicenseIssuerId", c => c.String(false, 50));
            AddColumn("dbo.TransponderSetTransponders", "SetDiscipline", c => c.String(false, 100));
            AddColumn("dbo.TransponderSetTransponders", "SetNumber", c => c.Int(false));
            AlterColumn("dbo.TransponderBags", "Name", c => c.String(false, 20));
            AddPrimaryKey("dbo.TransponderBags", new[] { "LicenseIssuerId", "Discipline", "Name" });
            AddPrimaryKey("dbo.TransponderSetTransponders", new[] { "SetLicenseIssuerId", "SetDiscipline", "SetNumber", "Location" });
            CreateIndex("dbo.TransponderSetTransponders", new[] { "SetLicenseIssuerId", "SetDiscipline", "SetNumber" });
            AddForeignKey("dbo.TransponderSetTransponders", new[] { "SetLicenseIssuerId", "SetDiscipline", "SetNumber" }, "dbo.TransponderSets",
                new[] { "LicenseIssuerId", "Discipline", "Number" }, true);
            DropColumn("dbo.TransponderBags", "Number");
            DropColumn("dbo.TransponderSetTransponders", "BagName");
            DropColumn("dbo.TransponderSetTransponders", "BagNumber");
            DropColumn("dbo.TransponderSetTransponders", "Number");
        }

        public override void Down()
        {
            AddColumn("dbo.TransponderSetTransponders", "Number", c => c.Int(false));
            AddColumn("dbo.TransponderSetTransponders", "BagNumber", c => c.Int(false));
            AddColumn("dbo.TransponderSetTransponders", "BagName", c => c.String(false, 128));
            AddColumn("dbo.TransponderBags", "Number", c => c.Int(false));
            DropForeignKey("dbo.TransponderBagSets", new[] { "LicenseIssuerId", "Discipline", "BagName" }, "dbo.TransponderBags");
            DropForeignKey("dbo.TransponderBagSets", new[] { "LicenseIssuerId", "Discipline", "SetNumber" }, "dbo.TransponderSets");
            DropForeignKey("dbo.TransponderSetTransponders", new[] { "SetLicenseIssuerId", "SetDiscipline", "SetNumber" }, "dbo.TransponderSets");
            DropIndex("dbo.TransponderSetTransponders", new[] { "SetLicenseIssuerId", "SetDiscipline", "SetNumber" });
            DropIndex("dbo.TransponderBagSets", new[] { "LicenseIssuerId", "Discipline", "SetNumber" });
            DropIndex("dbo.TransponderBagSets", new[] { "LicenseIssuerId", "Discipline", "BagName" });
            DropPrimaryKey("dbo.TransponderSetTransponders");
            DropPrimaryKey("dbo.TransponderBags");
            AlterColumn("dbo.TransponderBags", "Name", c => c.String(false, 128));
            DropColumn("dbo.TransponderSetTransponders", "SetNumber");
            DropColumn("dbo.TransponderSetTransponders", "SetDiscipline");
            DropColumn("dbo.TransponderSetTransponders", "SetLicenseIssuerId");
            DropColumn("dbo.TransponderBags", "Discipline");
            DropColumn("dbo.TransponderBags", "LicenseIssuerId");
            DropTable("dbo.TransponderSets");
            DropTable("dbo.TransponderBagSets");
            AddPrimaryKey("dbo.TransponderSetTransponders", new[] { "BagName", "BagNumber", "Number", "Location" });
            AddPrimaryKey("dbo.TransponderBags", new[] { "Name", "Number" });
            CreateIndex("dbo.TransponderSetTransponders", new[] { "BagName", "BagNumber" });
            AddForeignKey("dbo.TransponderSetTransponders", new[] { "BagName", "BagNumber" }, "dbo.TransponderBags", new[] { "Name", "Number" }, true);
        }
    }
}