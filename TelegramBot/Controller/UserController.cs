using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Model;

namespace TelegramBot.Controller
{
    public class UserController:ControllerBase
    {
        private Model.User currentUser;
        public List<Model.User> Users { get; }
        
        public Model.User User { get; }
        public UserController(string password, string login,string userName)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new System.ArgumentException("message", nameof(password));
            }

            if (string.IsNullOrWhiteSpace(login))
            {
                throw new System.ArgumentException("message", nameof(login));
            }

            Users = GetUsersData();


            

            var user = new Model.User(password, login);
            Users.Add(user);
            Save();

        }


        public UserController ()
        {
            var user = new Model.User("123", "321","jopa");
            Users = new List<Model.User>(); 
            
            Users.Add(user);
            Save();
            
        }

        public UserController(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                throw new System.ArgumentException("message", nameof(login));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new System.ArgumentException("message", nameof(password));
            }


        }

        private List<Model.User> GetUsersData()
        {
            return Load<Model.User>() ?? new List<Model.User>();
        }

        private void Save()
        {
            Save(Users);
        }

    }
}
