using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Model;

namespace TelegramBot.Controller
{
    public class UserController:ControllerBase
    {
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


            

            var user = new Model.User(password, login,userName);
            Users.Add(user);
            Save();

        }


        public UserController ()
        {
            Users = GetUsersData();
            
            
            
            
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
