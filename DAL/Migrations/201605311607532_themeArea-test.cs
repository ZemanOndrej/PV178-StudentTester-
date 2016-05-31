namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class themeAreatest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ThematicAreas", "TestTemplate_Id", "dbo.TestTemplates");
            DropIndex("dbo.ThematicAreas", new[] { "TestTemplate_Id" });
            CreateTable(
                "dbo.TestTemplateThematicAreas",
                c => new
                    {
                        TestTemplate_Id = c.Int(nullable: false),
                        ThematicArea_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TestTemplate_Id, t.ThematicArea_Id })
                .ForeignKey("dbo.TestTemplates", t => t.TestTemplate_Id, cascadeDelete: true)
                .ForeignKey("dbo.ThematicAreas", t => t.ThematicArea_Id, cascadeDelete: true)
                .Index(t => t.TestTemplate_Id)
                .Index(t => t.ThematicArea_Id);
            
            DropColumn("dbo.ThematicAreas", "TestTemplate_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ThematicAreas", "TestTemplate_Id", c => c.Int());
            DropForeignKey("dbo.TestTemplateThematicAreas", "ThematicArea_Id", "dbo.ThematicAreas");
            DropForeignKey("dbo.TestTemplateThematicAreas", "TestTemplate_Id", "dbo.TestTemplates");
            DropIndex("dbo.TestTemplateThematicAreas", new[] { "ThematicArea_Id" });
            DropIndex("dbo.TestTemplateThematicAreas", new[] { "TestTemplate_Id" });
            DropTable("dbo.TestTemplateThematicAreas");
            CreateIndex("dbo.ThematicAreas", "TestTemplate_Id");
            AddForeignKey("dbo.ThematicAreas", "TestTemplate_Id", "dbo.TestTemplates", "Id");
        }
    }
}
