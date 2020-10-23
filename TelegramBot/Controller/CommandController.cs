using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Model;

namespace TelegramBot.Controller
{
    public class CommandController: ControllerBase
    {


        private List<Command> Commands { get; }

        public CommandController(Message message,TelegramBotClient botClient)
        {
            Commands = new List<Command>();
            var commands = new CommandsList();
            
            foreach(var command in commands.GetCommands())
            {
                IsContains(command);
            }
            Save<Command>(Commands);

            foreach (var command in Load<Command>())
            {
                if (command.Contains(message.Text))
                {
                    command.Execute(message, botClient);
                        
                    return;
                }
            }
             

            


        }

        public void IsContains(Command command)
        {
            foreach (var dbCommand in Load<Command>())
            {
                if (dbCommand.Name == command.Name)
                {
                    return;
                }
            }
            Commands.Add(command);
        }

    }
}
