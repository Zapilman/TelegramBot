using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace TelegramBot.Controller
{
    public class SetPassword
    {
        private TelegramBotClient bot;
        private string password;
        public SetPassword(TelegramBotClient botClient)
        {
            bot = botClient;
            password = "";
        }

        public void InputNew(CallbackQuery callback)
        {
            bot.OnMessage += Bot_OnMessage;
            bot.SendTextMessageAsync(callback.Message.Chat.Id, "Введите новый пароль");

            var time = 10;
            do
            {

                if (time == 0)
                {
                    password = "None";
                    break;
                }
                else
                {
                    Thread.Sleep(1000);
                    time--;
                }

            } while (password == "");

        }

        private void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var text = e?.Message?.Text;

            password = text;

            bot.OnMessage -= Bot_OnMessage;
        }

        public string GetPassword()
        {
            return password;
        }
    }
}
