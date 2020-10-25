using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using TelegramBot.Controller;

namespace TelegramBot.Model
{
    public class LogInCommand : Command
    {
        private Model.User currentUser;
        private TelegramBotClient bot;

        public override string Name => "/log_in";

        public Model.User GetUser()
        {
            return currentUser;
        }

        public override bool Contains(string command)
        {
            return command.Contains(this.Name);
        }

        public override void Execute(Message message, TelegramBotClient botClient)
        {
            bot = botClient;
            var controller = new UserController();

            var setLog = new SetValue(bot);
            setLog.InputNew(message, "login");
            var login = setLog.GetValue();
            currentUser = controller.Users.SingleOrDefault(u => u.Login == login);
            if (currentUser == null)
            {
                bot.SendTextMessageAsync(message.Chat.Id, "wrong login");
                return;
            }

            var setPass = new SetValue(bot);
            setPass.InputNew(message, "password");
            var password = setPass.GetValue();
            currentUser = controller.Users.SingleOrDefault(u => u.Password == password);
            if (currentUser == null)
            {
                bot.SendTextMessageAsync(message.Chat.Id, "wrong password");
                return;
            }

            bot.SendTextMessageAsync(message.Chat.Id, $"Hello @{message.Chat.Username}");

        }

       
    }
}
