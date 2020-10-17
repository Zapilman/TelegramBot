using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Model;

namespace TelegramBot.Controller
{
    public class CommandController
    {

        BotContext db;

        

        public CommandController(BotContext db,Message message,TelegramBotClient botClient, Model.User user)
        {

            var commands = new CommandsList();
            this.db = db;
            
            foreach(var command in commands.GetCommands())
            {
                IsContains(command);
            }
            db.SaveChanges();

            foreach (var command in db.Commands)
            {
                if (command.Contains(message.Text, user))
                {
                    command.Execute(message, botClient);
                    botClient.SendTextMessageAsync(message.Chat.Id, $"{command.Id}");
                    return;
                }
            }
             

            


        }

        public void IsContains(Command command)
        {
            foreach (var dbCommand in db.Commands)
            {
                if (dbCommand.Name == command.Name)
                {
                    return;
                }
            }
            db.Commands.Add(command);
        }

    }
}
