using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private static void Bot_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var text = e?.Message?.Text;
            if(text == null)
            {
                return;
            }
            var controller = new CommandController();
            foreach(var command in db.Commands)
            {
                await botClient.SendTextMessageAsync(e.Message.Chat.Id, command.Name);
            }
            

        }
    }
}
