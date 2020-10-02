using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Controller
{
    public abstract class ControllerBase
    {
        protected IDataSaver manager = new DbSaver();

        protected void Save<T>(List<T> item) where T : class
        {
            manager.Save(item);
        }

        protected List<T> Load<T>() where T : class
        {
            return manager.Load<T>();
        }

    }
}
