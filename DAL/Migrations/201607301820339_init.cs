namespace DAL2try.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Description = c.String(),
                        Correct = c.Boolean(nullable: false),
                        Question_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.Question_Id)
                .Index(t => t.Question_Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        OneAnswer = c.Boolean(nullable: false),
                        Points = c.Int(nullable: false),
                        ThematicArea_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ThematicAreas", t => t.ThematicArea_Id, cascadeDelete: true)
                .Index(t => t.ThematicArea_Id);
            
            CreateTable(
                "dbo.ThematicAreas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TestTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NumOfQuestions = c.Int(nullable: false),
                        Date = c.String(),
                        CompletionTime = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        RegId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Code = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TestTemplateId = c.Int(nullable: false),
                        AppUserId = c.Int(nullable: false),
                        ResultString = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AppUserStudentGroups",
                c => new
                    {
                        AppUser_Id = c.Int(nullable: false),
                        StudentGroup_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AppUser_Id, t.StudentGroup_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.StudentGroups", t => t.StudentGroup_Id, cascadeDelete: true)
                .Index(t => t.AppUser_Id)
                .Index(t => t.StudentGroup_Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Questions", "ThematicArea_Id", "dbo.ThematicAreas");
            DropForeignKey("dbo.TestTemplateThematicAreas", "ThematicArea_Id", "dbo.ThematicAreas");
            DropForeignKey("dbo.TestTemplateThematicAreas", "TestTemplate_Id", "dbo.TestTemplates");
            DropForeignKey("dbo.StudentGroupTestTemplates", "TestTemplate_Id", "dbo.TestTemplates");
            DropForeignKey("dbo.StudentGroupTestTemplates", "StudentGroup_Id", "dbo.StudentGroups");
            DropForeignKey("dbo.AppUserStudentGroups", "StudentGroup_Id", "dbo.StudentGroups");
            DropForeignKey("dbo.AppUserStudentGroups", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Answers", "Question_Id", "dbo.Questions");
            DropIndex("dbo.TestTemplateThematicAreas", new[] { "ThematicArea_Id" });
            DropIndex("dbo.TestTemplateThematicAreas", new[] { "TestTemplate_Id" });
            DropIndex("dbo.StudentGroupTestTemplates", new[] { "TestTemplate_Id" });
            DropIndex("dbo.StudentGroupTestTemplates", new[] { "StudentGroup_Id" });
            DropIndex("dbo.AppUserStudentGroups", new[] { "StudentGroup_Id" });
            DropIndex("dbo.AppUserStudentGroups", new[] { "AppUser_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Questions", new[] { "ThematicArea_Id" });
            DropIndex("dbo.Answers", new[] { "Question_Id" });
            DropTable("dbo.TestTemplateThematicAreas");
            DropTable("dbo.StudentGroupTestTemplates");
            DropTable("dbo.AppUserStudentGroups");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Results");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.StudentGroups");
            DropTable("dbo.TestTemplates");
            DropTable("dbo.ThematicAreas");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
