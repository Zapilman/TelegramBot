using System;

using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Model
{
    public class AllUsersCommand : Command
    {
        public override string Name => "/all";

        public override bool Contains(string command)
        {
            return command.Contains(this.Name);
        }

        public override void Execute(Message message, TelegramBotClient botClient)
        {
            var controller = new Controller.UserController();
            foreach(var i in controller.Users)
            {
                botClient.SendTextMessageAsync(message.Chat.Id, i.Name);
            }
        }
    }
}
