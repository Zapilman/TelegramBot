namespace TelegramBot.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KEk : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Items", "User_Id", "dbo.Users");
            DropIndex("dbo.Items", new[] { "SiteId" });
            DropIndex("dbo.Items", new[] { "User_Id" });
            AddColumn("dbo.Sites", "Password", c => c.String());
            AddColumn("dbo.Sites", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sites", "UserId");
            AddForeignKey("dbo.Sites", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            DropTable("dbo.Items");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteId = c.Int(nullable: false),
                        Password = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Sites", "UserId", "dbo.Users");
            DropIndex("dbo.Sites", new[] { "UserId" });
            DropColumn("dbo.Sites", "UserId");
            DropColumn("dbo.Sites", "Password");
            CreateIndex("dbo.Items", "User_Id");
            CreateIndex("dbo.Items", "SiteId");
            AddForeignKey("dbo.Items", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Items", "SiteId", "dbo.Sites", "Id", cascadeDelete: true);
        }
    }
}
