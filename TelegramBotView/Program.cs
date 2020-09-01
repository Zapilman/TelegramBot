using System;

using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramBot.Model;

namespace TelegramBotView
{
    public class Program
    {
        private static BotContext db;
        private static TelegramBotClient botClient;
        static void Main(string[] args)
        {
            db = new BotContext();
            foreach(var site in db.Sites)
            {
                db.Sites.Remove(site);
            }
            var site1 = new Site()
            {
                Name = "Google"
            };

            var site2 = new Site()
            {
                Name = "Amazon"
            };

            db.Sites.Add(site1);
            db.Sites.Add(site2);
            db.SaveChanges();
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
            if (text == null)
            {
                return;
            }



            if (text == "/start")
            {
                

                foreach (var site in db.Sites)
                {
                    try
                    {
                        await botClient.SendTextMessageAsync(e.Message.From.Id, site.Name);
                    }
                    catch
                    {
                        return;
                    }
                }
            }
        }
    }

       

       
    
}
