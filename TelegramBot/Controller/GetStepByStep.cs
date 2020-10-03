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
            await bot.SendTextMessageAsync(e.Message.Chat.Id, "Запущен 2 оброботчик событий");
            
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

        public  void Execute( Message message, TelegramBotClient botClient)
        {
            bot = botClient;
             bot.SendTextMessageAsync(message.Chat.Id, $"Input login");
            item1 = "login";
            bot.OnMessage += Bot_OnMessage;
            bot.StartReceiving();
            var time = 0;
            while (time<10)
            {

                Thread.Sleep(3000);
                if (sucsess)
                {
                    sucsess = false;
                     botClient.SendTextMessageAsync(message.Chat.Id, $"Input password");
                    item1 = "password";
                    bot.StartReceiving();
                    while (time<10)
                    {
                        Thread.Sleep(3000);
                        if (sucsess)
                        {
                             bot.SendTextMessageAsync(message.Chat.Id, "Успешная авторизация");
                            bot.OnMessage -= Bot_OnMessage;
                            
                            return;

                        }
                        time++;
                    }
                }
                time++;
            }
             botClient.SendTextMessageAsync(message.Chat.Id, "Время для автроризации истекло  ");
            bot.OnMessage -= Bot_OnMessage;
            //bot.StopReceiving();
        }

        
        public Model.User GetUser()
        {
            return currentUser;
        }
    }
}
