

namespace TelegramBot.Model
{
    public class Site
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Password { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public Site() { }

        public Site(string name, Model.User user, string password, string url)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new System.ArgumentException("message", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new System.ArgumentException("message", nameof(password));
            }

            Name = name;
            UserId = user.Id;
            User = user;
            Password = password;
            Url = url;
        }

    }
}
