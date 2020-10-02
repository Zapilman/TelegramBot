using System;
using System.Collections.Generic;

using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace TelegramBot.Controller
{
    public interface IGetValues
    {
          void Execute( Message message, TelegramBotClient botClient);

          Model.User GetUser();
 

    }
}
