namespace PortSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class departmentid : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BOLDetails", "DepartmentId", c => c.Guid());
            AlterColumn("dbo.VoyageDetails", "PortId", c => c.Guid());
            AlterColumn("dbo.VoyageDetails", "DepartmentId", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VoyageDetails", "DepartmentId", c => c.Guid(nullable: false));
            AlterColumn("dbo.VoyageDetails", "PortId", c => c.Guid(nullable: false));
            AlterColumn("dbo.BOLDetails", "DepartmentId", c => c.Guid(nullable: false));
        }
    }
}
