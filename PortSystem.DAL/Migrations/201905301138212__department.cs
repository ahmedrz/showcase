namespace PortSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _department : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BOLDetails", "DepartmentId", c => c.Guid(nullable: false));
            AddColumn("dbo.VoyageDetails", "PortId", c => c.Guid(nullable: false));
            AddColumn("dbo.VoyageDetails", "DepartmentId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VoyageDetails", "DepartmentId");
            DropColumn("dbo.VoyageDetails", "PortId");
            DropColumn("dbo.BOLDetails", "DepartmentId");
        }
    }
}
