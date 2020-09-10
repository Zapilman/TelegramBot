
using System.Collections.Generic;

namespace TelegramBot.Model
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; }

        public string Password { get; }


        public string Name { get; }
        

        public virtual ICollection<Site> Sites { get; set; }

        public User() { }

        public User(string login,string password)
        {
            Login = login;
            Password = password;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
