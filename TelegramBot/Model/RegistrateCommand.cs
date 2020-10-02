using System;

using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using TelegramBot.Controller;

namespace TelegramBot.Model
{
    public class RegistrateCommand : Command
    {
        public override string Name => "/registrate";

        public override bool Contains(string command, User user)
        {
            return command.Contains(this.Name);
        }

        public override async void Execute(Message message, TelegramBotClient botClient)
        {
            var userController = new UserController();
            
        }

        
    }
}
