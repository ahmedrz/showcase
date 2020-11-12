namespace EManifestServices.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lines : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lines",
                c => new
                    {
                        LineId = c.Int(nullable: false),
                        LineCode = c.String(nullable: false, maxLength: 6),
                        LineName = c.String(nullable: false, maxLength: 30),
                        Departments = c.String(),
                        Serial = c.Int(nullable: false),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.LineId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Lines");
        }
    }
}
