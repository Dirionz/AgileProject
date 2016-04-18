namespace AgileProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatusId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Teacher_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.Teacher_Id)
                .Index(t => t.Teacher_Id);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Phone = c.String(),
                        Corridor_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Corridors", t => t.Corridor_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Corridor_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Corridors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Status", "Teacher_Id", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Teachers", "Corridor_Id", "dbo.Corridors");
            DropIndex("dbo.Teachers", new[] { "User_Id" });
            DropIndex("dbo.Teachers", new[] { "Corridor_Id" });
            DropIndex("dbo.Status", new[] { "Teacher_Id" });
            DropTable("dbo.Corridors");
            DropTable("dbo.Teachers");
            DropTable("dbo.Status");
        }
    }
}
