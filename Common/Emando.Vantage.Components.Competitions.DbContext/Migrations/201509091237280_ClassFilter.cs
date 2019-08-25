using System.Data.Entity.Migrations;

namespace Emando.Vantage.Components.Competitions.Migrations
{
    public partial class ClassFilter : DbMigration
    {
        public override void Up()
        {
            AddColumn("Competitions.DistanceCombinations", "ClassFilter", c => c.String(maxLength: 100));
            DropColumn("Competitions.DistanceCombinations", "Class");
        }

        public override void Down()
        {
            AddColumn("Competitions.DistanceCombinations", "Class", c => c.Int(false));
            DropColumn("Competitions.DistanceCombinations", "ClassFilter");
        }
    }
}