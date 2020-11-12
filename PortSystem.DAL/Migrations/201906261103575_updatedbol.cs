namespace PortSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedbol : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.BOLDetails", name: "ParentBOLDetailsId", newName: "UpdatedBOLDetailsId");
            RenameIndex(table: "dbo.BOLDetails", name: "IX_ParentBOLDetailsId", newName: "IX_UpdatedBOLDetailsId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.BOLDetails", name: "IX_UpdatedBOLDetailsId", newName: "IX_ParentBOLDetailsId");
            RenameColumn(table: "dbo.BOLDetails", name: "UpdatedBOLDetailsId", newName: "ParentBOLDetailsId");
        }
    }
}
