
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Model
{
    public class StartCommand : Command
    {
        
        public override string Name => "/start";

        public override async void Execute(Message message, TelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "What are you going to do?");
        }
    }
}
