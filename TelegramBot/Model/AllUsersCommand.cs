using System;

using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Model
{
    public class AllUsersCommand : Command
    {
        public override string Name => "/all";

        public override bool Contains(string command, User user)
        {
            if (user != null)
            {
                return command.Contains(this.Name);
            }
            return false;
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
