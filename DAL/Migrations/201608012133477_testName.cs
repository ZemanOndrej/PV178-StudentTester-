namespace DAL2try.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Results", "TestTemplateName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Results", "TestTemplateName");
        }
    }
}
