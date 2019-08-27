namespace DocumentManagement.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMaxLengthScopeOfApplicationInAppendice : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Appendice", "ScopeOfApplication", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Appendice", "ScopeOfApplication", c => c.String(maxLength: 512));
        }
    }
}
