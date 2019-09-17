namespace DocumentManagement.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAppendiceNumberToAppendice : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Appendice", "AppendiceNumber", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Appendice", "AppendiceNumber", c => c.String());
        }
    }
}
