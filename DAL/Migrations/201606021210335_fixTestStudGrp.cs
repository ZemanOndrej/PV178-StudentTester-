namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixTestStudGrp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TestTemplates", "StudentGroup_Id", "dbo.StudentGroups");
            DropIndex("dbo.TestTemplates", new[] { "StudentGroup_Id" });
            CreateTable(
                "dbo.StudentGroupTestTemplates",
                c => new
                    {
                        StudentGroup_Id = c.Int(nullable: false),
                        TestTemplate_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentGroup_Id, t.TestTemplate_Id })
                .ForeignKey("dbo.StudentGroups", t => t.StudentGroup_Id, cascadeDelete: true)
                .ForeignKey("dbo.TestTemplates", t => t.TestTemplate_Id, cascadeDelete: true)
                .Index(t => t.StudentGroup_Id)
                .Index(t => t.TestTemplate_Id);
            
            DropColumn("dbo.TestTemplates", "StudentGroup_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TestTemplates", "StudentGroup_Id", c => c.Int());
            DropForeignKey("dbo.StudentGroupTestTemplates", "TestTemplate_Id", "dbo.TestTemplates");
            DropForeignKey("dbo.StudentGroupTestTemplates", "StudentGroup_Id", "dbo.StudentGroups");
            DropIndex("dbo.StudentGroupTestTemplates", new[] { "TestTemplate_Id" });
            DropIndex("dbo.StudentGroupTestTemplates", new[] { "StudentGroup_Id" });
            DropTable("dbo.StudentGroupTestTemplates");
            CreateIndex("dbo.TestTemplates", "StudentGroup_Id");
            AddForeignKey("dbo.TestTemplates", "StudentGroup_Id", "dbo.StudentGroups", "Id");
        }
    }
}
