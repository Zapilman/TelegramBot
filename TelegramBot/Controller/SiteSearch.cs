using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using TelegramBot.Model;

namespace TelegramBot.Controller
{
    public class SiteSearch
    {

        private Site currentSite;
        private TelegramBotClient bot;
        private IReadOnlyList<Site> orSites;


        private Site Search(string name)
        {
            var site = orSites.SingleOrDefault(s => s.Name == name);
            return site;
        }


        public void ChangeSite(string name,CallbackQuery callback)
        {
            var newPass = new SetValue(bot);
            newPass.InputNew(callback.Message," новый password");
            var password = newPass.GetValue();

            using var context = new BotContext();
            var site = context.Sites.SingleOrDefault(s => s.Name == name);

            site.Password = password;
            context.SaveChanges();

            bot.SendTextMessageAsync(callback.Message.Chat.Id, $"Пароль к сайту {site.Name} был успешно изменён");
            bot.SendTextMessageAsync(callback.Message.Chat.Id, $"{site.Name}   {site.Password}   {site.Url}");
        }

        

        public SiteSearch(TelegramBotClient botClient, IReadOnlyList<Site> sites)
        {
            orSites = sites;
            bot = botClient;
            bot.OnMessage += Bot_OnMessage;

            var time = 10;
            
            do
            {

                if(time == 0)
                {
                    currentSite = new Site("None", null, "None", "None");
                    break;
                }
                else
                {
                    Thread.Sleep(1000);
                    time--;
                }

            } while (currentSite == null);
            
        }

        

        private void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var text = e?.Message?.Text;
            var site = Search(text);
            currentSite = site;



            bot.OnMessage -= Bot_OnMessage;
            bot.StopReceiving();
        }

        public Site GetSite()
        {
            return currentSite;
        }
    }
}
