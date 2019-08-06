namespace DocumentManagement.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMaxLengthContentAndScopeOfApplication : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Document", "ContentChange", c => c.String());
            AlterColumn("dbo.Document", "ScopeOfApplication", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Document", "ScopeOfApplication", c => c.String(maxLength: 512));
            AlterColumn("dbo.Document", "ContentChange", c => c.String(maxLength: 4000));
        }
    }
}
