
using System.Collections.Generic;

namespace TelegramBot.Model
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }


        public string Name { get; set; }
        

        public ICollection<Site> Sites { get; set; }


        public User() { }

        public User(string login,string password)
        {
            Login = login;
            Password = password;
        }


        public User(string login, string password, string userName)
        {
            

            Login = login;
            Password = password;
            Name = userName;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
