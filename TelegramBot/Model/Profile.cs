

using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.Bot.Types;
using System;
using Telegram.Bot.Args;
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
            //bot.OnMessage += Bot_OnMessage; Добавить в нужный момент, когда нужно будет изменить пароль или сохранить свой
            bot.OnCallbackQuery += Bot_OnCallbackQuery;
            bot.SendTextMessageAsync(message.Chat.Id, "Что вы хотите сделать ?", replyMarkup: keyboard);
        }

        

        private void Bot_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            var buttonText = e.CallbackQuery.Data;
            bot.OnMessage -= Bot_OnMessage;
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
                    //var password = new SiteController();
                    //password.ChangePassword();
                    break;
                case "Сохранить свой пароль":
                    //var password = new SiteController();
                    //password.CreateOwnPassword();
                    break;
                case "Выйти из профиля":
                    //var currentUser = null;
                    break;

            }
        }

        private void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var text = e?.Message?.Text;
            bot.SendTextMessageAsync(e.Message.Chat.Id, text.ToString());
        }
    }
}
