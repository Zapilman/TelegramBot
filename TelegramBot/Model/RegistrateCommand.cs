using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using TelegramBot.Controller;

namespace TelegramBot.Model
{
    public class RegistrateCommand : Command
    {
        private Model.User currentUser;
        private TelegramBotClient bot;
        



        public override string Name => "/registrate";


        public override bool Contains(string command)
        {
            return command.Contains(this.Name);
        }

        public override  void Execute(Message message, TelegramBotClient botClient)
        {
            var controller = new UserController();

            bot = botClient;
            var setLog = new SetValue(bot);
            setLog.InputNew(message, "login");
            var login = setLog.GetValue();
            currentUser = controller.Users.SingleOrDefault(u => u.Login == login);
            if(currentUser != null)
            {
                bot.SendTextMessageAsync(message.Chat.Id, "wrong login");
                return;
            }

            var setPass = new SetValue(bot);
            setPass.InputNew(message,"password");
            var password = setPass.GetValue();
            currentUser = controller.Users.SingleOrDefault(u => u.Password == password);
            if(currentUser != null)
            {
                bot.SendTextMessageAsync(message.Chat.Id, "wrong password");
                return;
            }

            var userController = new UserController(login, password, $"{message.Chat.Username}");
            bot.SendTextMessageAsync(message.Chat.Id, $"Hello @{message.Chat.Username}");

            

        }

        public Model.User GetUser()
        {
            return currentUser;
        }

        
    }
}
