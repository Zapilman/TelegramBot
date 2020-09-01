
using System.Collections.Generic;

namespace TelegramBot.Model
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; }

        public string Password { get; }

        public string Username { get; }

        public string Name { get; }

        public ICollection<Item> Items { get; set; }

        public override string ToString()
        {
            return Username;
        }
    }
}
