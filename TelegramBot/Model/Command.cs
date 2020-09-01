

using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Model
{
    public abstract class Command
    {
        public int Id { get; set; }
        public abstract string Name { get;}

        public abstract void Execute(Message message, TelegramBotClient botClient);

        public bool Contains(string command)
        {
            return command.Contains(this.Name);
        }
    }
}
