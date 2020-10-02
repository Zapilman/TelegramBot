
using System.Collections.Generic;

namespace TelegramBot.Model
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; }

        public string Password { get; }


        public string Name { get; }
        


        public User() { }

        public User(string login,string password)
        {
            Login = login;
            Password = password;
        }


        public User(string login, string password, string userName)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                throw new System.ArgumentException("message", nameof(login));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new System.ArgumentException("message", nameof(password));
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new System.ArgumentException("message", nameof(userName));
            }

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
