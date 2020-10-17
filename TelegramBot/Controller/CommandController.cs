using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Model;

namespace TelegramBot.Controller
{
    public class CommandController
    {


        private IDataSaver manager = new DbSaver();
        

        public CommandController(Message message,TelegramBotClient botClient, Model.User user)
        {

            var commands = new CommandsList();
            
            foreach(var command in commands.GetCommands())
            {
                IsContains(command);
            }
            

            foreach (var command in manager.Load<Command>())
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
            foreach (var dbCommand in manager.Load<Command>())
            {
                if (dbCommand.Name == command.Name)
                {
                    return;
                }
            }
            manager.Add<Command>(command);
        }

    }
}
