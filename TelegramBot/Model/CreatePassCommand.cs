using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Controller;

namespace TelegramBot.Model
{
    public class CreatePassCommand : Command
    {
        public override string Name => "/createPassword";
        private TelegramBotClient bot;
        private Model.User currentUser;

        public override bool Contains(string command)
        {
            return command.Contains(this.Name);
        }

        public override void Execute(Message message, TelegramBotClient botClient)
        {
            var pass = Guid.NewGuid().ToString();
            bot = botClient;
            botClient.OnCallbackQuery += Bot_OnCallbackQuery;
            
            

            botClient.SendTextMessageAsync(message.Chat.Id, pass.Replace('-', 'a'));
            

            var inlineKeyboard = new InlineKeyboardMarkup(new[]{
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Da"),
                    InlineKeyboardButton.WithCallbackData("Net")
                }

            });
            
            botClient.SendTextMessageAsync(message.Chat.Id, "Хотите сохранить пароль?", replyMarkup:inlineKeyboard);
           

        }

        private void Bot_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            var buttonText = e.CallbackQuery.Data;
            bot.OnCallbackQuery -= Bot_OnCallbackQuery;
            switch (buttonText)
            {
                case "Da":
                    if (currentUser != null)
                    {
                        bot.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, "Для какого сайта этот пароль?");
                        bot.StartReceiving();
                        bot.OnMessage += Bot_OnMessage;
                        


                    }
                    else
                    {
                        var inlineKeyboard = new InlineKeyboardMarkup(new[]{
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Зaрегистрироваться"),// В этом слове буква 'a' написана на англ, чтобы не совпадала с Program
                            InlineKeyboardButton.WithCallbackData("Авторизоваться")
                        }

                        });
                        bot.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, "Ваш аккаунт не был найден", replyMarkup: inlineKeyboard);
                    }
                    break;
                case "No":
                    bot.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, "Nu net tak net");
                    break;
                case "Зaрегистрироваться":
                    var registrate = new RegistrateCommand();
                    registrate.Execute(e.CallbackQuery.Message, bot);
                    currentUser = registrate.GetUser();
                    goto case "Da";
                case "Авторизоваться":
                    var logIn = new LogInCommand();
                    logIn.Execute(e.CallbackQuery.Message, bot);
                    currentUser = logIn.GetUser();
                    goto case "Da";

            };


            
            
            bot.OnCallbackQuery += Bot_OnCallbackQuery;
        }

        private void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var text = e?.Message?.Text;
            bot.SendTextMessageAsync(e.Message.Chat.Id, $"Site {text} has been saved");
            var site = new Site(text, currentUser.Id,currentUser);
            var controller = new SiteController();
            controller.AddSite(site);
            bot.StopReceiving();
            bot.OnMessage -= Bot_OnMessage;
        }
    }
}
