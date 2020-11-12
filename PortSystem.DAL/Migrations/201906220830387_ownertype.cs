namespace PortSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ownertype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContainerDetails", "ContainerOwnerType", c => c.String(maxLength: 3));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContainerDetails", "ContainerOwnerType");
        }
    }
}
