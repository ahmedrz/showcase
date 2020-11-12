namespace EManifestServices.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hazardcodes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UNHazardousCodes",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 4),
                        Description = c.String(nullable: false, maxLength: 150),
                        Class = c.String(nullable: false, maxLength: 4),
                        Serial = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Code);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UNHazardousCodes");
        }
    }
}