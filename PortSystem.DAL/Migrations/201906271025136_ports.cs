namespace PortSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ports : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Guid(nullable: false),
                        DepartmentName = c.String(nullable: false, maxLength: 30),
                        PortId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentId)
                .ForeignKey("dbo.Ports", t => t.PortId, cascadeDelete: true)
                .Index(t => t.PortId);
            
            CreateTable(
                "dbo.Ports",
                c => new
                    {
                        PortId = c.Guid(nullable: false),
                        PortName = c.String(nullable: false, maxLength: 30),
                        PortCode = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => t.PortId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "PortId", "dbo.Ports");
            DropIndex("dbo.Departments", new[] { "PortId" });
            DropTable("dbo.Ports");
            DropTable("dbo.Departments");
        }
    }
}
