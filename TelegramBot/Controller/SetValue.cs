﻿using System;
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
    public class SetValue
    {
        private TelegramBotClient bot;
        private string value;
        public SetValue(TelegramBotClient botClient)
        {
            bot = botClient;
            value = "";
        }

        public void InputNew(Message message, string item)
        {
            
            bot.SendTextMessageAsync(message.Chat.Id, $"Введите {item}");
            bot.OnMessage += Bot_OnMessage;
            bot.StartReceiving();

            var time = 10;
            do
            {

                if (time == 0)
                {
                    value = "None";
                    break;
                }
                else
                {
                    Thread.Sleep(1000);
                    time--;
                }

            } while (value == "");

        }

        private void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var text = e?.Message?.Text;
            value = text;
            
            bot.OnMessage -= Bot_OnMessage;
            bot.StopReceiving();
        }

        public string GetValue()
        {
            return value;
        }
    }
}
