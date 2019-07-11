namespace DocumentManagement.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModule : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Module",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 128),
                        Name = c.String(maxLength: 200),
                        ModifiedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.String(maxLength: 64),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Module");
        }
    }
}
