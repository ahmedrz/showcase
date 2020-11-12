namespace IQManClient.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isocodeshazardcodes : DbMigration
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
            DropTable("dbo.ContainerIsoCodes");
        }
    }
}
