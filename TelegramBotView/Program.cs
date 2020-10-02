using System;

using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramBot.Controller;
using TelegramBot.Model;

namespace TelegramView
{
    class Program
    {
        private static BotContext db;
        private static TelegramBotClient botClient;
        private static User currentUser;
        static void Main(string[] args)
        {
            db = new BotContext();
            
            botClient = new TelegramBotClient("1287472177:AAEEEdv8d0AIU2Un8ney1D0rwNA4MWeUTkA");
            var me = botClient.GetMeAsync().Result;
            botClient.OnMessage += Bot_OnMessage;
            botClient.OnCallbackQuery += Bot_OnCallbackQuery;
            botClient.StartReceiving();
            Console.ReadLine();
            botClient.StopReceiving();
        }

        private static async void Bot_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            var buttonText = e.CallbackQuery.Data;
            botClient.StopReceiving();
            switch (buttonText)
            {

                case "Создать пароль":
                    string password = Guid.NewGuid().ToString();
                    await botClient.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, $"Созданный пароль: \r\n {password}");
                    break;
                case "Зарегистрироваться":
                    var registrate = new RegistrateCommand();
                    registrate.Execute(e.CallbackQuery.Message,botClient);
                    break;
                case "Войти в аккаунт":
                    var logIn = new LogInCommand();
                    logIn.Execute(e.CallbackQuery.Message, botClient);
                    break;
                    
            }
            
        }

        private static  void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var text = e?.Message?.Text;
            if (text == null)
            {
                return;
            }
            new CommandController(db,e.Message,botClient,currentUser);

        }
    }
}
