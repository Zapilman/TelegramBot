

namespace TelegramBot.Model
{
    public class Item
    {
        public int Id { get; set; }

        public int SiteId { get; set; }
        public Site Site { get; set; }



        public string Password { get; set; }
    }
}
