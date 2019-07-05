namespace DocumentManagement.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appendice",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 128),
                        CompanyCode = c.String(nullable: false, maxLength: 64),
                        CompanyName = c.String(maxLength: 200),
                        DepartmentCode = c.String(maxLength: 64),
                        DepartmentName = c.String(maxLength: 200),
                        Module = c.String(maxLength: 128),
                        DocumentType = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 512),
                        FileName = c.String(maxLength: 128),
                        AppendiceNumber = c.String(),
                        ReviewNumber = c.String(maxLength: 128),
                        Approver = c.String(),
                        EffectiveDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ReviewDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ScopeOfApplication = c.String(maxLength: 512),
                        ScopeOfDeloyment = c.String(maxLength: 512),
                        ReplaceOf = c.String(maxLength: 128),
                        ReplaceEffectiveDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        RelateToDocuments = c.String(maxLength: 1024),
                        DDCAudited = c.Boolean(nullable: false),
                        LinkFile = c.String(maxLength: 1024),
                        StatusId = c.Int(nullable: false),
                        FormType = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        PromulgateStatusId = c.Int(nullable: false),
                        ModifiedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.String(maxLength: 64),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false, maxLength: 64),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Document",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 128),
                        CompanyCode = c.String(nullable: false, maxLength: 64),
                        CompanyName = c.String(maxLength: 200),
                        DepartmentCode = c.String(maxLength: 64),
                        DepartmentName = c.String(maxLength: 200),
                        Module = c.String(maxLength: 128),
                        DocumentType = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 512),
                        FileName = c.String(maxLength: 128),
                        DocumentNumber = c.String(maxLength: 128),
                        ReviewNumber = c.String(maxLength: 128),
                        Description = c.String(),
                        ContentChange = c.String(maxLength: 4000),
                        Drafter = c.String(maxLength: 128),
                        Auditor = c.String(maxLength: 128),
                        Approver = c.String(maxLength: 128),
                        EffectiveDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ReviewDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ScopeOfApplication = c.String(maxLength: 512),
                        ScopeOfDeloyment = c.String(maxLength: 512),
                        ReplaceOf = c.String(maxLength: 128),
                        ReplaceEffectiveDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        RelateToDocuments = c.String(maxLength: 1024),
                        DDCAudited = c.Boolean(nullable: false),
                        FolderName = c.String(maxLength: 64),
                        LinkFile = c.String(maxLength: 1024),
                        StatusId = c.Int(nullable: false),
                        FormType = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        PromulgateStatusId = c.Int(nullable: false),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DocumentType",
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
            
            CreateTable(
                "dbo.FormType",
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
            
            CreateTable(
                "dbo.PromulgateStatus",
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
            
            CreateTable(
                "dbo.Status",
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
            DropTable("dbo.Status");
            DropTable("dbo.PromulgateStatus");
            DropTable("dbo.FormType");
            DropTable("dbo.DocumentType");
            DropTable("dbo.Document");
            DropTable("dbo.Appendice");
        }
    }
}
