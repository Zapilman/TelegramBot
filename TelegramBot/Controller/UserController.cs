using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Model;

namespace TelegramBot.Controller
{
    public class UserController
    {
        private BotContext botContext;
        private Model.User currentUser;
        public UserController(string password, string login)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new System.ArgumentException("message", nameof(password));
            }

            if (string.IsNullOrWhiteSpace(login))
            {
                throw new System.ArgumentException("message", nameof(login));
            }

            botContext = new BotContext();


            

            var user = new Model.User(password, login);
            botContext.Users.Add(user);
            botContext.SaveChanges();

        }

    }
}
