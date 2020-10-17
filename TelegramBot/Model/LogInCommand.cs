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
        private IGetValues manager = new GetStepByStep();

        public override string Name => "/log_in";

        public override bool Contains(string command, User user)
        {
            return command.Contains(this.Name);
        }

        public override void Execute(Message message, TelegramBotClient botClient)
        {
            bot = botClient;
            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            manager.Execute(message, botClient);
            botClient.OnMessage -= Bot_OnMessage;
        }

        private void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var text = e.Message.Text;
             bot.SendTextMessageAsync(e.Message.Chat.Id, "Запущен LogIn оброботчик событий");

            if (CheckInput(text))
            {
                bot.StopReceiving();
                if(GetStepByStep.item1 == "password")
                {
                    bot.SendTextMessageAsync(e.Message.Chat.Id, $"Hello @{currentUser.Name}");
                }
            }
            else
            {
                 bot.SendTextMessageAsync(e.Message.Chat.Id, $"You inputed wrong {GetStepByStep.item1}");
            }
        }

        private bool CheckInput(string text)
        {
            UserController userController = new UserController();
            if (GetStepByStep.item1 == "login")
            {
                currentUser = userController.Users.SingleOrDefault(u => u.Login == text);
            }
            if (GetStepByStep.item1 == "password")
            {
                currentUser = userController.Users.SingleOrDefault(u => u.Password == text);
            }

            if (currentUser == null)
            {
                GetStepByStep.sucsess = false;
                return false;
            }

            GetStepByStep.sucsess = true;
            return true;
        }
    }
}
