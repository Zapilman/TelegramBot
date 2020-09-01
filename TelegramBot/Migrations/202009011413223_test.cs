namespace TelegramBot.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.StartCommands", newName: "Commands");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Commands", newName: "StartCommands");
        }
    }
}
