namespace TelegramBot.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Sites", "Url");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sites", "Url", c => c.String());
        }
    }
}
