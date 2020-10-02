using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Controller;

namespace TelegramBot.Model
{
    public class LogInCommand : Command
    {
        

        private IGetValues manager = new GetStepByStep();

        public override string Name => "/log_in";

        public override bool Contains(string command, User user)
        {
            return command.Contains(this.Name);
        }

        public override void Execute(Message message, TelegramBotClient botClient)
        {
            manager.Execute(message, botClient);
        }

        public Model.User GetUser()
        {
            return manager.GetUser();
        }
    }
}
