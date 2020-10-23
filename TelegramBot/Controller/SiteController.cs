using System.Collections.Generic;
using System.Linq;
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
