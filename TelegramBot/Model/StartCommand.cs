
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
                    InlineKeyboardButton.WithCallbackData("Создать пароль"),
                    InlineKeyboardButton.WithCallbackData("Войти в аккаунт")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Зарегистрироваться"),
                    InlineKeyboardButton.WithUrl("Написать автору какой он пусечка","https://www.instagram.com/_yuro4ka/?hl=ru")
                }
            });

            await botClient.SendTextMessageAsync(message.Chat.Id, "Что вы хотите сделать ?", replyMarkup: keyboard);


        }
    }
}
