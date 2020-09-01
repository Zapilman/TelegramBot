using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Model;

namespace TelegramBot.Controller
{
    public class CommandController
    {
        

        public CommandController(BotContext db,Message message,TelegramBotClient botClient)
        {
            
            foreach(var command in db.Commands)
            {
                if (command.Contains(message.Text))
                {
                    command.Execute(message, botClient);
                }
            }


        }

    }
}
