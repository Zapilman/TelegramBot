using System;

using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using TelegramBot.Controller;

namespace TelegramBot.Model
{
    public class RegistrateCommand : Command
    {
        private static string login = null;
        private static string password = null;
        private  TelegramBotClient botClient;
        public override string Name => "/registrate";

        public override bool Contains(string command, User user)
        {
            return command.Contains(this.Name);
        }

        public override async void Execute(Message message, TelegramBotClient botClient)
        {
            this.botClient = botClient;
            var me = this.botClient.GetMeAsync().Result;
            await this.botClient.SendTextMessageAsync(message.Chat.Id, "Введите логин");
            this.botClient.OnMessage += Bot_OnMessage;
            this.botClient.StartReceiving();
            Console.ReadLine();
            this.botClient.StopReceiving();
            
        }

        private  async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var text = e?.Message?.Text;
            while (login == null || password == null)
            {
                if (login == null)
                {
                    login = text;
                    await this.botClient.SendTextMessageAsync(e.Message.Chat.Id, "Введите пароль");
                    return;
                }
                else
                {
                    password = text;
                }
            }
            var userController = new UserController(password, login);
            await botClient.SendTextMessageAsync(e.Message.Chat.Id, "new user has been added");
        }
    }
}
