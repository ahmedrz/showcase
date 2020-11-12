namespace EManifestServices.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isocodes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContainerIsoCodes",
                c => new
                    {
                        IsoTypeCode = c.String(nullable: false, maxLength: 4),
                        IsoTypeDescription = c.String(nullable: false, maxLength: 60),
                        Serial = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IsoTypeCode);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ContainerIsoCodes");
        }
    }
}
