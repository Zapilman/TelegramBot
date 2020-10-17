

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

        public Site(string name, int userId)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new System.ArgumentException("message", nameof(name));
            }

            Name = name;
            UserId = userId;
        }

    }
}
