namespace IQManClient.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeadmin : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "IsAdmin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "IsAdmin", c => c.Boolean());
        }
    }
}
