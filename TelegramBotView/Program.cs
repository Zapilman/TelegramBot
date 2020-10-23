using System;

using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramBot.Controller;
using TelegramBot.Model;

namespace TelegramView
{
    class Program
    {
        private static TelegramBotClient botClient;
        static void Main()
        {

            botClient = new TelegramBotClient("1287472177:AAEEEdv8d0AIU2Un8ney1D0rwNA4MWeUTkA");
            var me = botClient.GetMeAsync().Result;
            botClient.OnMessage += Bot_OnMessage;
            botClient.OnCallbackQuery += Bot_OnCallbackQuery;
            botClient.StartReceiving();
            Console.ReadLine();
            botClient.StopReceiving();
        }

        private static void Bot_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            var buttonText = e.CallbackQuery.Data;
            botClient.OnMessage -= Bot_OnMessage;
            botClient.OnCallbackQuery -= Bot_OnCallbackQuery;
            switch (buttonText)
            {

               
                case "Зарегистрироваться":
                    var registrate = new RegistrateCommand();
                    registrate.Execute(e.CallbackQuery.Message, botClient);
                    break;
                case "Войти в аккаунт":
                    var logIn = new LogInCommand();

                    logIn.Execute(e.CallbackQuery.Message, botClient);
                    botClient.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, "Метод закончил выполнение");
                    break;

            }
            botClient.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, "Оброботчик событий работает");
            //botClient.OnMessage += Bot_OnMessage;
            //botClient.OnCallbackQuery += Bot_OnCallbackQuery;

        }

        private static void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            botClient.SendTextMessageAsync(e.Message.Chat.Id, "Запущен 1 обработчик событий");
            var text = e?.Message?.Text;
            if (text == null)
            {
                return;
            }
            new CommandController(e.Message, botClient);

        }
    }
}
