using System;

using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Model
{
    public class CreatePassCommand : Command
    {
        public override string Name => "/createPassword";

        public override bool Contains(string command, User user)
        {
            return command.Contains(this.Name);
        }

        public override async void Execute(Message message, TelegramBotClient botClient)
        {
            var pass = Guid.NewGuid().ToString();

            await botClient.SendTextMessageAsync(message.Chat.Id, pass.Replace('-', 'a'));
        }

        
    }
}
