

namespace TelegramBot.Model
{
    public class Site
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Password { get; set; }

        public int UserId { get; set; }
        public User user { get; set; }

    }
}
