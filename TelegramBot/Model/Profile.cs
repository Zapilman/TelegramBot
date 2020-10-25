

using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Controller;

namespace TelegramBot.Model
{
    public class Profile
    {
        private TelegramBotClient bot;
        private Model.User currentUser;
        public Profile(TelegramBotClient bot, Message message, User user)
        {
            this.bot = bot;
            currentUser = user;
            var keyboard = new InlineKeyboardMarkup(new[] {

                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Создать пароль"),
                    InlineKeyboardButton.WithCallbackData("Посмотреть все пароли")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Изменить сущ. пароль"),
                    InlineKeyboardButton.WithUrl("Написать автору какой он пусечка","https://www.instagram.com/_yuro4ka/?hl=ru")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Сохранить свой пароль"),
                    InlineKeyboardButton.WithCallbackData("Выйти из профиля")
                }
            });
            bot.StartReceiving();
            bot.OnCallbackQuery += Bot_OnCallbackQuery;
            bot.SendTextMessageAsync(message.Chat.Id, "Что вы хотите сделать ?", replyMarkup: keyboard);
        }



        private void Bot_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            var buttonText = e.CallbackQuery.Data;
            bot.OnCallbackQuery -= Bot_OnCallbackQuery;
            switch (buttonText)
            {

                case "Создать пароль":
                    var passComm = new CreatePassCommand();
                    passComm.Execute(e.CallbackQuery.Message, bot);
                    break;
                case "Посмотреть все пароли":
                    var passwords = new SiteController();
                    passwords.SeeAll(currentUser.Id, bot, e.CallbackQuery);
                    break;
                case "Изменить сущ. пароль":
                    var password = new SiteController();
                    password.ChangePassword(currentUser.Id, bot, e.CallbackQuery);
                    break;
                case "Сохранить свой пароль":
                    var owmPassword = new SiteController();
                    owmPassword.CreateOwnPassword(bot,e.CallbackQuery,currentUser);
                    break;
                case "Выйти из профиля":
                    var currentUser = null;
                    break;

            }
        }

        
    }
}
