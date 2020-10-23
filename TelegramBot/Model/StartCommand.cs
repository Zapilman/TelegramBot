
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Model
{
    public class StartCommand : Command
    {
        
        public override string Name => "/start";


        public override bool Contains(string command)
        {
            return command.Contains(this.Name);
        }

        public override async void Execute(Message message, TelegramBotClient botClient)
        {

            var keyboard = new InlineKeyboardMarkup(new[] {

                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Зарегистрироваться"),
                    InlineKeyboardButton.WithCallbackData("Войти в аккаунт")
                }
            });

            await botClient.SendTextMessageAsync(message.Chat.Id, "Что вы хотите сделать ?", replyMarkup: keyboard);


        }
    }
}
