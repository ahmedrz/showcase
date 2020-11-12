namespace PortSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BOLDetails", "Inserted", c => c.Boolean());
            AddColumn("dbo.BOLDetails", "Updated", c => c.Boolean());
            AddColumn("dbo.BOLDetails", "Deleted", c => c.Boolean());
            AddColumn("dbo.BOLDetails", "ParentBOLDetailsId", c => c.Guid());
            CreateIndex("dbo.BOLDetails", "ParentBOLDetailsId");
            AddForeignKey("dbo.BOLDetails", "ParentBOLDetailsId", "dbo.BOLDetails", "BOLDetailsId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BOLDetails", "ParentBOLDetailsId", "dbo.BOLDetails");
            DropIndex("dbo.BOLDetails", new[] { "ParentBOLDetailsId" });
            DropColumn("dbo.BOLDetails", "ParentBOLDetailsId");
            DropColumn("dbo.BOLDetails", "Deleted");
            DropColumn("dbo.BOLDetails", "Updated");
            DropColumn("dbo.BOLDetails", "Inserted");
        }
    }
}
