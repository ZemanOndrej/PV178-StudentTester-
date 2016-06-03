namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requiredadded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ThematicAreas", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.StudentGroups", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StudentGroups", "Name", c => c.String());
            AlterColumn("dbo.ThematicAreas", "Name", c => c.String());
        }
    }
}
