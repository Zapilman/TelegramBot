using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using TelegramBot.Model;

namespace TelegramBot.Controller
{
    public class GetStepByStep : IGetValues 
    {
        private static string item1;
        private static TelegramBotClient bot;
        private static Model.User currentUser;
        private static bool sucsess = false;
        public static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var text = e.Message.Text;
            if (CheckInput(text))
            {
                bot.StopReceiving();
            }
            else
            {
                await bot.SendTextMessageAsync(e.Message.Chat.Id, $"You inputed wrong {item1}");
            }

         }

        

        public static bool CheckInput(string data)
        {
            UserController userController = new UserController();
            if(item1 == "login")
            {
                currentUser = userController.Users.SingleOrDefault(u => u.Login == data);
            }
            if(item1 == "password")
            {
                currentUser = userController.Users.SingleOrDefault(u => u.Password == data);
            }

            if (currentUser == null)
            {
                sucsess = false;
                return sucsess;
            }

            sucsess = true;
            return sucsess;
        }

        public async void Execute( Message message, TelegramBotClient botClient)
        {
            bot = botClient;
            await bot.SendTextMessageAsync(message.Chat.Id, $"Input login");
            item1 = "login";
            bot.OnMessage += Bot_OnMessage;
            bot.StartReceiving();
            
            while (true)
            {
                
                Thread.Sleep(3000);
                if (sucsess)
                {
                    sucsess = false;
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Input password");
                    item1 = "password";
                    bot.StartReceiving();
                    while (true)
                    {
                        Thread.Sleep(3000);
                        if (sucsess)
                        {
                            await bot.SendTextMessageAsync(message.Chat.Id, "Успешная авторизация");
                            return;
                        }
                    }
                }
            }

        }

        
        public Model.User GetUser()
        {
            return currentUser;
        }
    }
}
