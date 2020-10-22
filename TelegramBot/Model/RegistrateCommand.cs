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
        private string login;
        private string password;



        public override string Name => "/registrate";

        private readonly IGetValues manager = new GetStepByStep();

        public override bool Contains(string command)
        {
            return command.Contains(this.Name);
        }

        public override  void Execute(Message message, TelegramBotClient botClient)
        {
            bot = botClient;
            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            manager.Execute(message, botClient);
            botClient.OnMessage -= Bot_OnMessage;

        }

        public Model.User GetUser()
        {
            return currentUser;
        }

        private void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var text = e.Message.Text;
            bot.SendTextMessageAsync(e.Message.Chat.Id, "Запущен SignUp оброботчик событий");

            if (CheckInput(text))
            {
                bot.StopReceiving();
                if(GetStepByStep.item1 == "password")
                {
                    var controller = new UserController(login, password, e.Message.Chat.Username);
                    var user = new User(login, password);

                    currentUser = controller.GetUser();
                    currentUser = user;
                    bot.SendTextMessageAsync(e.Message.Chat.Id, $"@{e.Message.Chat.Username} вы успешно зарегистрированы!");
                }
            }
            else
            {
                bot.SendTextMessageAsync(e.Message.Chat.Id, $"Уже занятый {GetStepByStep.item1}");
            }
        }

        private bool CheckInput(string text)
        {
            UserController userController = new UserController();
            if (GetStepByStep.item1 == "login")
            {
                currentUser = userController.Users.SingleOrDefault(u => u.Login == text);
                login = text;
            }
            if (GetStepByStep.item1 == "password")
            {
                currentUser = userController.Users.SingleOrDefault(u => u.Password == text);
                password = text;
            }

            if (currentUser != null)
            {
                GetStepByStep.sucsess = false;
                return false;
            }

            GetStepByStep.sucsess = true;
            return true;

            // TODO: упростить регистрация и автроризацию, очень похожи между собой
        }
    }
}
