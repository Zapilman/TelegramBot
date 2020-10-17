

using System.Data.Entity;

namespace TelegramBot.Model
{
    public class BotContext : DbContext
    {
        public BotContext() : base("Tgm") { }

        


        
        public DbSet<Model.User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Command> Commands { get; set; }
        public DbSet<Site> Sites { get; set; }
        
    }
}
