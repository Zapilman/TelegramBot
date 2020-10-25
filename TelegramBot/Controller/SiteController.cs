using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using TelegramBot.Model;

namespace TelegramBot.Controller
{
    public class SiteController: ControllerBase
    {
        private  List<Site> Sites { get; }
        
        

        public SiteController()
        {
            
            Sites = new List<Site>();
        }


        public void AddSite(string name, Model.User user, string password, string url = null)
        {
            var site = GetSites().SingleOrDefault(s => s.UserId == user.Id && s.Name == name);

            if (site != null)
            {
                return;
            }
            else
            {
                site = new Site(name, user, password, url);
            }
            
            
            Sites.Add(site);
            Save();
        }

        public void SeeAll(int userId, TelegramBotClient botClient, CallbackQuery callback)
        {
            botClient.SendTextMessageAsync(callback.Message.Chat.Id, "Список ваших сохр. сайтов:");
            var number = 0;
            foreach(var s in GetSites())
            {
                if(s.UserId == userId)
                {
                    number++;
                    botClient.SendTextMessageAsync(callback.Message.Chat.Id, $"{number}: {s.Name}");
                    
                }
            }
            botClient.SendTextMessageAsync(callback.Message.Chat.Id, "Пароль к какому сайту вы хотите увидеть?");
            var sitesearch = new SiteSearch(botClient, GetSites());
            var site = sitesearch.GetSite();
            botClient.SendTextMessageAsync(callback.Message.Chat.Id, $"{site.Name}   {site.Password}   {site.Url}");
        }

        
        public void ChangePassword(int userId, TelegramBotClient botClient, CallbackQuery callback)
        {
            botClient.SendTextMessageAsync(callback.Message.Chat.Id, "Список ваших сохр. сайтов:");
            var number = 0;
            foreach (var s in GetSites())
            {
                if (s.UserId == userId)
                {
                    number++;
                    botClient.SendTextMessageAsync(callback.Message.Chat.Id, $"{number}: {s.Name}");

                }
            }
            
            botClient.SendTextMessageAsync(callback.Message.Chat.Id, "Пароль к какому сайту вы хотите изменить?");
            var sitesearch = new SiteSearch(botClient, GetSites());
            var site = sitesearch.GetSite();
            sitesearch.ChangeSite(site.Name, callback);
            
            
        }

        public void CreateOwnPassword( TelegramBotClient bot, CallbackQuery callback, Model.User user)
        {
            var setName = new SetValue(bot);
            setName.InputNew(callback.Message, "sites name");
            var name = setName.GetValue();
            var setPass = new SetValue(bot);
            setPass.InputNew(callback.Message, "password");
            var password = setPass.GetValue();
            var setUrl = new SetValue(bot);
            setUrl.InputNew(callback.Message, "url");
            var url = setUrl.GetValue();

            Sites.Add(new Site(name, user, password, url));
            Save();

            bot.SendTextMessageAsync(callback.Message.Chat.Id, $"сайт {name} был сохранён");
        }

        private List<Site> GetSites()
        {
           return Load<Site>() ?? new List<Site>();
        }

        private void Save()
        {
            Save(Sites);
        }
    }
}
