﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Model
{
    public class SayHelloCommand : Command
    {
        public override string Name => "/sayhello";

        public override bool Contains(string command)
        {
            return command.Contains(this.Name);
        }

        public override async void Execute(Message message, TelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "Hello yopta");
            
        }
    }
}
