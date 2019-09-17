namespace DocumentManagement.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserGroups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 64),
                        Groups = c.String(maxLength: 4000),
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
            DropTable("dbo.UserGroup");
        }
    }
}
