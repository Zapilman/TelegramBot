using System.Collections.Generic;

namespace TelegramBot.Model
{
    public class CommandsList
    {
        private readonly List<Command> commands;

        

        public CommandsList()
        {
            commands = new List<Command>() { new StartCommand(), new RegistrateCommand(), new AllUsersCommand(), new CreatePassCommand(), new LogInCommand(), new SayHelloCommand() };
        }

        public List<Command> GetCommands()
        {
            return commands;
        }
    }
}
