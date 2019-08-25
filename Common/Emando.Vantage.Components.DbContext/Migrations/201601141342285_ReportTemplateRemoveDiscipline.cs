namespace Emando.Vantage.Components.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportTemplateRemoveDiscipline : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ReportTemplates", "Discipline");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReportTemplates", "Discipline", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
