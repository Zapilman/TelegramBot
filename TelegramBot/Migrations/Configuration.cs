﻿namespace TelegramBot.Migrations
{
 
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<TelegramBot.Model.BotContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TelegramBot.Model.BotContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
