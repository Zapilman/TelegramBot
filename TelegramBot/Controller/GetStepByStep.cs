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
        public static string item1;
        private static TelegramBotClient bot;
        public static bool sucsess = false;
       


        

        public  void Execute( Message message, TelegramBotClient botClient)
        {
            bot = botClient;
            bot.SendTextMessageAsync(message.Chat.Id, $"Input login");
            item1 = "login";
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
                             bot.SendTextMessageAsync(message.Chat.Id, "Операция прошла успешна");
                            
                            return;

                        }
                        time++;
                    }
                }
                time++;
            }
             botClient.SendTextMessageAsync(message.Chat.Id, "Время на выполнение операции истекло истекло  ");
        }

        
        

        
    }
}
