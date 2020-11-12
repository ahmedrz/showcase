namespace IQManClient.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lineupdate : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Lines");
            AddPrimaryKey("dbo.Lines", "LineCode");
            DropColumn("dbo.Lines", "LineId");
            DropColumn("dbo.Lines", "Departments");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lines", "Departments", c => c.String());
            AddColumn("dbo.Lines", "LineId", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Lines");
            AddPrimaryKey("dbo.Lines", "LineId");
        }
    }
}
