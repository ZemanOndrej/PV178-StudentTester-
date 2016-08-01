namespace DAL2try.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class namechange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Results", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Results", "AppUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Results", "AppUserId", c => c.Int(nullable: false));
            DropColumn("dbo.Results", "UserId");
        }
    }
}
