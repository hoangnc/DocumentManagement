namespace DocumentManagement.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDocumentIdToDocument : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appendice", "DocumentId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appendice", "DocumentId");
        }
    }
}
