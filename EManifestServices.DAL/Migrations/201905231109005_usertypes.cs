namespace EManifestServices.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usertypes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserType", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UserType");
        }
    }
}
