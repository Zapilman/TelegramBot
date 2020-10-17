using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Model;

namespace TelegramBot.Controller
{
    public class DbSaver : IDataSaver
    {
        public List<T> Load<T>() where T : class
        {
            using var botContext = new BotContext();
            var result = botContext.Set<T>().Where(t => true).ToList();
            return result;
        }

        public void Save<T>(List<T> items) where T : class
        {
            using var botContext = new BotContext();
            botContext.Set<T>().AddRange(items);
            botContext.SaveChanges();
        }
        
        public void Add<T>(T item) where T : class
        {
            using var botContext = new BotContext();
            botContext.Set<T>().Add(item);
            botContext.SaveChanges();
        }

    }
}
